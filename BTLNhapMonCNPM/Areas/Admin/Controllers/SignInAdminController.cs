using BTLNhapMonCNPM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SignInAdminController : Controller
    {
        private SignIn sign = new SignIn();
        [HttpGet]
        [Route("admin/signin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/signin")]
        public IActionResult Index(string username,string password)
        {
            if (sign.verifyAdmin(username, password) != null)
            {

                HttpContext.Session.SetString("UsernameAdmin", sign.verifyAdmin(username, password).SHoTen);
                return Redirect("/admin");
            }
            else
            {
                ViewData["error"] = true;
                return View("Index");
            }

        }
    }
}
