using Infrastructure.DataBases.Forum.Managers.ForkManager;
using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Infrastructure.DataBases.Forum.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forum;

public class ForumTopicsModel : PageModel
{
	public readonly IForkManager forkManager;
	public readonly ITopicManager topicManager;
    public ForumTopicsModel(
        IForkManager forkManager,
        ITopicManager topicManager) 
    {
        this.forkManager = forkManager;
        this.topicManager = topicManager;
    }


	public Guid ForumForkId;
	public ForumFork? forumFork;

	public IReadOnlyList<ForumTopic> Topics { get; set; } = new List<ForumTopic>();

    public void OnGet(Guid ForumForkId)
    {
		this.ForumForkId = ForumForkId;
        //Topics = topicManager.GetTopics(ForumForkId);
        forumFork = this.forkManager.GetById(ForumForkId);
        Topics = this.forkManager.GetTopics(ForumForkId);
    }


}


