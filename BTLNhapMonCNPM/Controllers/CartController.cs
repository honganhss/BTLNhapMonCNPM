using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace BTLNhapMonCNPM.Controllers
{
    public class CartController : Controller
    {
        private static PharmacyDbContext pharmacyDb = new PharmacyDbContext();
        [Route("/cart")]
        public IActionResult Index()
        {
            return View();
        }

        [Consumes("application/json")]
        [HttpPost]
        [Route("/cart/checkout")]
        public IActionResult Checkout([FromBody] Object cartData)
        {
            string username = HttpContext.Session.GetString("Username");
            string userId = HttpContext.Session.GetString("UsernameID");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userId))
            {
                return Redirect("/signin");
            }
            string json = cartData.ToString();

            var settings = new JsonSerializerSettings
            {
                Culture = new CultureInfo("en-US"), // Đảm bảo dấu thập phân là '.'
                FloatParseHandling = FloatParseHandling.Decimal // Xử lý số thực đúng kiểu
            };

            try
            {
                List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(json, settings);

                if (cart == null || cart.Count == 0)
                {
                    return BadRequest("Giỏ hàng trống hoặc không hợp lệ!");
                }
                HttpContext.Session.SetString("CartData", JsonConvert.SerializeObject(cart));
                return RedirectToAction("ConfirmCheckout");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xử lý giỏ hàng: {ex.Message}");
            }
            
        }

        [Route("/confirm-checkout")]
        public IActionResult ConfirmCheckout()
        {
            string cartJson = HttpContext.Session.GetString("CartData");
            if (string.IsNullOrEmpty(cartJson))
            {
                return RedirectToAction("Index");
            }

            List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            decimal totalPrice = cart.Sum(x => x.price * x.quantity);

            ViewBag.CartItems = cart;
            ViewBag.TotalPrice = totalPrice;

            return View();
        }

        [HttpPost]
        [Route("/confirm-payment")]
        public IActionResult ConfirmPayment(string note)
        {
            string cartJson = HttpContext.Session.GetString("CartData");
            string userId = HttpContext.Session.GetString("UsernameID");

            List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            if (string.IsNullOrEmpty(cartJson) || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }

            
            decimal totalPrice = cart.Sum(x => x.price * x.quantity);

            // Lưu hóa đơn vào database
            TblHoaDon newInvoice = new TblHoaDon
            {
                IMaKh = int.Parse(userId),
                DThoiGian = DateTime.Now,
                FTongGiaTri = totalPrice,
                SGhiChu = note
            };

            pharmacyDb.TblHoaDons.Add(newInvoice);
            pharmacyDb.SaveChanges();

            // Xóa giỏ hàng sau khi thanh toán
            HttpContext.Session.Remove("CartData");

            return RedirectToAction("Success");
        }

        [Route("/success")]
        public IActionResult Success()
        {
            return View();
        }
    }

    public class CartItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }


}

