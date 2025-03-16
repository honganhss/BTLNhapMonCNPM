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

		public int AddSanPham(TblSanPham sanPham)
		{
			sanPham.IMaSp = 0;
            sanPham.status = true;
			pharmacyDb.TblSanPhams.Add(sanPham);
			return pharmacyDb.SaveChanges();
		}

        public int UpdateSanPham(TblSanPham sanPham)
        {
            // Kiểm tra null
            if (sanPham == null)
                return -1;

            // Kiểm tra tên sản phẩm
            if (string.IsNullOrWhiteSpace(sanPham.STen))
                return -1;

            // Kiểm tra giá nhập và giá bán phải lớn hơn 0
            if (sanPham.FGiaNhap <= 0)
                return -1;

            if (sanPham.FGiaBan <= 0)
                return -1;

            // Kiểm tra số lượng không được âm
            if (sanPham.ISoLuong.HasValue && sanPham.ISoLuong < 0)
                return -1;

            // Kiểm tra mã nhà cung cấp hợp lệ
            if (sanPham.IMaNcc <= 0)
                return -1;

            // Kiểm tra mã loại sản phẩm hợp lệ
            if (sanPham.IMaLoaiSp <= 0)
                return -1;

            // Kiểm tra hạn sử dụng (định dạng hợp lệ & không phải ngày trong quá khứ)
            if (!string.IsNullOrWhiteSpace(sanPham.SHanSuDung) &&
                DateTime.TryParse(sanPham.SHanSuDung, out DateTime hanSuDung))
            {
                if (hanSuDung < DateTime.Now)
                    return -1;
            }

            var existingSanPham = pharmacyDb.TblSanPhams
                                    .FirstOrDefault(sp => sp.IMaSp == sanPham.IMaSp);
            if (existingSanPham == null)
            {
                return -1;
            }

            pharmacyDb.Entry(existingSanPham).CurrentValues.SetValues(sanPham);
            return pharmacyDb.SaveChanges();
        }

        public bool deleteSanPham(string id)
        {
            if(int.TryParse(id,out _) == false)
            {
                return false;
            }
            
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
