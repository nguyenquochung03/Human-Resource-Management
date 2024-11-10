using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class TrinhDoHocVan
    {
        [Required(ErrorMessage = "Mã trình độ học vấn không được để trống!")]
        public string MaTrinhDo { get; set; }
        [Required(ErrorMessage = "Tên trình độ học vấn không được để trống!")]
        public string TenTrinhDoHocVan { get; set; }
        [Required(ErrorMessage = "Chuyên ngành không được để trống!")]
        public string ChuyenNganh { get; set; }
        [Required(ErrorMessage = "Tên trường không được để trống!")]
        public string TenTruong { get; set; }
        [Required(ErrorMessage = "Năm tốt nghiệp không được để trống!")]
        public DateTime NameTotNghiep { get; set; }
        [Required(ErrorMessage = "Bằng cấp không được để trống!")]
        public string BangCap { get; set; }

        // Liên kết bảng Nhân viên:
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
