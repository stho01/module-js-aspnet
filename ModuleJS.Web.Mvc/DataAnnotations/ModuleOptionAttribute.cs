using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc.DataAnnotations
{
    /// <summary></summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ModuleOptionAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
