using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPCommunication;
using Interfaces;

//exits to handle:
//server exits by pressing x
//client exits by pressing x or clicking exit 
namespace As2Gui
{

    /// <summary>
    ///Authored by: Greg McLean 
    ///Purpose: Asycronous chat application between console and form
    /// </summary>
    public partial class FormNetworkGame : Form
    {
        TCPCommunication.Client client;
        Thread recieveMessageTread;
        bool clearedConectionMessage = false;


        private readonly ILoggingService ILogsMyChat;

        public FormNetworkGame(ILoggingService LogMethod)
        {
            InitializeComponent();
            this.ILogsMyChat = LogMethod;

            client = new Client(ILogsMyChat);

            client.MessageRecieved += new MessageReceiveEventHandler(RecieveMessageListener);

        }

        //when connect button is hit
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(client.IsConnected())
            {

                textBoxConversation.Text = "**Connected to Server";
                clearedConectionMessage = false;
                recieveMessageTread
                 = new Thread(new ThreadStart(client.RecieveServerMessage));

                recieveMessageTread.Name = "ChatListenerThread";
                //makes this a thread dependant on the UI
                //if the UI dies this tread should die too 
                recieveMessageTread.IsBackground = true;
                recieveMessageTread.Start();

            }
        }

        private void RecieveMessageListener(IncommingMessageEventArgs e)
        {



            MethodInvoker myMethod =
                   new MethodInvoker(delegate
                   {
                       //clear connected message if haven't alread
                       if (!clearedConectionMessage)
                       {
                           textBoxConversation.Clear();
                           clearedConectionMessage = true;
                       }

                       //check if connection is active
                       if (e.Connected)
                       {
                           string textBoxOutput = e.Message + Environment.NewLine + textBoxConversation.Text;
                           textBoxConversation.Text = textBoxOutput;
                       }
                       else
                       {
                           textBoxConversation.Text = "**Server Disconnected" + Environment.NewLine + textBoxConversation.Text;
                       }

                   });



            textBoxConversation.Invoke(myMethod);

          //  textBoxConversation.Text = e.Message;
        }

        //when send button is clicked
        private void buttonSend_Click(object sender, EventArgs e)
        {
            string userInput = textBoxMessageSend.Text;
            textBoxMessageSend.Clear();
            if (client.SendClientMessage(userInput))
            {
                if (!clearedConectionMessage)
                {
                    textBoxConversation.Clear();
                    clearedConectionMessage = true;
                }
                string textBoxOutput = ">> " + userInput + Environment.NewLine + textBoxConversation.Text;
                textBoxConversation.Text = textBoxOutput;
            }

        }

        //when exit button is clicked
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //call to log
            client.clientExits();
            //make sure thread is ended
            recieveMessageTread.Abort();
            //exit form
            this.Close();
        }

        //when disconnect button is clicked
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //call to log
            client.clientExits();
            //make sure thread is ended
            recieveMessageTread.Abort();
            //clear textbox
            textBoxConversation.Clear();

        }




    }
}
