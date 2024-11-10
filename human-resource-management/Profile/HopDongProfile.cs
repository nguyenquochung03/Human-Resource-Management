using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class HopDongProfile
    {
        public object MapToDto(HopDong hopDong)
        {
            return new
            {
                MaHopDong = hopDong.MaHopDong,
                LoaiHopDong = hopDong.LoaiHopDong,
                NoiDungHopDong = hopDong.NoiDungHopDong,
                ThoiHanHopDong = hopDong.ThoiHanHopDong,
                TrangThaiHopDong = hopDong.TrangThaiHopDong,
                NgayBatDau = hopDong.NgayBatDau,
                NgayKetThuc = hopDong.NgayKetThuc,
                NgayKy = hopDong.NgayKy,
                GhiChu = hopDong.GhiChu,
                NhanVien = hopDong.NhanVien != null ? new
                {
                    MaNhanVien = hopDong.NhanVien.MaNhanVien,
                    HoTen = hopDong.NhanVien.HoTen,
                } : null,
            };
        }

        public ICollection<object> MapToListDto(ICollection<HopDong> hopDongs)
        {
            return hopDongs.Select(MapToDto).ToList();
        }

        internal object MapToListDto(HopDong hd)
        {
            throw new NotImplementedException();
        }
    }
}
