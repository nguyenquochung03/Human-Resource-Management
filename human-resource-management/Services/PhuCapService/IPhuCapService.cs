using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Services.PhuCapService.PhuCapService
{
    public interface IPhuCapService
    {
        public Task<object> ThemPhuCap(PhuCapDTO phuCap);
        public Task<bool> SuaPhuCap(string id, PhuCapDTO phuCap);
        public Task<bool> XoaPhuCap(string id);
        public Task<ICollection<object>> GetPhuCaps();
        public dynamic GetListByPage(int page = 1, int pageSize = 10);
        public Task<ICollection<PhuCapNhanVien>> GetPhuCapNhanViens();
        public Task<object> GetPhuCapById(string id);
        public Task<ICollection<PhuCap>> GetPhuCapNhanVienByEmployeeId(string employeeId);
        public Task<object> AddPhuCapForNvs(CDPhuCapForNhanVienDTO dto);
        public Task<bool> XoaPhuCapForNvs(CDPhuCapForNhanVienDTO dto);
    }
}
