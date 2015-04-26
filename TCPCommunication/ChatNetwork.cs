using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatLogger;
using Interfaces;

namespace TCPCommunication
{
    public abstract class ChatNetwork 
    {

     //   Log log = new Log();


        public ILoggingService ILogMyChat;


        //port used by client/server
        protected const int PORT = 8080;

        public int PORT1
        {
            get { return PORT; }
        }

        //ip used by client/server
        protected const string IP = "127.0.0.1";

        public string IP1
        {
            get { return IP; }
        }


        //wait for message and return a string 
        protected string RecieveChatMessage(NetworkStream stream)
        {
            while (true)
            {

                Byte[] bytes = new Byte[256];
                String data = null;

                int i;

                // Loop to receive all the data sent by the client. 
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);



                    return data;
                }

            }

        }// end chatmessage




        //sends a message
        protected bool SendChatMessage(string message, NetworkStream stream)
        {
            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
