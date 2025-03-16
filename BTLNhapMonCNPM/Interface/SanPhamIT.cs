using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.Interface
{
    public interface SanPhamIT
    {

        public List<TblSanPham> getAllSanPham();
        public int AddSanPham(TblSanPham sanPham);
        public int UpdateSanPham(TblSanPham sanPham);

        public bool deleteSanPham(string id);
	}
}
