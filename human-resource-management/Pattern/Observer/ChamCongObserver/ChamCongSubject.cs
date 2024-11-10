using HumanResourceManagement.DTOs;
using HumanResourceManagement.Exceptions;

namespace HumanResourceManagement.Pattern.Observer.ChamCongObserver
{
    public class ChamCongSubject
    {
        private List<IChamCongObserver> _chamCongObservers = new List<IChamCongObserver>();
        private readonly string maNhanVien;
        private readonly int thang;
        private readonly int nam;

        public ChamCongSubject(string maNhanVien, int thang, int nam)
        {
            this.maNhanVien = maNhanVien;
            this.thang = thang;
            this.nam = nam;
        }

        public void Attach(IChamCongObserver chamCongObserver)
        {
            _chamCongObservers.Add(chamCongObserver);
        }

        public void Detach(IChamCongObserver chamCongObserver)
        {
            _chamCongObservers.Remove(chamCongObserver);
        }

        public async Task ChamCongNotify()
        {
            foreach (IChamCongObserver chamCongObserver in _chamCongObservers)
            {
                await chamCongObserver.NotifyNewChamCongAdded(maNhanVien, thang, nam);
            }
        }
    }
}
