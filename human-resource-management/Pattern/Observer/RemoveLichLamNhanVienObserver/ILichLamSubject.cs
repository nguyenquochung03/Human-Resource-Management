using System;

namespace HumanResourceManagement.Pattern.Observer.RemoveLichLamNhanVienObserver
{
    public interface ILichLamSubject
    {
        void Attach(ILichLamNhanVienObserver observer);

        void Detach(ILichLamNhanVienObserver observer);

        void Notify();
    }
}
