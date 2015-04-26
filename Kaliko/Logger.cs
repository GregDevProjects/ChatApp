using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using SimpleLogger.Logging;
using SimpleLogger.Logging.Formatters;

namespace GregSimpleLogger
{
    public class SimpleLoggerImplementation:ILoggingService
    {

        public void Log(string message)
        {
            SimpleLogger.Logger.LoggerHandlerManager
            .AddHandler(new ConsoleLoggerHandler())
            .AddHandler(new FileLoggerHandler())
            .AddHandler(new DebugConsoleLoggerHandler());

            SimpleLogger.Logger.DebugOn();

           SimpleLogger.Logger.Debug.Log(message);

        }


      

         
    }
}
