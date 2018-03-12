using System.Collections.Generic;

namespace ModuleJS.Web.Mvc.Abstraction
{
    /// <summary>
    /// A contract for module options providers.
    /// Implementations allowing custom handeling of the options creation proccess. 
    /// </summary>
    public interface IModuleOptionsProvider
    {
        /// <summary>Gets the options object.</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IDictionary<string, object> GetOptionsObject(object model);

        /// <summary>
        /// gets the options object. 
        /// Merges option values with the given additional options.
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="additionalOptions"></param>
        /// <returns></returns>
        IDictionary<string, object> GetOptionsObject(object model, IDictionary<string, object> additionalOptions);

    }
}