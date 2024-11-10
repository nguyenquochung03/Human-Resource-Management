namespace HumanResourceManagement.Pattern.State
{
    public class UnpaidAbsenceState : AbsenceState
    {
        public bool IsPaid()
        {
            return false;
        }
    }
}
