using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.DTOs
{
    public class DotTraLuongDTO
    {

        public string MaDotTraLuong { get; set; }
        [Required(ErrorMessage = "Tên đợt trả lương không được để trống!")]
        [MaxLength(100)]
        public string TenDotTraLuong { get; set; }

        [Required(ErrorMessage = "Thời gian trả lương không được để trống!")]
        public DateTime ThoiGian { get; set; }
        public int TongNhanVienCanPhaiTra { get; set; }
        public int TongNhanVienDaTra { get; set; }
        public int TongNhanVienConPhaiTra { get; set; }
        public double TongTienCanPhaiTra { get; set; }
        public int TongTienDaTra { get; set; }
        public int TongTienConPhaiTra { get; set; }
        [Required(ErrorMessage = "Người trả lương không được để trống!")]
        [MaxLength(150)]
        public string NguoiTraLuong { get; set; }
        [MaxLength(1000)]
        public string GhiChu { get; set; }
    }
}
