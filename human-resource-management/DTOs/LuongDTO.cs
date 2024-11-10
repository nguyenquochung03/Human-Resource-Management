using HumanResourceManagement.Models;

namespace HumanResourceManagement.DTOs
{
    public class LuongDTO
    {
        public string MaLuong { get; set; }
        public DateTime ThoiGian { get; set; }
        public NhanVien NhanVien { get; set; }
        public Thue Thue { get; set; }
    }
}
