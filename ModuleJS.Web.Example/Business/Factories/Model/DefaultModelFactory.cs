using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleJS.Web.Example.Business.Factories.Model
{
    public class DefaultModelFactory<T> : IModelFactory<T> where T : class, new()
    {
        public virtual T Create() => new T();
    }
}