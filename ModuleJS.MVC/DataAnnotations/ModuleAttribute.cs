﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Mvc.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
