using Infrastructure.Forums;
using Infrastructure.Forums.Managers.ForumManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Extensions;


namespace Net.Pf.Pages.Forums;

public class CreateForumModel : PageModel
{
    private readonly IForumManager forumManager;
    public CreateForumModel(IForumManager forumManager)
    {
        this.forumManager = forumManager;
    }

    public void OnGet()
    {
    }


    public record CreateForumCommand(ForumGroup Group,string Name, string Description)
    {
        public class Validator : AbstractValidator<CreateForumCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Group).NotNull().NotEmpty().IsInEnum();
                RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(120);
            }
        }

    }

    public CreateForumCommand command { get; set; }

    public async Task<ActionResult> OnPost(CreateForumCommand command)
    {
        this.command = command;
        if (this.ModelState.IsValid)
        {
            var forum = forumManager.Create(command.Group, command.Name, command.Description);
            return this.RedirectToPageByType<IndexModel>();
        }
        return Page();
    }



}


