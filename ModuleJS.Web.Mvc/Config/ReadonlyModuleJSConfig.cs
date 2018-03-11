using ModuleJS.Web.Mvc.Abstraction;

namespace ModuleJS.Web.Mvc.Config
{
    /// <summary>
    /// A readonly config object. 
    /// </summary>
    public class ReadonlyModuleJSConfig : IModuleJSConfig
    {
        //**********************************************
        //** properties:
        //**********************************************

        public string ModuleAttributeName { get; private set; }
        
        //**********************************************
        //** ctor:
        //**********************************************

        public ReadonlyModuleJSConfig(IModuleJSConfig config)
        {
            foreach (var property in GetType().GetProperties())
            {
                var valueToCopy = property.GetValue(config);
                property.SetValue(valueToCopy, this);
            }
        }
    }
}
