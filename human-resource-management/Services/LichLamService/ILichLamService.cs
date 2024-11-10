using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Services.LichLamService
{
    public interface ILichLamService
    {
        public Task<object> ThemLichLam(LichLamDTO lichLamDTO);
        public Task<bool> CapNhatLichLam(string id, LichLamDTO lichLamDTO);
        public Task<bool> XoaLichLam(string id);
        public Task<ICollection<object>> GetLichLams();
        public Task<ICollection<LichLamNhanVien>> GetLichLamNhanViens();
        public dynamic GetListByPage(int page, int pageSize);
        public Task<object> GetLichLamById(string id);
        public Task<ICollection<object>> GetLichLamByMaNhanVien(string maNhanVien);
        public Task<ICollection<object>> GetLichLamsFromTo(DateTime from, DateTime to);
        public Task<bool> ThemLichLamNhanVien(CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO);
        public Task<bool> XoaLichLamNhanVien(CDLichLamForNhanVienDTO cDLichLamForNhanVienDTO);
    }
}
