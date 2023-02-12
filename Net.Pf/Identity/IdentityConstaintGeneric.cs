using Microsoft.AspNetCore.Identity;



namespace Net.Pf.Identity;



public class IdentityConstaintGeneric<TKey> where TKey : IEquatable<TKey>
{
    public class AppIdentityUser : IdentityUser<TKey>
    {
    }

    public class AppIdentityRole : IdentityRole<TKey>
    {
    }
}



public class AppIdentity

    : IdentityConstaintGeneric<Guid>
{

}