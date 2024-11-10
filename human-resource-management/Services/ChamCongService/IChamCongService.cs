using HumanResourceManagement.DTOs;

namespace HumanResourceManagement.Services.ChamCongService
{
    public interface IChamCongService
    {
        public Task<object> ThemChamCong(ChamCongDTO chamCongDTO);
        public Task<bool> SuaChamCong(string id, ChamCongDTO chamCongDTO);
        public Task<bool> XoaChamCong(string id);
        public Task<ICollection<object>> GetChamCongs();
        public Task<dynamic> GetListByPage(int page, int pageSize, DateTime? day);
        public Task<ICollection<object>> GetChamCongsByDay(DateTime day);
        public Task<ICollection<object>> GetChamCongsFromTo(DateTime from, DateTime to, string employeeId);
        public Task<ICollection<object>> GetChamCongByMaNhanVien(string maNhanVien);
    }
}
