using BTLNhapMonCNPM.Areas.Admin.Interfaces;
using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.Areas.Admin.Repository
{
    public class CustomerAccountImpl : CustomerAccountIT
    {
        PharmacyDbContext pharmacyDb = new PharmacyDbContext();
        public List<TblKhachHang> getAllKhachHang()
        {
            List<TblKhachHang> list = (from s in pharmacyDb.TblKhachHangs select s).ToList();
            return list;
        }
    }
}
