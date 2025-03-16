using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using BTLNhapMonCNPM.Repository;
using BTLNhapMonCNPM.Models;
namespace BTLNhapMonCNPM.BTLNhapMonCNPM.Tests
{
    public class SanPhamImplTests
    {
        private Mock<PharmacyDbContext> _mockDbContext;
        private SanPhamImpl _sanPhamImpl;
        private List<TblSanPham> _sanPhamList;

        public SanPhamImplTests()
        {
            _mockDbContext = new Mock<PharmacyDbContext>();
            _sanPhamImpl = new SanPhamImpl();

            _sanPhamList = new List<TblSanPham>
            {
                new TblSanPham { IMaSp = 1, STen = "Thuốc A", status = true },
                new TblSanPham { IMaSp = 2, STen = "Thuốc B", status = true },
                new TblSanPham { IMaSp = 3, STen = "Thuốc C", status = false }
            };
        }

        [Fact]
        public void GetAllSanPham_ShouldReturnOnlyActiveProducts()
        {
            var result = _sanPhamImpl.getAllSanPham();
            Assert.NotNull(result);
            Assert.All(result, sp => Assert.True(sp.status));
        }

        [Theory]
        [InlineData("Thuốc A", "Viên nang", "Hộp", 100, 50000, 70000, "Uống sau ăn", "Nơi khô ráo", 1, "2026-12-31", false, false, true, 1)]
        [InlineData("Thuốc B", "Viên nén", "Chai", 50, 30000, 45000, "Uống trước ăn", "Tránh ánh nắng", 2, "2025-06-30", true, false, false, 2)]
        [InlineData("Thuốc C", "Siro", "Chai", null, 25000, 35000, "Lắc đều trước khi uống", "Bảo quản lạnh", 3, "2024-09-15", false, true, true, 3)]
        public void AddSanPham_ShouldAddNewProduct(
    string ten, string dangThuoc, string donViTinh, int? soLuong,
    decimal giaNhap, decimal giaBan, string cachSuDung, string dieuKienBaoQuan,
    int maNcc, string hanSuDung, bool bietTru, bool thuHoi, bool? canKeDon, int maLoaiSp)
        {
            // Arrange
            var newSanPham = new TblSanPham
            {
                STen = ten,
                SDangThuoc = dangThuoc,
                SDonViTinh = donViTinh,
                ISoLuong = soLuong,
                FGiaNhap = giaNhap,
                FGiaBan = giaBan,
                SCachSuDung = cachSuDung,
                SDieuKienBaoQuan = dieuKienBaoQuan,
                IMaNcc = maNcc,
                SHanSuDung = hanSuDung,
                BBietTru = bietTru,
                BThuHoi = thuHoi,
                BCanKeDon = canKeDon,
                IMaLoaiSp = maLoaiSp,
                status = true
            };

            // Act
            int result = _sanPhamImpl.AddSanPham(newSanPham);

            // Assert
            Assert.True(result > 0, "Sản phẩm phải được thêm thành công");
        }

        public static IEnumerable<object[]> GetSanPhamUpdateTestCases()
        {
            yield return new object[]
            {
       new TblSanPham
    {
        IMaSp = 1,
        STen = "Thuốc B",
        SDangThuoc = "Viên nén",
        SDonViTinh = "Hộp",
        ISoLuong = 50,
        FGiaNhap = 60000,
        FGiaBan = 80000,
        SCachSuDung = "Uống trước ăn",
        SDieuKienBaoQuan = "Nơi khô ráo",
        IMaNcc = 2,
        SHanSuDung = "2027-12-31",
        BBietTru = true,
        BThuHoi = false,
        BCanKeDon = true,
        IMaLoaiSp = 2
    },
            true
            };

            yield return new object[]
            {
        new TblSanPham
        {
            IMaSp = 2,
            STen = "",  //  Lỗi: Tên trống
            SDangThuoc = "Viên nén",
            SDonViTinh = "Chai",
            ISoLuong = 50,
            FGiaNhap = 30000,
            FGiaBan = 45000,
            SCachSuDung = "Uống trước ăn",
            SDieuKienBaoQuan = "Tránh ánh nắng",
            IMaNcc = 2,
            SHanSuDung = "2025-06-30",
            BBietTru = true,
            BThuHoi = false,
            BCanKeDon = false,
            IMaLoaiSp = 2
        },
        false
            };

            yield return new object[]
            {
        new TblSanPham
        {
            IMaSp = 3,
            STen = "Thuốc C",
            SDangThuoc = "Siro",
            SDonViTinh = "Chai",
            ISoLuong = -10, //  Lỗi: Số lượng âm
            FGiaNhap = 50000,
            FGiaBan = 70000,
            SCachSuDung = "Lắc đều trước khi uống",
            SDieuKienBaoQuan = "Bảo quản lạnh",
            IMaNcc = 3,
            SHanSuDung = "2026-09-15",
            BBietTru = false,
            BThuHoi = true,
            BCanKeDon = true,
            IMaLoaiSp = 3
        },
        false
            };

            yield return new object[]
            {
        new TblSanPham
        {
            IMaSp = 4,
            STen = "Thuốc D",
            SDangThuoc = "Viên nang",
            SDonViTinh = "Hộp",
            ISoLuong = 100,
            FGiaNhap = 25000,
            FGiaBan = 40000,
            SCachSuDung = "Uống sau ăn",
            SDieuKienBaoQuan = "Bảo quản nhiệt độ phòng",
            IMaNcc = 4,
            SHanSuDung = "2023-08-20", // quá hạn sử dụng
            BBietTru = false,
            BThuHoi = false,
            BCanKeDon = true,
            IMaLoaiSp = 4
        },
        false
            };
        }
        [Theory]
        [MemberData(nameof(GetSanPhamUpdateTestCases))]
        public void UpdateSanPham(TblSanPham sanPham,bool expected)
        {
            bool check = false;
            var res = _sanPhamImpl.UpdateSanPham(sanPham);
            if(res > 0)
            {
                check = true;
            }
            Assert.Equal(check, expected);
            
        }

        [Theory]
        [InlineData("1", true)]   // ID hợp lệ, xóa thành công
        [InlineData("abc", false)] // ID không hợp lệ, không thể xóa
        [InlineData("2", true)]   // ID hợp lệ, xóa thành công
        [InlineData("4", true)]   // ID hợp lệ, xóa thành công
        public void DeleteSanPham_ShouldMarkProductAsInactive(string id, bool expectedResult)
        {
            var result = _sanPhamImpl.deleteSanPham(id);
            Assert.Equal(expectedResult, result);
        }
    }
}
