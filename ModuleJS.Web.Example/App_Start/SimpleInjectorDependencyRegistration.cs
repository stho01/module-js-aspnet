using ModuleJS.Web.Example.Business.Factories.Model;
using ModuleJS.Web.Example.Models.View.Pages;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ModuleJS.Web.Example.App_Start
{
    public static class SimpleInjectorDependencyRegistration
    {
        //**********************************************
        //** public:
        //**********************************************

        /// <summary>
        /// Register a simple injector dependency resolver. 
        /// </summary>
        public static void Register()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            RegisterTypes(container);
            
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }


        //**********************************************
        //** private:
        //**********************************************

        /// <summary>Register resolvable types</summary>
        /// <param name="container"></param>
        private static void RegisterTypes(Container container)
        {
            container.Register(typeof(IModelFactory<>), new[] { typeof(IModelFactory<>).Assembly });
            container.Register<HttpContextBase>(() => new HttpContextWrapper(HttpContext.Current), Lifestyle.Scoped);
        }
    }
}