using HumanResourceManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Pattern.Builder.NghiVangBuild
{
    public interface INghiVangBuilder
    {
        public INghiVangBuilder MaNghiVang(string maNghiVang);
        public INghiVangBuilder LoaiNghiVang(string loaiNghiVang);
        public INghiVangBuilder NgayBatDau(DateTime ngayBatDau);
        public INghiVangBuilder NgayKetThuc(DateTime ngayKetThuc);
        public INghiVangBuilder SoNgayNghi(int soNgayNghi);
        public INghiVangBuilder LyDo(string liDo);
        public INghiVangBuilder TrangThai(string trangThai);
        public INghiVangBuilder GhiChu(string ghiChu);
        public INghiVangBuilder NhanVienId(string nhanVienId);
        public INghiVangBuilder NhanVien(NhanVien nhanVien);
        public NghiVang Build();
    }
}


