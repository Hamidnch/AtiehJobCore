using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    public class EssentialPhone : BaseEntity, IAuditableEntity
    {
        /// <summary>
        /// تلفن های ضروری
        /// </summary>
        public EssentialPhone() { }

        public EssentialPhone(string name, string family, string relationship,
            string phone, string mobile, int jobseekerId)
        {
            Name = name;
            Family = family;
            Relationship = relationship;
            Phone = phone;
            Mobile = mobile;
            JobseekerId = jobseekerId;
        }

        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// نسبت
        /// </summary>
        public string Relationship { get; set; }

        /// <summary>
        ///  تلفن تماس
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// شماره همراه
        /// </summary>
        public string Mobile { get; set; }

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