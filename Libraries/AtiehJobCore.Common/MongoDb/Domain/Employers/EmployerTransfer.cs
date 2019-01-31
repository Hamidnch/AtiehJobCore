using AtiehJobCore.Common.MongoDb.Domain.Address;

namespace AtiehJobCore.Common.MongoDb.Domain.Employers
{
    /// <summary>
    /// سرویس ایاب و ذهاب
    /// </summary>
    public class EmployerTransfer : BaseMongoEntity
    {
        public EmployerTransfer() { }

        public EmployerTransfer(string title, int? countryCode, int? provinceCode, int? shahrestanCode,
            int? sectionCode, int? cityCode, int? streetCode, string path, int employerCode)
        {
            Title = title;
            CountryCode = countryCode;
            ProvinceCode = provinceCode;
            ShahrestanCode = shahrestanCode;
            SectionCode = sectionCode;
            CityCode = cityCode;
            StreetCode = streetCode;
            Path = path;
            EmployerCode = employerCode;
        }
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// کشور
        /// </summary>
        public int? CountryCode { get; set; }

        public virtual Country Country { get; set; }

        /// <summary>
        /// استان
        /// </summary>
        public int? ProvinceCode { get; set; }

        public virtual Province Province { get; set; }

        /// <summary>
        /// شهرستان
        /// </summary>
        public int? ShahrestanCode { get; set; }

        public virtual Shahrestan Shahrestan { get; set; }

        /// <summary>
        /// بخش
        /// </summary>
        public int? SectionCode { get; set; }

        public virtual Section Section { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public int? CityCode { get; set; }

        public virtual City City { get; set; }

        /// <summary>
        /// خیابان 
        /// </summary>
        public int? StreetCode { get; set; }
        public virtual Street Street { get; set; }
        /// <summary>
        /// مسیر
        /// </summary>        
        public string Path { get; set; }
        /// <summary>
        /// کد کارفرما
        /// </summary>
        public int EmployerCode { get; set; }
        /// <summary>
        /// ارتباط با کلاس کارفرما
        /// Navigation Property
        /// </summary>
        public virtual Employer Employer { get; set; }
    }
}
