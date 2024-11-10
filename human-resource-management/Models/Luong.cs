using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class Luong
    {
        [Required(ErrorMessage = "Mã lương không được để trống!")]
        public string MaLuong { get; set; }
        [Required(ErrorMessage = "Lương cơ bản không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương cơ bản phải là số lớn hơn 0!")]
        public double LuongCoBan { get; set; }
        [Required(ErrorMessage = "Tổng tiền phụ cấp không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phụ cấp phải là số lớn hơn 0!")]
        public double TongTienPhuCap { get; set; }
        [Required(ErrorMessage = "Tổng tiền thưởng không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền thưởng phải là số lớn hơn 0!")]
        public double TongTienThuong { get; set; }
        [Required(ErrorMessage = "Tổng tiền không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải là số lớn hơn 0!")]
        public double TongTien { get; set; }
        [Required(ErrorMessage = "Các khoản khấu trừ không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Các khoản khấu trừ phải là số lớn hơn 0!")]
        public double CacKhoanKhauTru { get; set; }
        [Required(ErrorMessage = "Thu nhập ròng không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Thu nhập ròng phải là số lớn hơn 0!")]
        public double ThuNhapRong { get; set; }
        [Required(ErrorMessage = "Tháng tính lương không được để trống!")]
        [Range(0, 13, ErrorMessage = "Tháng tính lương phải là số từ 1 đến 12!")]
        public int ThangTinhLuong { get; set; }
        [Required(ErrorMessage = "Năm tính lương không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Năm tính lương phải là số lớn hơn hoặc bằng  0!")]
        public int NamTinhLuong { get; set; }

        // Liên kết bảng Nhân viên:
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
