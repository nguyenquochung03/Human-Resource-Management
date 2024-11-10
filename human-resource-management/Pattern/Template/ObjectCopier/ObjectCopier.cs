namespace HumanResourceManagement.Pattern.Template.ObjectCopier
{
    public abstract class ObjectCopier<T>
    {
        public ICollection<T> CopyObjects(ICollection<T> objects)
        {
            List<T> copiedObjects = new List<T>();

            foreach (T obj in objects)
            {
                T copy = CopyObject(obj);
                copiedObjects.Add(copy);
            }

            return copiedObjects;
        }

        protected abstract T CopyObject(T obj);
    }
}
