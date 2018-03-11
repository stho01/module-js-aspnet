using ModuleJS.Web.Mvc.Abstraction;
using ModuleJS.Web.Mvc.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc
{
    public class ModuleJS
    {
        /// <summary>
        /// The only instance of the module js class. 
        /// </summary>
        internal static readonly ModuleJS Instance = new ModuleJS();

        //**********************************************
        //** properties:
        //**********************************************

        /// <summary>Current ModuleJS configuration </summary>
        public IModuleJSConfig Config { get; set; }

        //**********************************************
        //** ctor:
        //**********************************************

        private ModuleJS() {}

        //**********************************************
        //** public:
        //**********************************************

        /// <summary>Configure the ModuleJS system.</summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ModuleJS Configure(ModuleJSConfig config)
        {

            return Instance;
        }
    }
}