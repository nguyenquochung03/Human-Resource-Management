using HumanResourceManagement.DTOs;
using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Observer.ChamCongObserver
{
    public interface IChamCongObserver
    {
        Task<bool> NotifyNewChamCongAdded(string maNhanVien, int thang, int nam);
    }
}
