using Newtonsoft.Json;

namespace ModuleJS.Web.Mvc.Abstraction
{
    public interface IModuleJSConfig
    {
        string ModuleAttributeName { get; }
        JsonSerializerSettings SerializatoinSettings { get; }
        IModuleOptionsProvider OptionsProvider { get; }
    }
}
