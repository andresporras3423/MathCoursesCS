using Microsoft.AspNetCore.Mvc;

namespace MathCoursesCS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
