using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Models.Account.Employer;
using FluentValidation;

namespace AtiehJobCore.Web.Framework.Validators.Account.Employer
{
    public class RegisterSimpleEmployerValidator : BaseMongoValidator<RegisterSimpleEmployerModel>
    {
        public RegisterSimpleEmployerValidator(ILocalizationService localizationService,
            UserSettings userSettings)
        {
            #region User name
            if (userSettings.UsernamesEnabled)
            {
                RuleFor(x => x.Username).NotEmpty()
                    .WithMessage(localizationService.GetResource("Account.Fields.Username.Required"));
            }
            #endregion User name

            #region Manager name
            RuleFor(x => x.ManagerName).NotEmpty()
                .WithMessage(localizationService.GetResource("Account.Fields.ManagerName.Required"));

            #endregion Manager name

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
