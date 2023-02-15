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
            //.Where(x => x != UserClaims.Administrator.ToString())
            .ToList();

        var roles = (await UserManager.GetRolesAsync(user))
			//.Where(x => x != UserRoles.Administrator.ToString())
			.ToList();



		UserModel = new UserDto(user.Id, user.UserName, claims, roles);
    }

	public record ClaimDto(string Type, string Value);
	public record UserDto(Guid UserId, string UserName, List<ClaimDto> Claims, List<string> Roles);
	public UserDto UserModel { get; set; }




	public record AddClaimCommand(Guid UserId, UserClaims userClaims, string returnUrl)
    {
        public class Validator : AbstractValidator<AddClaimCommand>
        {
            static readonly List<string> UserClaimsList = 
                Enum
                .GetNames(typeof(UserClaims))
                .Where(x => x != UserClaims.Administrator.ToString())
                .ToList();

            public Validator()
            {
                RuleFor(x => x.UserId).NotNull().NotEmpty();

                RuleFor(x => x.userClaims).Must(x => UserClaimsList.Contains(x.ToString()));

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




