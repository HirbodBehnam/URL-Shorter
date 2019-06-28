using CommandLine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;

namespace URL_Shorter
{
    class Program
    {
        private static string _dbPath;
        private static SQLiteConnection _db;
        private static TcpListener _server;
        public static bool Verbose;
        static void Main(string[] args)
        {
            int port = 3303;
            string bindAddress = "0.0.0.0";
            Parser.Default.ParseArguments<CommandLineArguments>(args)
                .WithParsed(opts =>
                {
                    Verbose = opts.Verbose;
                    _dbPath = string.IsNullOrWhiteSpace(opts.dbPath)
                        ? Path.Combine(Directory.GetCurrentDirectory(), "url.db")
                        : opts.dbPath;
                    port = opts.Port;
                    bindAddress = opts.Bind;
                })
                .WithNotParsed(errs =>
                {
                    Environment.Exit(2);
                });
            try
            {
                _db = new SQLiteConnection(_dbPath);
                ConsoleHelper.LogVerbose("DB path is " + _dbPath);
                _db.CreateTable<Database>();
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError(ex.Message);
                Environment.Exit(1);
            }
            Console.WriteLine("Database loaded.");
            Console.WriteLine("Press Ctrl+C to exit");
            new Task(async ()=>await RunServer(bindAddress,port)).Start();
            while (true)
                Thread.Sleep(int.MaxValue);
        }

        private static async Task RunServer(string bind,int port)
        {
            try
            {
                _server = new TcpListener(IPAddress.Parse(bind),port);
                _server.Start();
                while (true)
                {
                    TcpClient client = await _server.AcceptTcpClientAsync();
                    new Task(() =>
                    {
                        ConsoleHelper.LogVerbose("Connection accepted.");
                        using (NetworkStream stream = client.GetStream())
                        {
                            StringBuilder sb = new StringBuilder(); 
                            byte[] buffer = new byte[128];
                            do
                            {
                                int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
                                sb.Append(Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead));
                            } while (stream.DataAvailable); //Read whole buffer
                            ConsoleHelper.LogVerbose("Received " + sb);
                            JsonObjects.Response result;
                            try
                            {
                                JsonObjects.Input input = JsonConvert.DeserializeObject<JsonObjects.Input>(sb.ToString());
                                if (input.Shorten)
                                {
                                    //Shorten Url
                                    if(!CheckUrl(input.Url))
                                        throw new InvalidDataException("The URL you send is not a URL");
                                    Database db = new Database {Target = input.Url};
                                    _db.Insert(db);
                                    result = new JsonObjects.Response
                                        {OK = true, Result = db.Id.ToString().EncodeAsBase32String(false)};
                                }
                                else
                                {
                                    //Decode Url
                                    int index = int.Parse(input.Url.DecodeFromBase32String());
                                    Database db = _db.Get<Database>(index);
                                    if (db == null)
                                        throw new InvalidDataException(
                                            "Cannot find a entry with key of " + input.Url.DecodeFromBase32String());
                                    //Read from db
                                    result = new JsonObjects.Response {OK = true, Result = db.Target};
                                }
                            }
                            catch (Exception ex)
                            {
                                result = new JsonObjects.Response
                                    {OK = false, Result = ex.Message};
                            }
                            buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                            ConsoleHelper.LogVerbose("Sent message: "+ result.Result);
                            stream.Write(buffer,0,buffer.Length);
                        }
                        client.Close();
                    }).Start();
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError(ex.Message);
                Environment.Exit(1);
            }
        }

        //https://stackoverflow.com/a/7581824/4213397
        private static bool CheckUrl(string test)
        {
            Uri uriResult;
            return Uri.TryCreate(test, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
