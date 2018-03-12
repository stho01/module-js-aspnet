using ModuleJS.Web.Mvc.Abstraction;
using Newtonsoft.Json;

namespace ModuleJS.Web.Mvc.Config
{
    public class ModuleJSConfig : IModuleJSConfig
    {

        //**********************************************
        //** properties:
        //**********************************************

        public string ModuleAttributeName { get; set; }

        public JsonSerializerSettings SerializatoinSettings { get;set; }

        public IModuleOptionsProvider OptionsProvider { get; set; }

        
    }
}
