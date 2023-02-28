using Infrastructure.Forums.Managers.TopicManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Extensions;


namespace Net.Pf.Pages.Forums;


public class TopicDeleteModel : PageModel
{
    private readonly ITopicManager topicManager;
    public TopicDeleteModel(ITopicManager topicManager)
    {
        this.topicManager = topicManager;
    }



    public async Task<ActionResult> OnGet(Guid ForumId, Guid TopicId)
    {
        await topicManager.Delete(TopicId);

        return this.RedirectToPageByType<TopicsModel>(new { ForumId = ForumId });
    }


    public async Task<ActionResult> OnPost(Guid ForumId, Guid TopicId)
    {
        await topicManager.Delete(TopicId);

        return this.RedirectToPageByType<TopicsModel>(new { ForumId = ForumId });
    }


}



