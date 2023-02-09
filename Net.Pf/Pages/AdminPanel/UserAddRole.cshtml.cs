using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;

namespace Net.Pf.Pages.AdminPanel
{
    public class UserAddRoleModel : PageModel
    {
        SignInManager<AppIdentityUser> SignInManager;
        UserManager<AppIdentityUser> UserManager;
        public UserAddRoleModel(
            SignInManager<AppIdentityUser> SignInManager,
            UserManager<AppIdentityUser> UserManager)
        {
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
        }

        public Guid UserId { get; private set; }
        public UserRoles role { get; private set; }

        public async Task//<RedirectResult>
            OnGet(Guid UserId, UserRoles role, string returnUrl)
        {
            this.UserId = UserId;
            this.role = role;

            var user = await UserManager?.FindByIdAsync(UserId.ToString());




            this.UserManager.AddToRoleAsync(user, role.ToString());


            //return Redirect(returnUrl);
            //RedirectToPage

            //������� - �������

            handlerName = "Get";
        }

        public string handlerName = string.Empty;


        public async Task//<RedirectResult>
            OnGetAddRole(Guid UserId, UserRoles role, string returnUrl)
        {
            handlerName = "Get AddRole";

            this.UserId = UserId;
            this.role = role;

            var user = await UserManager?.FindByIdAsync(UserId.ToString());




            this.UserManager.AddToRoleAsync(user, role.ToString());


            //return Redirect(returnUrl);
            //RedirectToPage

            //������� - �������
        }




        public async Task OnPostAddRole(Guid UserId, UserRoles role)
        {
            if(role == UserRoles.Administrator)
            {

            }
            else if (role == UserRoles.Moderator)
            {

            }
            else if (role == UserRoles.Premium)
            {

            }
            else if (role == UserRoles.User)
            {

            }
            else if (role == UserRoles.None)
            {

            }

            this.role = role;




        }


    }
}
