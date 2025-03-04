using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblKhachHang")]
[Index("SEmail", Name = "UQ__tblKhach__07DACB08679F8ED0", IsUnique = true)]
[Index("STenDangNhap", Name = "UQ__tblKhach__2584CE9E610A2FA1", IsUnique = true)]
public partial class TblKhachHang
{
    [Key]
    [Column("iMaKH")]
    public int IMaKh { get; set; }

    [Column("sHoTen")]
    [StringLength(255)]
    public string SHoTen { get; set; } = null!;

    [Column("sTenDangNhap")]
    [StringLength(100)]
    public string STenDangNhap { get; set; } = null!;

    [Column("sMatKhau")]
    [StringLength(255)]
    public string SMatKhau { get; set; } = null!;

    [Column("sEmail")]
    [StringLength(255)]
    public string? SEmail { get; set; }

    [Column("sSDT")]
    [StringLength(20)]
    public string? SSdt { get; set; }

    [Column("dNgayTao", TypeName = "datetime")]
    public DateTime? DNgayTao { get; set; }

    [InverseProperty("IMaKhNavigation")]
    public virtual ICollection<TblHoaDon> TblHoaDons { get; set; } = new List<TblHoaDon>();

    [InverseProperty("IMaKhNavigation")]
    public virtual ICollection<TblKhieuNai> TblKhieuNais { get; set; } = new List<TblKhieuNai>();

    [InverseProperty("IMaKhNavigation")]
    public virtual ICollection<TblThuocKeDon> TblThuocKeDons { get; set; } = new List<TblThuocKeDon>();
}
