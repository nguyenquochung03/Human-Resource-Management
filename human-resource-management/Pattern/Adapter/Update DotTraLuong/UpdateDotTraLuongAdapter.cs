using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Singleton;

namespace HumanResourceManagement.Pattern.Adapter.Update_DotTraLuong
{
    public class UpdateDotTraLuongAdapter : IUpdateDotTraLuongTarget
    {
        private readonly UpdateDotTraLuongAdaptee updateDotTraLuongAdaptee;

        public UpdateDotTraLuongAdapter(UpdateDotTraLuongAdaptee updateDotTraLuongAdaptee)
        {
            this.updateDotTraLuongAdaptee = updateDotTraLuongAdaptee;
        }

        public async Task<bool> UpdateDotTraLuong(DotTraLuong checkDotTraLuong)
        {
            return await updateDotTraLuongAdaptee.UpdateDotTraLuong(checkDotTraLuong);
        }
    }
}
