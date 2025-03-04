using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblThuocKeDon")]
public partial class TblThuocKeDon
{
    [Key]
    [Column("iMaTKD")]
    public int IMaTkd { get; set; }

    [Column("iMaKH")]
    public int? IMaKh { get; set; }

    [Column("iMaNV")]
    public int IMaNv { get; set; }

    [Column("dNgayTao", TypeName = "datetime")]
    public DateTime DNgayTao { get; set; }

    [Column("sGhiChu")]
    public string? SGhiChu { get; set; }

    [ForeignKey("IMaKh")]
    [InverseProperty("TblThuocKeDons")]
    public virtual TblKhachHang? IMaKhNavigation { get; set; }

    [ForeignKey("IMaNv")]
    [InverseProperty("TblThuocKeDons")]
    public virtual TblNhanVien IMaNvNavigation { get; set; } = null!;
}
