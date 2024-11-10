using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Services.DotTraLuongService
{
    public interface IDotTraLuongService
    {
 
        public Task<bool> XoaDotTraLuong(string id);
        public Task<ICollection<object>> GetDotTraLuongs();
        public dynamic GetListByPage(DateTime? month, int page = 1, int pageSize = 10);
        public Task<object> GetDotTraLuongById(string id);
    }
}
