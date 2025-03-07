using BTLNhapMonCNPM.Areas.Admin.Interfaces;
using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CutomerAccountController : Controller
    {

        private readonly CustomerAccountIT _customer;

        public CutomerAccountController(CustomerAccountIT customer)
        {
            _customer = customer;
        }

        [HttpGet]
        [Route("/admin/account/customer")]
        public IActionResult Index()
        {
            List<TblKhachHang> list = _customer.getAllKhachHang();
            return View(list);
        }
    }
}
