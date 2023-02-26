using Infrastructure.DataBases.Forum.Managers.PostManager;
using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Infrastructure.DataBases.Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forum
{
    public class ForumPostsModel : PageModel
    {
        public readonly IPostManager postManager;
        public readonly ITopicManager topicManager;

        public ForumPostsModel(
            IPostManager postManager,
            ITopicManager topicManager)
        {
            this.postManager = postManager;
            this.topicManager = topicManager;
        }


        public Guid ForumTopicId { get; private set; }
        public ForumTopic? forumTopic { get; private set; }

        public IReadOnlyList<ForumPost> Posts { get; private set; } = new List<ForumPost>();

        public void OnGet(Guid ForumTopicId)
        {
            this.ForumTopicId = ForumTopicId;
            this.forumTopic = topicManager.GetById(ForumTopicId);
            this.Posts = postManager.GetPosts(ForumTopicId);
        }

    }

}

