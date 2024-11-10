using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class ChucVuProfile
    {
        public object MapToDto(ChucVu chucVu)
        {
            return new
            {
                MaChucVu = chucVu.MaChucVu,
                TenChucVu = chucVu.TenChucVu,
                MoTaCongViec = chucVu.MoTaCongViec,
                MucLuong = chucVu.MucLuong,
                PhongBan = chucVu.PhongBan != null ? new
                {
                    SoPhong = chucVu.PhongBan.SoPhong,
                    TenPhong = chucVu.PhongBan.TenPhong
                } : null
            };
        }

        public ICollection<object> MapToList(ICollection<ChucVu> chucVuList)
        {
            return chucVuList.Select(MapToDto).ToList();
        }
    }
}
