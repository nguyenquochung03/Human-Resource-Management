using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Services.TrinhDoHocVanService
{
    public interface ITrinhDoHocVanService
    {
        public Task<object> ThemTrinhDoHocVan(TrinhDoHocVan model);
        public Task<bool> SuaTrinhDoHocVan(string id, TrinhDoHocVan model);
        public Task<bool> XoaTrinhDoHocVan(string id);
        public Task<ICollection<object>> GetTrinhDoHocVan();
        public dynamic GetListByPage(int page, int pageSize);
        public Task<object> GetTrinhDoHocVanByEmployeeId(string id);
    }
}
