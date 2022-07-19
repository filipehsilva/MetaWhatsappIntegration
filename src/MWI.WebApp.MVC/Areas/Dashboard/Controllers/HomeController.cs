using Microsoft.AspNetCore.Mvc;

namespace MWI.WebApp.MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Route("placement")]
    public class HomeController : Controller
    {
        [Route("imconnector")]
        public IActionResult ConnDashboard()
        {
            return View();
        }
    }
}
