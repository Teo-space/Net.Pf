namespace Net.Pf.Infrastructure.Cryptography.Exceptions;


internal class ArgumentIsNullOrEmptyException : ArgumentException
{
    public ArgumentIsNullOrEmptyException(string argumentName) : base($"string {nameof(argumentName)} is null or empty") { }

}

