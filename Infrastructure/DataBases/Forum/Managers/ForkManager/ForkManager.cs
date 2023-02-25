using Infrastructure.DataBases.Forum.DbContexts;
using Infrastructure.DataBases.Forum.Models;


namespace Infrastructure.DataBases.Forum.Managers.ForkManager;


internal record ForkManager(ForumDbContext Context) : IForkManager
{
    public IReadOnlyList<ForumFork> GetAll() => Context.Forks.ToList();

    public ForumFork? GetById(Guid ForkId) => Context.Forks.Find(ForkId);

    public bool Exists(Guid ForkId) => GetById(ForkId) != default;


    public ForumFork Create(string Name, string Description)
    {
        ForumFork fork = new();
        fork.Name = Name;
        fork.Description = Description;

        Context.Add(fork);
        Context.SaveChanges();
        return fork;
    }


    public void Edit(Guid ForkId, string Name, string Description)
    {
        ForumFork fork = GetById(ForkId) ?? throw new InvalidOperationException($"Forum Fork {ForkId} not exists");

        fork.Name = Name;
        fork.Description = Description;

        Context.SaveChanges();
    }


    public void Delete(Guid ForkId)
    {
        ForumFork fork = GetById(ForkId) ?? throw new InvalidOperationException($"Forum Fork {ForkId} not exists");
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
