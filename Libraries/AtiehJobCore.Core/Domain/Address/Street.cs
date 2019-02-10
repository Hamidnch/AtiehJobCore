using System.Collections.Generic;
using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.Domain.Placements;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Address
{
    public class Street : BaseMongoEntity
    {
        /// <summary>
        /// نام خیابان
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// مختصات خیابان
        /// </summary>
        public string Side { get; set; }

        /// <summary>
        /// کد شهر
        /// </summary>
        public int CityCode { get; set; }

        public virtual City City { get; set; }

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
