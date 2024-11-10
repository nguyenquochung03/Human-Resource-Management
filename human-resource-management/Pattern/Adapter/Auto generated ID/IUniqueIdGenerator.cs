namespace HumanResourceManagement.Pattern.Adapter
{
    public interface IUniqueIdGenerator<T> where T : class
    {
        Task<string> GetUniqueID(string prefix, int length, string primaryKeyColumnName);
    }
}