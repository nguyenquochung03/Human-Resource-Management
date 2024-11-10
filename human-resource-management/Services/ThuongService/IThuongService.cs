using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Services.ThuongService
{
    public interface IThuongService
    {
        public Task<object> ThemThuong(ThuongDTO thuongDTO);
        public Task<bool> CapNhatThongTinThuong(string id, ThuongDTO thuongDTO);
        public Task<bool> XoaThuong(string id);
        Task<object> ThemThuongNhanVien(CDKhoanThuongNV addKhoanThuongNV);
        public Task<bool> XoaThuongNhanVien(CDKhoanThuongNV addKhoanThuongNV);
        public Task<ICollection<object>> GetThuongs();
        public dynamic GetListByPage(int page, int pageSize);
        public Task<ICollection<ThuongNhanVien>> GetThuongNhanVien();
        public Task<object> GetThuongById(string id);
        public Task<ICollection<object>> GetThuongByMaNhanVien(string maNhanVien);
    }
}
