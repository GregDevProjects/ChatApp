using System;

namespace ChatServer
{
    class Program
    {


        static void Main(string[] args)
        {

            Console.Clear();
            ConsoleServer consoleServer = new ConsoleServer();
            consoleServer.Start();

            Console.WriteLine("**Chat Session ended");

        }

    }
}
