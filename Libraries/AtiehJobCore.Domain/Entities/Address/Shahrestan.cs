using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Employers;
using AtiehJobCore.Domain.Entities.Jobseekers;
using AtiehJobCore.Domain.Entities.Placements;

namespace AtiehJobCore.Domain.Entities.Address
{
    public class Shahrestan : BaseEntity, IAuditableEntity
    {
        /// <summary>
        /// کلاس شهرستان
        /// </summary>
        public Shahrestan() { }

        public Shahrestan(string name, int provinceCode)
        {
            Name = name;
            ProvinceCode = provinceCode;
        }

        /// <summary>
        /// نام شهرستان
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد استان
        /// </summary>
        public int ProvinceCode { get; set; }

        public virtual Province Province { get; set; }

        /// <summary>
        /// یک شهرستان مجموعه ای بخش دارد
        /// </summary>
        public virtual ICollection<Section> Sections { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// </summary>
        public virtual ICollection<Jobseeker> Jobseekers { get; set; }
        /// <summary>
        /// ارتباط با کلاس آدرس های کارفرما
        /// </summary>
        public virtual ICollection<EmployerAddress> EmployerAddresses { get; set; }
        /// <summary>
        /// ارتباط با کلاس سرویس های کارفرما
        /// </summary>
        public virtual ICollection<EmployerTransfer> EmployerTransfers { get; set; }
        /// <summary>
        /// ارتباط با کلاس آدرس های کاریاب
        /// </summary>
        public virtual ICollection<PlacementAddress> PlacementAddresses { get; set; }
    }
}