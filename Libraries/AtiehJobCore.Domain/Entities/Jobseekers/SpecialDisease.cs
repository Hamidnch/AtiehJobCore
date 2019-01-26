using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// بیماری های خاص
    /// </summary>
    public class SpecialDisease : BaseEntity, IAuditableEntity
    {
        public SpecialDisease() { }

        public SpecialDisease(int? specialDiseaseNameCode, string diseaseDescription, int jobSeekerId)
        {
            SpecialDiseaseNameCode = specialDiseaseNameCode;
            DiseaseDescription = diseaseDescription;
            JobseekerId = jobSeekerId;
        }

        /// <summary>
        /// کد بیماری خاص
        /// </summary>
        public int? SpecialDiseaseNameCode { get; set; }

        public virtual SpecialDiseaseName SpecialDiseaseName { get; set; }

        /// <summary>
        /// شرح بیماری
        /// </summary>
        public string DiseaseDescription { get; set; }

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