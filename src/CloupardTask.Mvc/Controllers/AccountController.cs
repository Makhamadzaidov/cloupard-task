using CloupardTask.Api.Commons.Exceptions;
using CloupardTask.Service.DTOs.Customers;
using CloupardTask.Service.Interfaces.Accounts;
using CloupardTask.Service.Interfaces.Customers;
using Microsoft.AspNetCore.Mvc;

namespace CloupardTask.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(CustomerLoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Error");
            }

            var user = await _accountService.LoginAsync(dto);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Error");
            }

            if (dto.Email == "admin@gmail.com" && dto.Password == "1234")
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }

            return View("Index", user);
        }


    }
}
