using Net.Pf.DataBases.Forum.Models;

namespace Net.Pf.DataBases.Forum;




//ForumFork
//ForumTopic
//ForumPost
internal record ForkManager(ForumDbContext Context)

    //: IForkManager
{
    public IReadOnlyList<ForumFork> GetAll() => Context.Forks.ToList();

    public ForumFork? GetById(Guid ForumForkId) => Context.Forks.Find(ForumForkId);

    public ForumFork Create(string Name, string Description)
    {
        ForumFork fork = new ();
        fork.Name = Name; ;
        fork.Description = Description;

        Context.Add(fork);
        Context.SaveChanges();
        return fork;
    }


    public void Edit(Guid ForumForkId, string Name, string Description)
    {
        ForumFork fork = GetById(ForumForkId);

        fork.Name = Name; ;
        fork.Description = Description;

        Context.SaveChanges();
    }


    public void Delete(Guid ForumForkId)
    {
        ForumFork fork = GetById(ForumForkId);
        Context.Forks.Remove(fork);
        Context.SaveChanges();
    }




}



public class ForumFork11
{
    public Guid ForumForkId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public List<ForumTopic> forumTopics { get; set; } = new();
}