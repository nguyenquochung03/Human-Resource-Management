using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class HopDong
    {
        [Required(ErrorMessage = "Mã hợp đồng không được để trống!")]
        public string MaHopDong { get; set; }
        [Required(ErrorMessage = "Loại hợp đồng không được để trống!")]
        public string LoaiHopDong { get; set; }
        [Required(ErrorMessage = "Ngày bắt đầu hợp đồng không được để trống!")]
        public DateTime NgayBatDau { get; set; }
        [Required(ErrorMessage = "Ngày kết thúc hợp đồng không được để trống!")]
        public DateTime NgayKetThuc { get; set; }
        [Required(ErrorMessage = "Thời hạn hợp đồng không được để trống!")]
        public string ThoiHanHopDong { get; set; }
        [Required(ErrorMessage = "Nội dung hợp đồng không được để trống!")]
        public string NoiDungHopDong { get; set; }
        [Required(ErrorMessage = "Trạng thái hợp đồng không được để trống!")]
        public string TrangThaiHopDong { get; set; }
        [Required(ErrorMessage = "Ngày ký hợp đồng không được để trống!")]
        public DateTime NgayKy { get; set; }
        public string GhiChu { get; set; }

        // Liên kết bảng Nhân viên:
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
