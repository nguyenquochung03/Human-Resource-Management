namespace HumanResourceManagement.Models
{
    public class PhuCapNhanVien
    {
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }

        public string PhuCapId { get; set; }
        public PhuCap PhuCap { get; set; }
    }
}
