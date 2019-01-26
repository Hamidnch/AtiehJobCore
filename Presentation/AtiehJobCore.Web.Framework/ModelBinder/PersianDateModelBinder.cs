//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

//namespace AtiehJobCore.Web.Framework.ModelBinder
//{
//public class PersianDateModelBinder : IModelBinder
//    {

//        public Task BindModelAsync(ModelBindingContext bindingContext)
//        {
//            if (bindingContext == null)
//            {
//                throw new ArgumentNullException(nameof(bindingContext));
//            }
//            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
//            var modelState = new ModelState { Value = valueResult };
//            object actualValue = null;
//            try
//            {
//                var parts = valueResult.AttemptedValue.Split('/'); //ex. 1391/1/19
//                if (parts.Length != 3) return null;
//                int year = int.Parse(parts[0]);
//                int month = int.Parse(parts[1]);
//                int day = int.Parse(parts[2]);
//                actualValue = new DateTime(year, month, day, new PersianCalendar());
//            }
//            catch (FormatException e)
//            {
//                modelState.Errors.Add(e);
//            }

//            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
//            return actualValue;
//        }

//        public Task BindModelAsync(ModelBindingContext bindingContext)
//        {
//            if (bindingContext == null)
//            {
//                throw new ArgumentNullException(nameof(bindingContext));
//            }

//            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
//            if (valueProviderResult != ValueProviderResult.None)
//            {
//                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

//                var valueAsString = valueProviderResult.FirstValue;
//                if (string.IsNullOrWhiteSpace(valueAsString))
//                {
//                    return _fallbackBinder.BindModelAsync(bindingContext);
//                }

//                var model = valueAsString.Replace((char)1610, (char)1740).Replace((char)1603, (char)1705);
//                bindingContext.Result = ModelBindingResult.Success(model);
//                return Task.CompletedTask;
//            }

//            return _fallbackBinder.BindModelAsync(bindingContext);
//        }
//    }
//}
//}
