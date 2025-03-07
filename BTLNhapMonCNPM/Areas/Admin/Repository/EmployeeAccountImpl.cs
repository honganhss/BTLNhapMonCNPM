using BTLNhapMonCNPM.Areas.Admin.Interfaces;
using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.Areas.Admin.Repository
{

    public class EmployeeAccountImpl : EmployeeAccountIT
    {
        PharmacyDbContext pharmacyDb = PharmacyDbContext.getDbContext();
        public List<TblNhanVien> getAllNhanVien()
        {

            List<TblNhanVien> list = (from s in pharmacyDb.TblNhanViens select s).ToList();
            return list;

        }

    }
}
