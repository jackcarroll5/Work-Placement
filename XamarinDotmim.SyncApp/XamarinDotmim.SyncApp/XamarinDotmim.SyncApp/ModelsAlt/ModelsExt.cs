using SQLite;
using System;
using System.Collections.Generic;

namespace Sitecert.Mobile.Models
{
    public enum PeriodicityUnitEnum
    {
        Day = 1,
        Week = 2,
        Month = 4,
        Quarter = 8,
        Semester = 16,
        Year = 32
    }
    public enum InspectionResultEnum
    {
        Pass = 1,
        Fail = 2,
        Caution = 3
    }

    public enum CompanyRoleEnum
    {
        Admin = 1,
        ThirdPartyTenant = 2,
        Customer = 4,
        InHouseTenant = 8
    }

    public enum FolderTargetEnum
    {
        Group = 1,
        User = 2,
        Item = 3,
        Inspection = 4,
        Tenant = 5,
        Customer = 6
    }

    public enum GroupTypeEnum
    {
        Group = 1,
        SubGroup = 2,
        Description = 4
    }

    public enum LocationTypeEnum
    {
        Location = 1,
        SubLocation = 2,
    }

    public enum AnswerTypeEnum
    {
        Simple = 1,
        SingleChoice = 2,
        MultiChoice = 4
    }

    public enum AnswerWidgetEnum
    {
        TextBox = 1,
        CheckBox = 2,
        Notes = 3,
        DatePicker = 4,
        FileUpload = 5,
        Gauge = 6,
        Signature = 7,
        RFIDScan = 8,
        CameraCapture = 9,
        DropDownList = 10,
        ListBox = 11,
        OptionBox = 12,
        PassFail = 13,
        YesNo = 14,
        PassCautionFail = 15,
        MultiListBox = 16,
        MultiCheckBox = 17
    }

    public enum DataTypeEnum
    {
        Text = 1,
        Number = 2,
        DecimalNumber = 3,
        Boolean = 4,
        DateTime = 5,
        Time = 6,
        File = 7
    }

    public enum QuestionTargetEnum
    {
        ApplyToInspectionType = 1,
        ApplyToInspectionTypeAndGroups = 2,
        ApplyToGroups = 4,
        ApplyToInspectionDefect = 8,
        ApplyToItemExtField = 16,
        ApplyToGroupCustomField = 32
    }

    public enum UserRoleEnum
    {
        SitecertAdmin = 1, //(2^0)
        ThirdPartyTenantAdmin = 2, //(2^1)
        ThirdPartyTenantManager = 4, // (2^2)
        ThirdPartyTenantSupervisor = 8, // (2^3)
        ThirdPartyTenantFieldOperator = 16, // (2^4)
        ThirdPartyTenantCustomer = 32, //(2^5)
        InHouseTenantAdmin = 64, //(2^6)
        InHouseTenantManager = 128, //(2^7)
        InHouseTenantSupervisor = 256, //(2^7)
        InHouseTenantFieldOperator = 512, //(2^9)
        InHouseTenantViewer = 1024 //(2^10)
    }

    public partial class Item
    {
        [Ignore]
        public IList<ExtFieldAnswer> ExtFieldAnswers { get; private set; }

        [Ignore]
        public IList<GroupCustomFieldAnswer> GroupCustomFieldAnswers { get; private set; }

        public Item()
        {
            ExtFieldAnswers = new List<ExtFieldAnswer>();
            GroupCustomFieldAnswers = new List<GroupCustomFieldAnswer>();
        }
    }

    public partial class CurrentInspection
    {
        [Ignore]
        public IList<CurrentQuestionAnswer> QuestionAnswers { get; private set; }

        [Ignore]
        public IList<CurrentQuestionAnswer> DefectQuestionAnswers { get; private set; }

        [Ignore]
        public IList<Document> Documents { get; private set; }

        public CurrentInspection()
        {
            QuestionAnswers = new List<CurrentQuestionAnswer>();
            DefectQuestionAnswers = new List<CurrentQuestionAnswer>();
            Documents = new List<Document>();
        }

        public HistoricalInspection ToExamHist()
        {
            var examHist = new HistoricalInspection
            {
                ID = ID,
                JobNumber = JobNumber,
                PONumber = PONumber,
                LastTestedOn = LastTestedOn,
                TestedOn = TestedOn,
                ValidPeriodStartOn = ValidPeriodStartOn,
                ValidPeriodEndOn = ValidPeriodEndOn,
                NextTestOn = NextTestOn,
                Result = Result,
                Notes = Notes,
                LocationID = LocationID,
                PeriodicityID = PeriodicityID,
                TypeID = TypeID,
                StatusID = StatusID,
                ExaminerID = ExaminerID,
                ItemID = ItemID,
                FromMobile = FromMobile,
                CreatedOn = CreatedOn,
                ModifiedOn = ModifiedOn,
                CreatedBy = CreatedBy,
                TenantID = TenantID,
                CertNumber = CertNumber,
                IsApproved = IsApproved,
                ApprovedOn = ApprovedOn,
                ApproverID = ApproverID,
                ArchivedOn = DateTimeOffset.UtcNow
            };

            foreach (var q in QuestionAnswers)
            {
                examHist.QuestionAnswers.Add(new HistoricalQuestionAnswer
                {
                    InspectionID = q.InspectionID,
                    QuestionID = q.QuestionID,
                    Answer = q.Answer,
                    Target = q.Target,
                    TenantID = q.TenantID,
                    Notes = q.Notes,
                    DocumentID = q.DocumentID
                });
            }

            foreach (var q in DefectQuestionAnswers)
            {
                examHist.QuestionAnswers.Add(new HistoricalQuestionAnswer
                {
                    InspectionID = q.InspectionID,
                    QuestionID = q.QuestionID,
                    Answer = q.Answer,
                    Target = q.Target,
                    TenantID = q.TenantID,
                    Notes = q.Notes,
                    DocumentID = q.DocumentID
                });
            }

            foreach (var d in Documents)
            {
                examHist.Documents.Add(d);
            }

            return examHist;
        }
    }

    public partial class HistoricalInspection
    {
        [Ignore]
        public IList<HistoricalQuestionAnswer> QuestionAnswers { get; private set; }

        [Ignore]
        public IList<HistoricalQuestionAnswer> DefectQuestionAnswers { get; private set; }

        [Ignore]
        public IList<Document> Documents { get; private set; }

        public HistoricalInspection()
        {
            QuestionAnswers = new List<HistoricalQuestionAnswer>();
            DefectQuestionAnswers = new List<HistoricalQuestionAnswer>();
            Documents = new List<Document>();
        }
    }

    public partial class Periodicity
    {
        public string DurationUnit
        {
            get { return string.Format("{0} {1}(s)", Duration, Unit); }
        }

        public DateTimeOffset CalculateNextExamDate(DateTimeOffset examDate)
        {
            return CalculateNextExamDate(examDate, Unit, Duration);
        }

        public static DateTimeOffset CalculateNextExamDate(DateTimeOffset examDate, PeriodicityUnitEnum unit, int duration)
        {
            switch (unit)
            {
                case PeriodicityUnitEnum.Day:
                    return examDate.AddDays(duration);
                case PeriodicityUnitEnum.Week:
                    return examDate.AddDays(7 * duration);
                case PeriodicityUnitEnum.Month:
                    return examDate.AddMonths(duration);
                case PeriodicityUnitEnum.Quarter:
                    return examDate.AddMonths(3 * duration);
                case PeriodicityUnitEnum.Semester:
                    return examDate.AddMonths(6 * duration);
                case PeriodicityUnitEnum.Year:
                    return examDate.AddYears(duration);
                default:
                    return examDate;
            }
        }
    }

    public partial class Document
    {
        [Ignore]
        public int FolderID { get; set; }

        [Ignore]
        public string Path { get; set; }

        [Ignore]
        public string AlbumPath { get; set; }

        public bool IsNew()
        {
            return !string.IsNullOrEmpty(Path);
        }
    }
}
