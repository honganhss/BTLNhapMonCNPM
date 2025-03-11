using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BTLNhapMonCNPM.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SanPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult XoaSanPham()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TimSanPham(int iMaSP)
        {
            var sanPham = _context.tblSanPham
                .Where(sp => sp.iMaSP == iMaSP)
                .Select(sp => new SanPham
                {
                    iMaSP = sp.iMaSP,
                    sTen = sp.sTen,
                    //sDangThuoc = sp.sDangThuoc,
                    sDonViTinh = sp.sDonViTinh,
                    iSoLuong = sp.iSoLuong,
                    sCachSuDung = sp.sCachSuDung,
                    sDieuKienBaoQuan = sp.sDieuKienBaoQuan,
                    sHanSuDung = sp.sHanSuDung,
                    fGiaNhap = sp.fGiaNhap, 
                    fGiaBan = sp.fGiaBan 
                })
                .FirstOrDefault();

            if (sanPham == null)
            {
                ViewBag.Error = "Không tìm thấy sản phẩm!";
                return View("XoaSanPham");
            }
            return View("XoaSanPham", sanPham);
        }

        [HttpPost]
        public IActionResult XacNhanXoa(int iMaSP)
        {
            using (var transaction = _context.Database.BeginTransaction()) // Bắt đầu transaction
            {
                try
                {
                    // Xóa tất cả bản ghi trong tblLoSanPham có iSanPhamId = iMaSP
                    var loSanPhams = _context.tblLoSanPham.Where(lsp => lsp.iSanPhamId == iMaSP);
                    _context.tblLoSanPham.RemoveRange(loSanPhams);
                    _context.SaveChanges();

                    // Xóa sản phẩm trong tblSanPham
                    var sanPham = _context.tblSanPham.FirstOrDefault(sp => sp.iMaSP == iMaSP);
                    if (sanPham == null)
                    {
                        ViewBag.Error = "Sản phẩm không tồn tại!";
                        return View("XoaSanPham");
                    }

                    _context.tblSanPham.Remove(sanPham);
                    _context.SaveChanges(); // Lưu thay đổi vào database

                    transaction.Commit(); // Hoàn thành transaction
                    ViewBag.Message = "Xóa sản phẩm thành công!";
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hoàn tác nếu có lỗi
                    ViewBag.Error = "Lỗi khi xóa sản phẩm: " + ex.Message;
                }
            }
            return View("XoaSanPham");
        }
    }
}
