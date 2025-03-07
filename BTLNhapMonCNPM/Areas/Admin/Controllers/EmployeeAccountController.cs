using BTLNhapMonCNPM.Areas.Admin.Interfaces;
using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeAccountController : Controller
    {

        private readonly EmployeeAccountIT _employee;
        public EmployeeAccountController(EmployeeAccountIT employee)
        {
            _employee = employee;
        }

        [HttpGet]
        [Route("admin/account/employee")]
        public IActionResult Index()
        {
            List<TblNhanVien> list = _employee.getAllNhanVien();
            return View(list);
        }
    }
}
