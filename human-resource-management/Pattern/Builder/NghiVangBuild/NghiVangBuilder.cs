using HumanResourceManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Pattern.Builder.NghiVangBuild
{
    public class NghiVangBuilder : INghiVangBuilder
    {
        private readonly NghiVang nghiVang;

        public NghiVangBuilder()
        {
            nghiVang = new NghiVang();
        }

        public NghiVang Build()
        {
            return nghiVang;
        }

        public INghiVangBuilder GhiChu(string ghiChu)
        {
            nghiVang.GhiChu = ghiChu;
            return this;
        }

        public INghiVangBuilder LoaiNghiVang(string loaiNghiVang)
        {
            nghiVang.LoaiNghiVang = loaiNghiVang;
            return this;
        }

        public INghiVangBuilder LyDo(string liDo)
        {
            nghiVang.LyDo = liDo;
            return this;
        }

        public INghiVangBuilder MaNghiVang(string maNghiVang)
        {
            nghiVang.MaNghiVang = maNghiVang;
            return this;
        }

        public INghiVangBuilder NgayBatDau(DateTime ngayBatDau)
        {
            nghiVang.NgayBatDau = ngayBatDau;
            return this;
        }

        public INghiVangBuilder NgayKetThuc(DateTime ngayKetThuc)
        {
            nghiVang.NgayKetThuc = ngayKetThuc;
            return this;
        }

        public INghiVangBuilder NhanVien(NhanVien nhanVien)
        {
            nghiVang.NhanVien = nhanVien;
            return this;
        }

        public INghiVangBuilder NhanVienId(string nhanVienId)
        {
            nghiVang.NhanVienId = nhanVienId;
            return this;
        }

        public INghiVangBuilder SoNgayNghi(int soNgayNghi)
        {
            nghiVang.SoNgayNghi = soNgayNghi;
            return this;
        }

        public INghiVangBuilder TrangThai(string trangThai)
        {
            nghiVang.TrangThai = trangThai;
            return this;
        }
    }
}
