using Infrastructure.DataBases.Forum.Managers.TopicManager;
using Infrastructure.DataBases.Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Extensions;


namespace Net.Pf.Pages.Forum;


public class ForumTopicCreateModel : PageModel
{
	public readonly ITopicManager topicManager;
	public ForumTopicCreateModel(ITopicManager topicManager)
	{
        this.topicManager = topicManager;
    }

	public Guid ForumForkId;
	public void OnGet(Guid ForumForkId)
    {
		this.ForumForkId = ForumForkId;
    }


	public record CreateTopicCommand(Guid ForumForkId, 
					string Name,
					string ShortDescription,
					string Description)
	{
		public class Validator : AbstractValidator<CreateTopicCommand>
		{
			public Validator()
			{
				RuleFor(x => x.ForumForkId).NotNull().NotEmpty();
				RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
				RuleFor(x => x.ShortDescription).NotEmpty().MaximumLength(80);
				RuleFor(x => x.Description).NotEmpty().MaximumLength(255);
			}
		}
	}

	public string Result = "";
	public CreateTopicCommand? command { get; set; }

    public async Task<ActionResult> OnPost(CreateTopicCommand command)
	{
		this.command = command;
		if (this.ModelState.IsValid)
		{
			var forumTopic = topicManager.Create(command.ForumForkId, command.Name, command.ShortDescription, command.Description);
			Result = "Created";
            return this.RedirectToPageByType<ForumPostsModel>(new { ForumTopicId = forumTopic.ForumTopicId });
        }
        else
		{
			Result = "else";
            return Page();
        }
	}


}

