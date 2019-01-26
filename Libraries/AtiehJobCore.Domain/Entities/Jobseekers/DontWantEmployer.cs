using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// کارفرمایانی که کارجو تمایل به همکاری با آنها را ندارد.
    /// </summary>
    public class DontWantEmployer : BaseEntity, IAuditableEntity
    {
        public DontWantEmployer() { }

        public DontWantEmployer(string organizationName,
            string phone1, string phone2, string cause, int jobSeekerId)
        {
            OrganizationName = organizationName;
            Phone1 = phone1;
            Phone2 = phone2;
            Cause = cause;
            JobseekerId = jobSeekerId;
        }

        /// <summary>
        /// نام موسسه
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// تلفن 1
        /// </summary>
        public string Phone1 { get; set; }
        /// <summary>
        /// تلفن 2
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// علت عدم تمایل
        /// </summary>
        public string Cause { get; set; }

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
