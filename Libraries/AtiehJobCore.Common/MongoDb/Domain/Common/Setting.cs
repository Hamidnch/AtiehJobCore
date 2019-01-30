﻿using AtiehJobCore.Common.MongoDb.Domain.Localization;
using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Common
{
    public partial class Setting : BaseMongoEntity, ILocalizedEntity
    {
        public Setting()
        {
            Locales = new List<LocalizedProperty>();
        }

        public Setting(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the collection of locales
        /// </summary>
        public IList<LocalizedProperty> Locales { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
