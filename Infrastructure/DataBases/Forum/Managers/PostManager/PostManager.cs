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

    public IReadOnlyList<ForumPost> GetPosts(Guid TopicId)
    {
        var Topic = TopicManager.GetById(TopicId);
        if (Topic == default)
        {
            return new List<ForumPost>();
        }

        return Context.Posts.Where(p => p.ForumTopicId == TopicId).ToList();
    }


    public ForumPost? GetById(Guid PostId) => Context.Posts.Find(PostId);


    public bool Exists(Guid PostId) => GetById(PostId) != default;


    public void Create(Guid TopicId, string Text)
    {
        var forumTopic = TopicManager.GetById(TopicId) ?? throw new InvalidOperationException($"Forum Topic {TopicId} not exists");

		Guid UserId = userAccessor.UserId.NullCheck(nameof(userAccessor.UserId));
		string UserName = userAccessor.UserName.NullCheck(nameof(userAccessor.UserName));

		//Создать пост
		{
			var post = new ForumPost();
			post.Text = Text;
			post.addedAt = DateTime.Now;
			post.editedAt = default;
			//Creator
			post.CreatorUserId = UserId;
			post.CreatorUserName = UserName;
			//Parent
			post.ForumTopicId = TopicId;

			Context.Posts.Add(post);
		}
		//пометить топик
		{
			forumTopic.LastReplied = DateTime.Now;
			forumTopic.LastReplied_UserId = UserId;
			forumTopic.LastReplied_UserName = UserName;
			forumTopic.CountReplies += 1;
		}

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


