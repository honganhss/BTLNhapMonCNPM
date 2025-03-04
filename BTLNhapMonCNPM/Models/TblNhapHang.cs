using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblNhapHang")]
public partial class TblNhapHang
{
    [Key]
    [Column("iMaNH")]
    public int IMaNh { get; set; }

    [Column("dNgayTao", TypeName = "datetime")]
    public DateTime DNgayTao { get; set; }

    [Column("iMaNV")]
    public int? IMaNv { get; set; }

    [Column("iMaNCC")]
    public int IMaNcc { get; set; }

    [Column("fTongTien")]
    public double FTongTien { get; set; }

    [ForeignKey("IMaNcc")]
    [InverseProperty("TblNhapHangs")]
    public virtual TblNhaCungCap IMaNccNavigation { get; set; } = null!;

    [ForeignKey("IMaNv")]
    [InverseProperty("TblNhapHangs")]
    public virtual TblNhanVien? IMaNvNavigation { get; set; }

    [InverseProperty("IDonNhap")]
    public virtual ICollection<TblLoSanPham> TblLoSanPhams { get; set; } = new List<TblLoSanPham>();
}
