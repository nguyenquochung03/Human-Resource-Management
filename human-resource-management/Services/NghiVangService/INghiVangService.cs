using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.NghiVangService
{
    public interface INghiVangService
    {
        public Task<object> ThemNghiVang(NghiVangDTO nghiVangDTO);
        public Task<bool> CapNhatNghiVang(string id, NghiVangDTO nghiVangDTO);
        public Task<bool> XoaNghiVang(string id);
        public Task<ICollection<object>> GetNghiVangs();
        public dynamic GetListByPage(int page, int pageSize);
        public Task<ICollection<object>> GetNghiVangByMaNhanVien(string maNhanVien);
        public Task<ICollection<object>> GetNghiVangsFromTo(string maNhanVien, DateTime month);
    }
}
