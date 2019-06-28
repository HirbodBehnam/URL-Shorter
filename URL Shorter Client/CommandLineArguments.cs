using CommandLine;

namespace URL_Shorter_Client
{
    [Verb("short", HelpText = "Shorten a URL")]
    internal class CommandLineArgumentsShorten
    {
        [Option('u',"url",Required = true,HelpText = "URL to shorten")]
        public string Url { get; set; }
        [Option('s',"server",Required = true,HelpText = "The server to connect to.")]
        public string Server { get; set; }
        [Option('p',"port",Required = false,Default = 3303,HelpText = "The port to connect to for server.")]
        public int Port { get; set; }
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }
    [Verb("decode", HelpText = "Shorten a URL")]
    internal class CommandLineArgumentsDecode
    {
        [Option('u',"url",Required = true,HelpText = "URL or token to decode")]
        public string Url { get; set; }
        [Option('s',"server",Required = true,HelpText = "The server to connect to.")]
        public string Server { get; set; }
        [Option('p',"port",Required = false,Default = 3303,HelpText = "The port to connect to for server.")]
        public int Port { get; set; }
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }
}
