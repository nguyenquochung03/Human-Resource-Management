namespace HumanResourceManagement.Services.ThueService
{
    public interface IThueService
    {

        public Task<ICollection<object>> GetThues();
        public Task<object> GetThueById(string id);
        public Task<ICollection<object>> GetThueByMaNhanVien(string maNhanVien);
        public dynamic GetListByPage(int page, int pageSize, string employeeId, DateTime? filterByTime);
        public Task<object> GetThueByNhanVienVaThoiGian(string maNhanVien, int thang, int nam);
    }
}
