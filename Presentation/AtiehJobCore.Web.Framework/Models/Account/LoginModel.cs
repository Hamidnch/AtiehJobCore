﻿using System.ComponentModel.DataAnnotations;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Validators.Account;
using FluentValidation.Attributes;

namespace AtiehJobCore.Web.Framework.Models.Account
{
    [Validator(typeof(LoginValidator))]
    public partial class LoginModel : BaseMongoModel
    {
        //public bool CheckoutAsGuest { get; set; }
        //public UserLoginType UserLoginType { get; set; }
        public bool UsernamesEnabled { get; set; }

        [AtiehJobResourceDisplayName("Account.Login.Fields.UserName")]
        public string Username { get; set; }

        [AtiehJobResourceDisplayName("Account.Login.Fields.EmailOrMobileOrNationalCode")]
        public string EmailOrMobileOrNationalCode { get; set; }
        //[AtiehJobResourceDisplayName("Account.Login.Fields.Email")]
        //public string Email { get; set; }
        //[AtiehJobResourceDisplayName("Account.Login.Fields.MobileNumber")]
        //public string MobileNumber { get; set; }
        //[AtiehJobResourceDisplayName("Account.Login.Fields.NationalCode")]
        //public string NationalCode { get; set; }

        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Login.Fields.Password")]
        public string Password { get; set; }

        [AtiehJobResourceDisplayName("Account.Login.Fields.RememberMe")]
        public bool RememberMe { get; set; }
        /// <summary>
        /// نمایش یا عدم نمایش کپتچا
        /// </summary>
        public bool DisplayCaptcha { get; set; }
    }
}
