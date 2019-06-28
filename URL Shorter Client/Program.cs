using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Newtonsoft.Json;

namespace URL_Shorter_Client
{
    class Program
    {
        public static bool Verbose; 
        static async Task Main(string[] args)
        {
            bool shorten = true;
            string server = "localhost",url = "";
            int port = 3303;
            Parser.Default.ParseArguments<CommandLineArgumentsShorten,CommandLineArgumentsDecode>(args)
                .WithParsed<CommandLineArgumentsShorten>(opt =>
                {
                    shorten = true;
                    server = opt.Server;
                    port = opt.Port;
                    url = opt.Url;
                    Verbose = opt.Verbose;
                })
                .WithParsed<CommandLineArgumentsDecode>(opt =>
                {
                    shorten = false;
                    server = opt.Server;
                    port = opt.Port;
                    url = opt.Url;
                    Verbose = opt.Verbose;
                })
                .WithNotParsed(error => Environment.Exit(2));
            try
            {
                TcpClient client = new TcpClient(server, port);
                string toSend;
                {
                    JsonObjects.Input input = new JsonObjects.Input {Shorten = shorten, Url = url};
                    toSend = JsonConvert.SerializeObject(input);
                    ConsoleHelper.LogVerbose("Sending server: " + toSend);
                }
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(toSend);
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                    ConsoleHelper.LogVerbose("Sent: " + toSend);
                    buffer = new byte[256];
                    int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string responseData = Encoding.UTF8.GetString(buffer, 0, bytes);
                    ConsoleHelper.LogVerbose("Received: " + responseData);
                    JsonObjects.Response response = JsonConvert.DeserializeObject<JsonObjects.Response>(responseData);
                    if (!response.OK)
                        ConsoleHelper.WriteError("There was an error from server side: " + response.Result);
                    else
                    {
                        Console.WriteLine("Result:");
                        Console.WriteLine(response.Result);
                    }
                }
            }catch(Exception e) 
            {
                ConsoleHelper.WriteError(e.ToString());
            }
        }
    }
}
