using Infrastructure.Forums;
using Infrastructure.Forums.Managers.ForumManager;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Net.Pf.Pages.Forums;


public class IndexModel : PageModel
{
    private readonly IForumManager forumManager;
    public IndexModel(IForumManager forumManager) 
    {
        this.forumManager = forumManager;
    }

    public IReadOnlyList<IGrouping<ForumGroup, Infrastructure.Forums.Models.Forum>> Forums { get; private set; } 
        = new List<IGrouping<ForumGroup, Infrastructure.Forums.Models.Forum>>();

    public async Task OnGet()
    {
        Forums = await forumManager.GetAll();
    }


}


