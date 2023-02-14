namespace Net.Pf.Exceptions;

public class UserNotExistsException : Exception
{
	public UserNotExistsException(string message) : base($"id : {message}") { }
}



/// <summary>
/// The user already has this claim!
/// </summary>
public class UserHasClaim : Exception
{
	/// <summary>
	/// The user already has this claim!
	/// </summary>
	/// <param name="UserId"></param>
	public UserHasClaim(object UserId) : base($"The user already has this claim! User Id : {UserId?.ToString()}") { }
}




public static class Throw
{
	public static void UserNotExists(string message) => throw new UserNotExistsException(message);

	public static void UserNotExists(object o) => throw new UserNotExistsException(o?.ToString());

	/// <summary>
	/// The user already has this claim!
	/// </summary>
	/// <param name="o"></param>
	/// <exception cref="UserHasClaim"></exception>
	public static void UserHasClaims(object o) => throw new UserHasClaim(o?.ToString());



}



