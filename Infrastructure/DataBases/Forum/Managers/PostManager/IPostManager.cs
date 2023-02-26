using Infrastructure.DataBases.Forum.Models;

namespace Infrastructure.DataBases.Forum.Managers.PostManager;

public interface IPostManager
{
    public IReadOnlyList<ForumPost> GetPosts(Guid TopicId);
    public ForumPost? GetById(Guid PostId);
    public bool Exists(Guid PostId) => GetById(PostId) != default;


    public void Create(Guid TopicId, string Text);
    public void EditText(Guid PostId, string Text);
    public void Delete(Guid PostId);


}


