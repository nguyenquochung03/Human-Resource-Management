using HumanResourceManagement.Models;

namespace HumanResourceManagement.Profile
{
    public class ThuongProfile
    {
        public object MapToDto(Thuong thuong)
        {
            return new
            {
                MaKhoanThuong = thuong.MaKhoanThuong,
                NgayKhenThuong = thuong.NgayKhenThuong,
                LyDoKhenThuong = thuong.LyDoKhenThuong,
                SoTienThuong = thuong.SoTienThuong
            };
        }

        public ICollection<object> MapToListDto(ICollection<Thuong> thuongs)
        {
            return thuongs.Select(MapToDto).ToList();
        }
    }
}
