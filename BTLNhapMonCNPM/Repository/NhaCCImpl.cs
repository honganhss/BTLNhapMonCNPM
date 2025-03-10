using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.Repository
{
	// ReSharper disable once InconsistentNaming
	public class NhaCCImpl : NhaCCIT
    {
	    PharmacyDbContext PharmacyDb = PharmacyDbContext.getDbContext();

		public List<TblNhaCungCap> getAllNhaCC()
		{
			List<TblNhaCungCap> list = (from s in PharmacyDb.TblNhaCungCaps select s).ToList();
			return list;
		}
	}
}
