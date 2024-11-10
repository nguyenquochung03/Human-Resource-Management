using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Visitor;

namespace HumanResourceManagement.Pattern.Adapter.Recaculate_based_on_allowance_and_bonus
{
    public class RecalculateBasedOnAllowanceAndBonusAdapter : IRecalculateBasedOnAllowanceAndBonusTarget
    {
        private readonly RecalculateBasedOnAllowanceAndBonusAdaptee recalculateBasedOnAllowanceAndBonusAdaptee;

        public RecalculateBasedOnAllowanceAndBonusAdapter(RecalculateBasedOnAllowanceAndBonusAdaptee recalculateBasedOnAllowanceAndBonusAdaptee)
        {
            this.recalculateBasedOnAllowanceAndBonusAdaptee = recalculateBasedOnAllowanceAndBonusAdaptee;
        }

        public async Task<bool> RecalculateBasedOnAllowanceAndBonus(string id, string loaiTinhLai)
        {
            return await recalculateBasedOnAllowanceAndBonusAdaptee.RecalculateBasedOnAllowanceAndBonus(id, loaiTinhLai);
        }
    }
}
