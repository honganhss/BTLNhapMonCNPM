using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Controllers
{
    public class ProductController : Controller
    {

		private readonly LoaiSanPhamIT _danhmuc;
		private readonly SanPhamIT _sanpham;
		private readonly NhaCungCapImpl _nhacc;

		public ProductController(LoaiSanPhamIT danhmuc, SanPhamIT sanpham, NhaCungCapImpl nhacc)
		{
			_danhmuc = danhmuc;
			_sanpham = sanpham;
			_nhacc = nhacc;
		}

		[Route("product/{id?}")]
        public IActionResult Index(long id,int pagination)
        {
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
