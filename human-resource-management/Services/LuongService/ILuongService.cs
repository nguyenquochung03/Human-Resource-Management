using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.LuongService
{
    public interface ILuongService
    {
        public Task<ICollection<object>> GetLuongs();
        public Task<object> GetLuongById(string id);
        public dynamic GetListByPage(DateTime? month, int page = 1, int pageSize = 10, string maNhanVien = "");
        public Task<ICollection<object>> GetLuongByMaNhanVien(string maNhanVien);
    }
}
