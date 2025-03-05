using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTLNhapMonCNPM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblLoaiSanPham",
                columns: table => new
                {
                    iMaLoaiSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLoaiSanPham", x => x.iMaLoaiSP);
                });

            migrationBuilder.CreateTable(
                name: "tblSanPham",
                columns: table => new
                {
                    iMaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sDangThuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sDonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iSoLuong = table.Column<int>(type: "int", nullable: false),
                    sImageThuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fGiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fGiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sCachSuDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sDieuKienBaoQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iMaNCC = table.Column<int>(type: "int", nullable: false),
                    iMaLoaiSP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSanPham", x => x.iMaSP);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblLoaiSanPham");

            migrationBuilder.DropTable(
                name: "tblSanPham");
        }
    }
}
