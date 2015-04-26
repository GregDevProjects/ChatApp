using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPCommunication
{
    public class IncommingMessageEventArgs : EventArgs
    {

        private string message;
        private bool activeConnecion;

        public bool Connected
        {
            get { return activeConnecion; }
            set { activeConnecion = value; }
        }


        public IncommingMessageEventArgs(string value, bool connected)
        {
            message = value;
            activeConnecion = connected;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }

    }
}
