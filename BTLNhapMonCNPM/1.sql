IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [tblKhachHang] (
    [iMaKH] int NOT NULL IDENTITY,
    [sHoTen] nvarchar(255) NOT NULL,
    [sTenDangNhap] nvarchar(100) NOT NULL,
    [sMatKhau] nvarchar(255) NOT NULL,
    [sEmail] nvarchar(255) NULL,
    [sSDT] nvarchar(20) NULL,
    [dNgayTao] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__tblKhach__F20AA50860D00D03] PRIMARY KEY ([iMaKH])
);

CREATE TABLE [tblLoaiSanPham] (
    [iMaLoaiSP] int NOT NULL IDENTITY,
    [sTenLoai] nvarchar(255) NOT NULL,
    CONSTRAINT [PK__tblLoaiS__4B6603B6C6064CBF] PRIMARY KEY ([iMaLoaiSP])
);

CREATE TABLE [tblNhaCungCap] (
    [iMaNCC] int NOT NULL IDENTITY,
    [sTenNCC] nvarchar(255) NOT NULL,
    [sDiaChi] nvarchar(max) NULL,
    [sSDT] nvarchar(20) NULL,
    [sEmail] nvarchar(255) NULL,
    [bChungChi] varbinary(max) NOT NULL,
    CONSTRAINT [PK__tblNhaCu__204A73DC644A75C9] PRIMARY KEY ([iMaNCC])
);

CREATE TABLE [tblNhanVien] (
    [iMaNV] int NOT NULL IDENTITY,
    [sTenDangNhap] nvarchar(50) NOT NULL,
    [sMatKhau] nvarchar(255) NOT NULL,
    [sHoTen] nvarchar(255) NOT NULL,
    [sChucVu] nvarchar(20) NOT NULL,
    [fLuongCB] decimal(10,2) NOT NULL,
    [fHeSoLuong] decimal(10,2) NOT NULL,
    [dNgayVaoLam] date NOT NULL,
    CONSTRAINT [PK__tblNhanV__F20A8D79D2CAB3C0] PRIMARY KEY ([iMaNV])
);

CREATE TABLE [tblHoaDon] (
    [iMaHD] int NOT NULL IDENTITY,
    [iMaKH] int NULL,
    [dThoiGian] datetime NOT NULL,
    [fTongGiaTri] decimal(10,2) NOT NULL,
    [sGhiChu] nvarchar(max) NULL,
    CONSTRAINT [PK__tblHoaDo__F20ABED0701FF6C3] PRIMARY KEY ([iMaHD]),
    CONSTRAINT [FK__tblHoaDon__iMaKH__49C3F6B7] FOREIGN KEY ([iMaKH]) REFERENCES [tblKhachHang] ([iMaKH])
);

CREATE TABLE [tblKhieuNai] (
    [iMaKN] int NOT NULL IDENTITY,
    [dThoiGian] datetime NOT NULL,
    [iMaKH] int NULL,
    [sNguyenNhan] nvarchar(max) NOT NULL,
    [bDaGiaiQuyet] bit NOT NULL,
    CONSTRAINT [PK__tblKhieu__F20AA502DF963DC6] PRIMARY KEY ([iMaKN]),
    CONSTRAINT [FK__tblKhieuN__iMaKH__4CA06362] FOREIGN KEY ([iMaKH]) REFERENCES [tblKhachHang] ([iMaKH])
);

CREATE TABLE [tblSanPham] (
    [iMaSP] int NOT NULL IDENTITY,
    [sTen] nvarchar(255) NOT NULL,
    [sDangThuoc] nvarchar(100) NULL,
    [sDonViTinh] nvarchar(50) NULL,
    [iSoLuong] int NULL DEFAULT 0,
    [sImageThuoc] nvarchar(max) NULL,
    [fGiaNhap] decimal(10,2) NOT NULL,
    [fGiaBan] decimal(10,2) NOT NULL,
    [sCachSuDung] nvarchar(max) NULL,
    [sDieuKienBaoQuan] nvarchar(max) NULL,
    [iMaNCC] int NOT NULL,
    [sHanSuDung] nvarchar(100) NULL,
    [bBietTru] bit NOT NULL,
    [bThuHoi] bit NOT NULL,
    [bCanKeDon] bit NULL DEFAULT CAST(0 AS bit),
    [iMaLoaiSP] int NOT NULL,
    CONSTRAINT [PK__tblSanPh__F20A661E1F6C4D7D] PRIMARY KEY ([iMaSP]),
    CONSTRAINT [FK__tblSanPha__iMaLo__46E78A0C] FOREIGN KEY ([iMaLoaiSP]) REFERENCES [tblLoaiSanPham] ([iMaLoaiSP]),
    CONSTRAINT [FK__tblSanPha__iMaNC__45F365D3] FOREIGN KEY ([iMaNCC]) REFERENCES [tblNhaCungCap] ([iMaNCC])
);

CREATE TABLE [tblNhapHang] (
    [iMaNH] int NOT NULL IDENTITY,
    [dNgayTao] datetime NOT NULL,
    [iMaNV] int NULL,
    [iMaNCC] int NOT NULL,
    [fTongTien] float NOT NULL,
    CONSTRAINT [PK__tblNhapH__F20A8D6B5A0E4776] PRIMARY KEY ([iMaNH]),
    CONSTRAINT [FK__tblNhapHa__iMaNC__5070F446] FOREIGN KEY ([iMaNCC]) REFERENCES [tblNhaCungCap] ([iMaNCC]),
    CONSTRAINT [FK__tblNhapHa__iMaNV__4F7CD00D] FOREIGN KEY ([iMaNV]) REFERENCES [tblNhanVien] ([iMaNV])
);

CREATE TABLE [tblThuocKeDon] (
    [iMaTKD] int NOT NULL IDENTITY,
    [iMaKH] int NULL,
    [iMaNV] int NOT NULL,
    [dNgayTao] datetime NOT NULL,
    [sGhiChu] nvarchar(max) NULL,
    CONSTRAINT [PK__tblThuoc__22CA58456196E352] PRIMARY KEY ([iMaTKD]),
    CONSTRAINT [FK__tblThuocK__iMaKH__534D60F1] FOREIGN KEY ([iMaKH]) REFERENCES [tblKhachHang] ([iMaKH]),
    CONSTRAINT [FK__tblThuocK__iMaNV__5441852A] FOREIGN KEY ([iMaNV]) REFERENCES [tblNhanVien] ([iMaNV])
);

CREATE TABLE [tblLoSanPham] (
    [iLoId] int NOT NULL IDENTITY,
    [iSanPhamId] int NOT NULL,
    [dNgaySanXuat] date NOT NULL,
    [dNgayHetHan] date NOT NULL,
    [iSoLuongNhap] int NOT NULL,
    [iSoLuongTon] int NOT NULL,
    [dNgayNhap] date NOT NULL,
    [iDonNhapId] int NOT NULL,
    [fGiaNhap] decimal(10,2) NOT NULL,
    [sDonViTinh] nvarchar(50) NULL,
    CONSTRAINT [PK__tblLoSan__D6EA297DB9308BBB] PRIMARY KEY ([iLoId]),
    CONSTRAINT [FK__tblLoSanP__iDonN__59063A47] FOREIGN KEY ([iDonNhapId]) REFERENCES [tblNhapHang] ([iMaNH]),
    CONSTRAINT [FK__tblLoSanP__iSanP__5812160E] FOREIGN KEY ([iSanPhamId]) REFERENCES [tblSanPham] ([iMaSP])
);

CREATE INDEX [IX_tblHoaDon_iMaKH] ON [tblHoaDon] ([iMaKH]);

CREATE UNIQUE INDEX [UQ__tblKhach__07DACB08679F8ED0] ON [tblKhachHang] ([sEmail]) WHERE [sEmail] IS NOT NULL;

CREATE UNIQUE INDEX [UQ__tblKhach__2584CE9E610A2FA1] ON [tblKhachHang] ([sTenDangNhap]);

CREATE INDEX [IX_tblKhieuNai_iMaKH] ON [tblKhieuNai] ([iMaKH]);

CREATE INDEX [IX_tblLoSanPham_iDonNhapId] ON [tblLoSanPham] ([iDonNhapId]);

CREATE INDEX [IX_tblLoSanPham_iSanPhamId] ON [tblLoSanPham] ([iSanPhamId]);

CREATE UNIQUE INDEX [UQ__tblNhanV__2584CE9E1967B839] ON [tblNhanVien] ([sTenDangNhap]);

CREATE INDEX [IX_tblNhapHang_iMaNCC] ON [tblNhapHang] ([iMaNCC]);

CREATE INDEX [IX_tblNhapHang_iMaNV] ON [tblNhapHang] ([iMaNV]);

CREATE INDEX [IX_tblSanPham_iMaLoaiSP] ON [tblSanPham] ([iMaLoaiSP]);

CREATE INDEX [IX_tblSanPham_iMaNCC] ON [tblSanPham] ([iMaNCC]);

CREATE INDEX [IX_tblThuocKeDon_iMaKH] ON [tblThuocKeDon] ([iMaKH]);

CREATE INDEX [IX_tblThuocKeDon_iMaNV] ON [tblThuocKeDon] ([iMaNV]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250304040915_M0', N'9.0.2');

COMMIT;
GO

