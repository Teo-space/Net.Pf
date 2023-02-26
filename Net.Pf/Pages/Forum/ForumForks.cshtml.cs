using Infrastructure.DataBases.Forum.Managers.ForkManager;
using Infrastructure.DataBases.Forum.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Net.Pf.Pages.Forum;


public class ForumForksModel : PageModel
{
    public readonly IForkManager forkManager;
    public ForumForksModel(IForkManager forkManager)
    {
        this.forkManager = forkManager;
    }

    public IReadOnlyList<ForumFork> forumForks { get; set; } = new List<ForumFork>();
    public void OnGet()
    {
        forumForks = forkManager.GetAll();
    }


}


