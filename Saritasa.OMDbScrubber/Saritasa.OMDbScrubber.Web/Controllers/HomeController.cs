using Microsoft.AspNetCore.Mvc;

namespace Saritasa.OMDbScrubber.Web.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Homepage display.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}