using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

public partial class PharmacyDbContext : DbContext
{
    private static PharmacyDbContext pharmacyDb = new PharmacyDbContext();
    public PharmacyDbContext()
    {

    }

    public static PharmacyDbContext getDbContext()
    {
        return pharmacyDb;
    }

    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblHoaDon> TblHoaDons { get; set; }

    public virtual DbSet<TblKhachHang> TblKhachHangs { get; set; }

    public virtual DbSet<TblKhieuNai> TblKhieuNais { get; set; }

    public virtual DbSet<TblLoSanPham> TblLoSanPhams { get; set; }

    public virtual DbSet<TblLoaiSanPham> TblLoaiSanPhams { get; set; }

    public virtual DbSet<TblNhaCungCap> TblNhaCungCaps { get; set; }

    public virtual DbSet<TblNhanVien> TblNhanViens { get; set; }

    public virtual DbSet<TblNhapHang> TblNhapHangs { get; set; }

    public virtual DbSet<TblSanPham> TblSanPhams { get; set; }

    public virtual DbSet<TblThuocKeDon> TblThuocKeDons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        // => optionsBuilder.UseSqlServer("Data Source=BING-CHILLING;Initial Catalog=PharmacyDB;User ID=sa;Password=1234;Encrypt=True;TrustServerCertificate=True"); //dan truong's server
        => optionsBuilder.UseSqlServer("Server=LAPTOP-EMOA5K1J;Database=PharmacyDB;Trusted_Connection=True;TrustServerCertificate=True"); //dan truong's server




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblHoaDon>(entity =>
        {
            entity.HasKey(e => e.IMaHd).HasName("PK__tblHoaDo__F20ABED0701FF6C3");

            entity.HasOne(d => d.IMaKhNavigation).WithMany(p => p.TblHoaDons).HasConstraintName("FK__tblHoaDon__iMaKH__49C3F6B7");
        });

        modelBuilder.Entity<TblKhachHang>(entity =>
        {
            entity.HasKey(e => e.IMaKh).HasName("PK__tblKhach__F20AA50860D00D03");

            entity.Property(e => e.DNgayTao).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblKhieuNai>(entity =>
        {
            entity.HasKey(e => e.IMaKn).HasName("PK__tblKhieu__F20AA502DF963DC6");

            entity.HasOne(d => d.IMaKhNavigation).WithMany(p => p.TblKhieuNais).HasConstraintName("FK__tblKhieuN__iMaKH__4CA06362");
        });

        modelBuilder.Entity<TblLoSanPham>(entity =>
        {
            entity.HasKey(e => e.ILoId).HasName("PK__tblLoSan__D6EA297DB9308BBB");

            entity.HasOne(d => d.IDonNhap).WithMany(p => p.TblLoSanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblLoSanP__iDonN__59063A47");

            entity.HasOne(d => d.ISanPham).WithMany(p => p.TblLoSanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblLoSanP__iSanP__5812160E");
        });

        modelBuilder.Entity<TblLoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.IMaLoaiSp).HasName("PK__tblLoaiS__4B6603B6C6064CBF");
        });

        modelBuilder.Entity<TblNhaCungCap>(entity =>
        {
            entity.HasKey(e => e.IMaNcc).HasName("PK__tblNhaCu__204A73DC644A75C9");
        });

        modelBuilder.Entity<TblNhanVien>(entity =>
        {
            entity.HasKey(e => e.IMaNv).HasName("PK__tblNhanV__F20A8D79D2CAB3C0");
        });

        modelBuilder.Entity<TblNhapHang>(entity =>
        {
            entity.HasKey(e => e.IMaNh).HasName("PK__tblNhapH__F20A8D6B5A0E4776");

            entity.HasOne(d => d.IMaNccNavigation).WithMany(p => p.TblNhapHangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblNhapHa__iMaNC__5070F446");

            entity.HasOne(d => d.IMaNvNavigation).WithMany(p => p.TblNhapHangs).HasConstraintName("FK__tblNhapHa__iMaNV__4F7CD00D");
        });

        modelBuilder.Entity<TblSanPham>(entity =>
        {
            entity.HasKey(e => e.IMaSp).HasName("PK__tblSanPh__F20A661E1F6C4D7D");

            entity.Property(e => e.BCanKeDon).HasDefaultValue(false);
            entity.Property(e => e.ISoLuong).HasDefaultValue(0);

            entity.HasOne(d => d.IMaLoaiSpNavigation).WithMany(p => p.TblSanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblSanPha__iMaLo__46E78A0C");

            entity.HasOne(d => d.IMaNccNavigation).WithMany(p => p.TblSanPhams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblSanPha__iMaNC__45F365D3");
        });

        modelBuilder.Entity<TblThuocKeDon>(entity =>
        {
            entity.HasKey(e => e.IMaTkd).HasName("PK__tblThuoc__22CA58456196E352");

            entity.HasOne(d => d.IMaKhNavigation).WithMany(p => p.TblThuocKeDons).HasConstraintName("FK__tblThuocK__iMaKH__534D60F1");

            entity.HasOne(d => d.IMaNvNavigation).WithMany(p => p.TblThuocKeDons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblThuocK__iMaNV__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
