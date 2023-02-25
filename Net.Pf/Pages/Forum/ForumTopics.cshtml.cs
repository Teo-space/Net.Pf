using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Infrastructure.DataBases.Forum.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forum;

public class ForumTopicsModel : PageModel
{
	public readonly ITopicManager topicManager;
    public ForumTopicsModel(ITopicManager topicManager) { this.topicManager = topicManager; }

	public Guid ForumForkId;

	public IReadOnlyList<ForumTopic> Topics = new List<ForumTopic>();

    public void OnGet(Guid ForumForkId)
    {
		this.ForumForkId = ForumForkId;

        Topics = topicManager.GetTopics(ForumForkId);
    }


}


