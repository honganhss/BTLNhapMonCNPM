using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTLNhapMonCNPM.Migrations
{
    /// <inheritdoc />
    public partial class M0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblKhachHang",
                columns: table => new
                {
                    iMaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sHoTen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sTenDangNhap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    sMatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sSDT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblKhach__F20AA50860D00D03", x => x.iMaKH);
                });

            migrationBuilder.CreateTable(
                name: "tblLoaiSanPham",
                columns: table => new
                {
                    iMaLoaiSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTenLoai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblLoaiS__4B6603B6C6064CBF", x => x.iMaLoaiSP);
                });

            migrationBuilder.CreateTable(
                name: "tblNhaCungCap",
                columns: table => new
                {
                    iMaNCC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTenNCC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sDiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sSDT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    sEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    bChungChi = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblNhaCu__204A73DC644A75C9", x => x.iMaNCC);
                });

            migrationBuilder.CreateTable(
                name: "tblNhanVien",
                columns: table => new
                {
                    iMaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sMatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sHoTen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sChucVu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fLuongCB = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    fHeSoLuong = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    dNgayVaoLam = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblNhanV__F20A8D79D2CAB3C0", x => x.iMaNV);
                });

            migrationBuilder.CreateTable(
                name: "tblHoaDon",
                columns: table => new
                {
                    iMaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iMaKH = table.Column<int>(type: "int", nullable: true),
                    dThoiGian = table.Column<DateTime>(type: "datetime", nullable: false),
                    fTongGiaTri = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    sGhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblHoaDo__F20ABED0701FF6C3", x => x.iMaHD);
                    table.ForeignKey(
                        name: "FK__tblHoaDon__iMaKH__49C3F6B7",
                        column: x => x.iMaKH,
                        principalTable: "tblKhachHang",
                        principalColumn: "iMaKH");
                });

            migrationBuilder.CreateTable(
                name: "tblKhieuNai",
                columns: table => new
                {
                    iMaKN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dThoiGian = table.Column<DateTime>(type: "datetime", nullable: false),
                    iMaKH = table.Column<int>(type: "int", nullable: true),
                    sNguyenNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bDaGiaiQuyet = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblKhieu__F20AA502DF963DC6", x => x.iMaKN);
                    table.ForeignKey(
                        name: "FK__tblKhieuN__iMaKH__4CA06362",
                        column: x => x.iMaKH,
                        principalTable: "tblKhachHang",
                        principalColumn: "iMaKH");
                });

            migrationBuilder.CreateTable(
                name: "tblSanPham",
                columns: table => new
                {
                    iMaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sDangThuoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sDonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    iSoLuong = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    sImageThuoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fGiaNhap = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    fGiaBan = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    sCachSuDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sDieuKienBaoQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    iMaNCC = table.Column<int>(type: "int", nullable: false),
                    sHanSuDung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    bBietTru = table.Column<bool>(type: "bit", nullable: false),
                    bThuHoi = table.Column<bool>(type: "bit", nullable: false),
                    bCanKeDon = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    iMaLoaiSP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblSanPh__F20A661E1F6C4D7D", x => x.iMaSP);
                    table.ForeignKey(
                        name: "FK__tblSanPha__iMaLo__46E78A0C",
                        column: x => x.iMaLoaiSP,
                        principalTable: "tblLoaiSanPham",
                        principalColumn: "iMaLoaiSP");
                    table.ForeignKey(
                        name: "FK__tblSanPha__iMaNC__45F365D3",
                        column: x => x.iMaNCC,
                        principalTable: "tblNhaCungCap",
                        principalColumn: "iMaNCC");
                });

            migrationBuilder.CreateTable(
                name: "tblNhapHang",
                columns: table => new
                {
                    iMaNH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    iMaNV = table.Column<int>(type: "int", nullable: true),
                    iMaNCC = table.Column<int>(type: "int", nullable: false),
                    fTongTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblNhapH__F20A8D6B5A0E4776", x => x.iMaNH);
                    table.ForeignKey(
                        name: "FK__tblNhapHa__iMaNC__5070F446",
                        column: x => x.iMaNCC,
                        principalTable: "tblNhaCungCap",
                        principalColumn: "iMaNCC");
                    table.ForeignKey(
                        name: "FK__tblNhapHa__iMaNV__4F7CD00D",
                        column: x => x.iMaNV,
                        principalTable: "tblNhanVien",
                        principalColumn: "iMaNV");
                });

            migrationBuilder.CreateTable(
                name: "tblThuocKeDon",
                columns: table => new
                {
                    iMaTKD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iMaKH = table.Column<int>(type: "int", nullable: true),
                    iMaNV = table.Column<int>(type: "int", nullable: false),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    sGhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblThuoc__22CA58456196E352", x => x.iMaTKD);
                    table.ForeignKey(
                        name: "FK__tblThuocK__iMaKH__534D60F1",
                        column: x => x.iMaKH,
                        principalTable: "tblKhachHang",
                        principalColumn: "iMaKH");
                    table.ForeignKey(
                        name: "FK__tblThuocK__iMaNV__5441852A",
                        column: x => x.iMaNV,
                        principalTable: "tblNhanVien",
                        principalColumn: "iMaNV");
                });

            migrationBuilder.CreateTable(
                name: "tblLoSanPham",
                columns: table => new
                {
                    iLoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iSanPhamId = table.Column<int>(type: "int", nullable: false),
                    dNgaySanXuat = table.Column<DateOnly>(type: "date", nullable: false),
                    dNgayHetHan = table.Column<DateOnly>(type: "date", nullable: false),
                    iSoLuongNhap = table.Column<int>(type: "int", nullable: false),
                    iSoLuongTon = table.Column<int>(type: "int", nullable: false),
                    dNgayNhap = table.Column<DateOnly>(type: "date", nullable: false),
                    iDonNhapId = table.Column<int>(type: "int", nullable: false),
                    fGiaNhap = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    sDonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblLoSan__D6EA297DB9308BBB", x => x.iLoId);
                    table.ForeignKey(
                        name: "FK__tblLoSanP__iDonN__59063A47",
                        column: x => x.iDonNhapId,
                        principalTable: "tblNhapHang",
                        principalColumn: "iMaNH");
                    table.ForeignKey(
                        name: "FK__tblLoSanP__iSanP__5812160E",
                        column: x => x.iSanPhamId,
                        principalTable: "tblSanPham",
                        principalColumn: "iMaSP");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblHoaDon_iMaKH",
                table: "tblHoaDon",
                column: "iMaKH");

            migrationBuilder.CreateIndex(
                name: "UQ__tblKhach__07DACB08679F8ED0",
                table: "tblKhachHang",
                column: "sEmail",
                unique: true,
                filter: "[sEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__tblKhach__2584CE9E610A2FA1",
                table: "tblKhachHang",
                column: "sTenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblKhieuNai_iMaKH",
                table: "tblKhieuNai",
                column: "iMaKH");

            migrationBuilder.CreateIndex(
                name: "IX_tblLoSanPham_iDonNhapId",
                table: "tblLoSanPham",
                column: "iDonNhapId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLoSanPham_iSanPhamId",
                table: "tblLoSanPham",
                column: "iSanPhamId");

            migrationBuilder.CreateIndex(
                name: "UQ__tblNhanV__2584CE9E1967B839",
                table: "tblNhanVien",
                column: "sTenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblNhapHang_iMaNCC",
                table: "tblNhapHang",
                column: "iMaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_tblNhapHang_iMaNV",
                table: "tblNhapHang",
                column: "iMaNV");

            migrationBuilder.CreateIndex(
                name: "IX_tblSanPham_iMaLoaiSP",
                table: "tblSanPham",
                column: "iMaLoaiSP");

            migrationBuilder.CreateIndex(
                name: "IX_tblSanPham_iMaNCC",
                table: "tblSanPham",
                column: "iMaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_tblThuocKeDon_iMaKH",
                table: "tblThuocKeDon",
                column: "iMaKH");

            migrationBuilder.CreateIndex(
                name: "IX_tblThuocKeDon_iMaNV",
                table: "tblThuocKeDon",
                column: "iMaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblHoaDon");

            migrationBuilder.DropTable(
                name: "tblKhieuNai");

            migrationBuilder.DropTable(
                name: "tblLoSanPham");

            migrationBuilder.DropTable(
                name: "tblThuocKeDon");

            migrationBuilder.DropTable(
                name: "tblNhapHang");

            migrationBuilder.DropTable(
                name: "tblSanPham");

            migrationBuilder.DropTable(
                name: "tblKhachHang");

            migrationBuilder.DropTable(
                name: "tblNhanVien");

            migrationBuilder.DropTable(
                name: "tblLoaiSanPham");

            migrationBuilder.DropTable(
                name: "tblNhaCungCap");
        }
    }
}
