using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTLNhapMonCNPM.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SanPham> tblSanPham { get; set; }
        public DbSet<LoSanPham> tblLoSanPham { get; set; } // Thêm DbSet cho tblLoSanPham

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Định nghĩa khóa chính cho tblSanPham
            modelBuilder.Entity<SanPham>()
                .HasKey(sp => sp.iMaSP);

            // Định nghĩa khóa chính cho tblLoSanPham
            modelBuilder.Entity<LoSanPham>()
                .HasKey(lsp => lsp.iLoId);

            // Thiết lập quan hệ 1-n giữa tblSanPham và tblLoSanPham
            modelBuilder.Entity<LoSanPham>()
                .HasOne(lsp => lsp.SanPham)  // Một LoSanPham thuộc một SanPham
                .WithMany(sp => sp.LoSanPhams) // Một SanPham có nhiều LoSanPham
                .HasForeignKey(lsp => lsp.iSanPhamId) // Khóa ngoại trỏ về tblSanPham
                .OnDelete(DeleteBehavior.Restrict); // Ngăn chặn xóa nếu có ràng buộc
        }
    }
    public class SanPham
    {
        [Key] // Đánh dấu khóa chính
        public int iMaSP { get; set; }
        public string sTen { get; set; }
        //public string sDangThuoc { get; set; }
        public string sDonViTinh { get; set; }
        public int iSoLuong { get; set; }
        public string sCachSuDung { get; set; }
        public string sDieuKienBaoQuan { get; set; }
        public string sHanSuDung { get; set; }
        public decimal fGiaNhap { get; set; }
        public decimal fGiaBan { get; set; }

        // Thêm danh sách LoSanPham
        public ICollection<LoSanPham> LoSanPhams { get; set; }
    }
    public class LoSanPham
    {
        [Key]
        public int iLoId { get; set; }

        [ForeignKey("SanPham")]
        public int iSanPhamId { get; set; }

        // Thiết lập mối quan hệ với SanPham
        public SanPham SanPham { get; set; }
    }
}
