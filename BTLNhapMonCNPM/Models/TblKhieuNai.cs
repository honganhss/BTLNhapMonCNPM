using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblKhieuNai")]
public partial class TblKhieuNai
{
    [Key]
    [Column("iMaKN")]
    public int IMaKn { get; set; }

    [Column("dThoiGian", TypeName = "datetime")]
    public DateTime DThoiGian { get; set; }

    [Column("iMaKH")]
    public int? IMaKh { get; set; }

    [Column("sNguyenNhan")]
    public string SNguyenNhan { get; set; } = null!;

    [Column("bDaGiaiQuyet")]
    public bool BDaGiaiQuyet { get; set; }

    [ForeignKey("IMaKh")]
    [InverseProperty("TblKhieuNais")]
    public virtual TblKhachHang? IMaKhNavigation { get; set; }
}
