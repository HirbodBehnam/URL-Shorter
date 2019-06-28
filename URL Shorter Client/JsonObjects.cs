namespace URL_Shorter_Client
{
    public class JsonObjects
    {
        public class Input
        {
            /// <summary>
            /// If true it is a request to shorten the URL; If not, it is a request to get the main url from a shorten url
            /// </summary>
            public bool Shorten { get; set; }
            /// <summary>
            /// URL to shorten or reverse shorten :\
            /// </summary>
            public string Url { get; set; }
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
