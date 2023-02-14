using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Exceptions;
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


        public async Task<ActionResult> OnGetAddClaim(AddClaimCommand command)
        {
            var user = await UserManager.FindByIdAsync(command.UserId.ToString());
            if (user == null)
            {
				Throw.UserNotExists(command.UserId);
			}

            var claim = new Claim(command.claimType, command.claimValue);

            var userClaims = (await UserManager.GetClaimsAsync(user))
                .Select(x => x.ToString())
                .ToList();

            if (userClaims.Contains(claim.ToString()))
            {


                return BadRequest(new
                {
                    error = $"user has claim {claim.ToString()}"
                });
            }

            var result = await UserManager.AddClaimAsync(user, claim);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    error = $"Add Claim {claim.ToString()} Failed"
                });
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


        public async Task<ActionResult> OnGetDeleteClaim(DeleteClaimCommand command)
        {
            var user = await UserManager.FindByIdAsync(command.UserId.ToString());
            if (user == null)
            {
                return BadRequest(new
                {
                    error = "user does not exists"
                });
            }

            var userClaims = (await UserManager.GetClaimsAsync(user))
                .Where(claim => claim.Type == command.claimType)
                .ToList();

            if (userClaims.Count() == 0)
            {
                return BadRequest(new
                {
                    error = "The user has no such claims"
                });
            }
            else
            {
                await UserManager.RemoveClaimsAsync(user, userClaims);
            }

            return Redirect(command.returnUrl);
        }





    }


}
