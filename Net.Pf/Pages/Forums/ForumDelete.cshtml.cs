using Infrastructure.Forums.Managers.ForumManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Net.Pf.Pages.Forums
{
    public class DeleteForumModel : PageModel
    {
        private readonly IForumManager forumManager;
        public DeleteForumModel(IForumManager forumManager)
        {
            this.forumManager = forumManager;
        }


        public async Task<ActionResult> OnGet(Guid ForumId)
        {
            await forumManager.Delete(ForumId);
            return this.RedirectToPageByType<IndexModel>();
        }


        public async Task<ActionResult> OnPost(Guid ForumId)
        {
            await forumManager.Delete(ForumId);
            return this.RedirectToPageByType<IndexModel>();
        }


    }

}


