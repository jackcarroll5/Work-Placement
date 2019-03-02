using Dotmim.Sync.Builders;
using System;
using System.Text;
using Dotmim.Sync.Data;
using System.Data.Common;
using System.Linq;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace Dotmim.Sync.Sqlite
{
    public class SqliteBuilderTable : IDbBuilderTableHelper
    {
        private ObjectNameParser tableName;
        private ObjectNameParser trackingName;
        private DmTable tableDescription;
        private SqliteConnection connection;
        private SqliteTransaction transaction;
        private SqliteDbMetadata sqliteDbMetadata;

        public SqliteBuilderTable(DmTable tableDescription, DbConnection connection, DbTransaction transaction = null)
        {
            this.connection = connection as SqliteConnection;
            this.transaction = transaction as SqliteTransaction;
            this.tableDescription = tableDescription;
            (this.tableName, this.trackingName) = SqliteBuilder.GetParsers(this.tableDescription);
            this.sqliteDbMetadata = new SqliteDbMetadata();
        }
        private SqliteCommand BuildForeignKeyConstraintsCommand(DmRelation foreignKey)
        {
            SqliteCommand sqlCommand = new SqliteCommand();

            var childTable = foreignKey.ChildTable;
            var childTableName = new ObjectNameParser(childTable.TableName);
            var parentTable = foreignKey.ParentTable;
            var parentTableName = new ObjectNameParser(parentTable.TableName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("ALTER TABLE ");
            stringBuilder.AppendLine(childTableName.FullQuotedString);
            stringBuilder.Append("ADD CONSTRAINT ");
            stringBuilder.AppendLine(foreignKey.RelationName);
            stringBuilder.Append("FOREIGN KEY (");
            string empty = string.Empty;
            foreach (var childColumn in foreignKey.ChildColumns)
            {
                var childColumnName = new ObjectNameParser(childColumn.ColumnName);
                stringBuilder.Append($"{empty} {childColumnName.FullQuotedString}");
            }
            stringBuilder.AppendLine(" )");
            stringBuilder.Append("REFERENCES ");
            stringBuilder.Append(parentTableName.FullQuotedString).Append(" (");
            empty = string.Empty;
            foreach (var parentdColumn in foreignKey.ParentColumns)
            {
                var parentColumnName = new ObjectNameParser(parentdColumn.ColumnName);

                stringBuilder.Append($"{empty} {parentColumnName.FullQuotedString}");
                empty = ", ";
            }
            stringBuilder.Append(" ) ");
            sqlCommand.CommandText = stringBuilder.ToString();
            return sqlCommand;
        }

        public void CreatePrimaryKey()
        {
            return;

        }
        public string CreatePrimaryKeyScriptText()
        {
            return string.Empty;
        }


        private SqliteCommand BuildTableCommand()
        {
            SqliteCommand command = new SqliteCommand();

            StringBuilder stringBuilder = new StringBuilder($"CREATE TABLE IF NOT EXISTS {tableName.FullQuotedString} (");
            string empty = string.Empty;
            stringBuilder.AppendLine();
            foreach (var column in this.tableDescription.Columns)
            {
                var columnName = new ObjectNameParser(column.ColumnName);

                var columnTypeString = this.sqliteDbMetadata.TryGetOwnerDbTypeString(column.OriginalDbType, column.DbType, false, false, column.MaxLength, this.tableDescription.OriginalProvider, SqliteSyncProvider.ProviderType);
                var columnPrecisionString = this.sqliteDbMetadata.TryGetOwnerDbTypePrecision(column.OriginalDbType, column.DbType, false, false, column.MaxLength, column.Precision, column.Scale, this.tableDescription.OriginalProvider, SqliteSyncProvider.ProviderType);
                var columnType = $"{columnTypeString} {columnPrecisionString}";

                // check case
                string casesensitive = "";
                if (this.sqliteDbMetadata.IsTextType(column.DbType))
                {
                    casesensitive = this.tableDescription.CaseSensitive ? "" : "COLLATE NOCASE";

                    //check if it's a primary key, then, even if it's case sensitive, we turn on case insensitive
                    if (this.tableDescription.CaseSensitive)
                    {
                        if (this.tableDescription.PrimaryKey.Columns.Contains(column))
                            casesensitive = "COLLATE NOCASE";
                    }
                }

                var identity = string.Empty;

                if (column.IsAutoIncrement)
                {
                    var s = column.GetAutoIncrementSeedAndStep();
                    if (s.Seed > 1 || s.Step > 1)
                        throw new NotSupportedException("can't establish a seed / step in Sqlite autoinc value");

                    //identity = $"AUTOINCREMENT";
                    // Actually no need to set AutoIncrement, if we insert a null value
                    identity = "";
                }
                var nullString = column.AllowDBNull ? "NULL" : "NOT NULL";

                // if auto inc, don't specify NOT NULL option, since we need to insert a null value to make it auto inc.
                if (column.IsAutoIncrement)
                    nullString = "";
                // if it's a readonly column, it could be a computed column, so we need to allow null
                else if (column.IsReadOnly)
                    nullString = "NULL";

                stringBuilder.AppendLine($"\t{empty}{columnName.FullQuotedString} {columnType} {identity} {nullString} {casesensitive}");
                empty = ",";
            }
            stringBuilder.Append("\t,PRIMARY KEY (");
            for (int i = 0; i < this.tableDescription.PrimaryKey.Columns.Length; i++)
            {
                DmColumn pkColumn = this.tableDescription.PrimaryKey.Columns[i];
                var quotedColumnName = new ObjectNameParser(pkColumn.ColumnName).ObjectName;

                stringBuilder.Append(quotedColumnName);

                if (i < this.tableDescription.PrimaryKey.Columns.Length - 1)
                    stringBuilder.Append(", ");
            }
            stringBuilder.Append(")");

            // Constraints
            foreach (DmRelation constraint in this.tableDescription.ParentRelations)
            {

                // Don't want foreign key on same table since it could be a problem on first 
                // sync. We are not sure that parent row will be inserted in first position
                if (String.Equals(constraint.ParentTable.TableName, constraint.ChildTable.TableName, StringComparison.CurrentCultureIgnoreCase))
                    continue;

                var parentTable = constraint.ParentTable;
                var parentTableName = new ObjectNameParser(parentTable.TableName);
                stringBuilder.AppendLine();
                stringBuilder.Append($"\tFOREIGN KEY (");
                empty = string.Empty;
                foreach (var column in constraint.ChildColumns)
                {
                    var columnName = new ObjectNameParser(column.ColumnName);

                    stringBuilder.Append($"{empty} {columnName.FullQuotedString}");
                    empty = ", ";
                }
                stringBuilder.Append($") ");
                stringBuilder.Append($"REFERENCES {parentTableName.FullQuotedString}(");
                empty = string.Empty;
                foreach (var column in constraint.ParentColumns)
                {
                    var columnName = new ObjectNameParser(column.ColumnName);

                    stringBuilder.Append($"{empty} {columnName.FullQuotedString}");
                    empty = ", ";
                }
                stringBuilder.AppendLine(" )");
            }
            stringBuilder.Append(")");
            return new SqliteCommand(stringBuilder.ToString());
        }

        public void CreateTable()
        {
            bool alreadyOpened = connection.State == ConnectionState.Open;

            try
            {
                using (var command = BuildTableCommand())
                {
                    if (!alreadyOpened)
                        connection.Open();

                    if (transaction != null)
                        command.Transaction = transaction;

                    command.Connection = connection;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during CreateTable : {ex}");
                throw;

            }
            finally
            {
                if (!alreadyOpened && connection.State != ConnectionState.Closed)
                    connection.Close();

            }

        }
        public string CreateTableScriptText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var tableNameScript = $"Create Table {tableName.FullQuotedString}";
            var tableScript = BuildTableCommand().CommandText;
            stringBuilder.Append(SqliteBuilder.WrapScriptTextWithComments(tableScript, tableNameScript));
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }


        /// <summary>
        /// For a foreign key, check if the Parent table exists
        /// </summary>
        private bool EnsureForeignKeysTableExist(DmRelation foreignKey)
        {
            var childTable = foreignKey.ChildTable;
            var parentTable = foreignKey.ParentTable;

            // The foreignkey comes from the child table
            var ds = foreignKey.ChildTable.DmSet;

            if (ds == null)
                return false;

            // Check if the parent table is part of the sync configuration
            var exist = ds.Tables.Any(t => ds.IsEqual(t.TableName, parentTable.TableName));

            if (!exist)
                return false;

            bool alreadyOpened = connection.State == ConnectionState.Open;

            try
            {
                if (!alreadyOpened)
                    connection.Open();

                return SqliteManagementUtils.TableExists(connection, transaction, parentTable.TableName);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during EnsureForeignKeysTableExist : {ex}");
                throw;

            }
            finally
            {
                if (!alreadyOpened && connection.State != ConnectionState.Closed)
                    connection.Close();

            }


        }

        /// <summary>
        /// Check if we need to create the table in the current database
        /// </summary>
        public bool NeedToCreateTable()
        {
            return !SqliteManagementUtils.TableExists(connection, transaction, tableName.FullQuotedString);

        }

        public bool NeedToCreateSchema()
        {
            return false;
        }

        public void CreateSchema()
        {
            return;
        }

        public string CreateSchemaScriptText()
        {
            return string.Empty;
        }

        public bool NeedToCreateForeignKeyConstraints(DmRelation constraint)
        {
            return false;
        }

        public void CreateForeignKeyConstraints(DmRelation constraint)
        {
            return;
        }

        public string CreateForeignKeyConstraintsScriptText(DmRelation constraint)
        {
            return string.Empty;
        }

        public void DropTable()
        {
            bool alreadyOpened = connection.State == ConnectionState.Open;

            try
            {
                using (var command = new SqliteCommand($"DROP TABLE IF EXISTS {tableName.FullQuotedString}", connection))
                {
                    if (!alreadyOpened)
                        connection.Open();

                    if (transaction != null)
                        command.Transaction = transaction;

                    command.Connection = connection;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during DropTable : {ex}");
                throw;

            }
            finally
            {
                if (!alreadyOpened && connection.State != ConnectionState.Closed)
                    connection.Close();

            }

        }

        public string DropTableScriptText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var tableNameScript = $"Drop Table {tableName.FullQuotedString}";
            var tableScript = $"DROP TABLE IF EXISTS {tableName.FullQuotedString}";
            stringBuilder.Append(SqliteBuilder.WrapScriptTextWithComments(tableScript, tableNameScript));
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }
    }
}
