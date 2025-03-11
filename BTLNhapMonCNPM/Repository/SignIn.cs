using BTLNhapMonCNPM.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLNhapMonCNPM.Repository
{
    public class SignIn 
    {
        private readonly PharmacyDbContext pharmacyDb = new PharmacyDbContext();
        
       public TblKhachHang verify(string username,string password)
       {
            TblKhachHang khachHang = (from s in pharmacyDb.TblKhachHangs 
                                      where s.STenDangNhap == username && s.SMatKhau == password
                                      select s).FirstOrDefault();

            return khachHang ;
       }
        public TblNhanVien verifyAdmin(string username, string password)
        {
            TblNhanVien nhanVien = (from s in pharmacyDb.TblNhanViens
                                      where s.STenDangNhap == username && s.SMatKhau == password
                                      select s).FirstOrDefault();

            return nhanVien;
        }
    }
}
