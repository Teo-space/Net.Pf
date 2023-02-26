using Infrastructure.DataBases.Forum.Managers.PostManager;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Forum
{
    public class ForumPostCreateModel : PageModel
    {
        public readonly IPostManager postManager;
        public ForumPostCreateModel(IPostManager postManager)
        {
            this.postManager = postManager;

        }

        
        public void OnGet()
        {
        }


    }


}
