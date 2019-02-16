using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Models.Account;
using FluentValidation;

namespace AtiehJobCore.Web.Framework.Validators.Account
{
    public class LoginValidator : BaseMongoValidator<LoginModel>
    {
        public LoginValidator(ILocalizationService localizationService, UserSettings userSettings)
        {
            if (userSettings.UsernamesEnabled)
            {
                return;
            }

            //login by email
            RuleFor(x => x.EmailOrMobileOrNationalCode)
               .NotEmpty()
               .WithMessage(localizationService.GetResource("Account.Login.Fields.EmailOrMobileOrNationalCode.Required"));
            RuleFor(x => x.EmailOrMobileOrNationalCode)
               .Must(y => y.IsValidMobileNumberOrEmailOrNationalCode())
               .WithMessage(localizationService.GetResource("Common.WrongEmailOrMobileOrNationalCode"));

            //switch (userSettings.UserLoginType)
            //{
            //    case UserLoginType.Username:
            //        {
            //            //login by username
            //            RuleFor(x => x.Username).NotEmpty()
            //               .WithMessage(
            //                    localizationService.GetResource("Account.Login.Fields.Username.Required"));
            //            break;
            //        }
            //    case UserLoginType.Email:
            //        {
            //            //login by email
            //            RuleFor(x => x.Email).NotEmpty()
            //                .WithMessage(localizationService.GetResource("Account.Login.Fields.Email.Required"));
            //            RuleFor(x => x.Email).EmailAddress()
            //                .WithMessage(localizationService.GetResource("Common.WrongEmail"));
            //            break;
            //        }
            //    case UserLoginType.MobileNumber:
            //        {
            //            //login by mobile number
            //            RuleFor(x => x.MobileNumber).NotEmpty()
            //                .WithMessage(
            //                    localizationService.GetResource("Account.Login.Fields.MobileNumber.Required"));
            //            break;
            //        }
            //    case UserLoginType.NationalCode:
            //        {
            //            //]login by national code
            //            RuleFor(x => x.NationalCode).NotEmpty()
            //                .WithMessage(
            //                    localizationService.GetResource("Account.Login.Fields.NationalCode.Required"));
            //            break;
            //        }
            //}

            //var userLoginType = userSettings.UserLoginType;

            //RuleFor(x => x.Username).NotEmpty()
            //   .WithMessage(
            //        localizationService
            //           .GetResource($"Account.Login.Fields.{userLoginType.ToString()}.Required"));

            //if (userLoginType == UserLoginType.Email)
            //{
            //    RuleFor(x => x.Email).EmailAddress()
            //       .WithMessage(localizationService.GetResource("Common.WrongEmail"));
        }
    }
}
