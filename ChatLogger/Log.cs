using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace ChatLogger
{
    public class ChatLog :ILoggingService
    {
        string logStartTime;
        //string array that will be written to text file
        List<string> lines;
        /// <summary>
        ///Authored by: Greg McLean 
        ///Purpose: Asycronous chat application between console and form
        /// </summary>
        public ChatLog()
        {
            //create new list to hold the log lines
            lines = new List<string> { };
            //get current date that will be used for log filename
            System.DateTime moment = new System.DateTime();
            moment = DateTime.Now;
            int hour = moment.Hour;
            int minuite = moment.Minute;
            int second = moment.Second;


            logStartTime = hour.ToString() + minuite.ToString() + second.ToString();

        }


        public void Log(string line)
        {
            string logLine = DateTime.Now.ToString() + " - " + line;

            lines.Add(logLine);

            System.IO.File.WriteAllLines("ChatClientLog-" + logStartTime + ".txt", lines);
        }


    }
}
