﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AtiehJobCore.Web.Framework.Models
{
    /// <summary>
    /// Represents base model
    /// </summary>
    public partial class BaseMongoModel
    {
        #region Ctor

        public BaseMongoModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Perform additional actions for binding the model
        /// </summary>
        /// <param name="bindingContext">Model binding context</param>
        /// <remarks>Developers can override this method in custom partial classes in order to add some custom model binding</remarks>
        public virtual void BindModel(ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Perform additional actions for the model initialization
        /// </summary>
        /// <remarks>Developers can override this method in custom partial classes in order to add some custom initialization code to constructors</remarks>
        protected virtual void PostInitialize()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets property to store any custom values for models 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }

        #endregion

    }

    /// <inheritdoc />
    /// <summary>
    /// Represents base entity model
    /// </summary>
    public partial class BaseGrandEntityModel : BaseMongoModel
    {
        /// <summary>
        /// Gets or sets model identifier
        /// </summary>
        public virtual string Id { get; set; }
    }
}
