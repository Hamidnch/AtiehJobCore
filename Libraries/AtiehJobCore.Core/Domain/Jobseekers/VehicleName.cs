using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    public class VehicleName : BaseMongoEntity
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
