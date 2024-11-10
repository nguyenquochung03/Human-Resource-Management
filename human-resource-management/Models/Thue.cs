using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class Thue
    {
        [Required(ErrorMessage = "Mã số thuế không được để trống!")]
        public string MaSoThue { get; set; }
        [Required(ErrorMessage = "Bảo hiểm xã hội không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền bảo hiểm xã hội phải là số lớn hơn hoặc bằng 0!")]
        public double BaoHiemXaHoi { get; set; }
        [Required(ErrorMessage = "Bảo hiểm y tế không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền bảo hiểm y tế phải là số lớn hơn hoặc bằng 0!")]
        public double BaoHienYTe { get; set; }
        [Required(ErrorMessage = "Bảo hiểm thất nghiệp không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền bảo hiểm thất nghiệp phải là số lớn hơn hoặc bằng 0!")]
        public double BaoHiemThatNghiep { get; set; }
        [Required(ErrorMessage = "Mức thu nhập chịu thuế không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Mức thu nhập chịu thuế phải là số lớn hơn hoặc bằng 0!")]
        public double MucThuNhapChiuThue { get; set; }
        [Required(ErrorMessage = "Thuế suất không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Thuế suất phải là số lớn hơn hoặc bằng 0!")]
        public double ThueSuat { get; set; }
        [Required(ErrorMessage = "Thuế thu nhập cá nhân thực tế không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Thuế thu nhập cá nhân thực tế phải là số lớn hơn hoặc bằng  0!")]
        public double ThueThuNhapCaNhanThucTe { get; set; }
        [Required(ErrorMessage = "Tháng tính thuê không được để trống!")]
        [Range(0, 13, ErrorMessage = "Tháng tính thuế phải là số từ 1 đến 12!")]
        public int ThangTinhThue { get; set; }
        [Required(ErrorMessage = "Năm tính thuế không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Năm tính thuế phải là số lớn hơn hoặc bằng  0!")]
        public int NamTinhThue { get; set; }

        // Liên kết bảng Nhân viên:
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
