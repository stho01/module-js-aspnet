using ModuleJS.Web.Mvc.Html.Builders;
using System;
using System.Collections.Generic;

namespace ModuleJS.Web.Mvc.Abstraction
{
    /// <summary>
    /// A contract for module options providers.
    /// Implementations allowing custom handeling of the options creation proccess. 
    /// </summary>
    public interface IModuleOptionsProvider
    {
        /// <summary>Appends a options object to the container </summary>
        /// <param name="container"></param>
        /// <param name="model"></param>
        void AppendOptionsObject(HtmlElement container, object model, object additionalOptions = null);
    }
}