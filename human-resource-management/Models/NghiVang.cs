using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class NghiVang
    {
        [Required(ErrorMessage = "Mã nghỉ vắng không được để trống!")]
        public string MaNghiVang { get; set; }
        [Required(ErrorMessage = "Loại nghỉ vắng không được để trống!")]
        public string LoaiNghiVang { get; set; }
        [Required(ErrorMessage = "Ngày bắt đầu nghỉ vắng không được để trống!")]
        public DateTime NgayBatDau { get; set; }
        [Required(ErrorMessage = "Ngày kết thúc nghỉ vắng không được để trống!")]
        public DateTime NgayKetThuc { get; set; }
        [Required(ErrorMessage = "Số ngày nghỉ vắng không được để trống!")]
        [Range(0, int.MaxValue, ErrorMessage = "Số ngày nghỉ phải là số lớn hơn 0!")]
        public int SoNgayNghi { get; set; }
        [Required(ErrorMessage = "Lý do nghỉ vắng không được để trống!")]
        public string LyDo { get; set; }
        [Required(ErrorMessage = "Trạng thái nghỉ vắng không được để trống!")]
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }

        // Liên kết bảng Nhân viên:
        public string NhanVienId { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
