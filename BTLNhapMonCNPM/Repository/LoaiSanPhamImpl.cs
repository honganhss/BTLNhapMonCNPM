using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.Repository
{
    public class LoaiSanPhamImpl : LoaiSanPhamIT
    {
        PharmacyDbContext PharmacyDb = PharmacyDbContext.getDbContext();
        public List<TblLoaiSanPham> getAllDanhMuc()
        {
            List<TblLoaiSanPham> list = (from s in PharmacyDb.TblLoaiSanPhams
                                         select s
                                         ).ToList();
            return list;
        }
    }
}
