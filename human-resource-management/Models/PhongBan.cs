using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class PhongBan
    {
        [Required(ErrorMessage = "Số phòng không được để trống!")]
        public string SoPhong { get; set; }
        [Required(ErrorMessage = "Tên phòng không được để trống!")]
        public string TenPhong { get; set; }
        public string NguoiQuanLy { get; set; }
        [Required(ErrorMessage = "Email phòng không được để trống!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Địa điểm phòng không được để trống!")]
        public string DiaDiem { get; set; }
        public string MoTa { get; set; }

        // Liên kết bảng Chức vụ:
        public ICollection<ChucVu> ChucVus { get; set; }

        // Liên kết bảng Nhân viên:
        public ICollection<NhanVien> NhanViens { get; set; }
    }
}
