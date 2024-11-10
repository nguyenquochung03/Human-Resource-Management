using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class LichLamProfile
    {
        public object MapToDto(LichLam lich)
        {
            return new
            {
                MaLichLam = lich.MaLichLam,
                NgayLam = lich.NgayLam,
                GioBatDau = lich.GioBatDau,
                GioKetThuc = lich.GioKetThuc,
                GhiChu = lich.GhiChu,
                MoTaCongViec = lich.MoTaCongViec,
                LoaiLich = lich.LoaiLich,
            };
        }

        public ICollection<object> MapToListDto(ICollection<LichLam> lichs)
        {
            return lichs.Select(MapToDto).ToList();
        }
    }
}
