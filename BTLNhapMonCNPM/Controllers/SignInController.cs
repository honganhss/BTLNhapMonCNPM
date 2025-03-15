using BTLNhapMonCNPM.Models;
using BTLNhapMonCNPM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Controllers
{
    public class SignInController : Controller
    {

        private  SignIn sign = new SignIn();

        [HttpGet]
        [Route("signin")]
        public IActionResult Index(string username, string password)
        {

            return View();
        }

        [HttpGet]
        [Route("signout")]
        public IActionResult hehe(string username, string password)
        {
            HttpContext.Session.Remove("Username");
            return Redirect("/");
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Index1(string username,string password)
        {
            if (sign.verify(username, password) != null)
            {
                HttpContext.Session.SetString("Username", sign.verify(username, password).SHoTen);
                string makh = "" + sign.verify(username, password).IMaKh;
                Console.WriteLine(makh);
                HttpContext.Session.SetString("UsernameID", makh);
                return Redirect("/");
            }
            else
            {
                ViewData["error"] = true;
                return View("Index");
            }
        }
        
    }
}
