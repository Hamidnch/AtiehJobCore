﻿namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
{
    /// <summary>
    /// وسیله نقلیه کارجو
    /// </summary>
    public class Vehicle : BaseMongoEntity
    {
        public Vehicle() { }

        public Vehicle(int? vehicleNameCode, string vehicleModel, int jobSeekerId)
        {
            VehicleNameCode = vehicleNameCode;
            VehicleModel = vehicleModel;
            JobseekerId = jobSeekerId;
        }
        /// <summary>
        /// کد وسیله نقلیه
        /// </summary>
        public int? VehicleNameCode { get; set; }
        public virtual VehicleName VehicleName { get; set; }
        /// <summary>
        /// مدل وسیله نقلیه
        /// </summary>
        public string VehicleModel { get; set; }
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
