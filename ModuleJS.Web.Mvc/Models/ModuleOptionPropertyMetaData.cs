using ModuleJS.Web.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc.Models
{
    public class ModuleOptionPropertyMetaData
    {
        public string OptionName => !string.IsNullOrEmpty(Attribute?.Name) ? Attribute.Name : Property?.Name;
        public PropertyInfo Property { get; set; }
        public ModuleOptionAttribute Attribute { get; set; }
    }
}
