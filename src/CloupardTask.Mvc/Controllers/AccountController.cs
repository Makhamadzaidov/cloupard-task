using CloupardTask.Service.Interfaces.Customers;
using Microsoft.AspNetCore.Mvc;

namespace CloupardTask.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;

        public AccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /*public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }*/
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
