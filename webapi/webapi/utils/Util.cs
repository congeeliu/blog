using System.Security.Cryptography;
using System.Text;

namespace webapi.utils
{
    public class Util
    {
        public static string GetMD5(string input)
        {
            var byteResult = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            var strResult = BitConverter.ToString(byteResult);
            string res = strResult.Replace("-", "");
            return res;
        }
    }
}
