using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Services.PhongBanService
{
    public interface IPhongBanService
    {
        public Task<PhongBan> ThemPhongBan(PhongBanDTO phongBanDto);
        public Task<bool> SuaPhongBan(string id, PhongBanDTO phongBanDto);
        public Task<bool> XoaPhongBan(string id);
        public Task<ICollection<PhongBan>> GetPhongBans();
        public dynamic GetListByPage(int page = 1, int pageSize = 10);
        public Task<PhongBan> GetPhongBanById(string id);
    }
}
