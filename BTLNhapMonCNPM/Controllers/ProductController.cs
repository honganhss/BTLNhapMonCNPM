using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Controllers
{
    public class ProductController : Controller
    {

        private readonly LoaiSanPhamIT _danhmuc;
        private readonly SanPhamIT _sanpham;

        public ProductController(LoaiSanPhamIT danhmuc, SanPhamIT sanpham)
        {
            _danhmuc = danhmuc;
            _sanpham = sanpham;
        }

        [Route("product/{id?}")]
        public IActionResult Index(long id,int pagination)
        {
            Console.WriteLine(pagination);
            List<TblLoaiSanPham> listLoaiSP = _danhmuc.getAllDanhMuc();
            List<TblSanPham> listSP = _sanpham.getAllSanPham();
            Dictionary<String,object> list = new Dictionary<string, object>();
            list["danhmuc"] = listLoaiSP;
            list["danhmucid"] = id;
            list["pagination"] = pagination;
            list["dsSP"] = listSP;
            return View(list);
        }
    }
}
