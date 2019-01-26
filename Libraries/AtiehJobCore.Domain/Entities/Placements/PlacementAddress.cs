using AtiehJobCore.Domain.Entities.Address;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Placements
{
    public class PlacementAddress : BaseEntity
    {
        public PlacementAddress() { }

        public PlacementAddress(int? countryCode, int? provinceCode, int? shahrestanCode,
            int? sectionCode, int? cityCode, int? streetCode, string address, string postalCode,
            string phone1, string phone2, string fax, int placementCode)
        {
            CountryCode = countryCode;
            ProvinceCode = provinceCode;
            ShahrestanCode = shahrestanCode;
            SectionCode = sectionCode;
            CityCode = cityCode;
            StreetCode = streetCode;
            Address = address;
            PostalCode = postalCode;
            Phone1 = phone1;
            Phone2 = phone2;
            Fax = fax;
            PlacementCode = placementCode;
        }
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
        /// کد کاریاب
        /// </summary>
        public int PlacementCode { get; set; }
        /// <summary>
        /// ارتباط با کلاس کاریاب
        /// Navigation Property
        /// </summary>
        public virtual Placement Placement { get; set; }
    }
}
