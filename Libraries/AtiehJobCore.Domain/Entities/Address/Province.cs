using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Employers;
using AtiehJobCore.Domain.Entities.Jobseekers;
using AtiehJobCore.Domain.Entities.Placements;

namespace AtiehJobCore.Domain.Entities.Address
{
    /// <summary>
    /// کلاس استان
    /// </summary>
    public class Province : BaseEntity, IAuditableEntity
    {
        public Province() { }

        public Province(string name, int countryCode)
        {
            //Code = code;
            Name = name;
            CountryCode = countryCode;
        }

        //public int Code { get; set; }

        /// <summary>
        /// نام استان
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the abbreviation
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// کد کشور
        /// </summary>
        public int CountryCode { get; set; }

        public virtual Country Country { get; set; }

        /// <summary>
        /// یک استان مجموعه ای شهرستان دارد
        /// </summary>
        public virtual ICollection<Shahrestan> Shahrestans { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// </summary>
        public virtual ICollection<Jobseeker> Jobseekers { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
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
        /// <summary>
        /// ارتباط با کاریابی
        /// </summary>
        public virtual ICollection<Placement> Placements { get; set; }
    }
}