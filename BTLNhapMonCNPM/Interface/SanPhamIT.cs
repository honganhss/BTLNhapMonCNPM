using BTLNhapMonCNPM.Models;

namespace BTLNhapMonCNPM.Interface
{
    public interface SanPhamIT
    {

        public List<TblSanPham> getAllSanPham();
        public void AddSanPham(TblSanPham sanPham);
        public void UpdateSanPham(TblSanPham sanPham);

        public bool deleteSanPham(string id);
	}
}
