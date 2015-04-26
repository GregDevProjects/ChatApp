using System;
using System.Windows.Forms;
using ChatLogger;
using Microsoft.Practices.Unity;
using Interfaces;
using ChatLogger;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
//using CortlandsLogger_Nlog;
//using GregSimpleLogger;
//using NLog;

namespace As2Gui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


     //UNCOMMENT THIS SECTION FOR UNITY CONTAINER   
     //       UnityContainer Container = new UnityContainer();
     //       Container.RegisterType<ILoggingService, ChatLog>();



    //UNCOMMENT THIS SECTION FOR WINDSOR CONTAINER 
            var Container = new WindsorContainer();
            Container.Register(Component.For<FormNetworkGame>());

            //USE FILE LOGGER I MADE
         //   Container.Register(Component.For<ILoggingService>().ImplementedBy<ChatLog>());

            //USE SIMPLE CHAT LOGGER
            //Container.Register(Component.For<ILoggingService>().ImplementedBy<SimpleLoggerImplementation>());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



    //UNCOMMENT THIS SECTION TO RUN FROM CONAINER
            Application.Run(Container.Resolve<FormNetworkGame>());

    //UNCOMMENT THIS TO RUN CORTLANDS DLL
     //       Application.Run(new FormNetworkGame(new CortlandsLogger_Nlog.CortlandsNLog()));


        

        }
    }
}
