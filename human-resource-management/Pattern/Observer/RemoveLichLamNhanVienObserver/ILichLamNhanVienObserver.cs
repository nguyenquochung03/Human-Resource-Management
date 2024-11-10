using HumanResourceManagement.Models;
using System.Runtime.InteropServices.JavaScript;

namespace HumanResourceManagement.Pattern.Observer.RemoveLichLamNhanVienObserver
{
    public interface ILichLamNhanVienObserver
    {
        void Update(LichLam lichLam);
    }
}
