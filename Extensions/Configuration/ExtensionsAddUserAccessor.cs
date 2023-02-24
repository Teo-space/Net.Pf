using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Extensions.Configuration;

public static class ExtensionsAddUserAccessor
{
    public static IServiceCollection AddUserAccessor(this IServiceCollection services)
        => services.AddTransient<IUserAccessor, UserAccessor>();
}


public interface IUserAccessor
{
    ClaimsPrincipal User { get; }

    Guid UserId { get => User.ClaimId(); }
    string UserName { get => User.ClaimName(); }
    string UserEmail { get => User.ClaimEmail(); }
}

internal class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public UserAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    public ClaimsPrincipal User => _accessor.HttpContext.User;


}

