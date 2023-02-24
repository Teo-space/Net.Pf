using Extensions.Configuration;
using Infrastructure.DataBases.Forum.DbContexts;
using Infrastructure.DataBases.Forum.Models;

namespace Infrastructure.DataBases.Forum.Managers.TopicManager;


internal interface ITopicManager
{
    public IReadOnlyList<ForumTopic> GetTopics(Guid ForumForkId);
    public ForumTopic? GetById(Guid Id);
    public bool Exists(Guid Id) => GetById(Id) != default;


    public ForumTopic Create(Guid ForumForkId,
        string Name,
        string ShortDescription,
        string Description);


    public void EditName(Guid TopicId, string Name);
    public void EditDescription(Guid TopicId, string Description);
    public void Delete(Guid Id);

}

