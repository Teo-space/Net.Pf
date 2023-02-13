using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Linq;
using System.Security.Claims;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class RoleManagerModel : PageModel
    {
        readonly UserManager<AppIdentityUser> UserManager;
        readonly RoleManager<AppIdentityRole> roleManager;

        public PageRoleManagerModel(UserManager<AppIdentityUser> UserManager,
            RoleManager<AppIdentityRole> roleManager)
        {
            this.UserManager = UserManager;
            this.roleManager = roleManager;
        }

        public void OnGet()
        {
        }

		public record AddRoleCommand(Guid UserId,
			string RoleName,
			string returnUrl)

		{
			public class Validator : AbstractValidator<AddRoleCommand>
			{
				public Validator()
				{
					RuleFor(x => x.UserId).NotNull().NotEmpty();
					RuleFor(x => x.RoleName).NotNull().NotEmpty();
					RuleFor(x => x.returnUrl).NotNull().NotEmpty();
				}
			}
		}

		public async Task<ActionResult> OnGetAddClaim(AddRoleCommand command)
		{
			var user = await UserManager.FindByIdAsync(command.UserId.ToString());
			if (user == null)
			{
				return BadRequest(new
				{
					error = "user does not exists"
				});
			}

			var roles = (await UserManager.GetRolesAsync(user)).ToList();

			if (roles.Contains(command.RoleName))
			{
				return BadRequest(new
				{
					error = $"user has role {command.RoleName}"
				});
			}
			else
			{
				await UserManager.AddToRoleAsync(user, command.RoleName);
			}



			return Redirect(command.returnUrl);
		}





	}


}
