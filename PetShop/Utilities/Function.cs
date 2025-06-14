using System.Security.Cryptography;
using System.Text;

namespace PetShop.Utilities
{
    public class Function
    {
        public static int _AccountId = 0;
        public static string _Avatar = String.Empty;
        public static string _FullName = String.Empty;
        public static string _Phone = String.Empty;
        public static string _Email = String.Empty;
        public static string _Birthday = String.Empty;
        public static string _Address = String.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;
        public static int _CustomerId = 0;

        public static string TitlesluGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        public static string MD5Password(string? text)
        {
            string str = MD5Hash(text);
            //Lặp thêm 5 lần mã hóa xâu đảm bảo tính bảo mật
            //Mỗi lần lặp nhân đôi xâu mã hóa, ở giữa thêm "_"
            for (int i = 0; i <= 5; i++)
                str = MD5Hash(str + "_" + str);
            return str;
        }
        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Function._FullName) || string.IsNullOrEmpty(Function._Email) || (Function._AccountId <= 0))
                return false;
            return true;
        }
    }
}
