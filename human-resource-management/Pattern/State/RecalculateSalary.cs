using HumanResourceManagement.Pattern.Adapter.Create_ChamcongObserver;

namespace HumanResourceManagement.Pattern.State
{
    public class RecalculateSalary
    {
        private AbsenceState absenceState;
        private readonly ICreateChamCongObserverTarget createChamCongObserverTarget;

        public RecalculateSalary(ICreateChamCongObserverTarget createChamCongObserverTarget)
        {
            absenceState = new UnpaidAbsenceState();
            this.createChamCongObserverTarget = createChamCongObserverTarget;
        }

        public void ChangeState(AbsenceState absenceState)
        {
            this.absenceState = absenceState;
        }

        public async Task CalculateSalary(string maNhanVien, int thang, int nam)
        {
            if (absenceState.IsPaid())
            {
                await createChamCongObserverTarget.CreateChamCongObserver(maNhanVien, thang, nam);
            }
        }
    }
}
