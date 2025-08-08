using FamilyDataCollector.Data.Models;

namespace FamilyDataCollector.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
         IRepo<Father> RepoFather { get; set; }
         IRepo<Mother> RepoMother { get; set; }
         IRepo<Child> RepoChild { get; set; }
         IRepo<Family> RepoFamily { get; set; }
        int CommitChange();
    }
}
