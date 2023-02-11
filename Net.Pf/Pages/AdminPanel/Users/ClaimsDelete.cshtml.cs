using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Security.Claims;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class ClaimsDeleteModel : PageModel
    {
		UserManager<AppIdentityUser> UserManager;
		public ClaimsDeleteModel(UserManager<AppIdentityUser> UserManager)
		{
			this.UserManager = UserManager;
		}

		public record DeleteClaimCommand(Guid UserId, UserClaims userClaims, string returnUrl)
		{
			public class Validator : AbstractValidator<DeleteClaimCommand>
			{
				public Validator()
				{
					RuleFor(x => x.UserId).NotNull().NotEmpty();

					RuleFor(x => x.userClaims)
						.Must(x => Enum
						.GetNames(typeof(UserClaims))
						.Where(x => x != UserClaims.Administrator.ToString())
						.ToList()
						.Contains(x.ToString()));

					RuleFor(x => x.returnUrl).NotNull().NotEmpty();

				}
			}
		}


		public async Task<RedirectResult> OnGetDeleteClaim(DeleteClaimCommand command)
		{
            var user = await UserManager.FindByIdAsync(command.UserId.ToString());
            if (user != null)
            {
                var claim = new Claim(command.userClaims.ToString(), command.userClaims.ToString());

                var userClaims = (await UserManager.GetClaimsAsync(user)).Where(c => c.ToString() == claim.ToString()).ToList();
                if (userClaims.Count() > 0)
                {
                    await UserManager.RemoveClaimsAsync(user, userClaims);
                }
            }

            return Redirect(command.returnUrl);
		}
	}
}
