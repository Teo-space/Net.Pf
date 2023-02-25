using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forum;

public class ForumTopicsModel : PageModel
{
	public readonly ITopicManager topicManager;
    public ForumTopicsModel() { }

	public Guid ForumForkId;
	public void OnGet(Guid ForumForkId)
    {
		this.ForumForkId = ForumForkId;
    }


}


