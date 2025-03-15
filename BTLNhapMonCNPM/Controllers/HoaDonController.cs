using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly PharmacyDbContext _context;

        public HoaDonController(PharmacyDbContext context)
        {
            _context = context;
        }

        public IActionResult DanhSachHoaDon(int maKhachHang)
        {
            var hoaDons = _context.TblHoaDons
                .Where(hd => hd.IMaKh == maKhachHang)
                .OrderByDescending(hd => hd.DThoiGian) // Sắp xếp mới nhất trước
                .ToList();

            ViewBag.KhachHangId = maKhachHang; // Gửi ID khách hàng xuống view

            return View(hoaDons);
        }
    }
}
