using System.Collections.Generic;
using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.Domain.Placements;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Address
{
    public class Shahrestan : BaseMongoEntity
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
