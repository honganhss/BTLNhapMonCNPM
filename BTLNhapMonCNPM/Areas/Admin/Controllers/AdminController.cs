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
        return View();
    }
}