using System.Collections.Generic;
using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.Domain.Placements;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Address
{
    public class City : BaseMongoEntity
    {
        public City() { }

        public City(string name, int sectionCode)
        {
            //Code = code;
            Name = name;
            SectionCode = sectionCode;
        }

        ///// <summary>
        ///// کد شهر
        ///// </summary>
        //public int Code { get; set; }

        /// <summary>
        /// نام شهر
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد بخش
        /// </summary>
        public int SectionCode { get; set; }

        public virtual Section Section { get; set; }

        /// <summary>
        /// یک شهر مجموعه ای خیابان دارد
        /// </summary>
        public virtual ICollection<Street> Streets { get; set; }

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
