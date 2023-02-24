using Infrastructure.DataBases.Forum.Models;

namespace Infrastructure.DataBases.Forum.Managers.PostManager;

internal interface IPostManager
{
    public IReadOnlyList<ForumPost> GetPosts(Guid Id);

    public ForumPost? GetById(Guid Id);

    public bool Exists(Guid Id) => GetById(Id) != default;


    public void Create(Guid TopicId, string Text);
    public void EditText(Guid PostId, string Text);
    public void Delete(Guid PostId);


}

