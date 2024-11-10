using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class HieuSuatProfile
    {
        public object MapToDto(HieuSuat hieuSuat)
        {
            return new
            {
                MaHieuSuat = hieuSuat.MaHieuSuat,
                MucTieuHieuSuat = hieuSuat.MucTieuHieuSuat,
                DanhGiaHieuSuat = hieuSuat.DanhGiaHieuSuat,
                PhanHoi = hieuSuat.PhanHoi,
                KyDanhGia = hieuSuat.KyDanhGia,
                NguoiDanhGia = hieuSuat.NguoiDanhGia,
                KeHoachPhatTrien = hieuSuat.KeHoachPhatTrien,
                NhanVien = hieuSuat.NhanVien != null ? new
                {
                    MaNhanVien = hieuSuat.NhanVien.MaNhanVien,
                    HoTen = hieuSuat.NhanVien.HoTen,
                } : null,
            };
        }

        public ICollection<object> MapToListDto(ICollection<HieuSuat> hieuSuats)
        {
            return hieuSuats.Select(MapToDto).ToList();
        }
    }
}
