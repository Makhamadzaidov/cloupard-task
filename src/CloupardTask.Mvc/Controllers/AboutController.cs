using Microsoft.AspNetCore.Mvc;

namespace CloupardTask.Mvc.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
