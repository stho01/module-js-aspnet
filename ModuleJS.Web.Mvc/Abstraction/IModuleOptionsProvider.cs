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
        string GetOptionsObject(object model);
    }
}