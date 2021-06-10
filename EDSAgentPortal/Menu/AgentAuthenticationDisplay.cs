using EDSAgentPortal.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDSAgentPortal.Menu
{
    public class AgentAuthenticationDisplay
    {
        public static void LoginScreen()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t\tWelcome To EPMS Agent PORTAL.\n\n");
            Console.WriteLine($"\t\t\t\t\t\t   What would you like to do?\n");
            Console.WriteLine("\t\t1 : Login\n\t\t2 : Register\n\t\t3 : Exit");
            Console.Write($"\t\t  : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AgentAuthenticationService.AgentLogin(); 
                    break;
                case "2":
                    Console.Clear();
                    AgentAuthenticationService.RegisterationForAgents();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    LoginScreen();
                    break;
            }
        }

    }
}
