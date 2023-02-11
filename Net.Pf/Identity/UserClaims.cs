using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Net.Pf.Identity;

public enum UserClaims
{
    None = 0,
    User = 100,
    Premium = 1000,
    Moderator = 10000,

    Administrator = 1000000,



    SoftBootstrap = 5000000



}


public static class UserClaimList
{
    static readonly ReadOnlyCollection<string> userClaimList = 
        Enum.GetNames<UserClaims>().Select(x => x.ToString()).ToList().AsReadOnly();

    public static ReadOnlyCollection<string> Get() => userClaimList;



}
