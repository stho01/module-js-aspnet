using ModuleJS.Web.Example.Business.Factories.Model;
using ModuleJS.Web.Example.Models.View.Pages;
using System.Web.Mvc;

namespace ModuleJS.Web.Example.Controllers
{
    public class HomeController : Controller
    {

        //**********************************************
        //** fields:
        //**********************************************

        private readonly IModelFactory<HomeViewModel> _homeModelFactory;


        //**********************************************
        //** ctor:
        //**********************************************

        public HomeController(IModelFactory<HomeViewModel> homeModelFactory)
        {
            _homeModelFactory = homeModelFactory;
        }
        
        //**********************************************
        //** public:
        //**********************************************

        public ActionResult Index()
        {
            return View(_homeModelFactory.Create());
        }
    }
}