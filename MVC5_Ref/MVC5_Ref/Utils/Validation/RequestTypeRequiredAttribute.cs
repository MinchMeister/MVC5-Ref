using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MVC5_Ref.Utils.Validation
{
    public class RequestTypeRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string TypeID = null;
        private readonly Regex RequestTypeRegex = null;

        public RequestTypeRequiredAttribute(string TypeID, string RequestType)
        {
            this.TypeID = TypeID;
            this.RequestTypeRegex = new Regex(RequestType);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var objectInstanceType = validationContext.ObjectInstance.GetType();
            var objectInstanceProperty = objectInstanceType.GetProperty(this.TypeID);
            var selectedTypeID = objectInstanceProperty.GetValue(validationContext.ObjectInstance, null);

            if (this.RequestTypeRegex.IsMatch(selectedTypeID.ToString()))
            {
                if(value == null)
                {
                    return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
            //return base.IsValid(value, validationContext);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var requesttyperequired = new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "requesttyperequired", ErrorMessage = this.ErrorMessage } };

            requesttyperequired[0].ValidationParameters.Add("selectedtypeid", this.TypeID);
            requesttyperequired[0].ValidationParameters.Add("requesttyperegex", this.RequestTypeRegex);

            yield return requesttyperequired[0];
        }

    }
}