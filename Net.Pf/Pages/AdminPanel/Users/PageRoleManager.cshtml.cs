using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class PageRoleManagerModel : PageModel
    {
        readonly RoleManager<AppIdentityRole> roleManager;

        public PageRoleManagerModel(RoleManager<AppIdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public void OnGet()
        {
        }


    }


}
