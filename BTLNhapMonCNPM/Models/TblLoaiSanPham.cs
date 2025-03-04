using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BTLNhapMonCNPM.Models;

[Table("tblLoaiSanPham")]
public partial class TblLoaiSanPham
{
    [Key]
    [Column("iMaLoaiSP")]
    public int IMaLoaiSp { get; set; }

    [Column("sTenLoai")]
    [StringLength(255)]
    public string STenLoai { get; set; } = null!;

    [InverseProperty("IMaLoaiSpNavigation")]
    public virtual ICollection<TblSanPham> TblSanPhams { get; set; } = new List<TblSanPham>();
}
