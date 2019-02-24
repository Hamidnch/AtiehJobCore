﻿using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Employers;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Users;
using AtiehJobCore.Web.Framework.Models.Account.Employer;
using FluentValidation;

namespace AtiehJobCore.Web.Framework.Validators.Account.Employer
{
    public class RegisterEmployerValidator : BaseMongoValidator<RegisterEmployerModel>
    {
        public RegisterEmployerValidator(ILocalizationService localizationService,
            UserSettings userSettings, EmployerSettings employerSettings,
            IUserService userService, IEmployerService employerService)
        {
            #region User name
            if (userSettings.UsernamesEnabled)
            {
                RuleFor(x => x.Username).NotEmpty()
                    .WithMessage(localizationService.GetResource("Account.Fields.Username.Required"));
            }
            #endregion User name

            #region Company and manager name

            RuleFor(x => x.CompanyName).NotEmpty()
                .WithMessage(localizationService.GetResource("Account.Fields.CompanyName.Required"));

            RuleFor(x => x.ManagerName).NotEmpty()
                .WithMessage(localizationService.GetResource("Account.Fields.ManagerName.Required"));

            #endregion Company and manager name

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

            if (userSettings.IsDisplayEmail)
            {
                if (!userSettings.IsOptionalEmail)
                {
                    RuleFor(x => x.Email).NotEmpty()
                        .WithMessage(localizationService.GetResource("Account.Fields.Email.Required"));
                }

                if (userSettings.ForceEmailValidation)
                {
                    RuleFor(x => x.Email).Must(x => x.IsValidEmail())
                        .WithMessage(localizationService.GetResource("Common.WrongEmail"));
                }

                if (!userSettings.AllowDuplicateEmail)
                {
                    RuleFor(x => x.Email).Must(userService.IsDuplicateEmail)
                        .WithMessage(localizationService.GetResource("Common.DuplicateEmail"));
                }
            }

            #endregion Email

            #region Mobile number

            if (userSettings.IsDisplayMobileNumber)
            {
                if (!userSettings.IsOptionalMobileNumber)
                {
                    RuleFor(x => x.MobileNumber)
                        .NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.MobileNumber.Required"));
                }

                if (userSettings.ForceMobileNumberValidation)
                {
                    RuleFor(x => x.MobileNumber).Must(x => x.IsValidMobileNumber())
                        .WithMessage(localizationService.GetResource("Common.WrongMobileNumber"));
                }

                if (!userSettings.AllowDuplicateMobileNumber)
                {
                    RuleFor(x => x.MobileNumber).Must(userService.IsDuplicateMobileNumber)
                        .WithMessage(localizationService.GetResource("Common.DuplicateMobileNumber"));
                }
            }

            #endregion Mobile number

            #region National code

            if (userSettings.IsDisplayNationalCode)
            {
                if (!userSettings.IsOptionalNationalCode)
                {
                    RuleFor(x => x.NationalCode)
                        .NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.NationalCode.Required"));
                }

                if (userSettings.ForceNationalCodeValidation)
                {
                    RuleFor(x => x.NationalCode).Must(x => x.IsValidNationalCode())
                        .WithMessage(localizationService.GetResource("Common.WrongNationalCode"));
                }

                if (!userSettings.AllowDuplicateNationalCode)
                {
                    RuleFor(x => x.NationalCode).Must(userService.IsDuplicateNationalCode)
                        .WithMessage(localizationService.GetResource("Common.DuplicateNationalCode"));
                }
            }

            #endregion National code

            #region Insurance Code

            if (!employerSettings.AllowDuplicateInsuranceCode)
            {
                RuleFor(x => x.NationalCode).Must(employerService.IsDuplicateInsuranceCode)
                    .WithMessage(localizationService.GetResource("Common.DuplicateInsuranceCode"));
            }

            #endregion Insurance Code
        }
    }
}