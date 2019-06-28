using System;

namespace URL_Shorter_Client
{
    class ConsoleHelper
    {
        /// <summary>
        /// Prints an Error to terminal
        /// </summary>
        /// <param name="message">The error message</param>
        public static void WriteError(string message)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error: ");
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }
        /// <summary>
        /// Prints warning to terminal
        /// </summary>
        /// <param name="message">The warning message</param>
        public static void WriteWarning(string message)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Error: ");
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }
        /// <summary>
        /// If verbose is on prints the message
        /// </summary>
        /// <param name="message">The Message</param>
        public static void LogVerbose(string message)
        {
            if (Program.Verbose)
                Console.WriteLine("Verbose: " + message);
        }
    }
}
