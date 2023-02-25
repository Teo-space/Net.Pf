using Infrastructure.DataBases.Forum.Managers.ForkManager;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forum;

public class ForumForkCreateModel : PageModel
{
    private readonly IForkManager forkManager;
	public ForumForkCreateModel(IForkManager forkManager) => this.forkManager = forkManager;

	public record CreateForkCommand(string Name, string Description)
    {
        public class Validator : AbstractValidator<CreateForkCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(120);
            }
        }

	}
    //[BindProperty]
    public CreateForkCommand command { get; set; }

    public void OnPost(CreateForkCommand command)
    {
        this.command = command;
        if(this.ModelState.IsValid)
        {
				forkManager.Create(command.Name, command.Description);
		}
    }


}

