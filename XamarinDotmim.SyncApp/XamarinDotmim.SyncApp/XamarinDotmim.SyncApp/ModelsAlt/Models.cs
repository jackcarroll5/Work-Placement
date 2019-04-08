using System;
using SQLite;

namespace Sitecert.Mobile.Models
{
    public partial class Folder : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public FolderTargetEnum Target { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Question : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(350)]
        [Collation("nocase")]
        public string Text { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Help { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string DefaultAnswer { get; set; }

        [NotNull]
        public bool IsRequired { get; set; }

        [NotNull]
        public AnswerTypeEnum AnswerType { get; set; }

        [NotNull]
        public AnswerWidgetEnum AnswerWidget { get; set; }

        [Collation("nocase")]
        public string AnswerChoices { get; set; }

        [NotNull]
        public DataTypeEnum DataType { get; set; }

        [NotNull]
        public bool IsShared { get; set; }

        [NotNull]
        public bool IsActive { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Periodicity : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public int Duration { get; set; }

        [NotNull]
        public PeriodicityUnitEnum Unit { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Manufacturer : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Status : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string Display { get; set; }

        [MaxLength(25)]
        [Collation("nocase")]
        public string Color { get; set; }

        [NotNull]
        public bool ShowDefect { get; set; }

        [NotNull]
        public int Rank { get; set; }

        [NotNull]
        public InspectionResultEnum InspectionResult { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Role : BaseModel
    {
        [PrimaryKey]
        public UserRoleEnum ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(400)]
        [Collation("nocase")]
        public string Description { get; set; }

        [NotNull]
        public CompanyRoleEnum CompanyRole { get; set; }
    }

    public partial class Group : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(150)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(8000)]
        public byte[] Photo { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [NotNull]
        public GroupTypeEnum Type { get; set; }

        public Guid? ParentID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Country : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(25)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(8000)]
        public byte[] Flag { get; set; }
    }

    public partial class InspectionType : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string CertTitle { get; set; }

        [MaxLength(25)]
        [Collation("nocase")]
        public string CertTemplateName { get; set; }

        [NotNull]
        public bool CanEditPeriod { get; set; }

        [NotNull]
        public bool IsModel { get; set; }

        [NotNull]
        public int PeriodicityID { get; set; }

        [NotNull]
        public DateTimeOffset CreatedOn { get; set; }

        [NotNull]
        public DateTimeOffset ModifiedOn { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string CreatedBy { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class GroupCustomField : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public Guid GroupID { get; set; }

        [NotNull]
        public int Rank { get; set; }

        [NotNull]
        public int QuestionID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class ExtField : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int Rank { get; set; }

        [NotNull]
        public int QuestionID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class GroupManufacturer : BaseModel
    {
        [PrimaryKey]
        public Guid GroupID { get; set; }

        [PrimaryKey]
        public Guid ManufacturerID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class QuestionString : BaseModel
    {
        [PrimaryKey]
        public int InspectionTypeID { get; set; }

        [PrimaryKey]
        public int CurrentQuestionSetupID { get; set; }

        [PrimaryKey]
        public int SkippedQuestionSetupID { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Condition { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class QuestionHeader : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(250)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public int Rank { get; set; }

        [NotNull]
        public int InspectionTypeID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Company : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(20)]
        [Collation("nocase")]
        public string Acronym { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string AccountNo { get; set; }

        public int? LastCertNo { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string Profile { get; set; }

        [MaxLength(20)]
        [Collation("nocase")]
        public string HomeDir { get; set; }

        [NotNull]
        public DateTime RegStartOn { get; set; }

        public DateTime? RegEndOn { get; set; }

        [MaxLength(8000)]
        public byte[] Logo { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [NotNull]
        public CompanyRoleEnum Role { get; set; }

        [NotNull]
        public bool AutoApprove { get; set; }

        [NotNull]
        public bool IsActive { get; set; }

        public int? ORInspectionTypeID { get; set; }

        public int? InHouseTenantID { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string AddressLine1 { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string AddressLine2 { get; set; }

        [MaxLength(25)]
        [Collation("nocase")]
        public string City { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string Region { get; set; }

        [MaxLength(10)]
        [Collation("nocase")]
        public string PostCode { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string Email { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string WebSite { get; set; }

        [MaxLength(20)]
        [Collation("nocase")]
        public string Phone { get; set; }

        [MaxLength(20)]
        [Collation("nocase")]
        public string Fax { get; set; }

        [NotNull]
        public int CountryID { get; set; }

        public int? SupplierID { get; set; }

        [NotNull]
        public DateTimeOffset CreatedOn { get; set; }

        [NotNull]
        public DateTimeOffset ModifiedOn { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string CreatedBy { get; set; }
    }

    public partial class Division : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public int Type { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string AddressLine1 { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string AddressLine2 { get; set; }

        [MaxLength(25)]
        [Collation("nocase")]
        public string City { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string Region { get; set; }

        [MaxLength(10)]
        [Collation("nocase")]
        public string PostCode { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string Email { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string WebSite { get; set; }

        [MaxLength(20)]
        [Collation("nocase")]
        public string Phone { get; set; }

        [MaxLength(20)]
        [Collation("nocase")]
        public string Fax { get; set; }

        [NotNull]
        public int CountryID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Assignee : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(150)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        public int? CustomerID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class User : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string UserName { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string Email { get; set; }

        [MaxLength(128)]
        [Collation("nocase")]
        public string Password { get; set; }

        [NotNull]
        public int PasswordFormat { get; set; }

        [MaxLength(128)]
        [Collation("nocase")]
        public string PasswordSalt { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string LastName { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string Profile { get; set; }

        [NotNull]
        public bool IsActive { get; set; }

        [MaxLength(8000)]
        public byte[] Photo { get; set; }

        [MaxLength(8000)]
        public byte[] Signature { get; set; }

        [MaxLength(10)]
        [Collation("nocase")]
        public string Culture { get; set; }

        public DateTimeOffset? LastLoginOn { get; set; }

        public DateTimeOffset? LastPasswordChangedOn { get; set; }

        [NotNull]
        public int CompanyID { get; set; }

        [NotNull]
        public DateTimeOffset CreatedOn { get; set; }

        [NotNull]
        public DateTimeOffset ModifiedOn { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string CreatedBy { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Location : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(150)]
        [Collation("nocase")]
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public LocationTypeEnum Type { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string AddressLine1 { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string AddressLine2 { get; set; }

        [MaxLength(25)]
        [Collation("nocase")]
        public string City { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string Region { get; set; }

        [MaxLength(10)]
        [Collation("nocase")]
        public string PostCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? CountryID { get; set; }

        [NotNull]
        public int CustomerID { get; set; }

        public Guid? ParentID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class QuestionAllocation : BaseModel
    {
        [PrimaryKey]
        // [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int QuestionID { get; set; }

        public int? InspectionTypeID { get; set; }

        public Guid? GroupID { get; set; }

        [NotNull]
        public int Rank { get; set; }

        [NotNull]
        public QuestionTargetEnum Target { get; set; }

        public int? QuestionHeaderID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class UserRole : BaseModel
    {
        [PrimaryKey]
        public int UserID { get; set; }

        [PrimaryKey]
        public UserRoleEnum RoleID { get; set; }
    }

    public partial class UserDivision : BaseModel
    {
        [PrimaryKey]
        public int UserID { get; set; }

        [PrimaryKey]
        public int DivisionID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Item : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(30)]
        [Collation("nocase")]
        public string SerialID { get; set; }

        [MaxLength(30)]
        [Collation("nocase")]
        public string ScanID { get; set; }

        [MaxLength(30)]
        [Collation("nocase")]
        public string ORCertNo { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [NotNull]
        public bool IsActive { get; set; }

        [NotNull]
        public bool IsFixed { get; set; }

        [NotNull]
        public Guid GroupID { get; set; }

        [NotNull]
        public Guid LocationID { get; set; }

        public Guid? AssigneeID { get; set; }

        public Guid? ManufacturerID { get; set; }

        public int? CustomerID { get; set; }

        [NotNull]
        public int DivisionID { get; set; }

        [NotNull]
        public bool FromMobile { get; set; }

        [NotNull]
        public DateTimeOffset CreatedOn { get; set; }

        [NotNull]
        public DateTimeOffset ModifiedOn { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string CreatedBy { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class CurrentInspection : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string JobNumber { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string PONumber { get; set; }

        public DateTimeOffset? LastTestedOn { get; set; }

        [NotNull]
        public DateTimeOffset TestedOn { get; set; }

        [NotNull]
        public DateTimeOffset ValidPeriodStartOn { get; set; }

        [NotNull]
        public DateTimeOffset ValidPeriodEndOn { get; set; }

        [NotNull]
        public DateTimeOffset NextTestOn { get; set; }

        public DateTimeOffset? ApprovedOn { get; set; }

        [NotNull]
        public bool IsApproved { get; set; }

        public int? CertNumber { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [NotNull]
        public Guid LocationID { get; set; }

        [NotNull]
        public int PeriodicityID { get; set; }

        [NotNull]
        public int TypeID { get; set; }

        [NotNull]
        public int StatusID { get; set; }

        [NotNull]
        public int ExaminerID { get; set; }

        public int? ApproverID { get; set; }

        [NotNull]
        public Guid ItemID { get; set; }

        [NotNull]
        public InspectionResultEnum Result { get; set; }

        [NotNull]
        public bool FromMobile { get; set; }

        [NotNull]
        public bool IsCurrentCert { get; set; }

        [NotNull]
        public bool IsCurrentTypedCert { get; set; }

        [NotNull]
        public bool IsOngoing { get; set; }

        [NotNull]
        public DateTimeOffset CreatedOn { get; set; }

        [NotNull]
        public DateTimeOffset ModifiedOn { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string CreatedBy { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class HistoricalInspection : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string JobNumber { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string PONumber { get; set; }

        public DateTimeOffset? LastTestedOn { get; set; }

        [NotNull]
        public DateTimeOffset TestedOn { get; set; }

        [NotNull]
        public DateTimeOffset ValidPeriodStartOn { get; set; }

        [NotNull]
        public DateTimeOffset ValidPeriodEndOn { get; set; }

        [NotNull]
        public DateTimeOffset NextTestOn { get; set; }

        public DateTimeOffset? ApprovedOn { get; set; }

        [NotNull]
        public bool IsApproved { get; set; }

        public int? CertNumber { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [NotNull]
        public Guid LocationID { get; set; }

        [NotNull]
        public int PeriodicityID { get; set; }

        [NotNull]
        public int TypeID { get; set; }

        [NotNull]
        public int StatusID { get; set; }

        [NotNull]
        public int ExaminerID { get; set; }

        public int? ApproverID { get; set; }

        [NotNull]
        public Guid ItemID { get; set; }

        [NotNull]
        public InspectionResultEnum Result { get; set; }

        [NotNull]
        public bool FromMobile { get; set; }

        [NotNull]
        public DateTimeOffset CreatedOn { get; set; }

        [NotNull]
        public DateTimeOffset ModifiedOn { get; set; }

        [MaxLength(50)]
        [Collation("nocase")]
        public string CreatedBy { get; set; }

        [NotNull]
        public DateTimeOffset ArchivedOn { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class ExtFieldAnswer : BaseModel
    {
        [PrimaryKey]
        public int QuestionID { get; set; }

        [PrimaryKey]
        public Guid ItemID { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Value { get; set; }

        public Guid? DocumentID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class GroupCustomFieldAnswer : BaseModel
    {
        [PrimaryKey]
        public int QuestionID { get; set; }

        [PrimaryKey]
        public Guid ItemID { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Value { get; set; }

        public Guid? DocumentID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class CurrentQuestionAnswer : BaseModel
    {
        [PrimaryKey]
        public Guid InspectionID { get; set; }

        [PrimaryKey]
        public int QuestionID { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Answer { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [NotNull]
        public QuestionTargetEnum Target { get; set; }

        public Guid? DocumentID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class HistoricalQuestionAnswer : BaseModel
    {
        [PrimaryKey]
        public Guid InspectionID { get; set; }

        [PrimaryKey]
        public int QuestionID { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Answer { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Notes { get; set; }

        [NotNull]
        public QuestionTargetEnum Target { get; set; }

        public Guid? DocumentID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class Document : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        [MaxLength(80)]
        [Collation("nocase")]
        public string Title { get; set; }

        [MaxLength(160)]
        [Collation("nocase")]
        public string FileName { get; set; }

        [MaxLength(10)]
        [Collation("nocase")]
        public string FileExtension { get; set; }

        [MaxLength(8000)]
        [Collation("nocase")]
        public string Summary { get; set; }

        [MaxLength(160)]
        [Collation("nocase")]
        public string ContentType { get; set; }

        [MaxLength(255)]
        [Collation("nocase")]
        public string URL { get; set; }

        [NotNull]
        public int Location { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }

    public partial class FolderDocument : BaseModel
    {
        [PrimaryKey]
        public Guid DocumentID { get; set; }

        [NotNull]
        public int FolderID { get; set; }

        public int? UserID { get; set; }

        public Guid? GroupID { get; set; }

        public Guid? ItemID { get; set; }

        public Guid? InspectionID { get; set; }

        public int? CompanyID { get; set; }

        [NotNull]
        public int TenantID { get; set; }
    }
    public partial class QueueItem : BaseModel
    {
        [PrimaryKey]
        public Guid ItemID { get; set; }

        [PrimaryKey]
        public int UserID { get; set; }

        [NotNull]
        public int CustomerID { get; set; }

        [NotNull]
        public int TimeStamp { get; set; }
    }
    public partial class LocalDocuent : BaseModel
    {
        [PrimaryKey]
        public Guid DocumentID { get; set; }

        public string Path { get; set; }

        public string AlbumPath { get; set; }

        [NotNull]
        public bool Uploaded { get; set; }
    }

    public class BaseModel
    {
    }
}
