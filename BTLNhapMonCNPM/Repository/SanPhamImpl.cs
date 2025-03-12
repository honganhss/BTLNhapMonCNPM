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

		public void AddSanPham(TblSanPham sanPham)
		{
			sanPham.IMaSp = 0;
			pharmacyDb.TblSanPhams.Add(sanPham);
			pharmacyDb.SaveChanges();
		}

        public void UpdateSanPham(TblSanPham sanPham)
        {
            pharmacyDb.TblSanPhams.Update(sanPham);
            pharmacyDb.SaveChanges();
        }

		public void DeleteSanPham(TblSanPham sanpham)
		{
			pharmacyDb.TblSanPhams.Remove(sanpham);
			pharmacyDb.SaveChanges();
		}
	}
}
