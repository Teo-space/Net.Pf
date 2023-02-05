using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;

namespace Net.Pf.Pages.AdminPanel;

public class UsersShowModel : PageModel
{
	SignInManager<AppIdentityUser> SignInManager;
	UserManager<AppIdentityUser> UserManager;
	public UsersShowModel(
		SignInManager<AppIdentityUser> SignInManager,
		UserManager<AppIdentityUser> UserManager)
	{
		this.SignInManager = SignInManager;
		this.UserManager = UserManager;
	}


	public record UserDto(Guid Id, string UserName, string Email);
	public List<UserDto> OnGetUsers = new();

	public async Task OnGet()
    {
        OnGetUsers = await UserManager.Users.ProjectToType<UserDto>().ToListAsync();
    }
	

	





}
