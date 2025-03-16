using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using BTLNhapMonCNPM.Repository;

namespace BTLNhapMonCNPM.BTLNhapMonCNPM.Tests
{
    public class LoaiSanPhamImplTest
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly LoaiSanPhamImpl _service;

        public LoaiSanPhamImplTest()
        {
            // Khởi tạo DbContext
            _dbContext = PharmacyDbContext.getDbContext();
            _service = new LoaiSanPhamImpl();
        }

        [Fact]
        public void GetAllDanhMuc_ShouldNotThrowException()
        {
            try
            {
                // Gọi hàm cần test
                var result = _service.getAllDanhMuc();

                // Nếu chạy đến đây mà không có lỗi, thì test pass
                Assert.True(true);
            }
            catch (Exception ex)
            {
                // Nếu có exception, test fail và hiển thị lỗi
                Assert.False(true, $"Test thất bại vì gặp exception: {ex.Message}");
            }
        }
    }
}
