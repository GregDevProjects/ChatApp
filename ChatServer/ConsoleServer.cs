using System;
using TCPCommunication;
using System.Threading;



namespace ChatServer
{
    /// <summary>
    ///Authored by: Greg McLean 
    ///Purpose: Asycronous chat application between console and form
    /// </summary>
    class ConsoleServer
    {

        

        private bool endSession = false;
        Server server;
        Thread recieveMessageTread;

        public void Start()
        {
            server = new Server();
            StartListening();
            RecieveClientConnection();
            startChatSession();
        }


        public void StartListening()
        {


            if (server.StartListener())
            {

                Console.WriteLine("Listenting on " + server.IP1 + " at port " + server.PORT1);

            }
            else
            {
                Console.WriteLine("Could not Start listener");
            }

        }

        public void RecieveClientConnection()
        {
            if (server.ConnectToClient())
            {
                Console.WriteLine("Connected");

            }

        }

        //starts custom eventarg
        private void startChatSession()
        {
            
           
            server.MessageRecieved += new MessageReceiveEventHandler(RecieveMessageListener);
            recieveMessageTread
               = new Thread(new ThreadStart(server.RecieveClientMessage));

            recieveMessageTread.Name = "ChatListenerThreadServer";
            //makes this a thread dependant on the UI
            //if the UI dies this tread should die too 
            recieveMessageTread.IsBackground = true;
            recieveMessageTread.Start();


            while (true)
            {
                try
                {
                    if (!endSession)
                    {
                        string input = Console.ReadLine();
                        server.SendServerMessage(input);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }

       //     Console.WriteLine("**reached end of chatsession void");
       //     Console.ReadLine();
        }

        void RecieveMessageListener(IncommingMessageEventArgs e)
        {
            if (e.Connected)
            {
                Console.WriteLine(e.Message);
            }
            else
            {
                Console.WriteLine("**Client disconnected, chat session over");
                Console.WriteLine("**Press any key to exit");
                endSession = true;
              //  recieveMessageTread.Join();
                
            }
        }

     
     

        }//end class


    }//end namespace

