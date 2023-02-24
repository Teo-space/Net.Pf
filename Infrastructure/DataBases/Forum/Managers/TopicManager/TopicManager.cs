using Extensions.Configuration;
using Infrastructure.DataBases.Forum.DbContexts;
using Infrastructure.DataBases.Forum.Managers.ForkManager;
using Infrastructure.DataBases.Forum.Models;
using System;


namespace Infrastructure.DataBases.Forum.Managers.TopicManager;



internal record TopicManager(ForumDbContext Context,
    IUserAccessor userAccessor,
    IForkManager ForkManager)

    : ITopicManager
{

    public IReadOnlyList<ForumTopic> GetTopics(Guid ForumForkId)
        => Context.Topics.Where(topic => topic.ForumForkId == ForumForkId).ToList();


    public ForumTopic? GetById(Guid Id) => Context.Topics.Find(Id);


    public bool Exists(Guid Id) => GetById(Id) != default;


    public ForumTopic Create(Guid ForumForkId,
        string Name,
        string ShortDescription,
        string Description)
    {
        if (!ForkManager.Exists(ForumForkId))
        {
            throw new InvalidOperationException($"ForumFork {ForumForkId} not exists");
        }

        ForumTopic forumTopic = new();
        forumTopic.ForumForkId = ForumForkId;

        forumTopic.Name = Name;
        forumTopic.ShortDescription = ShortDescription;
        forumTopic.Description = Description;

        forumTopic.userId = userAccessor.UserId.NullCheck(nameof(userAccessor.UserId));
        forumTopic.userName = userAccessor.UserName.NullCheck(userAccessor.UserName);


        forumTopic.LastReplied = DateTime.Now;
        forumTopic.LastReplied_UserId = userAccessor.UserId.NullCheck(nameof(userAccessor.UserId));
        forumTopic.LastReplied_UserName = userAccessor.UserName.NullCheck(userAccessor.UserName);
        forumTopic.CountReplies += 1;


        Context.Add(forumTopic);
        Context.SaveChanges();
        return forumTopic;
    }


    public void EditName(Guid TopicId, string Name)
    {
        var forumTopic = Context.Topics.Find(TopicId)
            ?? throw new InvalidOperationException($"forumTopic {TopicId} not found");

        forumTopic.Name = Name;
        Context.Attach(forumTopic);
        Context.SaveChanges();
    }


    public void EditDescription(Guid TopicId, string Description)
    {
        var forumTopic = Context.Topics.Find(TopicId)
            ?? throw new InvalidOperationException($"forumTopic {TopicId} not found");

        forumTopic.Description = Description;
        Context.Attach(forumTopic);
        Context.SaveChanges();
    }


    public void Delete(Guid TopicId)
    {
        var Topic = Context.Topics.Find(TopicId) 
            ?? throw new InvalidOperationException($"Forum Topic {TopicId} not exists");

        Context.Topics.Remove(Topic);
        Context.SaveChanges();
    }

}

