using Infrastructure.Forums;
using Infrastructure.Forums.Managers.ForumManager;
using Infrastructure.Forums.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Extensions;

namespace Net.Pf.Pages.Forums;


public class EditForumModel : PageModel
{
    private readonly IForumManager forumManager;
    public EditForumModel(IForumManager forumManager)
    {
        this.forumManager = forumManager;
    }


    //public ForumDto Forum { get; set; }

    public void OnGet(EditForumCommand command)
    {
        this.command = command;
    }



    public record EditForumCommand(Guid ForumId, ForumGroup Group, string Name, string Description)
    {
        public class Validator : AbstractValidator<EditForumCommand>
        {
            public Validator()
            {
                RuleFor(x => x.ForumId).NotNull().NotEmpty();
				RuleFor(x => x.Group).NotNull().NotEmpty().IsInEnum();
				RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(120);
            }
        }

    }

    public EditForumCommand command { get; set; }
    public async Task<ActionResult> OnPost(EditForumCommand command)
    {
        this.command = command;
        if (this.ModelState.IsValid)
        {
            await forumManager.Edit(command.ForumId, command.Group, command.Name, command.Description);
            return this.RedirectToPageByType<IndexModel>();
        }
        return Page();
    }




}


