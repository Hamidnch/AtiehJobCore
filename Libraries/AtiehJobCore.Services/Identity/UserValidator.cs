﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using Microsoft.AspNetCore.Identity;

namespace AtiehJobCore.Services.Identity
{
    public class UserValidator : UserValidator<User>
    {
        private readonly ISet<string> _emailsBanList;

        public UserValidator(IdentityErrorDescriber errors,
            //,IOptionsSnapshot<SiteSettings> configurationRoot
            SiteSettings siteSettings
            ) : base(errors)
        {
            //configurationRoot.CheckArgumentIsNull(nameof(configurationRoot));
            //_emailsBanList = new HashSet<string>(
            //    configurationRoot.Value.EmailsBanList, StringComparer.OrdinalIgnoreCase);

            //var siteSettings = EngineContext.Current.Resolve<SiteSettings>();
            siteSettings.CheckArgumentIsNull(nameof(siteSettings));

            _emailsBanList = new HashSet<string>(
                siteSettings.EmailsBanList, StringComparer.OrdinalIgnoreCase);
            if (!_emailsBanList.Any())
            {
                throw new InvalidOperationException(
                    "Please fill the emails ban list in the appsettings.json file.");
            }
        }

        public override async Task<IdentityResult> ValidateAsync(
            UserManager<User> manager, User user)
        {
            // First use the built-in validator
            var result = await base.ValidateAsync(manager, user);
            var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            // Extending the built-in validator
            ValidateEmail(user, errors);
            ValidateUserName(user, errors);

            return !errors.Any() ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

        private void ValidateEmail(User user, ICollection<IdentityError> errors)
        {
            var userEmail = user?.Email;
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    errors.Add(new IdentityError
                    {
                        Code = "EmailIsNotSet",
                        Description = "لطفا اطلاعات ایمیل را تکمیل کنید."
                    });
                }
                return; // base.ValidateAsync() will cover this case
            }

            if (_emailsBanList.Any(
                email => userEmail.EndsWith(email, StringComparison.OrdinalIgnoreCase)))
            {
                errors.Add(new IdentityError
                {
                    Code = "BadEmailDomainError",
                    Description = "لطفا یک ایمیل پروایدر معتبر را وارد نمائید."
                });
            }
        }

        private static void ValidateUserName(User user, ICollection<IdentityError> errors)
        {
            var userName = user?.UserName;
            if (string.IsNullOrWhiteSpace(userName))
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    errors.Add(new IdentityError
                    {
                        Code = "UserIsNotSet",
                        Description = "لطفا اطلاعات کاربری را تکمیل کنید."
                    });
                }
                return;  // base.ValidateAsync() will cover this case
            }

            if (userName.IsNumeric() || userName.ContainsNumber())
            {
                errors.Add(new IdentityError
                {
                    Code = "BadUserNameError",
                    Description = "نام کاربری وارد شده نمی‌تواند حاوی اعداد باشد."
                });
            }

            if (userName.HasConsecutiveChars())
            {
                errors.Add(new IdentityError
                {
                    Code = "BadUserNameError",
                    Description = "نام کاربری وارد شده معتبر نیست."
                });
            }
        }
    }
}
