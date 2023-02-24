using Infrastructure.DataBases.Forum.Managers.ForkManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forum
{
    public class ForumForkCreateModel : PageModel
    {
        private readonly IForkManager forkManager;

		public ForumForkCreateModel(IForkManager forkManager)
        {
            this.forkManager = forkManager;
        }
		public void OnGet()
		{
		}


		public record CreateForkCommand(string Name, string Description);
        //[BindProperty]
        public CreateForkCommand command { get; set; }

        public void OnPost(CreateForkCommand command)
        {
            this.command = command;
            forkManager.Create(command.Name, command.Description);
        }


    }


}

