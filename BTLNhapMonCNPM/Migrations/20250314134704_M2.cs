using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTLNhapMonCNPM.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bStatus",
                table: "tblSanPham",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "bStatus",
                table: "tblLoaiSanPham",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bStatus",
                table: "tblSanPham");

            migrationBuilder.DropColumn(
                name: "bStatus",
                table: "tblLoaiSanPham");
        }
    }
}
