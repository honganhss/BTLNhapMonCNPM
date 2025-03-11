using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.Controllers
{
    public class SearchProductController : Controller
    {
        private readonly PharmacyDbContext _context;

        public SearchProductController(PharmacyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return Json(new { success = false, message = "Không có dữ liệu." });
            }

            var results = await _context.TblSanPhams
                .Join(_context.TblNhaCungCaps,
                    sp => sp.IMaNcc,
                    ncc => ncc.IMaNcc,
                    (sp, ncc) => new { sp, ncc })
                .Where(t => t.sp.STen.Contains(keyword) ||
                            t.sp.IMaSp.ToString().Contains(keyword) ||
                            t.ncc.STenNcc.Contains(keyword))
                .Select(t => new
                {
                    id = t.sp.IMaSp,
                    tenThuoc = t.sp.STen,
                    giaBan = t.sp.FGiaBan,
                    soLuong = t.sp.ISoLuong,
                    image = !string.IsNullOrWhiteSpace(t.sp.SImageThuoc) && System.IO.File.Exists($"wwwroot{t.sp.SImageThuoc}")
                    ? t.sp.SImageThuoc
                    : "/images/no-images.png"
                })
                .ToListAsync();

            return Json(results);
        }

    }
}
