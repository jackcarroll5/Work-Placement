﻿using Dotmim.Sync.Data;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using Dotmim.Sync.Builders;
using System.Collections.Generic;
using System.Linq;

namespace Dotmim.Sync.MySql
{
    public static class MySqlExtensionsMethods
    {
        internal static MySqlParameter[] DeriveParameters(this MySqlConnection connection, MySqlCommand cmd, bool includeReturnValueParameter = false, MySqlTransaction transaction = null)
        {
            if (cmd == null) throw new ArgumentNullException("SqlCommand");

            var textParser = new ObjectNameParser(cmd.CommandText);

            // Hack to check for schema name in the spName
            string schemaName = "dbo";
            string spName = textParser.UnquotedString;
            int firstDot = spName.IndexOf('.');
            if (firstDot > 0)
            {
                schemaName = cmd.CommandText.Substring(0, firstDot);
                spName = spName.Substring(firstDot + 1);
            }

            var alreadyOpened = connection.State == ConnectionState.Open;

            if (!alreadyOpened)
                connection.Open();

            try
            {
                MySqlCommandBuilder.DeriveParameters(cmd);
            }
            finally
            {
                if (!alreadyOpened)
                    connection.Close();
            }

            if (!includeReturnValueParameter && cmd.Parameters.Count > 0)
                cmd.Parameters.RemoveAt(0);

            MySqlParameter[] discoveredParameters = new MySqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (MySqlParameter discoveredParameter in discoveredParameters)
                discoveredParameter.Value = DBNull.Value;

            return discoveredParameters;

        }

    }
}
