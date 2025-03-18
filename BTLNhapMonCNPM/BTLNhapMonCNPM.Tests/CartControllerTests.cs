using BTLNhapMonCNPM.Controllers;
using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;

namespace BTLNhapMonCNPM.BTLNhapMonCNPM.Tests
{
    public class CartControllerTests
    {
        private CartController _controller;
        private Mock<PharmacyDbContext> _mockDbContext;
        private Mock<ISession> _mockSession;
        private DefaultHttpContext _httpContext;

        public CartControllerTests()
        {
            // Mock DbContext
            _mockDbContext = new Mock<PharmacyDbContext>();

            // Mock HttpContext & Session
            _mockSession = new Mock<ISession>();
            _httpContext = new DefaultHttpContext();
            _httpContext.Session = new TestSession();

            // Khởi tạo controller với session mock
            _controller = new CartController(_mockDbContext.Object);
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };
        }

        [Fact]
        public void Checkout_RedirectsToSignIn_WhenUserNotLoggedIn()
        {
            // Arrange
            var cartData = new List<CartItem> { new CartItem { id = 1, name = "Test", price = 100, quantity = 1 } };
            _httpContext.Session.SetString("Username", ""); // Không có user đăng nhập
            _httpContext.Session.SetString("UsernameID", "");

            // Act
            var result = _controller.Checkout(cartData) as RedirectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("/signin", result.Url);
        }

        [Fact]
        public void Checkout_ReturnsBadRequest_WhenCartIsEmpty()
        {
            // Arrange
            var emptyCartData = "[]";
            _httpContext.Session.SetString("Username", "testUser");
            _httpContext.Session.SetString("UsernameID", "1");

            // Act
            var result = _controller.Checkout(emptyCartData) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Giỏ hàng trống hoặc không hợp lệ!", result.Value);
        }

        [Fact]
        public void ConfirmCheckout_RedirectsToIndex_WhenCartIsEmpty()
        {
            // Arrange
            _httpContext.Session.Remove("CartData");

            // Act
            var result = _controller.ConfirmCheckout() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void ConfirmCheckout_ReturnsView_WithCorrectCartData()
        {
            // Arrange
            var cart = new List<CartItem> { new CartItem { id = 1, name = "Test", price = 100, quantity = 2 } };
            _httpContext.Session.SetString("CartData", JsonConvert.SerializeObject(cart));

            // Act
            var result = _controller.ConfirmCheckout() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void ConfirmPayment_AddsInvoice_AndRedirectsToSuccess()
        {
            // Arrange: Thiết lập session với dữ liệu giỏ hàng và UsernameID
            var cart = new List<CartItem>
    {
        new CartItem { id = 1, name = "Test", price = 100, quantity = 2 }
    };
            _httpContext.Session.SetString("CartData", JsonConvert.SerializeObject(cart));
            _httpContext.Session.SetString("UsernameID", "1");

            string testNote = "Test Payment Note";

            // Mock DbSet của TblHoaDon
            var invoiceSet = new Mock<DbSet<TblHoaDon>>();
            _mockDbContext.Setup(db => db.TblHoaDons).Returns(invoiceSet.Object);

            // Cho phép gọi Add() trên DbSet mock
            invoiceSet.Setup(db => db.Add(It.IsAny<TblHoaDon>())).Verifiable();

            // Đảm bảo SaveChanges() được gọi
            _mockDbContext.Setup(db => db.SaveChanges()).Returns(1);

            // Act: Gọi ConfirmPayment với testNote
            var result = _controller.ConfirmPayment(testNote) as RedirectToActionResult;

            // Assert: Kiểm tra Redirect đến "Success"
            Assert.NotNull(result);
            Assert.Equal("Success", result.ActionName);

            // Kiểm tra xem invoice được thêm vào database với đúng ghi chú
            invoiceSet.Verify(db => db.Add(It.Is<TblHoaDon>(invoice => invoice.SGhiChu == testNote)), Times.Once);

            // Kiểm tra SaveChanges() được gọi đúng 1 lần
            _mockDbContext.Verify(db => db.SaveChanges(), Times.Once);

            // Kiểm tra Session bị xóa sau khi thanh toán
            Assert.Null(_httpContext.Session.GetString("CartData"));
        }
        
    }
    }
