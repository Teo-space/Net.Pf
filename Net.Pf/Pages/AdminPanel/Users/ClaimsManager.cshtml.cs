using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net.Pf.Identity;
using System.Security.Claims;
using static Net.Pf.Pages.AdminPanel.Users.RoleManagerModel;

namespace Net.Pf.Pages.AdminPanel.Users
{
    public class ClaimsManagerModel : PageModel
    {
        UserManager<AppIdentityUser> UserManager;

        readonly IMediator mediator;

        public ClaimsManagerModel(
            UserManager<AppIdentityUser> UserManager
            ,IMediator mediator)
        {
            this.UserManager = UserManager;
            this.mediator = mediator;
        }

        public void OnGet()
        {
        }





        public async Task<ActionResult> OnGetAddClaimToUser(AddClaimToUserCommand command)
        {
			await mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
        }
        public async Task<ActionResult> OnPostAddClaimToUser(AddClaimToUserCommand command)
        {
			await mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
        }


        public record AddClaimToUserCommand(Guid UserId, 
            string claimType, string claimValue) 
            
            : IRequest<string>

        {
            public class Validator : AbstractValidator<AddClaimToUserCommand>
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
                }
            }


            public record Handler(
                     UserManager<AppIdentityUser> UserManager,
                     RoleManager<AppIdentityRole> roleManager)

                     : IRequestHandler<AddClaimToUserCommand, string>
            {

                public async Task<string> Handle(AddClaimToUserCommand command, CancellationToken cancellationToken)
                {
                    var user = await UserManager.FindByIdAsync(command.UserId.ToString());
                    if (user == null)
                    {
                        //Throw.UserNotExists(command.UserId);
                        throw new InvalidOperationException($"User Not Exists! User Id : {command.UserId}");
                    }
                    var claim = new Claim(command.claimType, command.claimValue);

                    var userClaims = (await UserManager.GetClaimsAsync(user))
                                        .Select(x => x.ToString())
                                        .ToList();

                    if (userClaims.Contains(claim.ToString()))
                    {
                        throw new Exception($"AddClaim {claim.ToString()}");
                    }

                    var result = await UserManager.AddClaimAsync(user, claim);

                    if (result.Succeeded)
                    {
                        return claim.ToString();
                    }
                    else
                    {
                        throw new Exception($"AddClaim {claim.ToString()} command failed");
                    }
                }


            }

        }











        public async Task<ActionResult> OnGetDeleteClaimFromUser(DeleteClaimFromUserCommand command)
        {
			await mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
        }
        public async Task<ActionResult> OnPostDeleteClaimFromUser(DeleteClaimFromUserCommand command)
        {
            await mediator.Send(command);
            return RedirectToPage("Profile", new { UserId = command.UserId });
        }


        public record DeleteClaimFromUserCommand(Guid UserId, string claimType) 
            : IRequest<string>
        {
            public class Validator : AbstractValidator<DeleteClaimFromUserCommand>
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
                        //.Must(x => UserClaimsList.Contains(x.ToString()))
                        ;
                }
            }


            public record Handler(
                     UserManager<AppIdentityUser> UserManager,
                     RoleManager<AppIdentityRole> roleManager)

                     : IRequestHandler<DeleteClaimFromUserCommand, string>
            {

                public async Task<string> Handle(DeleteClaimFromUserCommand command, CancellationToken cancellationToken)
                {
					var user = await UserManager.FindByIdAsync(command.UserId.ToString());
                    if (user == null)
                    {
                        //Throw.UserNotExists(command.UserId);
                        throw new InvalidOperationException($"User Not Exists! User Id : {command.UserId}");
                    }

                    var userClaims = (await UserManager.GetClaimsAsync(user))
                                    .Where(claim => claim.Type == command.claimType)
                                    .ToList();
                    if (userClaims.Count() == 0)
                    {
                        throw new Exception($"The user has no such claims {command.claimType}");
                    }

                    var result = await UserManager.RemoveClaimsAsync(user, userClaims);
                    if (result.Succeeded)
                    {
						return command.claimType;
                    }
                    else
                    {
                        throw new Exception($"Delete Claim {command.claimType} command failed");
                    }
                }


            }

        }


    }


}
