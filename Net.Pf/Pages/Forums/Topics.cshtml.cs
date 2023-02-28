using Infrastructure.Forums.Managers.ForumManager;
using Infrastructure.Forums.Managers.TopicManager;
using Infrastructure.Forums.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forums;


public class TopicsModel : PageModel
{
    private readonly IForumManager forumManager;
    private readonly ITopicManager topicManager;
    public TopicsModel(
        IForumManager forumManager, 
        ITopicManager topicManager) 
    {
        this.forumManager = forumManager;
        this.topicManager = topicManager;
    }


    public Guid ForumId { get; private set; }
    public Infrastructure.Forums.Models.Forum? Forum { get; private set; }
    public IReadOnlyList<Topic> Topics { get; private set; } = new List<Topic>();


    public async Task OnGet(Guid ForumId)
    {
        this.ForumId = ForumId;
        this.Forum = await forumManager.GetById(ForumId);
        Topics = await topicManager.GetTopics(ForumId);
    }


}


