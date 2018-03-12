using ModuleJS.Web.Mvc.Abstraction;
using ModuleJS.Web.Mvc.Config;
using ModuleJS.Web.Mvc.SystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc
{
    public class ModuleJSManager
    {
        /// <summary>
        /// The only instance of the module js class. 
        /// </summary>
        private static readonly ModuleJSManager _instance = new ModuleJSManager();

        //**********************************************
        //** properties:
        //**********************************************

        /// <summary>The one and only instance of the ModuleJSManager class.</summary>
        public static ModuleJSManager Instance => _instance;
        
        /// <summary>Current ModuleJS configuration </summary>
        public IModuleJSConfig Config => _config;

        /// <summary>
        /// The options provider that creats the module js options object passed down 
        /// to the javascript module. 
        /// </summary>
        public IModuleOptionsProvider OptionsProvider { get; private set; }


        //**********************************************
        //** fields:
        //**********************************************
        

        private readonly ModuleJSConfig _config = new ModuleJSConfig();

        //**********************************************
        //** ctor:
        //**********************************************

        private ModuleJSManager() { }

        //**********************************************
        //** public:
        //**********************************************

        /// <summary>Initializes the ModuleJSManager</summary>
        /// <remarks>Sets the defaults and makes the system ready to be used by the client.</remarks>
        /// <returns>Instance of it self</returns>
        public ModuleJSManager Initialize()
        {
            OptionsProvider = new ModuleOptionsProvider();
            return this;
        }

        /// <summary>Configure the ModuleJS system.</summary>
        /// <param name="config"></param>
        /// <returns>Instance of it self</returns>
        public ModuleJSManager Configure(Action<ModuleJSConfig> configure)
        {
            configure(_config);
            return this;
        }
    }
}