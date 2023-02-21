using Net.Pf.DataBases.Forum.Models;

namespace Net.Pf.DataBases.Forum;


public interface IGenericRepository<TEntity, TId>
{
    public IEnumerable<TEntity> GetAll();

    TEntity GetById(TId id);
    public void Create(TEntity entity);

    public void Delete(TId id);
    public void Delete(TEntity entity);

    public void Update(TEntity entity);

}


public interface IForkManager
{
    public IEnumerable<ForumFork> GetForks();

    public void Create(ForumFork forumFork);
    public void Get(Guid Id);

    public void EditFork(ForumFork forumFork);

    public void RemoveFork(ForumFork forumFork);
}




//ForumFork
//ForumTopic
//ForumPost
internal record ForkManager(ForumDbContext Context)

    : IForkManager
{
    public IEnumerable<ForumFork> GetForks() => Context.Forks.ToList();



}



