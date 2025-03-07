using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductManagerController : Controller
    {
        private readonly LoaiSanPhamIT _danhmuc;
        private readonly SanPhamIT _sanpham;
        public ProductManagerController(LoaiSanPhamIT danhmuc, SanPhamIT sanpham, NhaCCIT nhacc)
        {
            _danhmuc = danhmuc;
            _sanpham = sanpham;
        }

        
        
        [HttpGet]
        [Route("admin/product/{id?}")]
        public IActionResult Index(long id, int pagination)
        {
            Console.WriteLine(id);
            List<TblLoaiSanPham> listLoaiSP = _danhmuc.getAllDanhMuc();
            List<TblSanPham> listSP = _sanpham.getAllSanPham();
            Dictionary<String, object> list = new Dictionary<string, object>();
            list["danhmuc"] = listLoaiSP;
            list["danhmucid"] = id;
            list["pagination"] = pagination;
            list["dsSP"] = listSP;
            return View(list);
        }
    }
}
