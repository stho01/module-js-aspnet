  using ModuleJS.Web.Mvc.DataAnnotations;
using ModuleJS.Web.Mvc.Helpers;
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
            var metaData = ModuleMetaHelpers.GetMetaData(module);

            var element = HtmlElement.CreateElement(div => {
                div.MergeAttribute(ModuleJSManager.Instance.Config.ModuleAttributeName, metaData.ModuleName);
                ModuleJSManager.Instance.Config.OptionsProvider.AppendOptionsObject(div, module, null);
                div.AppendRawHtml(helper.DisplayFor(x => module));
            });

            return MvcHtmlString.Create(element.ToString());
        }

        /// <summary></summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MvcModule BeginModule<TModel>(this HtmlHelper helper)
        {
            var module = new MvcModule(helper.ViewContext);
            return module;
        }
    }
}