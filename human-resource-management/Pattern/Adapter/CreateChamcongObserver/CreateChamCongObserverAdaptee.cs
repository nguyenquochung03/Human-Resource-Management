using HumanResourceManagement.Models;
using HumanResourceManagement.Pattern.Observer.ChamCongObserver;
using HumanResourceManagement.Pattern.State;
using HumanResourceManagement.Exceptions;

namespace HumanResourceManagement.Pattern.Singleton
{
    public class CreateChamCongObserverAdaptee
    {
        private readonly DotTraLuongConcrete dotTraLuongConcrete;
        private readonly LuongConcrete luongConcrete;
        private readonly ThueConcrete thueConcrete;
        private readonly TraLuongConcrete traLuongConcrete;

        public CreateChamCongObserverAdaptee(DotTraLuongConcrete dotTraLuongConcrete,
            LuongConcrete luongConcrete,
            ThueConcrete thueConcrete,
            TraLuongConcrete traLuongConcrete)
        {
            this.dotTraLuongConcrete = dotTraLuongConcrete;
            this.luongConcrete = luongConcrete;
            this.thueConcrete = thueConcrete;
            this.traLuongConcrete = traLuongConcrete;
        }

        public async Task CreateChamCongObserver(string maNhanVien, int thang, int nam)
        {
            ChamCongSubject chamCongSubject = new ChamCongSubject(maNhanVien, thang, nam);

            chamCongSubject.Attach(luongConcrete);
            chamCongSubject.Attach(dotTraLuongConcrete);
            chamCongSubject.Attach(thueConcrete);
            chamCongSubject.Attach(traLuongConcrete);
            await chamCongSubject.ChamCongNotify();
        }
    }
}
