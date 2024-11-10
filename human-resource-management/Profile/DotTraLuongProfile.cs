using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class DotTraLuongProfile
    {
        public object MapToDto(DotTraLuong dotTraLuong)
        {
            return new
            {
                MaDotTraLuong = dotTraLuong.MaDotTraLuong,
                NguoiTraLuong = dotTraLuong.NguoiTraLuong,
                TenDotTraLuong = dotTraLuong.TenDotTraLuong,
                ThoiGian = new DateTime(dotTraLuong.NamDotTraLuong, dotTraLuong.ThangDotTraLuong, 1),
                TongTienCanPhaiTra = dotTraLuong.TongTienCanPhaiTra,
                TongTienDaTra = dotTraLuong.TongTienDaTra,
                TongTienConPhaiTra = dotTraLuong.TongTienConPhaiTra,
                TongNhanVienCanPhaiTra = dotTraLuong.TongNhanVienCanPhaiTra,
                TongNhanVienDaTra = dotTraLuong.TongNhanVienDaTra,
                GhiChu = dotTraLuong.GhiChu,
                TongNhanVienConPhaiTra = dotTraLuong.TongNhanVienConPhaiTra
            };
        }

        public ICollection<object> MapToListDto(ICollection<DotTraLuong> dotTraLuongs)
        {
            return dotTraLuongs.Select(MapToDto).ToList();
        }
    }
}
