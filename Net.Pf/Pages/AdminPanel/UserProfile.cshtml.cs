using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Security.Claims;

namespace Net.Pf.Pages.AdminPanel
{
    public class UserProfileModel : PageModel
    {
        UserManager<AppIdentityUser> UserManager;
        public UserProfileModel(UserManager<AppIdentityUser> UserManager)
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
            var claims = (await UserManager.GetClaimsAsync(user)).Select(claim => claim.ToString()).ToList();




            UserModel = new UserDto(user.Id, user.UserName, claims);
        }

        public UserDto UserModel { get; set; }
        public record UserDto(Guid UserId, string UserName, List<string> Claims);






    }


}
