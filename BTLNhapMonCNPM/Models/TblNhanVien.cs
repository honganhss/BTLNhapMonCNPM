using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblNhanVien")]
[Index("STenDangNhap", Name = "UQ__tblNhanV__2584CE9E1967B839", IsUnique = true)]
public partial class TblNhanVien
{
    [Key]
    [Column("iMaNV")]
    public int IMaNv { get; set; }

    [Column("sTenDangNhap")]
    [StringLength(50)]
    public string STenDangNhap { get; set; } = null!;

    [Column("sMatKhau")]
    [StringLength(255)]
    public string SMatKhau { get; set; } = null!;

    [Column("sHoTen")]
    [StringLength(255)]
    public string SHoTen { get; set; } = null!;

    [Column("sChucVu")]
    [StringLength(20)]
    public string SChucVu { get; set; } = null!;

    [Column("fLuongCB", TypeName = "decimal(10, 2)")]
    public decimal FLuongCb { get; set; }

    [Column("fHeSoLuong", TypeName = "decimal(10, 2)")]
    public decimal FHeSoLuong { get; set; }

    [Column("dNgayVaoLam")]
    public DateOnly DNgayVaoLam { get; set; }

    [InverseProperty("IMaNvNavigation")]
    public virtual ICollection<TblNhapHang> TblNhapHangs { get; set; } = new List<TblNhapHang>();

    [InverseProperty("IMaNvNavigation")]
    public virtual ICollection<TblThuocKeDon> TblThuocKeDons { get; set; } = new List<TblThuocKeDon>();
}
