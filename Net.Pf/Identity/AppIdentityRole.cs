using Microsoft.AspNetCore.Identity;


namespace Net.Pf.Identity;


public class AppIdentityRole : IdentityRole<Guid>
{
    public AppIdentityRole() { }
    public AppIdentityRole(string roleName) { base.Name = roleName; }




}
