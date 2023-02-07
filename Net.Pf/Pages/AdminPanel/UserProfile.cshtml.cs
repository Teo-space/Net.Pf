using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;

namespace Net.Pf.Pages.AdminPanel
{
    public class UserProfileModel : PageModel
    {
        SignInManager<AppIdentityUser> SignInManager;
        UserManager<AppIdentityUser> UserManager;
        public UserProfileModel(
            SignInManager<AppIdentityUser> SignInManager,
            UserManager<AppIdentityUser> UserManager)
        {
            this.SignInManager = SignInManager;
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
            UserModel = (await UserManager?.FindByIdAsync(query.UserId.ToString()))
                ?.Adapt<UserDto>();
        }

        public UserDto UserModel { get; set; }
        public record UserDto(Guid UserId, string UserName);






    }


}
