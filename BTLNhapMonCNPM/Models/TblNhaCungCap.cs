using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblNhaCungCap")]
public partial class TblNhaCungCap
{
    [Key]
    [Column("iMaNCC")]
    public int IMaNcc { get; set; }

    [Column("sTenNCC")]
    [StringLength(255)]
    public string STenNcc { get; set; } = null!;

    [Column("sDiaChi")]
    public string? SDiaChi { get; set; }

    [Column("sSDT")]
    [StringLength(20)]
    public string? SSdt { get; set; }

    [Column("sEmail")]
    [StringLength(255)]
    public string? SEmail { get; set; }

    [Column("bChungChi")]
    public byte[] BChungChi { get; set; } = null!;

    [InverseProperty("IMaNccNavigation")]
    public virtual ICollection<TblNhapHang> TblNhapHangs { get; set; } = new List<TblNhapHang>();

    [InverseProperty("IMaNccNavigation")]
    public virtual ICollection<TblSanPham> TblSanPhams { get; set; } = new List<TblSanPham>();
}
