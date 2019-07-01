using System.Text;

namespace URL_Shorter
{
    static class Hashing
    {
        public static string SHA512(string value)
        {
            //https://stackoverflow.com/a/39131803/4213397
            var bytes = Encoding.UTF8.GetBytes(value);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (byte b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
