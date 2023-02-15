using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Data;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Net.Pf.Pages.AdminPanel.Users.RoleManagerModel;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class RoleManagerModel : PageModel
    {
        readonly UserManager<AppIdentityUser> UserManager;
        readonly RoleManager<AppIdentityRole> roleManager;

        readonly IMediator mediator;

		public DateTime starts;
        public RoleManagerModel(
			UserManager<AppIdentityUser> UserManager
            ,RoleManager<AppIdentityRole> roleManager
			,IMediator mediator
			)
        {
			starts = DateTime.Now;
            this.UserManager = UserManager;
            this.roleManager = roleManager;
			this.mediator = mediator;
        }


		public record RoleDto(Guid RoleId, string Name);
        public List<RoleDto> Roles { get; set; } = new();

        public async Task OnGet()
        {
            Roles = await roleManager.Roles.ProjectToType<RoleDto>().ToListAsync();
        }




        public async Task<ActionResult> OnPostCreateRole(CreateRoleCommand command)
        {
            await this.mediator.Send(command);
            return RedirectToPage("RoleManager");
        }


        public record CreateRoleCommand(string RoleName) : IRequest<AppIdentityRole>
        {
            public class Validator : AbstractValidator<CreateRoleCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.RoleName).NotNull().NotEmpty().MaximumLength(40);
                }
            }


			public record Handler(
				UserManager<AppIdentityUser> UserManager, 
				RoleManager<AppIdentityRole> roleManager) 
				
				: IRequestHandler<CreateRoleCommand, AppIdentityRole>
			{

				public async Task<AppIdentityRole> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
				{
					if (await roleManager.RoleExistsAsync(command.RoleName))
					{
						throw new Exception($"Role ({command.RoleName}) Exists");
					}

					var role = new AppIdentityRole(command.RoleName);

					var result = await roleManager.CreateAsync(role);
					if (!result.Succeeded)
					{
						throw new Exception($"Create Role ({command.RoleName}) Failed");
					}
					return role;
				}
			}

		}









        public async Task<ActionResult> OnPostDeleteRole(DeleteRoleCommand command)
        {
            await this.mediator.Send(command);
            return RedirectToPage("RoleManager");
        }

        public record DeleteRoleCommand(string RoleName) : IRequest<string>
		{
			public class Validator : AbstractValidator<DeleteRoleCommand>
			{
				public Validator()
				{
					RuleFor(x => x.RoleName).NotNull().NotEmpty().MaximumLength(40);
				}
			}


            public record Handler(
                    UserManager<AppIdentityUser> UserManager,
                    RoleManager<AppIdentityRole> roleManager)

                    : IRequestHandler<DeleteRoleCommand, string>
            {
                public async Task<string> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
				{
                    if (!await roleManager.RoleExistsAsync(command.RoleName))
					{
                        throw new Exception($"Role {command.RoleName} not exists");
					}

                    var role = await roleManager.FindByNameAsync(command.RoleName);
                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return command.RoleName;
                    }
                    else
                    {
                        throw new Exception($"Delete Failed: {result.ToString()}");
                    }
                }
            }
        }


















		public async Task<ActionResult> 
            OnGetAddUserToRole(AddUserToRoleCommand command)
		{
            await this.mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
            //return Redirect(command.returnUrl);
		}


		public async Task<ActionResult> OnPostAddUserToRole(AddUserToRoleCommand command)
		{
            await this.mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
		}


        public record AddUserToRoleCommand(Guid UserId,
			string RoleName) : IRequest<string>

        {
            public class Validator : AbstractValidator<AddUserToRoleCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.UserId).NotNull().NotEmpty();
                    RuleFor(x => x.RoleName).NotNull().NotEmpty();
                }
            }


            public record Handler(
					UserManager<AppIdentityUser> UserManager,
					RoleManager<AppIdentityRole> roleManager)

					: IRequestHandler<AddUserToRoleCommand, string>
            {

                public async Task<string> Handle(AddUserToRoleCommand command, CancellationToken cancellationToken)
                {
                    var user = await UserManager.FindByIdAsync(command.UserId.ToString());
                    if (user == null)
                    {
                        throw new Exception($"User {command.UserId} not exists");
                    }
                    if (!await roleManager.RoleExistsAsync(command.RoleName))
                    {
                        throw new Exception($"Role {command.RoleName} not exists");
                    }

                    var role = new AppIdentityRole(command.RoleName);
                    var result = await UserManager.AddToRoleAsync(user, command.RoleName);
                    if (result.Succeeded)
                    {
                        return command.RoleName;
                    }
                    else
                    {
                        throw new Exception($"Add User To Role Failed {result.ToString()}");
                    }

                }


            }

        }









        public async Task<ActionResult> OnGetDeleteUserFromRole(DeleteUserFromRoleCommand command)
        {
            await this.mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
        }


        public async Task<ActionResult> OnPostDeleteUserFromRole(DeleteUserFromRoleCommand command)
        {
            await this.mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
        }




        public record DeleteUserFromRoleCommand(Guid UserId, string RoleName) : IRequest<string>
		{
			public class Validator : AbstractValidator<DeleteUserFromRoleCommand>
			{
				public Validator()
				{
					RuleFor(x => x.UserId).NotNull().NotEmpty();
					RuleFor(x => x.RoleName).NotNull().NotEmpty().MaximumLength(40);
				}
			}

            public record Handler(
                    UserManager<AppIdentityUser> UserManager,
                    RoleManager<AppIdentityRole> roleManager)

                    : IRequestHandler<DeleteUserFromRoleCommand, string>
            {

                public async Task<string> Handle(DeleteUserFromRoleCommand command, CancellationToken cancellationToken)
                {
                    var user = await UserManager.FindByIdAsync(command.UserId.ToString());
                    if (user == null)
                    {
                        throw new Exception($"User {command.UserId} not exists");
                    }
                    if (!await roleManager.RoleExistsAsync(command.RoleName))
                    {
                        throw new Exception($"Role {command.RoleName} not exists");
                    }

                    var role = new AppIdentityRole(command.RoleName);
                    var result = await UserManager.RemoveFromRoleAsync(user, command.RoleName);
                    if (result.Succeeded)
                    {
                        return command.RoleName;
                    }
                    else
                    {
                        throw new Exception($"Delete User From Role {result.ToString()}");
                    }
                }


            }

        }







	}


}
