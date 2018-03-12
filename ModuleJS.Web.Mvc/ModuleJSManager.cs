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
        
        private IModuleJSConfig _config = new DefaultModuleJSConfig();

        //**********************************************
        //** ctor:
        //**********************************************

        private ModuleJSManager() {}

        //**********************************************
        //** public:
        //**********************************************
        
        /// <summary>Configure the ModuleJS system.</summary>
        /// <param name="config"></param>
        /// <returns>Instance of it self</returns>
        public ModuleJSManager Initialize(Action<ModuleJSConfig> configure)
        {
            var initializer = new Initializer();
            _config = initializer.Initialize(configure);
            return this;
        }
    }
}