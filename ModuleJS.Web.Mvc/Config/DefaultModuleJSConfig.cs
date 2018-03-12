using ModuleJS.Web.Mvc.Abstraction;
using ModuleJS.Web.Mvc.SystemServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc.Config
{
    internal class DefaultModuleJSConfig : IModuleJSConfig
    {
        //**********************************************
        //** properties:
        //**********************************************

        public string ModuleAttributeName { get; }

        public JsonSerializerSettings SerializatoinSettings { get; }

        public IModuleOptionsProvider OptionsProvider { get; }


        //**********************************************
        //** ctor:
        //**********************************************

        public DefaultModuleJSConfig()
        {
            ModuleAttributeName = "data-module";
            OptionsProvider = new ModuleOptionsProvider();
            SerializatoinSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
