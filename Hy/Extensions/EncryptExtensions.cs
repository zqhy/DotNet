using System.Security.Cryptography;
using System.Text;

namespace Hy.Extensions;

public static class EncryptExtensions
{
    public static string Md5(this string source)
    {
        using var md5 = MD5.Create();
        var result = md5.ComputeHash(Encoding.ASCII.GetBytes(source));
        var strResult = BitConverter.ToString(result);
        return strResult.Replace("-", "").ToLower();
    }
}