using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Xml.Linq;

namespace Net.Pf.Pages.AdminPanel;

[Authorize(Policy = "AccessRights")]
public class UsersShowModel : PageModel
{
	SignInManager<AppIdentityUser> SignInManager;
	UserManager<AppIdentityUser> UserManager;
	//RoleManager<IdentityRole> roleManager;

    public UsersShowModel(
		SignInManager<AppIdentityUser> SignInManager,
		UserManager<AppIdentityUser> UserManager
        //RoleManager<IdentityRole> roleManager
        )
	{
		this.SignInManager = SignInManager;
		this.UserManager = UserManager;
		//this.roleManager = roleManager;

    }


	public record UserDto(Guid Id, string UserName, string Email);
	public List<UserDto> OnGetUsers = new();
	public IList<string> Roles;


	public async Task OnGet()
    {
		OnGetUsers = await UserManager.Users.ProjectToType<UserDto>().ToListAsync();

        //var self = await UserManager.GetUserAsync(User);

        //Roles = await UserManager.GetRolesAsync(self);

        //var self = await UserManager.FindByIdAsync(User.ClaimNameIdentifier());
        //Roles = await UserManager.GetRolesAsync(self);

        //await roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator.ToString()));

        //await UserManager.AddToRoleAsync(self, "Test");
        //var self = await UserManager.FindByIdAsync(User.ClaimNameIdentifier());
        //await UserManager.AddClaimAsync(self, new System.Security.Claims.Claim("AccessRights", UserRoles.Administrator.ToString()));


        //UserManager.GetClaimsAsync


    }








}
