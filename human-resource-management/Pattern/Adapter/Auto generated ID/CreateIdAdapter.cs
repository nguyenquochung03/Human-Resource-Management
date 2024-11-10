namespace HumanResourceManagement.Pattern.Adapter
{
    public class CreateIdAdapter<T> : IUniqueIdGenerator<T> where T : class
    {
        private readonly CreateId<T> _createId;

        public CreateIdAdapter(CreateId<T> createId)
        {
            _createId = createId;
        }

        public async Task<string> GetUniqueID(string prefix, int length, string primaryKeyColumnName)
        {
            return await _createId.GetUniqueID(prefix, length, primaryKeyColumnName);
        }
    }
}