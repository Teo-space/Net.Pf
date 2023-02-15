using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public RoleManagerModel(UserManager<AppIdentityUser> UserManager,
            RoleManager<AppIdentityRole> roleManager)
        {
            this.UserManager = UserManager;
            this.roleManager = roleManager;
        }


		//public List<AppIdentityRole> Roles;

		public record RoleDto(Guid RoleId, string Name);
		public List<RoleDto> Roles { get; set; }

        public async Task OnGet()
        {
            Roles = await roleManager.Roles.ProjectToType<RoleDto>().ToListAsync();
        }





        public record CreateRoleCommand(string RoleName)
        {
            public class Validator : AbstractValidator<CreateRoleCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.RoleName).NotNull().NotEmpty().MaximumLength(40);
                }
            }
        }

		public async Task<ActionResult> OnPostCreateRole(CreateRoleCommand command)
		{
			if (!await roleManager.RoleExistsAsync(command.RoleName))
			{
				var role = new AppIdentityRole(command.RoleName);

				await roleManager.CreateAsync(role);
			}
			//return new OkObjectResult("Success");
			return Redirect("RoleManager");
		}




		public record DeleteRoleCommand(string RoleName)
		{
			public class Validator : AbstractValidator<DeleteRoleCommand>
			{
				public Validator()
				{
					RuleFor(x => x.RoleName).NotNull().NotEmpty().MaximumLength(40);
				}
			}
		}

		public async Task<ActionResult> OnPostDeleteRole(DeleteRoleCommand command)
		{
			if (await roleManager.RoleExistsAsync(command.RoleName))
			{
				var role = await roleManager.FindByNameAsync(command.RoleName);

				var result = await roleManager.DeleteAsync(role);
				if (result.Succeeded)
				{
					//return new OkObjectResult("Success");
					return Redirect("RoleManager");
				}
				else
				{
					throw new Exception($"Delete Failed: {result.ToString()}");
				}
			}
			else
			{
				throw new Exception("Role not exists");
			}

			
		}











		public record AddToRoleCommand(Guid UserId,
			string RoleName,
			string returnUrl)

		{
			public class Validator : AbstractValidator<AddToRoleCommand>
			{
				public Validator()
				{
					RuleFor(x => x.UserId).NotNull().NotEmpty();
					RuleFor(x => x.RoleName).NotNull().NotEmpty();
					RuleFor(x => x.returnUrl).NotNull().NotEmpty();
				}
			}
		}

		public async Task<ActionResult> OnGetAddToRole(AddToRoleCommand command)
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
