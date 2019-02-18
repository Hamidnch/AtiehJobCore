using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Models.Account.Jobseeker;
using FluentValidation;

namespace AtiehJobCore.Web.Framework.Validators.Account.Jobseeker
{
    public class RegisterSimpleJobseekerModelValidator : BaseMongoValidator<RegisterSimpleJobseekerModel>
    {
        public RegisterSimpleJobseekerModelValidator(ILocalizationService localizationService,
            UserSettings userSettings)
        {
            #region User name
            if (userSettings.UsernamesEnabled)
            {
                RuleFor(x => x.Username).NotEmpty()
                    .WithMessage(localizationService.GetResource("Account.Fields.Username.Required"));
            }
            #endregion User name

            #region First name and last name
            RuleFor(x => x.FirstName).NotEmpty()
                .When(x => string.IsNullOrEmpty(x.LastName))
                .WithMessage(localizationService.GetResource("Account.Fields.FirstName.Required"));

            RuleFor(x => x.LastName).NotEmpty()
                .When(x => string.IsNullOrEmpty(x.FirstName))
                .WithMessage(localizationService.GetResource("Account.Fields.LastName.Required"));
            #endregion First name and last name

            #region Password
            if (userSettings.IsDisplayPassword)
            {
                RuleFor(x => x.Password).NotEmpty()
                    .WithMessage(localizationService.GetResource("Account.Fields.Password.Required"));

                RuleFor(x => x.Password).Length(userSettings.PasswordMinLength, 999)
                    .WithMessage(string.Format(
                        localizationService.GetResource("Account.Fields.Password.LengthValidation"),
                        userSettings.PasswordMinLength));

                RuleFor(x => x.ConfirmPassword).NotEmpty()
                    .WithMessage(localizationService.GetResource("Account.Fields.ConfirmPassword.Required"));

                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
                    .WithMessage(localizationService.GetResource("Account.Fields.Password.EnteredPasswordsDoNotMatch"));
            }
            #endregion Password

            #region Email
            if (userSettings.IsDisplayEmail && !userSettings.IsOptionalEmail)
            {
                RuleFor(x => x.Email).NotEmpty()
                    .WithMessage(localizationService.GetResource("Account.Fields.Email.Required"));
            }

            if (userSettings.IsDisplayEmail && userSettings.ForceEmailValidation)
            {
                RuleFor(x => x.Email).Must(x => x.IsValidEmail())
                    .WithMessage(localizationService.GetResource("Common.WrongEmail"));
            }
            #endregion Email

            #region Mobile number
            if (userSettings.IsDisplayMobileNumber && !userSettings.IsOptionalMobileNumber)
            {
                RuleFor(x => x.MobileNumber)
                    .NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.MobileNumber.Required"));
            }

            if (userSettings.IsDisplayMobileNumber && userSettings.ForceMobileNumberValidation)
            {
                RuleFor(x => x.MobileNumber).Must(x => x.IsValidMobileNumber())
                    .WithMessage(localizationService.GetResource("Common.WrongMobileNumber"));
            }

            #endregion Mobile number

            #region National code
            if (userSettings.IsDisplayNationalCode && !userSettings.IsOptionalNationalCode)
            {
                RuleFor(x => x.NationalCode)
                    .NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.NationalCode.Required"));
            }

            if (userSettings.IsDisplayNationalCode && userSettings.ForceNationalCodeValidation)
            {
                RuleFor(x => x.NationalCode).Must(x => x.IsValidNationalCode())
                    .WithMessage(localizationService.GetResource("Common.WrongNationalCode"));
            }
            #endregion National code
        }
    }
}
