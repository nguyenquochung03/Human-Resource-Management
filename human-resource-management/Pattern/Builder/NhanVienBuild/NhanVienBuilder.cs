using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Builder.NhanVienBuilder
{
    public class NhanVienBuilder : INhanVienBuilder
    {
        private readonly NhanVien nhanVien;

        public NhanVienBuilder()
        {
            nhanVien = new NhanVien();
        }

        public NhanVien Build()
        {
            return nhanVien;
        }

        public INhanVienBuilder Cccd(string cccd)
        {
            nhanVien.Cccd = cccd;
            return this;
        }

        public INhanVienBuilder ChucVuId(string chucVuId)
        {
            nhanVien.ChucVuId = chucVuId;
            return this;
        }

        public INhanVienBuilder DiaChi(string diaChi)
        {
            nhanVien.DiaChi = diaChi;
            return this;
        }

        public INhanVienBuilder Email(string email)
        {
            nhanVien.Email = email;
            return this;
        }

        public INhanVienBuilder GioiTinh(string gioiTinh)
        {
            nhanVien.GioiTinh = gioiTinh;
            return this;
        }

        public INhanVienBuilder HoTen(string hoTen)
        {
            nhanVien.HoTen = hoTen;
            return this;
        }

        public INhanVienBuilder MaNhanVien(string maNhanVien)
        {
            nhanVien.MaNhanVien = maNhanVien;
            return this;
        }

        public INhanVienBuilder MucLuong(double mucLuong)
        {
            nhanVien.MucLuong = mucLuong;
            return this;
        }

        public INhanVienBuilder NgaySinh(DateTime ngaySinh)
        {
            nhanVien.NgaySinh = ngaySinh;
            return this;
        }

        public INhanVienBuilder NgayVaoLam(DateTime ngayVaoLam)
        {
            nhanVien.NgayVaoLam = ngayVaoLam;
            return this;
        }

        public INhanVienBuilder PhongBanId(string phongBanId)
        {
            nhanVien.PhongBanId = phongBanId;
            return this;
        }

        public INhanVienBuilder SoDienThoai(string soDienThoai)
        {
            nhanVien.SoDienThoai = soDienThoai;
            return this;
        }

        public INhanVienBuilder SoNguoiPhuThuoc(int soNguoiPhuThuoc)
        {
            nhanVien.SoNguoiPhuThuoc = soNguoiPhuThuoc;
            return this;
        }

        public INhanVienBuilder TonGiao(string tonGiao)
        {
            nhanVien.TonGiao = tonGiao;
            return this;
        }

        public INhanVienBuilder TrangThai(string trangThai)
        {
            nhanVien.TrangThai = trangThai;
            return this;
        }

        public INhanVienBuilder TrangThaiHonNhan(string trangThaiHonNhan)
        {
            nhanVien.TrangThaiHonNhan = trangThaiHonNhan;
            return this;
        }

        public INhanVienBuilder TrinhDoHocVans(ICollection<TrinhDoHocVan> trinhDoHocVans)
        {
            nhanVien.TrinhDoHocVans = trinhDoHocVans;
            return this;
        }

        public INhanVienBuilder Luongs(ICollection<Luong> luongs)
        {
            nhanVien.Luongs = luongs;
            return this;
        }

        public INhanVienBuilder TraLuongs(ICollection<TraLuong> traLuongs)
        {
            nhanVien.TraLuongs = traLuongs;
            return this;
        }

        public INhanVienBuilder PhuCapNhanViens(ICollection<PhuCapNhanVien> phuCapNhanViens)
        {
            nhanVien.PhuCapNhanViens = phuCapNhanViens;
            return this;
        }

        public INhanVienBuilder ThuongNhanViens(ICollection<ThuongNhanVien> thuongNhanViens)
        {
            nhanVien.ThuongNhanViens = thuongNhanViens;
            return this;
        }

        public INhanVienBuilder Thues(ICollection<Thue> thues)
        {
            nhanVien.Thues = thues;
            return this;
        }

        public INhanVienBuilder HopDongs(ICollection<HopDong> hopDongs)
        {
            nhanVien.HopDongs = hopDongs;
            return this;
        }

        public INhanVienBuilder HieuSuats(ICollection<HieuSuat> hieuSuats)
        {
            nhanVien.HieuSuats = hieuSuats;
            return this;
        }

        public INhanVienBuilder LichLamNhanViens(ICollection<LichLamNhanVien> lichLamNhanViens)
        {
            nhanVien.LichLamNhanViens = lichLamNhanViens;
            return this;
        }

        public INhanVienBuilder ChamCongs(ICollection<ChamCong> chamCongs)
        {
            nhanVien.ChamCongs = chamCongs;
            return this;
        }

        public INhanVienBuilder NghiVangs(ICollection<NghiVang> nghiVangs)
        {
            nhanVien.NghiVangs = nghiVangs;
            return this;
        }
    }
}
