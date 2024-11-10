using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class NhanVienProfile
    {
        public object MapToDto(NhanVien nhanVien)
        {
            return new
            {
                MaNhanVien = nhanVien.MaNhanVien,
                HoTen = nhanVien.HoTen,
                Cccd = nhanVien.Cccd,
                TonGiao = nhanVien.TonGiao,
                DiaChi = nhanVien.DiaChi,
                Email = nhanVien.Email,
                NgayVaoLam = nhanVien.NgayVaoLam,
                GioiTinh = nhanVien.GioiTinh,
                NgaySinh = nhanVien.NgaySinh,
                SoDienThoai = nhanVien.SoDienThoai,
                TrangThai = nhanVien.TrangThai,
                MucLuong = nhanVien.MucLuong,
                SoNguoiPhuThuoc = nhanVien.SoNguoiPhuThuoc,
                TrangThaiHonNhan = nhanVien.TrangThaiHonNhan,
                PhongBan = nhanVien.PhongBan != null ? new
                {
                    SoPhong = nhanVien.PhongBan.SoPhong,
                    TenPhong = nhanVien.PhongBan.TenPhong,
                    NguoiQuanLy = nhanVien.PhongBan.NguoiQuanLy,
                    MoTa = nhanVien.PhongBan.MoTa,
                    DiaDiem = nhanVien.PhongBan.DiaDiem,
                    Email = nhanVien.PhongBan.Email,

                } : null,
                ChucVu = nhanVien.ChucVu != null ? new
                {
                    MaChucVu = nhanVien.ChucVu.MaChucVu,
                    TenChucVu = nhanVien.ChucVu.TenChucVu,
                    MoTaCongViec = nhanVien.ChucVu.MoTaCongViec,
                    MucLuong = nhanVien.ChucVu.MucLuong,

                } : null,
                PhuCaps = nhanVien.PhuCapNhanViens != null ? nhanVien.PhuCapNhanViens.Select(nv => new
                {
                    MaPhuCap = nv.PhuCapId,
                    TenPhuCap = nv.PhuCap.TenPhuCap,
                    SoTienPhuCap = nv.PhuCap.SoTienPhuCap,
                    TrangThai = nv.PhuCap.TrangThai,
                    TanSuat = nv.PhuCap.TanSuat
                }) : null,
                LichLams = nhanVien.LichLamNhanViens != null ? nhanVien.LichLamNhanViens.Select(nv => new
                {
                    MaLichLam = nv.LichLamId,
                    NgayLam = nv.LichLam.NgayLam,
                    GioBatDau = nv.LichLam.GioBatDau,
                    GioKetThuc = nv.LichLam.GioKetThuc,
                }) : null,
            };
        }

        public ICollection<object> MapToListDto(ICollection<NhanVien> nhanViens)
        {
            return nhanViens.Select(MapToDto).ToList();
        }
    }
}
