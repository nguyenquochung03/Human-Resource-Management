using HumanResourceManagement.Models;

namespace HumanResourceManagement.Pattern.Observer.RemoveLichLamNhanVienObserver
{
    public class LichLamSubject : ILichLamSubject
    {
        private LichLam lichLam;
        private List<ILichLamNhanVienObserver> _observers = new List<ILichLamNhanVienObserver>();

        public LichLamSubject(LichLam lichLam)
        {
            this.lichLam = lichLam;
        }


        public void Attach(ILichLamNhanVienObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Detach(ILichLamNhanVienObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(lichLam);
            }
        }
    }
}
