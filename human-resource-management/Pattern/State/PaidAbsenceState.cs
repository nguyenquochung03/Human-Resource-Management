namespace HumanResourceManagement.Pattern.State
{
    public class PaidAbsenceState : AbsenceState
    {
        public bool IsPaid()
        {
            return true;
        }
    }
}
