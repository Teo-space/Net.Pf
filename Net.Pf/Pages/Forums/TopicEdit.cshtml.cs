using Infrastructure.Forums.Managers.ForumManager;
using Infrastructure.Forums.Managers.TopicManager;
using Infrastructure.Forums.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Extensions;
using System.Xml.Linq;
using static Net.Pf.Pages.Forums.EditTopicModel;


namespace Net.Pf.Pages.Forums;


public class EditTopicModel : PageModel
{
    private readonly IForumManager forumManager;
    private readonly ITopicManager topicManager;
    public EditTopicModel(
        IForumManager forumManager,
        ITopicManager topicManager)
    {
        this.forumManager = forumManager;
        this.topicManager = topicManager;
    }


    public void OnGet(EditTopicCommand command)
    {
        this.command = command;
    }



    public record EditTopicCommand(Guid TopicId, string Name, string ShortDescription, string Description)
    {
        public class Validator : AbstractValidator<EditTopicCommand>
        {
            public Validator()
            {
                RuleFor(x => x.TopicId).NotNull().NotEmpty();
                RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.ShortDescription).NotEmpty().MaximumLength(255);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(4000);
            }
        }

    }


    public EditTopicCommand command { get; set; }
    public async Task<ActionResult> OnPost(EditTopicCommand command)
    {
        this.command = command;
        if (this.ModelState.IsValid)
        {
            var topic = await topicManager.Edit(command.TopicId, command.Name, command.ShortDescription, command.Description);
            return this.RedirectToPageByType<TopicsModel>(new { ForumId = topic.ForumId });
        }
        return Page();
    }

}


