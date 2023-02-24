using System.Security.Claims;

namespace Extensions;


public static class ExtensionsClaims
{
    public static string Claim(this ClaimsPrincipal principal, string claimType) => principal.FindFirstValue(claimType);

    public static string ClaimName(this ClaimsPrincipal principal) 
        => principal.Claim(ClaimTypes.Name);

    public static string ClaimEmail(this ClaimsPrincipal principal) 
        => principal.Claim(ClaimTypes.Email);

    public static string ClaimNameIdentifier(this ClaimsPrincipal principal)
        => principal.Claim(ClaimTypes.NameIdentifier);

    public static Guid ClaimId(this ClaimsPrincipal principal) => 
        Guid.Parse(principal.Claim(ClaimTypes.NameIdentifier));




}
