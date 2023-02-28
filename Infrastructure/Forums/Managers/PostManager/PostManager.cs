using Extensions.Configuration;
using Infrastructure.Forums.DbContexts;
using Infrastructure.Forums.Managers.TopicManager;
using Infrastructure.Forums.Models;

namespace Infrastructure.Forums.Managers.PostManager;


public interface IPostManager
{
    Task<IReadOnlyList<Post>> GetPosts(Guid TopicId);
    Task<Post?> GetById(Guid PostId);
    Task<bool> Exists(Guid PostId);


    Task Create(Guid TopicId, string Text);
    Task EditText(Guid PostId, string Text);
    Task Delete(Guid PostId);
}



internal record PostManager(ForumDbContext Context,
    IUserAccessor userAccessor,
    ITopicManager TopicManager)

    : IPostManager
{

    public async Task<IReadOnlyList<Post>> GetPosts(Guid TopicId)
    {
        var Topic = await TopicManager.GetById(TopicId);
        if (Topic == default)
        {
            return new List<Post>();
        }

        return await Context.Posts.Where(p => p.TopicId == TopicId).ToListAsync();
    }


    public async Task<Post?> GetById(Guid PostId) => await Context.Posts.FindAsync(PostId);


    public async Task<bool> Exists(Guid PostId) => (await GetById(PostId)) != default;


    public async Task Create(Guid TopicId, string Text)
    {
        var topic = await TopicManager.GetById(TopicId) ?? throw new InvalidOperationException($"Forum Topic {TopicId} not exists");

        Guid UserId = userAccessor.UserId.NullCheck(nameof(userAccessor.UserId));
        string UserName = userAccessor.UserName.NullCheck(nameof(userAccessor.UserName));

        //Создать пост
        {
            var post = new Post();
            post.Text = Text;
            post.addedAt = DateTime.Now;
            post.editedAt = default;
            //Creator
            post.CreatorUserId = UserId;
            post.CreatorUserName = UserName;
            //Parent
            post.TopicId = TopicId;

            Context.Posts.Add(post);
        }
        //пометить топик
        {
            topic.LastReplied = DateTime.Now;
            topic.LastReplied_UserId = UserId;
            topic.LastReplied_UserName = UserName;
            topic.CountReplies += 1;
        }

        await Context.SaveChangesAsync();
    }


    public async Task EditText(Guid PostId, string Text)
    {
        var post = await GetById(PostId) ?? throw new InvalidOperationException($"Forum Post {PostId} not exists");
        post.Text = Text;
        Context.Attach(post);
        await Context.SaveChangesAsync();
    }


    public async Task Delete(Guid PostId)
    {
        var post = await GetById(PostId) ?? throw new InvalidOperationException($"Forum Post {PostId} not exists");

        Context.Posts.Remove(post);
        await Context.SaveChangesAsync();
    }


}


