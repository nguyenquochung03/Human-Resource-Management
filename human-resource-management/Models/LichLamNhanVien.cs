namespace HumanResourceManagement.Models
{
    public class LichLamNhanVien
    {
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }

        public string LichLamId { get; set; }
        public LichLam LichLam { get; set; }
    }
}
