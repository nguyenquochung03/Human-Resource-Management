using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.HopDongService
{
    public interface IHopDongService
    {
        public Task<object> ThemHopDong(HopDongDTO hopDongDTO);
        public Task<bool> CapNhatHopDong(string id, HopDongDTO hopDongDTO);
        public Task<bool> XoaHopDong(string id);
        public Task<ICollection<object>> GetHopDongs();
        public dynamic GetListByPage(int page, int pageSize);
        public Task<object> GetContractById(string contractId);
        public Task<ICollection<object>> GetHopDongByEmployeeId(string employeeId);
    }
}
