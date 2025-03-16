using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using BTLNhapMonCNPM.Repository;

namespace BTLNhapMonCNPM.BTLNhapMonCNPM.Tests
{
    public class NhaCungCapImplTest
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly NhaCCImpl _service;

        public NhaCungCapImplTest()
        {
            // Khởi tạo DbContext
            _dbContext = PharmacyDbContext.getDbContext();
            _service = new NhaCCImpl();
        }

        [Fact]
        public void GetAllNhaCC_ShouldNotThrowException()
        {
            try
            {
                // Gọi hàm cần test
                var result = _service.getAllNhaCC();

                // Nếu không có lỗi, test pass
                Assert.True(true);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, test fail và hiển thị thông báo lỗi
                Assert.False(true, $"Test thất bại vì gặp exception: {ex.Message}");
            }
        }
    }
}
