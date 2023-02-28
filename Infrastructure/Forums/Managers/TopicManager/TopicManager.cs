using Extensions.Configuration;
using Infrastructure.DataBases.Forum.Managers.ForkManager;
using Infrastructure.DataBases.Forum.Models;
using Infrastructure.Forums.DbContexts;
using Infrastructure.Forums.Managers.ForumManager;
using Infrastructure.Forums.Models;


namespace Infrastructure.Forums.Managers.TopicManager;


public interface ITopicManager
{
    Task<IReadOnlyList<Topic>> GetTopics(Guid ForumId);
    Task<Topic?> GetById(Guid TopicId);
    Task<bool> Exists(Guid TopicId);


    Task<Topic> Create(Guid ForumForkId,
        string Name,
        string ShortDescription,
        string Description);

    Task<Topic> Edit(Guid TopicId,
        string Name,
        string ShortDescription,
        string Description);

    public Task Delete(Guid TopicId);

}



internal record TopicManager(ForumDbContext Context,
    IUserAccessor userAccessor,
    IForumManager forumManager)

    : ITopicManager
{

    public async Task<IReadOnlyList<Topic>> GetTopics(Guid ForumId)
        => await Context.Topics.Where(topic => topic.ForumId == ForumId).ToListAsync();


    public async Task<Topic?>  GetById(Guid TopicId) => await Context.Topics.FindAsync(TopicId);


    public async Task<bool> Exists(Guid TopicId) => (await GetById(TopicId)) != default;


    public async Task<Topic> Create(Guid ForumId,
        string Name,
        string ShortDescription,
        string Description)
    {
        if (!await forumManager.Exists(ForumId))
        {
            throw new InvalidOperationException($"Forum {ForumId} not exists");
        }


        Topic topic = new();
        topic.ForumId = ForumId;

        topic.Name = Name;
        topic.ShortDescription = ShortDescription;
        topic.Description = Description;

        topic.userId = userAccessor.UserId.NullCheck(nameof(userAccessor.UserId));
        topic.userName = userAccessor.UserName.NullCheck(userAccessor.UserName);


        topic.LastReplied = DateTime.Now;
        topic.LastReplied_UserId = userAccessor.UserId.NullCheck(nameof(userAccessor.UserId));
        topic.LastReplied_UserName = userAccessor.UserName.NullCheck(userAccessor.UserName);
        topic.CountReplies += 1;


        await Context.AddAsync(topic);
        await Context.SaveChangesAsync();
        return topic;
    }



    public async Task<Topic> Edit(Guid TopicId,
        string Name,
        string ShortDescription,
        string Description)
    {
        var topic = await GetById(TopicId) ?? throw new InvalidOperationException($"Topic {TopicId} not found");

        topic.Name = Name;
        topic.ShortDescription = ShortDescription;
        topic.Description = Description;

        await Context.SaveChangesAsync();

        return topic;
    }


    public async Task Delete(Guid TopicId)
    {
        var Topic = await GetById(TopicId) ?? throw new InvalidOperationException($"Forum Topic {TopicId} not exists");

        Context.Topics.Remove(Topic);
        await Context.SaveChangesAsync();
    }


}


