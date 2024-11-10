using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class NghiVangProfile
    {
        public object MapToDto(NghiVang nghiVang)
        {
            return new
            {
                MaNghiVang = nghiVang.MaNghiVang,
                LoaiNghiVang = nghiVang.LoaiNghiVang,
                GhiChu = nghiVang.GhiChu,
                SoNgayNghi = nghiVang.SoNgayNghi,
                LyDo = nghiVang.LyDo,
                NgayBatDau = nghiVang.NgayBatDau,
                NgayKetThuc = nghiVang.NgayKetThuc,
                NhanVien = nghiVang.NhanVien != null ? new
                {
                    MaNhanVien = nghiVang.NhanVien.MaNhanVien,
                    HoTen = nghiVang.NhanVien.HoTen
                } : null
            };
        }

        public ICollection<object> MapToListDto(ICollection<NghiVang> nghiVangs)
        {
            return nghiVangs.Select(MapToDto).ToList();
        }
    }
}
