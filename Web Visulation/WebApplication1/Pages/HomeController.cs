using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Pages
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
