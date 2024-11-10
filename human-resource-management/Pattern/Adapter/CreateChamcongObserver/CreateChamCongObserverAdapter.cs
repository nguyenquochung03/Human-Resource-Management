
    using HumanResourceManagement.Pattern.Singleton;

    namespace HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver
    {
        public class CreateChamCongObserverAdapter : ICreateChamCongObserverTarget
        {
            private readonly CreateChamCongObserverAdaptee createChamCongObserverAdaptee;

            public CreateChamCongObserverAdapter(CreateChamCongObserverAdaptee createChamCongObserverAdaptee)
            {
                this.createChamCongObserverAdaptee = createChamCongObserverAdaptee;
            }

            public async Task CreateChamCongObserver(string maNhanVien, int thang, int nam)
            {
                await createChamCongObserverAdaptee.CreateChamCongObserver(maNhanVien, thang, nam);
            }
        }
    }
