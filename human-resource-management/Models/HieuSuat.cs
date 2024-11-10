using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class HieuSuat
    {
        [Required(ErrorMessage = "Mã hiệu suất không được để trống!")]
        public string MaHieuSuat { get; set; }
        [Required(ErrorMessage = "Người đánh giá không được để trống!")]
        public string NguoiDanhGia { get; set; }
        [Required(ErrorMessage = "Kỳ đánh giá không được để trống!")]
        public string KyDanhGia { get; set; }
        [Required(ErrorMessage = "Mục tiêu hiệu suất không được để trống!")]
        public string MucTieuHieuSuat { get; set; }
        [Required(ErrorMessage = "Đánh giá hiệu suất không được để trống!")]
        public string DanhGiaHieuSuat { get; set; }
        [Required(ErrorMessage = "Phản hồi hiệu suất không được để trống!")]
        public string PhanHoi { get; set; }
        [Required(ErrorMessage = "Kế hoạch phát triển hiệu suất không được để trống!")]
        public string KeHoachPhatTrien { get; set; }

        // Liên kết bảng Nhân viên:
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
