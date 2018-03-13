using ModuleJS.Web.Mvc.Abstraction;
using ModuleJS.Web.Mvc.DataAnnotations;
using ModuleJS.Web.Mvc.Helpers;
using ModuleJS.Web.Mvc.Html.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModuleJS.Web.Mvc.SystemServices
{
    public class ModuleOptionsProvider : IModuleOptionsProvider
    {
        //**********************************************
        //** public api:
        //**********************************************

        /// <summary></summary>
        /// <param name="container"></param>
        /// <param name="model"></param>
        /// <param name="additionalOptions"></param>
        public void AppendOptionsObject(HtmlElement container, object model, object additionalOptions = null)
        {
            var options = GetOptionsObject(model);
            var optionsAsJson = JsonConvert.SerializeObject(options, ModuleJSManager.Instance.Config.SerializatoinSettings);

            container.MergeAttribute("data-options", optionsAsJson);
        }

        //**********************************************
        //** private api:
        //**********************************************

        /// <summary>Gets the options object.</summary>
        /// <returns>A dictionary where the key is the name of the option</returns>
        protected virtual IDictionary<string, object> GetOptionsObject(object model) => GetOptionsObject(model, null);

        /// <summary>
        /// Gets the options object. 
        /// Merges option values with the given additional options.
        /// 
        /// Note:
        ///     Additional options are prioritized over annotated options.
        /// </summary>
        /// <remarks>
        /// Create a ModuleJS option object. 
        /// Uses reflection to find all Module Option Attributes on the model and 
        /// returns a dictionary where the key is option property name and the value is the property value. 
        /// </remarks>
        /// <param name="model"></param>
        /// <param name="additionalOptions"></param>
        /// <returns>A dictionary where the key is the name of the option</returns>
        protected virtual IDictionary<string, object> GetOptionsObject(object model, IDictionary<string, object> additionalOptions)
        {
            var options = new Dictionary<string, object>();
            foreach (var meta in ModuleMetaHelpers.ExtractOptionsMetaData(model))
                options.Add(meta.OptionName, meta.Property.GetValue(model));

            // Merge with additional options.
            options = MergeOptions(options, additionalOptions);

            if (options.Count == 0)
                return null;

            return options;
        }
        
        /// <summary>
        /// Merge options
        /// Note: 
        ///     Additional Options will be prioritized.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="additionalOptions"></param>
        /// <returns></returns>
        /// <remarks>
        /// Handles null values. 
        /// </remarks>
        protected Dictionary<string, object> MergeOptions(IDictionary<string, object> options, IDictionary<string, object> additionalOptions)
        {
            if (additionalOptions == null)
                return options as Dictionary<string, object>;

            var distinctKeys = options.Keys.Union(additionalOptions.Keys);
            return distinctKeys.ToDictionary(key => key, key => additionalOptions.ContainsKey(key) ? additionalOptions[key] : options[key]);
        }
    }
}