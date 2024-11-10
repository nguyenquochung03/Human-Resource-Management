using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class PhuCap
    {
        [Required(ErrorMessage = "Mã phụ cấp không được để trống!")]
        public string MaPhuCap { get; set; }
        [Required(ErrorMessage = "Tên phụ cấp không được để trống!")]
        public string TenPhuCap { get; set; }
        [Required(ErrorMessage = "Số tiền phụ cấp không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền phụ cấp phải là số lớn hơn 0!")]
        public double SoTienPhuCap { get; set; }
        [Required(ErrorMessage = "Tần suất phụ cấp không được để trống!")]
        public string TanSuat { get; set; }
        [Required(ErrorMessage = "Trạng thái phụ cấp không được để trống!")]
        public string TrangThai { get; set; }

        // Liên kết bảng Nhân viên:
        public ICollection<PhuCapNhanVien> PhuCapNhanViens { get; set; }
    }
}
