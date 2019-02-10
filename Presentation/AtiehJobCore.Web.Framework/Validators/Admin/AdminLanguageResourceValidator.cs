using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Models.Admin;
using FluentValidation;

namespace AtiehJobCore.Web.Framework.Validators.Admin
{
    public class AdminLanguageResourceValidator : BaseMongoValidator<AdminLanguageResourceModel>
    {
        public AdminLanguageResourceValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(localizationService
                .GetResource("Admin.Configuration.Languages.Resources.Fields.Name.Required"));
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Admin.Configuration.Languages.Resources.Fields.Value.Required"));
        }
    }
}
