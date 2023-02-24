using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Security.Claims;


namespace Net.Pf.Pages.AdminPanel.Users;



public class IndexModel : PageModel
{
    UserManager<AppIdentityUser> UserManager;
    public IndexModel(UserManager<AppIdentityUser> UserManager, RoleManager<AppIdentityRole> roleManager)
    {
        this.UserManager = UserManager;
    }


    public record UserDto(Guid Id, string UserName, string Email);
    public List<UserDto> OnGetUsers = new();

    public async Task OnGet()
    {
        OnGetUsers = await UserManager.Users.ProjectToType<UserDto>().ToListAsync();
    }


}
