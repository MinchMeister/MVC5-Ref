using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_Ref.Utils
{
    public class TrimModelBinder : IModelBinder
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TrimModelBinder));

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                var attemptedValue = value?.AttemptedValue;

                return string.IsNullOrWhiteSpace(attemptedValue) ? attemptedValue : attemptedValue.Trim();
            }
            catch (HttpRequestValidationException)
            {
                Trace.TraceWarning("Ilegal characters were found in field {0}", bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName);
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, string.Format("{0} cannot contains HTML <p>, SQL, or Entity Numbers &#38;", bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName));
            }

            //Cast the value provider to an IUnvalidatedValueProvider, which allows to skip validation
            IUnvalidatedValueProvider provider = bindingContext.ValueProvider as IUnvalidatedValueProvider;
            if (provider == null) return null;

            //Get the attempted value, skiping the validation
            var result = provider.GetValue(bindingContext.ModelName, skipValidation: true);
            Debug.Assert(result != null, "result is null");

            return result.AttemptedValue;





            //var shouldPerformRequestValidation = controllerContext.Controller.ValidateRequest && bindingContext.ModelMetadata.RequestValidationEnabled;

            //var valueProviderResult = bindingContext.GetValueFromValueProvider(shouldPerformRequestValidation);

            //if (valueProviderResult != null)
            //{
            //  var theValue = valueProviderResult.AttemptedValue;
            //  return string.IsNullOrWhiteSpace(theValue) ? theValue : theValue.Trim();
            //}
            //return null
        }
    }
}