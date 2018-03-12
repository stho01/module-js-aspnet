using ModuleJS.Web.Mvc.Abstraction;
using ModuleJS.Web.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModuleJS.Web.Mvc.SystemServices
{
    public class ModuleOptionsProvider : IModuleOptionsProvider
    {
        //**********************************************
        //** public:
        //**********************************************

        /// <summary>Gets the options object.</summary>
        /// <returns>A dictionary where the key is the name of the option</returns>
        public IDictionary<string, object> GetOptionsObject(object model) => GetOptionsObject(model, null);

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
        public IDictionary<string, object> GetOptionsObject(object model, IDictionary<string, object> additionalOptions)
        {

            var options = new Dictionary<string, object>();
            var optionProperties = model.GetType().GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => Attribute.IsDefined(prop, typeof(ModuleOptionAttribute)))
                .Select(prop => new Tuple<PropertyInfo, ModuleOptionAttribute>(prop, prop.GetCustomAttribute<ModuleOptionAttribute>())).ToList();

            optionProperties.ForEach(data => {
                string optionName = string.IsNullOrEmpty(data.Item2.Name) ? data.Item1.Name : data.Item2.Name;

                options.Add(optionName, data.Item1.GetValue(model));
            });

            // Merge with additional options.
            options = MergeOptions(options, additionalOptions);

            if (options.Count == 0)
                return null;


            return options;
        }

        //**********************************************
        //** private:
        //**********************************************

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
        private Dictionary<string, object> MergeOptions(IDictionary<string, object> options, IDictionary<string, object> additionalOptions)
        {
            if (additionalOptions == null)
                return options as Dictionary<string, object>;

            var distinctKeys = options.Keys.Union(additionalOptions.Keys);
            return distinctKeys.ToDictionary(key => key, key => additionalOptions.ContainsKey(key) ? additionalOptions[key] : options[key]);
        }
    }
}
