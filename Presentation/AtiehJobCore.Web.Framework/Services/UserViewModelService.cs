using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Security;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Users;
using AtiehJobCore.Web.Framework.Models.Account;
using AtiehJobCore.Web.Framework.Models.Account.Jobseeker;
using AtiehJobCore.Web.Framework.Models.User;
using AtiehJobCore.Web.Framework.Mvc.Captcha;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial class UserViewModelService : IUserViewModelService
    {
        private readonly IWorkContext _workContext;
        private readonly IUserAttributeService _userAttributeService;
        private readonly IUserAttributeParser _userAttributeParser;
        private readonly UserSettings _userSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly CaptchaSettings _captchaSettings;
        public UserViewModelService(UserSettings userSettings
            , CaptchaSettings captchaSettings, IWorkContext workContext,
            IUserAttributeService userAttributeService,
            IUserAttributeParser userAttributeParser,
            SecuritySettings securitySettings)
        {
            _userSettings = userSettings;
            _captchaSettings = captchaSettings;
            _workContext = workContext;
            _userAttributeService = userAttributeService;
            _userAttributeParser = userAttributeParser;
            _securitySettings = securitySettings;
        }

        public virtual LoginModel PrepareLogin()
        {
            var model = new LoginModel
            {
                UsernamesEnabled = _userSettings.UsernamesEnabled,
                DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage
            };
            return model;
        }

        public RegisterSimpleJobseekerModel PrepareRegisterSimpleJobseekerModel(RegisterSimpleJobseekerModel model, bool excludeProperties,
            string overrideCustomUserAttributesXml = "")
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            //form fields
            model.GenderEnabled = _userSettings.GenderEnabled;
            model.UsernamesEnabled = _userSettings.UsernamesEnabled;
            model.CheckUsernameAvailabilityEnabled = _userSettings.CheckUsernameAvailabilityEnabled;
            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnRegistrationPage;
            model.AcceptPrivacyPolicyEnabled = _userSettings.AcceptPrivacyPolicyEnabled;
            model.HoneypotEnabled = _securitySettings.HoneypotEnabled;
            //custom user attributes
            var customAttributes = PrepareCustomAttributes(_workContext.CurrentUser, overrideCustomUserAttributesXml);
            foreach (var item in customAttributes)
            {
                model.UserAttributes.Add(item);
            }

            return model;
        }
        public virtual IList<UserAttributeModel> PrepareCustomAttributes(User user, string overrideAttributesXml = "")
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var result = new List<UserAttributeModel>();

            var userAttributes = _userAttributeService.GetAllUserAttributes();
            foreach (var attribute in userAttributes)
            {
                var attributeModel = new UserAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = attribute.UserAttributeValues;
                    foreach (var attributeValue in attributeValues)
                    {
                        var valueModel = new UserAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.GetLocalized(x => x.Name),
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(valueModel);
                    }
                }

                //set already selected attributes
                var selectedAttributesXml = !string.IsNullOrEmpty(overrideAttributesXml) ?
                    overrideAttributesXml :
                    user.GetAttribute<string>(SystemUserAttributeNames.CustomUserAttributes);
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!string.IsNullOrEmpty(selectedAttributesXml))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = _userAttributeParser.ParseUserAttributeValues(selectedAttributesXml);
                                foreach (var attributeValue in selectedValues)
                                    if (attributeModel.Id == attributeValue.UserAttributeId)
                                        foreach (var item in attributeModel.Values)
                                            if (attributeValue.Id == item.Id)
                                                item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextBox:
                        {
                            if (!string.IsNullOrEmpty(selectedAttributesXml))
                            {
                                var enteredText = _userAttributeParser.ParseValues(selectedAttributesXml, attribute.Id);
                                if (enteredText.Any())
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                result.Add(attributeModel);
            }


            return result;

        }

        public virtual string ParseCustomAttributes(IFormCollection form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var attributesXml = "";
            var attributes = _userAttributeService.GetAllUserAttributes();
            foreach (var attribute in attributes)
            {
                var controlId = $"user_attribute_{attribute.Id}";
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!string.IsNullOrEmpty(ctrlAttributes))
                            {
                                attributesXml = _userAttributeParser.AddUserAttribute(attributesXml,
                                    attribute, ctrlAttributes);
                            }
                        }
                        break;
                    case AttributeControlType.Checkboxes:
                        {
                            var cblAttributes = form[controlId].ToString();
                            if (!string.IsNullOrEmpty(cblAttributes))
                            {
                                attributesXml = cblAttributes.Split(new[] { ',' },
                                    StringSplitOptions.RemoveEmptyEntries)
                                    .Where(item => !string.IsNullOrEmpty(item)).Aggregate(attributesXml,
                                        (current, item) => _userAttributeParser.AddUserAttribute(current, attribute, item));
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //load read-only (already server-side selected) values
                            var attributeValues = attribute.UserAttributeValues;
                            foreach (var selectedAttributeId in attributeValues
                                .Where(v => v.IsPreSelected)
                                .Select(v => v.Id)
                                .ToList())
                            {
                                attributesXml = _userAttributeParser.AddUserAttribute(attributesXml,
                                            attribute, selectedAttributeId);
                            }
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextBox:
                        {
                            var ctrlAttributes = form[controlId].ToString();
                            if (!string.IsNullOrEmpty(ctrlAttributes))
                            {
                                var enteredText = ctrlAttributes.Trim();
                                attributesXml = _userAttributeParser.AddUserAttribute(attributesXml,
                                    attribute, enteredText);
                            }
                        }
                        break;
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.FileUpload:
                    //not supported user attributes
                    default:
                        break;
                }
            }

            return attributesXml;

        }
    }
}
