using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Adapter.Update_DotTraLuong
{
    public interface IUpdateDotTraLuongTarget
    {
        Task<bool> UpdateDotTraLuong(DotTraLuong checkDotTraLuong);
    }
}
