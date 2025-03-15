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
            List<TblSanPham> list = (from s in pharmacyDb.TblSanPhams where s.status == true select s).ToList();
            return list;
        }

		public void AddSanPham(TblSanPham sanPham)
		{
			sanPham.IMaSp = 0;
            sanPham.status = true;
			pharmacyDb.TblSanPhams.Add(sanPham);
			pharmacyDb.SaveChanges();
		}

        public void UpdateSanPham(TblSanPham sanPham)
        {
            pharmacyDb.TblSanPhams.Update(sanPham);
            pharmacyDb.SaveChanges();
        }

        public bool deleteSanPham(string id)
        {
            TblSanPham sanPham = (from s in pharmacyDb.TblSanPhams where s.IMaSp == int.Parse(id) select s).FirstOrDefault();
            if(sanPham != null)
            {
                sanPham.status = false;

                pharmacyDb.TblSanPhams.Update(sanPham);

                if(pharmacyDb.SaveChanges() != 0)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
