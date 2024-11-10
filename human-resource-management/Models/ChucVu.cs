using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class ChucVu
    {
        [Required(ErrorMessage = "Mã chức vụ không được để trống!")]
        public string MaChucVu { get; set; }
        [Required(ErrorMessage = "Tên chức vụ không được để trống!")]
        public string TenChucVu { get; set; }
        [Required(ErrorMessage = "Mức lương chức vụ không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Mức lương phải là số lớn hơn 0!")]
        public double MucLuong { get; set; }
        public string MoTaCongViec { get; set; }

        // Liên kết bảng Phòng ban:
        public string PhongBanId { get; set; }
        public PhongBan PhongBan { get; set; }

        // Liên kết bảng Nhân viên:
        public ICollection<NhanVien> NhanViens { get; set; }
    }
}
