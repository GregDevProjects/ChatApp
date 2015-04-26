using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatLogger;
using Interfaces;

namespace TCPCommunication
{
    public class Server : ChatNetwork
    {
        TcpListener listener;
        NetworkStream stream;
      //  Log log;

        //public Server(ILoggingService ILog)
        //{
        //    this.ILogMyChat = ILog;

        //}

        public MessageReceiveEventHandler MessageRecieved;

        //returns true if TCP listener started
        public bool StartListener()
        {

            try
            {
                IPAddress serverListenIp = IPAddress.Parse(IP1);
                listener = new TcpListener(serverListenIp, PORT1);
                listener.Start();
            }
            catch (Exception e)
            {
                return false;
            }


            return true;
        }


        //waits for an incomming client connection after listener is started
        //returns true upon connection
        public bool ConnectToClient()
        {

            try
            {
                TcpClient client = listener.AcceptTcpClient();
                stream = client.GetStream();
            }
            catch (Exception e)
            {
                return false;
            }



            return true;
        }

        //wait for message and return a string 
        //public String RecieveClientMessage()
        //{
        //    return RecieveChatMessage(stream);
        //}

        public void SendServerMessage(string outGoingMessage)
        {
            SendChatMessage(outGoingMessage, stream);
        }

        public void  RecieveClientMessage()
        {
            while (true)
            {
                try
                {
                    string incommingMessage = RecieveChatMessage(stream);
                    //raising an event
                    MessageRecieved(new IncommingMessageEventArgs(incommingMessage, true));
                }
                catch
                {
                    //handles an exit from the client
                    MessageRecieved(new IncommingMessageEventArgs("", false));
                    break;
                }
            }
        }



    }//end class

}//end namespace