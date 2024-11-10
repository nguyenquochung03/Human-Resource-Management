using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.NhanVienService
{
    public interface INhanVienService
    {
        public Task<object> ThemNhanVien(NhanVienDTO nhanVienDto);
        public Task<bool> CapNhatNhanVien(string id, NhanVienDTO nhanVienDto);
        public Task<bool> XoaNhanVien(string id);
        public Task<ICollection<object>> GetNhanViens();
        public Task<object> GetNhanVienById(string id);
        public Task<ICollection<object>> GetNhanVienByName(string name);
        public Task<object> GetListByPage(int page = 1, int pageSize = 10, string queryType = "", string queryValue = "");
        public Task<object> GetNhanVienByPhone(string phone);
    }
}
