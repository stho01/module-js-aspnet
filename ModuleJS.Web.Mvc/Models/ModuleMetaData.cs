using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc.Models
{
    public class ModuleMetaData
    {
        public string ModuleName { get; set; }
        public IEnumerable<ModuleOptionPropertyMetaData> ModuleOptionProperties { get; set; }
    }
}
