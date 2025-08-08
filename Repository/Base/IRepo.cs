namespace FamilyDataCollector.Repository.Base
{
    public interface IRepo<T> where T : class
    {
        Task<T> FindByIdAsync(int id);
        void AddAsync (T item);
        void UpdateAsync (T item);

    }
}
