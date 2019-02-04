using AtiehJobCore.Common.Enums.MongoDb;
using AtiehJobCore.Common.MongoDb.Domain.Users;
using AtiehJobCore.Services.MongoDb.Localization;
using AtiehJobCore.Web.Framework.Models.Account;
using FluentValidation;

namespace AtiehJobCore.Web.Framework.Validators.Account
{
    public class LoginValidator : BaseAtiehJobValidator<LoginModel>
    {
        public LoginValidator(ILocalizationService localizationService, UserSettings userSettings)
        {
            switch (userSettings.UserLoginType)
            {
                case UserLoginType.Username:
                    {
                        //login by username
                        RuleFor(x => x.Username).NotEmpty()
                           .WithMessage(
                                localizationService.GetResource("Account.Login.Fields.Username.Required"));
                        break;
                    }
                case UserLoginType.Email:
                    {
                        //login by email
                        RuleFor(x => x.Email).NotEmpty()
                            .WithMessage(localizationService.GetResource("Account.Login.Fields.Email.Required"));
                        RuleFor(x => x.Email).EmailAddress()
                            .WithMessage(localizationService.GetResource("Common.WrongEmail"));
                        break;
                    }
                case UserLoginType.MobileNumber:
                    {
                        //login by mobile number
                        RuleFor(x => x.MobileNumber).NotEmpty()
                            .WithMessage(
                                localizationService.GetResource("Account.Login.Fields.MobileNumber.Required"));
                        break;
                    }
                case UserLoginType.NationalCode:
                    {
                        //]login by national code
                        RuleFor(x => x.NationalCode).NotEmpty()
                            .WithMessage(
                                localizationService.GetResource("Account.Login.Fields.NationalCode.Required"));
                        break;
                    }
            }

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
