using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblHoaDon")]
public partial class TblHoaDon
{
    [Key]
    [Column("iMaHD")]
    public int IMaHd { get; set; }

    [Column("iMaKH")]
    public int? IMaKh { get; set; }

    [Column("dThoiGian", TypeName = "datetime")]
    public DateTime DThoiGian { get; set; }

    [Column("fTongGiaTri", TypeName = "decimal(10, 2)")]
    public decimal FTongGiaTri { get; set; }

    [Column("sGhiChu")]
    public string? SGhiChu { get; set; }

    [ForeignKey("IMaKh")]
    [InverseProperty("TblHoaDons")]
    public virtual TblKhachHang? IMaKhNavigation { get; set; }
}
