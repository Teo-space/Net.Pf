using Infrastructure.DataBases.Forum.Models;

namespace Infrastructure.DataBases.Forum.Managers.ForkManager;


public interface IForkManager
{
    public IReadOnlyList<ForumFork> GetAll();
    public ForumFork? GetById(Guid ForumForkId);
    public bool Exists(Guid Id) => GetById(Id) != default;


    public ForumFork Create(string Name, string Description);
    public void Edit(Guid ForumForkId, string Name, string Description);
    public void Delete(Guid ForumForkId);
    public IReadOnlyList<ForumTopic> GetTopics(Guid ForumForkId);


}

