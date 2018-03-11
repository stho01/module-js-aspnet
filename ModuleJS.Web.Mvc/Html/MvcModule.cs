using System;
using System.Web.Mvc;

namespace ModuleJS.Web.Mvc.Html
{
    public class MvcModule : IDisposable
    {
        //**********************************************
        //** fields:
        //**********************************************

        private readonly ViewContext _viewContext;
        
        //**********************************************
        //** ctor:
        //**********************************************

        public MvcModule(ViewContext viewContext)
        {
            _viewContext = viewContext;
        }

        //**********************************************
        //** public:
        //**********************************************

        public void Dispose()
        {
            
        }
    }
}