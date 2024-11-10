using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class LichLam
    {
        [Required(ErrorMessage = "Mã lịch làm không được để trống!")]
        public string MaLichLam { get; set; }
        [Required(ErrorMessage = "Ngày làm không được để trống!")]
        public DateTime NgayLam { get; set; }
        [Required(ErrorMessage = "Giờ bắt đầu không được để trống!")]
        public string GioBatDau { get; set; }
        [Required(ErrorMessage = "Giờ kết thúc không được để trống!")]
        public string GioKetThuc { get; set; }
    
        [Required(ErrorMessage = "Mô tả công việc không được để trống!")]
        public string MoTaCongViec { get; set; }
        public string GhiChu { get; set; }
        public string LoaiLich { get; set; }

        // Liên kết bảng Nhân viên:
        public ICollection<LichLamNhanVien> LichLamNhanViens { get; set; }
    }
}
