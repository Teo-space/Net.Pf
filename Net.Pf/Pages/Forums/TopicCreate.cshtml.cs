using Infrastructure.Forums.Managers.TopicManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Extensions;

namespace Net.Pf.Pages.Forums;


public class CreateTopicModel : PageModel
{
	private readonly ITopicManager topicManager;
	public CreateTopicModel(ITopicManager topicManager)
	{
		this.topicManager = topicManager;
	}


    public Guid ForumId;
    public void OnGet(Guid ForumId)
    {
        this.ForumId = ForumId;
    }




    public record CreateTopicCommand(Guid ForumId,
            string Name,
            string ShortDescription,
            string Description)
    {
        public class Validator : AbstractValidator<CreateTopicCommand>
        {
            public Validator()
            {
                RuleFor(x => x.ForumId).NotNull().NotEmpty();
                RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
                RuleFor(x => x.ShortDescription).NotEmpty().MaximumLength(80);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(255);
            }
        }
    }

    public CreateTopicCommand command { get; set; }
    public async Task<ActionResult> OnPost(CreateTopicCommand command)
    {
        this.command = command;
        if (this.ModelState.IsValid)
        {
            var topic = await topicManager.Create(
                command.ForumId, 
                command.Name, 
                command.ShortDescription, 
                command.Description);

            //Увеличение счетчика топиков у форума
            return this.RedirectToPageByType<TopicsModel>(new { ForumId = command.ForumId });
        }
        return Page();
    }











}

