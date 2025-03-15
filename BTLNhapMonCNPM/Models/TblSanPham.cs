using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblSanPham")]
public partial class TblSanPham
{
    [Key]
    [Column("iMaSP")]
    public int IMaSp { get; set; }

    [Column("sTen")]
    [StringLength(255)]
    public string STen { get; set; } = null!;

    [Column("sDangThuoc")]
    [StringLength(100)]
    public string? SDangThuoc { get; set; }

    [Column("sDonViTinh")]
    [StringLength(50)]
    public string? SDonViTinh { get; set; }

    [Column("iSoLuong")]
    public int? ISoLuong { get; set; }

    [Column("sImageThuoc")]
    public string? SImageThuoc { get; set; }

    [Column("fGiaNhap", TypeName = "decimal(10, 2)")]
    public decimal FGiaNhap { get; set; }

    [Column("fGiaBan", TypeName = "decimal(10, 2)")]
    public decimal FGiaBan { get; set; }

    [Column("sCachSuDung")]
    public string? SCachSuDung { get; set; }

    [Column("sDieuKienBaoQuan")]
    public string? SDieuKienBaoQuan { get; set; }

    [Column("iMaNCC")]
    public int IMaNcc { get; set; }

    [Column("sHanSuDung")]
    [StringLength(100)]
    public string? SHanSuDung { get; set; }

    [Column("bBietTru")]
    public bool BBietTru { get; set; }

    [Column("bThuHoi")]
    public bool BThuHoi { get; set; }

    [Column("bCanKeDon")]
    public bool? BCanKeDon { get; set; }

    [Column("iMaLoaiSP")]
    public int IMaLoaiSp { get; set; }

    [ForeignKey("IMaLoaiSp")]
    [InverseProperty("TblSanPhams")]
    public virtual TblLoaiSanPham IMaLoaiSpNavigation { get; set; } = null!;

    [ForeignKey("IMaNcc")]
    [InverseProperty("TblSanPhams")]
    public virtual TblNhaCungCap IMaNccNavigation { get; set; } = null!;

    [InverseProperty("ISanPham")]
    public virtual ICollection<TblLoSanPham> TblLoSanPhams { get; set; } = new List<TblLoSanPham>();

    [Column("bStatus")]
    [NotNull]
    public bool status { get; set; }
}
