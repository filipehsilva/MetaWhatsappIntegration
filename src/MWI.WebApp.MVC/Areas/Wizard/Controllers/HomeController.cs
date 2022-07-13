using Microsoft.AspNetCore.Mvc;

namespace MWI.WebApp.MVC.Areas.Wizard.Controllers
{
    [Area("Wizard")]
    [Route("wizard")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("payment")]
        public IActionResult Payment()
        {
            return View();
        }
    }
}
