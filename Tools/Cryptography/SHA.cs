using Tools.Cryptography.Exceptions;
using System.Security.Cryptography;
using Tools.Serialization;

namespace Tools.Cryptography;


public static class SHA
{
    //static SHA512CryptoServiceProvider SHA512Provider = SHA512CryptoServiceProvider.Create();
    static SHA512 SHA512Provider = System.Security.Cryptography.SHA512.Create();

    //static SHA256CryptoServiceProvider SHA256Provider = new SHA256CryptoServiceProvider();
    static SHA256 SHA256Provider = System.Security.Cryptography.SHA256.Create();


    public static string SHA512(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        return BitConverter.ToString(SHA512Provider
        .ComputeHash(Serialization.Encoding.GetBytes(data))).Replace("-", null);
    }


    public static string SHA256(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentIsNullOrEmptyException(nameof(data));
        }
        return BitConverter.ToString(SHA256Provider
            .ComputeHash(Serialization.Encoding.GetBytes(data))).Replace("-", null);
    }

}
