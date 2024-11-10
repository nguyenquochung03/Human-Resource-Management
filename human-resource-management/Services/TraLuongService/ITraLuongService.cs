using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.TraLuongService
{
    public interface ITraLuongService
    {
        public Task<object> ThemTraLuong(TraLuongDTO traLuongDTO);
        public Task<ICollection<object>> GetTraLuongs();
        public dynamic GetListByPage(int page, int pageSize, string employeeName, DateTime? month);
        public Task<ICollection<object>> GetTraLuongByMaNhanVien(string maNhanVien);
        public Task<object> GetTraLuongById(string dotTraLuongId, string maNhanVien);
    }
}
