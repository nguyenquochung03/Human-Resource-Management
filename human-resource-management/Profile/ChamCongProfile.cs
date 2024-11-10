using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class ChamCongProfile
    {
        public object MapToDto(ChamCong chamCong)
        {
            return new
            {
                MaChamCong = chamCong.MaChamCong,
                NgayChamCong = chamCong.NgayChamCong,
                GioVaoLam = chamCong.GioVaoLam,
                GioRaLam = chamCong.GioRaLam,
                SoGioLamThem = chamCong.SoGioLamThem,
                SoGioNghiPhep = chamCong.SoGioNghiPhep,
                SoGioNghiKhongPhep = chamCong.SoGioNghiKhongPhep,
                SoGioLamViec = chamCong.SoGioLamViec,
                TongGioLam = chamCong.TongGioLam,
                NhanVien = chamCong.NhanVien != null ? new
                {
                    MaNhanVien = chamCong.NhanVien.MaNhanVien,
                    HoTen = chamCong.NhanVien.HoTen,
                } : null,
            };
        }

        public ICollection<object> MapToListDto(ICollection<ChamCong> list)
        {
            return list.Select(MapToDto).ToList();
        }
    }
}
