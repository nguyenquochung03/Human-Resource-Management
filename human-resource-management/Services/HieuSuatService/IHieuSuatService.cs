using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.HieuSuatService
{
    public interface IHieuSuatService
    {
        public Task<object> ThemHieuSuat(HieuSuatDTO hieuSuatDTO);
        public Task<bool> CapNhatHieuSuat(string id, HieuSuatDTO hieuSuatDTO);
        public Task<bool> XoaHieuSuat(string id);
        public Task<ICollection<object>> GetHieuSuats();
        public dynamic GetListByPage(int page = 1, int pageSize = 10);
        public Task<ICollection<object>> GetHieuSuatByMaNhanVien(string maNhanVien);
    }
}
