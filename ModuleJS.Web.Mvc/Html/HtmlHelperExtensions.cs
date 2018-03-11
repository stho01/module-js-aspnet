using ModuleJS.Web.Mvc.Html.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModuleJS.Web.Mvc.Html
{
    /// <summary>HTML Helper extensions that provides easy to use methods for creating an module element.</summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>Uses the current view context to create a module js wrapper.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcModule ModuleFor(this HtmlHelper helper)
        {
            var model = helper.ViewData.Model;

            //var element = HtmlElement.CreateElement(div => {
            //    div.MergeAttribute(ModuleJS.Instance.);
            //});

            return null;    
        }
        
        
    }
}