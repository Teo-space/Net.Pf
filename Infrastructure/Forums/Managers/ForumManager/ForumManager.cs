using Infrastructure.Forums.DbContexts;
using Infrastructure.Forums.Models;

namespace Infrastructure.Forums.Managers.ForumManager;


public interface IForumManager
{
    //public IReadOnlyList<Infrastructure.Forums.Models.Forum> GetAll();
    Task<IReadOnlyList<IGrouping<ForumGroup, Infrastructure.Forums.Models.Forum>>> GetAll();

    Task<Infrastructure.Forums.Models.Forum?> GetById(Guid forumId);

    Task<bool> Exists(Guid forumId);


    Task<Infrastructure.Forums.Models.Forum> Create(ForumGroup group, string Name, string Description);
    Task Edit(Guid forumId, ForumGroup Group, string Name, string Description);
    Task Delete(Guid forumId);

}




internal record ForumManager(ForumDbContext Context) : IForumManager
{

    //public IReadOnlyList<Infrastructure.Forums.Models.Forum> GetAll() 
    public async Task<IReadOnlyList<IGrouping<ForumGroup, Infrastructure.Forums.Models.Forum>>> GetAll() 
        => (await Context.Forums.ToListAsync())
        .OrderBy(x => x.Group).ThenBy(x => x.Name).GroupBy(x => x.Group).ToList()
        ;

    public async Task<Infrastructure.Forums.Models.Forum?> GetById(Guid forumId) 
        => await Context.Forums.FindAsync(forumId);

    public async Task<bool> Exists(Guid forumId) => (await GetById(forumId)) != default;



    public async Task<Infrastructure.Forums.Models.Forum> Create(ForumGroup group, string Name, string Description)
    {
        Infrastructure.Forums.Models.Forum forum = new();

        forum.Group = group;
        forum.Name = Name;
        forum.Description = Description;

        await Context.AddAsync(forum);
        await Context.SaveChangesAsync();
        return forum;
    }


    public async Task Edit(Guid forumId, ForumGroup Group, string Name, string Description)
    {
        var forum = await GetById(forumId) ?? throw new InvalidOperationException($"Forum {forumId} not exists");

        forum.Group = Group;
        forum.Name = Name;
        forum.Description = Description;

        await Context.SaveChangesAsync();
    }


    public async Task Delete(Guid forumId)
    {
        var forum = await GetById(forumId) ?? throw new InvalidOperationException($"Forum {forumId} not exists");

        Context.Forums.Remove(forum);
        await Context.SaveChangesAsync();
    }



}


