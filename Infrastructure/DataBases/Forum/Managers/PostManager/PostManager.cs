using Extensions.Configuration;
using Infrastructure.DataBases.Forum.DbContexts;
using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Infrastructure.DataBases.Forum.Models;

namespace Infrastructure.DataBases.Forum.Managers.PostManager;


internal record PostManager(ForumDbContext Context,
    IUserAccessor userAccessor,
    ITopicManager TopicManager)

    : IPostManager
{

    public IReadOnlyList<ForumPost> GetPosts(Guid Id)
    {
        var Topic = GetById(Id);
        if (Topic == default)
        {
            return new List<ForumPost>();
        }

        return Context.Posts.Where(p => p.ForumTopicId == Id).ToList();
    }


    public ForumPost? GetById(Guid Id) => Context.Posts.Find(Id);


    public bool Exists(Guid Id) => GetById(Id) != default;


    public void Create(Guid TopicId, string Text)
    {
        if (!TopicManager.Exists(TopicId))
        {
            throw new InvalidOperationException($"Forum Topic {TopicId} not exists");
        }

        var post = new ForumPost();
        post.Text = Text;
        post.addedAt = DateTime.Now;
        post.editedAt = DateTime.Now;
        //Creator
        post.CreatorUserId = userAccessor.UserId.NullCheck(nameof(userAccessor.UserId));
        post.CreatorUserName = userAccessor.UserName.NullCheck(userAccessor.UserName);
        //Parent
        post.ForumTopicId = TopicId;

        Context.Posts.Add(post);
        Context.SaveChanges();
    }


    public void EditText(Guid PostId, string Text)
    {
        var post = GetById(PostId) ?? throw new InvalidOperationException($"Forum Post {PostId} not exists");
        post.Text = Text;
        Context.Attach(post);
        Context.SaveChanges();
    }


    public void Delete(Guid PostId)
    {
        var post = GetById(PostId) ?? throw new InvalidOperationException($"Forum Post {PostId} not exists");

        Context.Posts.Remove(post);
        Context.SaveChanges();
    }


}


