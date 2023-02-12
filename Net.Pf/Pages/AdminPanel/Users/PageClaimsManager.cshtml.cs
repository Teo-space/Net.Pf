using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Security.Claims;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class PageClaimsManagerModel : PageModel
    {
        UserManager<AppIdentityUser> UserManager;
        public PageClaimsManagerModel(UserManager<AppIdentityUser> UserManager)
        {
            this.UserManager = UserManager;
        }

        public void OnGet()
        {
        }

        public record AddClaimCommand(Guid UserId, 
            string claimType, string claimValue,
            string returnUrl)

        {
            public class Validator : AbstractValidator<AddClaimCommand>
            {
                static readonly List<string> UserClaimsList =
                    Enum
                    .GetNames(typeof(UserClaims))
                    //.Where(x => x != UserClaims.Administrator.ToString())
                    .ToList();

                public Validator()
                {
                    RuleFor(x => x.UserId).NotNull().NotEmpty();
                    RuleFor(x => x.claimType).NotNull().NotEmpty()
                        .Must(x => UserClaimsList.Contains(x.ToString()))
                        ;
                    RuleFor(x => x.claimValue).NotNull().NotEmpty();

 
                    RuleFor(x => x.returnUrl).NotNull().NotEmpty();

                }
            }
        }

        public async Task<RedirectResult> OnGetAddClaim(AddClaimCommand command)
        {
            var user = await UserManager.FindByIdAsync(command.UserId.ToString());
            if (user != null)
            {
                var claim = new Claim(command.claimType, command.claimValue);

                var userClaims = (await UserManager.GetClaimsAsync(user)).Select(x => x.ToString()).ToList();

                if (!userClaims.Contains(claim.ToString()))
                {
                    await UserManager.AddClaimAsync(user, claim);
                }
            }

            return Redirect(command.returnUrl);
        }








        public record DeleteClaimCommand(Guid UserId,
            string claimType, 
            string returnUrl)
        {
            public class Validator : AbstractValidator<DeleteClaimCommand>
            {
                static readonly List<string> UserClaimsList =
                    Enum
                        .GetNames(typeof(UserClaims))
                        //.Where(x => x != UserClaims.Administrator.ToString())
                        .ToList();

                public Validator()
                {
                    RuleFor(x => x.UserId).NotNull().NotEmpty();
                    RuleFor(x => x.claimType).NotNull().NotEmpty()
                        .Must(x => UserClaimsList.Contains(x.ToString()));
                    RuleFor(x => x.returnUrl).NotNull().NotEmpty();
                }
            }
        }


        public async Task<RedirectResult> OnGetDeleteClaim(DeleteClaimCommand command)
        {
            var user = await UserManager.FindByIdAsync(command.UserId.ToString());
            if (user != null)
            {
                var userClaims = (await UserManager.GetClaimsAsync(user))
                    .Where(claim => claim.Type == command.claimType).ToList();
                if (userClaims.Count() > 0)
                {
                    await UserManager.RemoveClaimsAsync(user, userClaims);
                }
            }

            return Redirect(command.returnUrl);
        }





    }


}
