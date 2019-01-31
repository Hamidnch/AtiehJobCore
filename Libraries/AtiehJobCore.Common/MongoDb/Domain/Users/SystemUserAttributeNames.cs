namespace AtiehJobCore.Common.MongoDb.Domain.Users
{
    public static partial class SystemUserAttributeNames
    {
        //Form fields
        public static string FirstName => "FirstName";
        public static string LastName => "LastName";
        public static string Gender => "Gender";
        public static string DateOfBirth => "DateOfBirth";
        public static string Company => "Company";
        public static string StreetAddress => "StreetAddress";
        public static string StreetAddress2 => "StreetAddress2";
        public static string City => "City";
        public static string CountryId => "CountryId";
        public static string StateProvinceId => "StateProvinceId";
        public static string Phone => "Phone";
        public static string Fax => "Fax";
        public static string TimeZoneId => "TimeZoneId";
        public static string CustomUserAttributes => "CustomUserAttributes";

        //Other attributes
        public static string AvatarPictureId => "AvatarPictureId";
        public static string Signature => "Signature";
        public static string PasswordRecoveryToken => "PasswordRecoveryToken";
        public static string PasswordRecoveryTokenDateGenerated => "PasswordRecoveryTokenDateGenerated";
        public static string AccountActivationToken => "AccountActivationToken";
        public static string LastVisitedPage => "LastVisitedPage";
        public static string LastUrlReferrer => "LastUrlReferrer";

        public static string LanguageId => "LanguageId";
        public static string LanguageAutomaticallyDetected => "LanguageAutomaticallyDetected";
        public static string SelectedShippingOption => "SelectedShippingOption";

        public static string NotifiedAboutNewPrivateMessages => "NotifiedAboutNewPrivateMessages";
        public static string WorkingThemeName => "WorkingThemeName";
        public static string EuCookieLawAccepted => "EuCookieLaw.Accepted";
    }
}
