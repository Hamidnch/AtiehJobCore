using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    /// <summary>
    /// زبان خارجه
    /// </summary>
    public class ForeignLanguage : BaseMongoEntity
    {
        public ForeignLanguage() { }

        public ForeignLanguage(int? foreignLanguageNameCode, bool? isLanguageDegree,
            int? languageDegreeTypeCode, ExpertStatus? conversation, ExpertStatus? read,
            ExpertStatus? write, ExpertStatus? translation, int? privilege, int jobseekerId)
        {
            ForeignLanguageNameCode = foreignLanguageNameCode;
            IsLanguageDegree = isLanguageDegree;
            LanguageDegreeTypeCode = languageDegreeTypeCode;
            Conversation = conversation;
            Read = read;
            Write = write;
            Translation = translation;
            Privilege = privilege;
            JobseekerId = jobseekerId;
        }
        /// <summary>
        /// نام زبان خارجه
        /// </summary>
        public int? ForeignLanguageNameCode { get; set; }
        public virtual ForeignLanguageName ForeignLanguageName { get; set; }

        /// <summary>
        /// دارای مدرک زبان؟
        /// </summary>
        public bool? IsLanguageDegree { get; set; }

        /// <summary>
        /// نوع مدرک
        /// </summary>
        public int? LanguageDegreeTypeCode { get; set; }
        public virtual LanguageDegreeType LanguageDegreeType { get; set; }

        /// <summary>
        /// وضعیت مکالمه
        /// </summary>
        public ExpertStatus? Conversation { get; set; }

        /// <summary>
        ///وضعیت خواندن
        /// </summary>
        public ExpertStatus? Read { get; set; }

        /// <summary>
        /// وضعیت نوشتن
        /// </summary>
        public ExpertStatus? Write { get; set; }

        /// <summary>
        /// وضعیت ترجمه
        /// </summary>
        public ExpertStatus? Translation { get; set; }

        /// <summary>
        /// امتیاز
        /// </summary>
        public int? Privilege { get; set; }
        /// <summary>
        /// کد کارجو
        /// </summary>
        public int JobseekerId { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// Navigation Property
        /// </summary>
        public virtual Jobseeker Jobseeker { get; set; }
    }
}
