using Infrastructure.DataBases.Forum.DbContexts;
using Infrastructure.DataBases.Forum.Models;


namespace Infrastructure.DataBases.Forum.Managers.ForkManager;


internal record ForkManager(ForumDbContext Context) : IForkManager
{
    public IReadOnlyList<ForumFork> GetAll() => Context.Forks.ToList();

    public ForumFork? GetById(Guid ForumForkId) => Context.Forks.Find(ForumForkId);

    public bool Exists(Guid Id) => GetById(Id) != default;


    public ForumFork Create(string Name, string Description)
    {
        ForumFork fork = new();
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


    public IReadOnlyList<ForumTopic> GetTopics(Guid ForumForkId)
    {
        var fork = GetById(ForumForkId);
        if (fork == default)
        {
            return new List<ForumTopic>();
        }
        return Context.Topics.Where(topic => topic.ForumForkId == ForumForkId).ToList();
    }

}
