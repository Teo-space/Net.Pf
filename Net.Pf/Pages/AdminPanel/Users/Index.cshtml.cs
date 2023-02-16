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

    public void SetSelf()
    {
        //var self = await UserManager.FindByIdAsync(User.ClaimNameIdentifier());
        //await UserManager.AddClaimAsync(self, new System.Security.Claims.Claim("AccessRights", UserRoles.Administrator.ToString()));
        //UserManager.GetClaimsAsync

        var x = UserClaims.Moderator;
        //((int)x)
    }


    public record AddClaimCommand(Guid UserId, UserClaims userClaims)
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
            }
        }
    }


    //public async Task OnGetAddClaim(Guid UserId, UserClaims userClaims)
    public async Task OnGetAddClaim(AddClaimCommand command)
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
        OnGetUsers = await UserManager.Users.ProjectToType<UserDto>().ToListAsync();
    }
    //UserManager.GetClaimsAsync

}
