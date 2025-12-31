using System.Security.Cryptography;
using System.Text;

namespace InvCore360_App.BL.Utils
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            if (password == null) return string.Empty;
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
