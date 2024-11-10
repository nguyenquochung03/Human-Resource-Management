using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class TraLuongProfile
    {
        public object MapToDto(TraLuong traLuong)
        {
            return new
            {
                MaTraLuong = traLuong.MaTraLuong,
                NgayTraLuong = traLuong.NgayTraLuong,
                TrangThai = traLuong.TrangThai,
                SoTienCanPhaiTra = traLuong.SoTienCanPhaiTra,
                SoTienConPhaiTra = traLuong.SoTienConPhaiTra,
                SoTienDaTra = traLuong.SoTienDaTra,
                NhanVien = traLuong.NhanVien != null ? new
                {
                    MaNhanVien = traLuong.NhanVien.MaNhanVien,
                    HoTen = traLuong.NhanVien.HoTen
                } : null,
                DotTraLuong = traLuong.DotTraLuong != null ? new
                {
                    MaDotTraLuong = traLuong.DotTraLuong.MaDotTraLuong,
                    NguoiTraLuong = traLuong.DotTraLuong.NguoiTraLuong,
                    TenDotTraLuong = traLuong.DotTraLuong.TenDotTraLuong,
                    ThangDotTraLuong = traLuong.DotTraLuong.ThangDotTraLuong,
                    NamDotTraLuong = traLuong.DotTraLuong.NamDotTraLuong,
                    TongTienCanPhaiTra = traLuong.DotTraLuong.TongTienCanPhaiTra,
                    TongNhanVienCanPhaiTra = traLuong.DotTraLuong.TongNhanVienCanPhaiTra,

                } : null
            };
        }

        public ICollection<object> MapToListDto(ICollection<TraLuong> traLuongs)
        {
            return traLuongs.Select(MapToDto).ToList();
        }
    }
}
