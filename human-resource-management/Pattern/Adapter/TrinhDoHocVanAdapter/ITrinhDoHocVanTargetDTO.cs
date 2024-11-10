using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Adapter.TrinhDoHocVanAdapter
{
    public interface ITrinhDoHocVanTargetDTO
    {
        public Task<object> ThemTrinhDoHocVan(TrinhDoHocVanDTO trinhDoHocVanDTO);
        public Task<bool> SuaTrinhDoHocVan(string id, TrinhDoHocVanDTO trinhDoHocVanDTO);
    }
}
