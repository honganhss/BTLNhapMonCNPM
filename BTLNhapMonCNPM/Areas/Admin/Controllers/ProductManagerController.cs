using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTLNhapMonCNPM.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductManagerController : Controller
    {
        private readonly LoaiSanPhamIT _danhmuc;
        private readonly SanPhamIT _sanpham;
		private readonly NhaCCIT _nhacc;
		public ProductManagerController(LoaiSanPhamIT danhmuc, SanPhamIT sanpham, NhaCCIT nhacc)
        {
            _danhmuc = danhmuc;
            _sanpham = sanpham;
			_nhacc = nhacc;
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


		[HttpGet]
		[Route("admin/product/add")]
		public IActionResult Add()
		{
			var suppliers = _nhacc.getAllNhaCC();
			var productTypes = _danhmuc.getAllDanhMuc();
			ViewBag.Suppliers = new SelectList(suppliers, "IMaNcc", "STenNcc");
			ViewBag.ProductTypes = new SelectList(productTypes, "IMaLoaiSp", "STenLoai");
			return View(new TblSanPham());
		}


		[HttpPost]
		[Route("admin/product/add")]
		public IActionResult AddProduct(TblSanPham product)
		{
			_sanpham.AddSanPham(product);
			return RedirectToAction("Index");
		}
	}
}
