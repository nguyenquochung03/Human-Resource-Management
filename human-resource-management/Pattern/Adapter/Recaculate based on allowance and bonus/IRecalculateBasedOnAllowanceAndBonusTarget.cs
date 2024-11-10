using HumanResourceManagement.Constants;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Adapter.Recaculate_based_on_allowance_and_bonus
{
    public interface IRecalculateBasedOnAllowanceAndBonusTarget
    {
        Task<bool> RecalculateBasedOnAllowanceAndBonus(string id, string loaiTinhLai);
    }
}
