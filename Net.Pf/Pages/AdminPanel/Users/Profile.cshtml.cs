using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Net.Pf.Pages.AdminPanel.Users;



public class ProfileModel : PageModel
{
    UserManager<AppIdentityUser> UserManager;
    public ProfileModel(UserManager<AppIdentityUser> UserManager)
    {
        this.UserManager = UserManager;
    }





	public record Query(Guid UserId)
    {
		public class Validator : AbstractValidator<Query>
		{
			public Validator()
			{
				RuleFor(x => x.UserId).NotNull().NotEmpty();
			}
		}
	}


	public async Task OnGet(Query query)
    {
        var user = await UserManager.FindByIdAsync(query.UserId.ToString());

        var claims = (await UserManager.GetClaimsAsync(user))
            .Select(claim => new ClaimDto(claim.Type, claim.Value))
            .Where(x => x.Type != UserClaims.Administrator.ToString())
            .ToList();

        var roles = (await UserManager.GetRolesAsync(user))
			.Where(x => x != UserRoles.Administrator.ToString())
			.ToList();



		UserModel = new UserDto(user.Id, user.UserName, claims, roles);
    }

	public record ClaimDto(string Type, string Value);
	public record UserDto(Guid UserId, string UserName, List<ClaimDto> Claims, List<string> Roles);
	public UserDto UserModel { get; set; }




}




