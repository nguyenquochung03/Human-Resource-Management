using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.ChucVuService
{
    public interface IChucVuService
    {
        public Task<object> ThemChucVu(ChucVuDTO chucVuDTO);
        public Task<bool> SuaChucVu(string id, ChucVuDTO chucVuDTO);
        public Task<bool> XoaChucVu(string id);
        public Task<ICollection<object>> GetChucVus();
        public Task<object> GetChucVuById(string id);
        public dynamic GetListByPage(int page, int pageSize, string phongBan, string searchValue);
        public Task<ICollection<object>> GetChucVuByPhongBanId(string id);
    }
}
