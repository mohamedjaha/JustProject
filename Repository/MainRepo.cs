using FamilyDataCollector.Data;
using FamilyDataCollector.Repository.Base;
using System.Threading.Tasks;

namespace FamilyDataCollector.Repository
{
    public class MainRepo<T> : IRepo<T> where T : class
    {
        private AppDbContext _db;
        public MainRepo(AppDbContext db)
        {
            _db = db;
        }
        public void AddAsync(T item)
        {
            _db.Set<T>().Add(item);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await (_db.Set<T>().FindAsync(id));
        }

        public void UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        
    }
}
