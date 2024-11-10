namespace HumanResourceManagement.Models
{
    public class ThuongNhanVien
    {
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }

        public string ThuongId { get; set; }
        public Thuong Thuong { get; set; }
        public int LanThu { get; set; }
    }
}
