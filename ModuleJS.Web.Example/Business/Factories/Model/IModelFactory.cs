using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleJS.Web.Example.Business.Factories.Model
{
    public interface IModelFactory<T> where T : class, new()
    {
        T Create();
    }
}
