using FamilyDataCollector.Data;
using FamilyDataCollector.Data.Models;
using FamilyDataCollector.Repository.Base;

namespace FamilyDataCollector.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IRepo<Father> RepoFather { get; set; }
        public IRepo<Mother> RepoMother { get; set; }
        public IRepo<Child> RepoChild { get; set; }
        public IRepo<Family> RepoFamily { get; set; }
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            RepoFather = new MainRepo<Father>(_db);
            RepoMother = new MainRepo<Mother>(_db);
            RepoChild = new MainRepo<Child>(_db);
            RepoFamily = new MainRepo<Family>(_db);

        }


        public int CommitChange()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
