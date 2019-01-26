using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Address;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.JobOpportunities;

namespace AtiehJobCore.Domain.Entities.Employers
{
    /// <summary>
    /// آدرس های کارفرما
    /// </summary>
    public class EmployerAddress : BaseEntity
    {
        public EmployerAddress() { }

        public EmployerAddress(int? addressTypeCode, int? countryCode, int? provinceCode,
            int? shahrestanCode, int? sectionCode, int? cityCode, int? streetCode,
            string address, string postalCode, string postBox, string phone1,
            string phone2, string fax, int employerCode)
        {
            AddressTypeCode = addressTypeCode;
            CountryCode = countryCode;
            ProvinceCode = provinceCode;
            ShahrestanCode = shahrestanCode;
            SectionCode = sectionCode;
            CityCode = cityCode;
            StreetCode = streetCode;
            Address = address;
            PostalCode = postalCode;
            PostBox = postBox;
            Phone1 = phone1;
            Phone2 = phone2;
            Fax = fax;
            EmployerCode = employerCode;
        }
        /// <summary>
        /// کد نوع آدرس
        /// </summary>
        public int? AddressTypeCode { get; set; }
        public virtual AddressType AddressType { get; set; }

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
        /// آدرس
        /// </summary>        
        public string Address { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// صندوق پستی
        /// </summary>
        public string PostBox { get; set; }
        /// <summary>
        /// تلفن 1
        /// </summary>
        public string Phone1 { get; set; }
        /// <summary>
        /// تلفن 2
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// نمابر
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// کد کارفرما
        /// </summary>
        public int EmployerCode { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارفرما
        /// Navigation Property
        /// </summary>
        public virtual Employer Employer { get; set; }
        /// <summary>
        /// ارتباط با کلاس فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunity> JobOpportunities { get; set; }
    }
}
