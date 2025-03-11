using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Areas.Admin.Controllers;


[Area("Admin")]
public class AdminController : Controller
{
    // GET
    [HttpGet]
    [Route("admin")]
    public IActionResult Index()
    {
        string usernameAdmin = HttpContext.Session.GetString("UsernameAdmin");
        if (usernameAdmin != null)
        {
            return View();
        }
        else
        {
            return Redirect("/admin/signin");
        }
    }
}