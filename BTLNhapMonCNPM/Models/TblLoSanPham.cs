using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblLoSanPham")]
public partial class TblLoSanPham
{
    [Key]
    [Column("iLoId")]
    public int ILoId { get; set; }

    [Column("iSanPhamId")]
    public int ISanPhamId { get; set; }

    [Column("dNgaySanXuat")]
    public DateOnly DNgaySanXuat { get; set; }

    [Column("dNgayHetHan")]
    public DateOnly DNgayHetHan { get; set; }

    [Column("iSoLuongNhap")]
    public int ISoLuongNhap { get; set; }

    [Column("iSoLuongTon")]
    public int ISoLuongTon { get; set; }

    [Column("dNgayNhap")]
    public DateOnly DNgayNhap { get; set; }

    [Column("iDonNhapId")]
    public int IDonNhapId { get; set; }

    [Column("fGiaNhap", TypeName = "decimal(10, 2)")]
    public decimal FGiaNhap { get; set; }

    [Column("sDonViTinh")]
    [StringLength(50)]
    public string? SDonViTinh { get; set; }

    [ForeignKey("IDonNhapId")]
    [InverseProperty("TblLoSanPhams")]
    public virtual TblNhapHang IDonNhap { get; set; } = null!;

    [ForeignKey("ISanPhamId")]
    [InverseProperty("TblLoSanPhams")]
    public virtual TblSanPham ISanPham { get; set; } = null!;
}
