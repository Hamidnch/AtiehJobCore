using System.Collections.Generic;
using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.Domain.Placements;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Address
{
    public class Section : BaseMongoEntity
    {
        /// <summary>
        /// کلاس بخش
        /// </summary>
        public Section() { }

        public Section(string name, int shahrestanCode)
        {
            //Code = code;
            Name = name;
            ShahrestanCode = shahrestanCode;
        }

        ///// <summary>
        ///// کد بخش
        ///// </summary>
        //public int Code { get; set; }

        /// <summary>
        /// نام بخش
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد شهرستان
        /// </summary>
        public int ShahrestanCode { get; set; }

        public virtual Shahrestan Shahrestan { get; set; }

        /// <summary>
        /// یک بخش مجموعه ای شهر دارد
        /// </summary>
        public virtual ICollection<City> Cities { get; set; }

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
