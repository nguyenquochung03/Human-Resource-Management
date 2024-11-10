﻿using HumanResourceManagement.Pattern.Visitor;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Models
{
    public class NhanVien : INhanVienVisitable
    {
        [Required(ErrorMessage = "Mã nhân viên không được để trống!")]
        public string MaNhanVien { get; set; }
        [Required(ErrorMessage = "Họ tên nhân viên không được để trống!")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Ngày sinh nhân viên không được để trống!")]
        public DateTime NgaySinh { get; set; }
        [Required(ErrorMessage = "Giới tính nhân viên không được để trống!")]
        public string GioiTinh { get; set; }
        [Required(ErrorMessage = "Số người phụ thuộc không được để trống!")]
        public int SoNguoiPhuThuoc { get; set; }
        [Required(ErrorMessage = "Trạng thái hôn nhân không được để trống!")]
        public string TrangThaiHonNhan { get; set; }
        [Required(ErrorMessage = "Căn cước của nhân viên không được để trống!")]
        public string Cccd { get; set; }
        public string TonGiao { get; set; }
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Số điện thoại nhân viên không được để trống!")]
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Ngày nhân viên vào làm không được để trống!")]
        public DateTime NgayVaoLam { get; set; }
        [Required(ErrorMessage = "Mức lương cơ bản của nhân viên không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Lương cơ bản của nhân viên phải là số lớn hơn 0!")]
        public double MucLuong { get; set; }
        [Required(ErrorMessage = "Trạng thái làm của nhân viên không được để trống!")]
        public string TrangThai { get; set; }

        // Liên kết bảng Phòng ban:
        public string PhongBanId { get; set; }
        public PhongBan PhongBan { get; set; }

        // Liên kết bảng Chức vụ:
        public string ChucVuId { get; set; }
        public ChucVu ChucVu { get; set; }

        // Liên kết bảng Trình độ học vấn:
        public ICollection<TrinhDoHocVan> TrinhDoHocVans { get; set; }

        // Liên kết bảng Lương:
        public ICollection<Luong> Luongs { get; set; }

        // Liên kết bảng trả lương:
        public ICollection<TraLuong> TraLuongs { get; set; }

        // Liên kết bảng Phụ cấp:
        public ICollection<PhuCapNhanVien> PhuCapNhanViens { get; set; }

        // Liên kết bảng Thưởng:
        public ICollection<ThuongNhanVien> ThuongNhanViens { get; set; }

        // Liên kết bảng Thuế:
        public ICollection<Thue> Thues { get; set; }

        // Liên kết bảng Hợp đồng:
        public ICollection<HopDong> HopDongs { get; set; }

        // Liên kết bảng Hiệu suất:
        public ICollection<HieuSuat> HieuSuats { get; set; }

        // Liên kết bảng Lịch làm:
        public ICollection<LichLamNhanVien> LichLamNhanViens { get; set; }

        // Liên kết bảng Chấm công:
        public ICollection<ChamCong> ChamCongs { get; set; }

        // Liên kết bảng Nghỉ vắng:
        public ICollection<NghiVang> NghiVangs { get; set; }

        public async Task Accept(INhanVienVisitor visitor)
        {
            await visitor.Visit(this);
        }
    }
}
