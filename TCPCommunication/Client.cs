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
    public class Client : ChatNetwork
    {
    


        public Client(ILoggingService Ilog)
        {
            this.ILogMyChat = Ilog;
        }


        public MessageReceiveEventHandler MessageRecieved;

        NetworkStream stream;
      //  Log log;
        //returns true if connection is established
        //false if not able to
       public bool IsConnected()
        {
            try
            {
                TcpClient client = new TcpClient(IP1, PORT1);

                if (client.Connected)
                {
                    stream = client.GetStream();

             //       log = new Log();
                    ILogMyChat.Log("Client Connected to server at port " + IP + " at port " + PORT);

                    return true;
                }
                else
                {
                    return false;
                }
            }
           catch(Exception e)
            {
                return false;
            }

        }//end connection request

       //wait for message and return a string 
       public  void RecieveServerMessage()
       {
           try
           {
               while (true)
               {
                   string incommingMessage = RecieveChatMessage(stream);
                   //raising an event
                   MessageRecieved(new IncommingMessageEventArgs(incommingMessage, true));

                   ILogMyChat.Log("Server: " + incommingMessage);
                
               }
           }
           catch
           {
               //if server goes offline
               ILogMyChat.Log("Server disconnected. Chat session ended");
               MessageRecieved(new IncommingMessageEventArgs("", false));
           }
       }

       public bool SendClientMessage(string outGoingMessage)
       {

           ILogMyChat.Log("Client: " + outGoingMessage);
          return SendChatMessage(outGoingMessage, stream);

         
       }

        public void clientExits()
       {
           try
           {
               ILogMyChat.Log("Client disconnected. Chat session ended");
               SendClientMessage("**Client has disconnected");
               stream.Close();
           }
            catch
           {

           }
       }

    }
}
