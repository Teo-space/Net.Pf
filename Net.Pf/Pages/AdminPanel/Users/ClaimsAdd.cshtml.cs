using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Security.Claims;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class ClaimsAddModel : PageModel
    {
        UserManager<AppIdentityUser> UserManager;
        public ClaimsAddModel(UserManager<AppIdentityUser> UserManager)
        {
            this.UserManager = UserManager;
        }
        public void OnGet()
        {
        }


        public record AddClaimCommand(Guid UserId, UserClaims userClaims, string returnUrl)
        {
            public class Validator : AbstractValidator<AddClaimCommand>
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


        public async Task<RedirectResult> OnGetAddClaim(AddClaimCommand command)
        {
            var user = await UserManager.FindByIdAsync(command.UserId.ToString());
            if (user != null)
            {
                var claim = new Claim(command.userClaims.ToString(), command.userClaims.ToString());

                var userClaims = (await UserManager.GetClaimsAsync(user)).Select(x => x.ToString()).ToList();

                if (!userClaims.Contains(claim.ToString()))
                {
                    await UserManager.AddClaimAsync(user, claim);
                }
                else
                {

                }
            }

            return Redirect(command.returnUrl);
        }


    }
}
