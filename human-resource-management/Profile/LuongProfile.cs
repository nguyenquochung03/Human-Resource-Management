using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class LuongProfile
    {
        public object MapToDto(Luong luong)
        {
            return new
            {
                MaLuong = luong.MaLuong,
                LuongCoBan = luong.LuongCoBan,
                TongTienPhuCap = luong.TongTienPhuCap,
                TongTienThuong = luong.TongTienThuong,
                TongTien = luong.TongTien,
                CacKhoanKhauTru = luong.CacKhoanKhauTru,
                ThuNhapRong = luong.ThuNhapRong,
                ThoiGian = new DateTime(luong.NamTinhLuong, luong.ThangTinhLuong, 1),
                NhanVien = luong.NhanVien != null ? new
                {
                    MaNhanVien = luong.NhanVien.MaNhanVien,
                    HoTen = luong.NhanVien.HoTen
                } : null
            };
        }

        public ICollection<object> MapToListDto(ICollection<Luong> luongs)
        {
            return luongs.Select(MapToDto).ToList();
        }
    }
}
