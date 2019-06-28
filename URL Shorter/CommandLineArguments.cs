using CommandLine;

namespace URL_Shorter
{
    class CommandLineArguments
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
        [Option("db",Required = false, HelpText = "Path to the database file.")]
        public string dbPath { get; set; }
        [Option('p', "port",Default = 3303,Required = false, HelpText = "Set the port that server must listen on it.")]
        public int Port { get; set; }
        [Option('b', "bind",Default = "0.0.0.0",Required = false, HelpText = "Set the IP to listen on.")]
        public string Bind { get; set; }
    }
}
