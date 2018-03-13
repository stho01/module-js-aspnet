using ModuleJS.Web.Mvc.DataAnnotations;
using ModuleJS.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc.Helpers
{
    public static class ModuleMetaHelpers
    {

        //**********************************************
        //** public:
        //**********************************************

        /// <summary>Gets a <see cref="ModuleMetaData"/> that contains usefull meta data of a Module</summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static ModuleMetaData GetMetaData(object module)
        {
            var moduleType = module.GetType();
            var moduleName = moduleType.Name;
            var moduleMeta = moduleType.GetCustomAttributes(typeof(ModuleAttribute), true).FirstOrDefault() as ModuleAttribute;
            
            return new ModuleMetaData
            {
                ModuleName = moduleName,
                ModuleOptionProperties = ExtractOptionsMetaData(moduleType)
            };
        }

        /// <summary>
        /// Gets a collection with property metadata for properties 
        /// that is decorated with the <see cref="ModuleOptionAttribute"/>
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static IEnumerable<ModuleOptionPropertyMetaData> ExtractOptionsMetaData(Type moduleType)
        {
            var metaData = 
                from prop in moduleType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public)
                let attribute = prop.GetCustomAttribute<ModuleOptionAttribute>()
                where attribute != null
                select new ModuleOptionPropertyMetaData { Attribute = attribute, Property = prop };
            
            return metaData;
        }

        /// <summary>
        /// Gets a collection with property metadata for properties 
        /// that is decorated with the <see cref="ModuleOptionAttribute"/>
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static IEnumerable<ModuleOptionPropertyMetaData> ExtractOptionsMetaData(object module) => ExtractOptionsMetaData(module.GetType());
    }
}
