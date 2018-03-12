  using ModuleJS.Web.Mvc.DataAnnotations;
using ModuleJS.Web.Mvc.Html.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ModuleJS.Web.Mvc.Html
{
    /// <summary>HTML Helper extensions that provides easy to use methods for creating an module element.</summary>
    public static class HtmlHelperExtensions
    {
        
        /// <summary>Uses the current view context to create a module js wrapper.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString Module<TModel, TModule>(this HtmlHelper<TModel> helper, TModule module)
        {
            var moduleType = module.GetType();
            var moduleName = moduleType.Name;
            var moduleMeta = moduleType.GetCustomAttributes(typeof(ModuleAttribute), true).FirstOrDefault() as ModuleAttribute;

            var element = HtmlElement.CreateElement(div => {
                div.MergeAttribute(ModuleJSManager.Instance.Config.ModuleAttributeName, moduleName);
                ModuleJSManager.Instance.Config.OptionsProvider.AppendOptionsObject(div, module, null);
                div.AppendRawHtml(helper.DisplayFor(x => module));
            });

            return MvcHtmlString.Create(element.ToString());
        }
        
    }
}