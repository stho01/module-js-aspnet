using ModuleJS.Web.Example.Models;
using ModuleJS.Web.Example.Models.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleJS.Web.Example.Business.Factories.Model
{
    public class HomeFactory : DefaultModelFactory<HomeViewModel>
    {
        /// <summary></summary>
        /// <returns></returns>
        public override HomeViewModel Create()
        {
            var viewModel = base.Create();

            viewModel.TimerModule = new TimerModule
            {
                RememberTime = true
            };

            return viewModel;
        }
    }
}