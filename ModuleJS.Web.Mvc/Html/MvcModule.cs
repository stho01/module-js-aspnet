using ModuleJS.Web.Mvc.Helpers;
using ModuleJS.Web.Mvc.Html.Builders;
using ModuleJS.Web.Mvc.Models;
using System;
using System.Web.Mvc;

namespace ModuleJS.Web.Mvc.Html
{
    public class MvcModule : IDisposable
    {

        //**********************************************
        //** properties:
        //**********************************************

        public ModuleMetaData ModuleMetaData { get; }

        //**********************************************
        //** fields:
        //**********************************************

        private readonly ViewContext _viewContext;
        private readonly TagBuilder _tagBuilder;

        //**********************************************
        //** ctor:
        //**********************************************

        public MvcModule(ViewContext viewContext) : this(viewContext, "div") { }
        public MvcModule(ViewContext viewContext, string tagName)
        {
            ModuleMetaData = ModuleMetaHelpers.GetMetaData(viewContext.ViewData.Model);
            _viewContext = viewContext;
            _tagBuilder = new TagBuilder(tagName);
            WriteHead();
        }

        //**********************************************
        //** public:
        //**********************************************

        /// <summary></summary>
        /// <param name="html"></param>
        public void SetContent(string html)
        {
            _tagBuilder.InnerHtml = html;
        }

        public void Dispose()
        {
            
        }
        
        //**********************************************
        //** private:
        //**********************************************

        private void WriteHead()
        {
            _tagBuilder.Attributes.Add(ModuleJSManager.Instance.Config.ModuleAttributeName, ModuleMetaData.ModuleName);
            //TODO: Add options...
        }

        private void WriteEnd()
        {

        }

    }
}