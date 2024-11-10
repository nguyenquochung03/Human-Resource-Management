using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class DotTraLuong
    {
        [Required(ErrorMessage = "Mã đợt trả lương không được để trống!")]
        public string MaDotTraLuong { get; set; }
        [Required(ErrorMessage = "Tên đợt trả lương không được để trống!")]
        public string TenDotTraLuong { get; set; }
        [Required(ErrorMessage = "Tháng trả lương không được để trống!")]
        [Range(0, 13, ErrorMessage = "Tháng đợt trả lương phải là số từ 1 đến 12!")]
        public int ThangDotTraLuong { get; set; }
        [Required(ErrorMessage = "Năm trả lương không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Tháng đợt trả lương phải là số lớn hơn 0!")]
        public int NamDotTraLuong { get; set; }
        public int TongNhanVienCanPhaiTra { get; set; }
        public int TongNhanVienDaTra { get; set; }
        public int TongNhanVienConPhaiTra { get; set; }
        public double TongTienCanPhaiTra { get; set; }
        public double TongTienDaTra { get; set; }
        public double TongTienConPhaiTra { get; set; }
        [Required(ErrorMessage = "Người trả lương không được để trống!")]
        public string NguoiTraLuong { get; set; }
        public string GhiChu { get; set; }

        // Liên kết bảng trả lương nhân viên:
        public ICollection<TraLuong> TraLuongs { get; set; }
    }
}
