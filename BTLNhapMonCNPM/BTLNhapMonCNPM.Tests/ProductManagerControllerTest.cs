using Xunit;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BTLNhapMonCNPM.Areas.Admin.Controllers;
using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.BTLNhapMonCNPM.Tests
{
	public class ProductManagerControllerTests
	{
		private readonly Mock<LoaiSanPhamIT> _mockDanhmuc;
		private readonly Mock<SanPhamIT> _mockSanpham;
		private readonly Mock<NhaCCIT> _mockNhacc;
		private readonly ProductManagerController _controller;

		public ProductManagerControllerTests()
		{
			_mockDanhmuc = new Mock<LoaiSanPhamIT>();
			_mockSanpham = new Mock<SanPhamIT>();
			_mockNhacc = new Mock<NhaCCIT>();
			_controller = new ProductManagerController(_mockDanhmuc.Object, _mockSanpham.Object, _mockNhacc.Object);
		}

		[Fact]
		public void Index_ReturnsViewResult_WithListOfProducts()
		{
			var danhMucList = new List<TblLoaiSanPham> { new TblLoaiSanPham { IMaLoaiSp = 1, STenLoai = "Category1" } };
			var sanPhamList = new List<TblSanPham> { new TblSanPham { IMaSp = 1, STen = "Product1" } };
			_mockDanhmuc.Setup(repo => repo.getAllDanhMuc()).Returns(danhMucList);
			_mockSanpham.Setup(repo => repo.getAllSanPham()).Returns(sanPhamList);

			var result = _controller.Index(1, 1);

			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<Dictionary<string, object>>(viewResult.ViewData.Model);
			Assert.Equal(danhMucList, model["danhmuc"]);
			Assert.Equal(sanPhamList, model["dsSP"]);
		}

		[Fact]
		public void Add_ReturnsViewResult_WithNewProduct()
		{
			var suppliers = new List<TblNhaCungCap> { new TblNhaCungCap { IMaNcc = 1, STenNcc = "Supplier1" } };
			var productTypes = new List<TblLoaiSanPham> { new TblLoaiSanPham { IMaLoaiSp = 1, STenLoai = "Category1" } };
			_mockNhacc.Setup(repo => repo.getAllNhaCC()).Returns(suppliers);
			_mockDanhmuc.Setup(repo => repo.getAllDanhMuc()).Returns(productTypes);

			var result = _controller.Add();

			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsType<TblSanPham>(viewResult.ViewData.Model);
			Assert.NotNull(viewResult.ViewData["Suppliers"]);
			Assert.NotNull(viewResult.ViewData["Suppliers"]);
		}

		[Fact]
		public void AddProduct_RedirectsToIndex()
		{
			var product = new TblSanPham { IMaSp = 1, STen = "Product1" };

			var result = _controller.AddProduct(product);

			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirectToActionResult.ActionName);
		}

		[Fact]
		public void Edit_ReturnsViewResult_WithProduct()
		{
			var danhMucList = new List<TblLoaiSanPham> { new TblLoaiSanPham { IMaLoaiSp = 1, STenLoai = "Category1" } };
			var sanPhamList = new List<TblSanPham> { new TblSanPham { IMaSp = 1, STen = "Product1" } };
			var suppliers = new List<TblNhaCungCap> { new TblNhaCungCap { IMaNcc = 1, STenNcc = "Supplier1" } };
			_mockDanhmuc.Setup(repo => repo.getAllDanhMuc()).Returns(danhMucList);
			_mockSanpham.Setup(repo => repo.getAllSanPham()).Returns(sanPhamList);
			_mockNhacc.Setup(repo => repo.getAllNhaCC()).Returns(suppliers);

			var result = _controller.Edit(1);

			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<Dictionary<string, object>>(viewResult.ViewData.Model);
			Assert.Equal(danhMucList, model["danhmuc"]);
			Assert.Equal(sanPhamList[0], model["sanpham"]);
		}

		[Fact]
		public void EditProduct_RedirectsToIndex()
		{
			var product = new TblSanPham { IMaSp = 1, STen = "Product1" };
			_mockSanpham.Setup(repo => repo.getAllSanPham()).Returns(new List<TblSanPham> { product });

			var result = _controller.EditProduct(product);

			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirectToActionResult.ActionName);
		}

		[Fact]
		public void DeleteProduct_ReturnsJsonResult()
		{
			_mockSanpham.Setup(repo => repo.deleteSanPham(It.IsAny<string>())).Returns(true);

			var result = _controller.deleteProduct("1");

			var jsonResult = Assert.IsType<JsonResult>(result);
			Assert.Equal(new { success = "true" }, jsonResult.Value);
		}
	}
}
