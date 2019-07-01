namespace URL_Shorter_Client
{
    public class JsonObjects
    {
        public class Input
        {
            private string _hashedPassword;
            /// <summary>
            /// If true it is a request to shorten the URL; If not, it is a request to get the main url from a shorten url
            /// </summary>
            public bool Shorten { get; set; }
            /// <summary>
            /// URL to shorten or reverse shorten :\
            /// </summary>
            public string Url { get; set; }
            /// <summary>
            /// Hashed password that is send through internet
            /// </summary>
            public string HashedPassword {  
                get => _hashedPassword;
                set
                {
                    if (value == null)
                    {
                        _hashedPassword = null;
                        return;
                    }
                    //https://stackoverflow.com/a/39131803/4213397
                    var bytes = System.Text.Encoding.UTF8.GetBytes(value);
                    using (var hash = System.Security.Cryptography.SHA512.Create())
                    {
                        var hashedInputBytes = hash.ComputeHash(bytes);
                        var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                        foreach (byte b in hashedInputBytes)
                            hashedInputStringBuilder.Append(b.ToString("X2"));
                        _hashedPassword = hashedInputStringBuilder.ToString();
                    }
                }  
            }
        }
        public class Response
        {
            /// <summary>
            /// If true the operation was successful and you can get the result in <see cref="Result"/> value
            /// </summary>
            public bool OK { get; set; }
            /// <summary>
            /// If true the result URL, otherwise the error message
            /// </summary>
            public string Result { get; set; }
        }
    }
}
