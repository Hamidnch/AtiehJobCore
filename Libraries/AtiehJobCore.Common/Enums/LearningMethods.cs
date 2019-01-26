﻿using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum LearningMethods
    {
        /// <summary>
        /// تجربی
        /// </summary>
        [Display(Name = "تجربی")]
        Experiential = 1,

        /// <summary>
        /// آموزشی
        /// </summary>
        [Display(Name = "آموزشی")]
        Academic = 2
    }
}
