using Infrastructure.DataBases.Forum.Managers.PostManager;
using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Infrastructure.DataBases.Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Net.Pf.Pages.Forum;


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
	public Guid ForumForkId { get; private set; }
	public IReadOnlyList<ForumPost> Posts { get; private set; } = new List<ForumPost>();


    public void OnGet(Guid ForumTopicId) => Setup(ForumTopicId);

	void Setup(Guid ForumTopicId)
	{
		this.ForumTopicId = ForumTopicId;
		this.forumTopic = topicManager.GetById(ForumTopicId);
        this.ForumForkId = this.forumTopic.ForumForkId;

		this.Posts = postManager.GetPosts(ForumTopicId);
	}





	public CreatePostCommand createPostCommand { get; private set; }
	public record CreatePostCommand(Guid TopicId, string Text)
    {
        public class Validator : AbstractValidator<CreatePostCommand>
        {
            public Validator() 
            {
				RuleFor(x => x.TopicId).NotNull().NotEmpty();
				RuleFor(x => x.Text).NotEmpty().MaximumLength(255);
            }
		}

	}

	public async Task<ActionResult> OnPost(CreatePostCommand createPostCommand)
    {
        //this.createPostCommand = createPostCommand;
        if(this.ModelState.IsValid)
        {
			postManager.Create(createPostCommand.TopicId, createPostCommand.Text);
            //this.createPostCommand = default;
			return this.RedirectToPage(new { ForumTopicId = createPostCommand.TopicId });
			//Setup(createPostCommand.TopicId);
		}
		return Page();
	}



}



