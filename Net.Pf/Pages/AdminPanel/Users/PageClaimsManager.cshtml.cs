using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class PageClaimsManagerModel : PageModel
    {
        UserManager<AppIdentityUser> UserManager;
        public PageClaimsManagerModel(UserManager<AppIdentityUser> UserManager)
        {
            this.UserManager = UserManager;
        }

        public void OnGet()
        {
        }


    }


}
