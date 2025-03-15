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

        [HttpGet]
        [Route("admin/product/edit/{id?}")]
        public IActionResult Edit(int id)
        {
            Console.WriteLine(id);
            List<TblLoaiSanPham> listLoaiSP = _danhmuc.getAllDanhMuc();
            List<TblSanPham> listSP = _sanpham.getAllSanPham();
            Dictionary<String, object> list = new Dictionary<string, object>();
            list["danhmuc"] = listLoaiSP;
            list["danhmucid"] = id;
            list["sanpham"] = listSP.FirstOrDefault(sp => sp.IMaSp == id);
            var suppliers = _nhacc.getAllNhaCC();
            var productTypes = _danhmuc.getAllDanhMuc();
            ViewBag.Suppliers = new SelectList(suppliers, "IMaNcc", "STenNcc");
            ViewBag.ProductTypes = new SelectList(productTypes, "IMaLoaiSp", "STenLoai");

            return View(list);
        }

        [HttpPost]
        [Route("admin/product/edit/{id?}")]
        public IActionResult EditProduct(TblSanPham updateProduct)
        {
            List<TblSanPham> listSP = _sanpham.getAllSanPham();
            TblSanPham existingProduct = listSP.FirstOrDefault(sp => sp.IMaSp == updateProduct.IMaSp);

            if (existingProduct == null)
            {
                return NotFound();
            }


            existingProduct.STen = updateProduct.STen;
            existingProduct.SDangThuoc = updateProduct.SDangThuoc;
            existingProduct.SDonViTinh = updateProduct.SDonViTinh;
            existingProduct.ISoLuong = updateProduct.ISoLuong;
            existingProduct.SImageThuoc = updateProduct.SImageThuoc;
            existingProduct.FGiaNhap = updateProduct.FGiaNhap;
            existingProduct.FGiaBan = updateProduct.FGiaBan;
            existingProduct.SCachSuDung = updateProduct.SCachSuDung;
            existingProduct.SDieuKienBaoQuan = updateProduct.SDieuKienBaoQuan;
            existingProduct.IMaNcc = updateProduct.IMaNcc;
            existingProduct.SHanSuDung = updateProduct.SHanSuDung;
            existingProduct.BBietTru = updateProduct.BBietTru;
            existingProduct.BThuHoi = updateProduct.BThuHoi;
            existingProduct.BCanKeDon = updateProduct.BCanKeDon;
            existingProduct.IMaLoaiSp = updateProduct.IMaLoaiSp;

            _sanpham.UpdateSanPham(existingProduct);

            return RedirectToAction("Index");
        }

        [Route("/admin/product/delete/{id}")]
        public IActionResult deleteProduct(string id)
        {
            if(_sanpham.deleteSanPham(id) == true)
            return Json(new { success = "true"});
            else
            {
                return Json(new { success = "false" });
            }
        }
    }
}
