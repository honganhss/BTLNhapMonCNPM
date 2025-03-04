using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using System.Collections.Generic;

namespace BTLNhapMonCNPM.Repository
{
    public class SanPhamImpl : SanPhamIT
    {
        private readonly PharmacyDbContext pharmacyDb = PharmacyDbContext.getDbContext();
        
        public List<TblSanPham> getAllSanPham()
        {
            List<TblSanPham> list = (from s in pharmacyDb.TblSanPhams select s).ToList();
            return list;
        }
    }
}
