using ModuleJS.Web.Mvc.Abstraction;
using ModuleJS.Web.Mvc.Config;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc.SystemServices
{
    public class Initializer
    {
        
        //**********************************************
        //** public api: 
        //**********************************************
        
        /// <summary></summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public IModuleJSConfig Initialize(Action<ModuleJSConfig> configure)
        {
            var config = new ModuleJSConfig();
            configure(config);

            return MergeConfig(new DefaultModuleJSConfig(), config);
        }

        /// <summary></summary>
        /// <param name="configs"></param>
        /// <returns></returns>
        protected IModuleJSConfig MergeConfig(params IModuleJSConfig[] configs)
        {

            // Just for now.... 
            // TODO: Merge configs...
            return configs.FirstOrDefault();
        }
    }
}
