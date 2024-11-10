using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Builder.NhanVienBuilder
{
    public interface INhanVienBuilder
    {
        public INhanVienBuilder MaNhanVien(string maNhanVien);
        public INhanVienBuilder HoTen(string hoTen);
        public INhanVienBuilder NgaySinh(DateTime ngaySinh);
        public INhanVienBuilder GioiTinh(string gioiTinh);
        public INhanVienBuilder SoNguoiPhuThuoc(int soNguoiPhuThuoc);
        public INhanVienBuilder TrangThaiHonNhan(string trangThaiHonNhan);
        public INhanVienBuilder Cccd(string cccd);
        public INhanVienBuilder TonGiao(string tonGiao);
        public INhanVienBuilder DiaChi(string diaChi);
        public INhanVienBuilder SoDienThoai(string soDienThoai);
        public INhanVienBuilder Email(string email);
        public INhanVienBuilder NgayVaoLam(DateTime ngayVaoLam);
        public INhanVienBuilder MucLuong(double mucLuong);
        public INhanVienBuilder TrangThai(string trangThai);
        public INhanVienBuilder PhongBanId(string phongBanId);
        public INhanVienBuilder ChucVuId(string chucVuId);
        public INhanVienBuilder TrinhDoHocVans(ICollection<TrinhDoHocVan> trinhDoHocVans);
        public INhanVienBuilder Luongs(ICollection<Luong> luongs);
        public INhanVienBuilder TraLuongs(ICollection<TraLuong> traLuongs);
        public INhanVienBuilder PhuCapNhanViens(ICollection<PhuCapNhanVien> phuCapNhanViens);
        public INhanVienBuilder ThuongNhanViens(ICollection<ThuongNhanVien> thuongNhanViens);
        public INhanVienBuilder Thues(ICollection<Thue> thues);
        public INhanVienBuilder HopDongs(ICollection<HopDong> hopDongs);
        public INhanVienBuilder HieuSuats(ICollection<HieuSuat> hieuSuats);
        public INhanVienBuilder LichLamNhanViens(ICollection<LichLamNhanVien> lichLamNhanViens);
        public INhanVienBuilder ChamCongs(ICollection<ChamCong> chamCongs);
        public INhanVienBuilder NghiVangs(ICollection<NghiVang> nghiVangs);
        public NhanVien Build();
    }
}
