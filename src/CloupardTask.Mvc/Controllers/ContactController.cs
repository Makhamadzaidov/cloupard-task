using Microsoft.AspNetCore.Mvc;

namespace CloupardTask.Mvc.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
