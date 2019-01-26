using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    public class VehicleName : BaseEntity, IAuditableEntity
    {
        /// <summary>
        /// وسایل نقلیه
        /// </summary>

        public VehicleName()
        {
        }

        public VehicleName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// نام وسیله نقلیه
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ارتباط با کلاس وسایل نقلیه
        /// </summary>
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
