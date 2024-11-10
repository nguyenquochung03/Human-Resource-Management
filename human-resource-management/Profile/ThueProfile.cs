using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class ThueProfile
    {
        public object MapToDto(Thue thue)
        {
            return new
            {
                MaSoThue = thue.MaSoThue,
                BaoHiemXaHoi = thue.BaoHiemXaHoi,
                BaoHiemYTe = thue.BaoHienYTe,
                BaoHiemThatNghiep = thue.BaoHiemThatNghiep,
                MucThuNhapChiuThue = thue.MucThuNhapChiuThue,
                ThueSuat = thue.ThueSuat,
                ThueThuNhapCaNhanThucTe = thue.ThueThuNhapCaNhanThucTe,
                ThoiGianTinh = new DateTime(thue.NamTinhThue, thue.ThangTinhThue, 1),
                NhanVien = thue.NhanVien != null ? new
                {
                    MaNhanVien = thue.NhanVien.MaNhanVien,
                    HoTen = thue.NhanVien.HoTen
                } : null
            };
        }

        public ICollection<object> MapToListDto(ICollection<Thue> thues)
        {
            return thues.Select(MapToDto).ToList();
        }
    }
}
