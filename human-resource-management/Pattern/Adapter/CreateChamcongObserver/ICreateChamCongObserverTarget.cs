namespace HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver
{
    public interface ICreateChamCongObserverTarget
    {
        Task CreateChamCongObserver(string maNhanVien, int thang, int nam);
    }
}
