using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MVC5_Ref.Utils
{
    public static class HtmlHelpers
    {
        public static IHtmlString DisplayHelper<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression)
        {
            string value = Convert.ToString(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);

            //do stuffs here and return

            return new HtmlString(value);
        }

        public static string ActivePage(this HtmlHelper helper, string controller, string action)
        {
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();



            string classValue = "";

            return classValue;
        }



    }
}