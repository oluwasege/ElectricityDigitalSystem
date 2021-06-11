using EDSAgentPortal.Services;
using ElectricityDigitalSystem.Data;
using ElectricityDigitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDSAgentPortal.Menu
{
    public class AgentHomeMenu
    {
        public static int CurrentStage { get; set; } = 1;

        public static void HomeMenuDisplay(AgentModel agent)
        {
            bool appIsRunning = true;
            while (appIsRunning)
            {

                while (CurrentStage == 1)
                {

                    Console.Clear();
                    Console.WriteLine($"\t\t\t\t\t\tWelcome {agent.FirstName} {agent.LastName}\n\n\t\t\t\t\t\tPortal Dashboard");
                    Console.WriteLine();
                    Console.WriteLine($"\n\n\t\t1 : View Information\n\n\t\t2 : View registered customers\n\n\t\t3 : View Customer Subscription History\n\n\t\t4 : Exit");
                    Console.Write($"\n\t\t  : ");
                    string selection = Console.ReadLine();
                    Console.Write($"\n\t\tProccessing");
                    Security.PrintDotAnimation();

                    switch (selection)
                    {

                        case "1":
                            AgentAccess agentAccess = new AgentAccess();
                            agentAccess.ViewAgentInformation();
                            break;
                        case "2":
                            AgentAccess agentAccess2 = new AgentAccess();
                            agentAccess2.ViewCustomersInformation();
                            break;
                        case "3":
                            AgentAccess agentAccess3 = new AgentAccess();
                            agentAccess3.ViewCustomerSubscriptionHistory();
                            break;

                        default:
                            break;
                    }
                    //Console.Clear();
                }

            }


        }

        public static void AgentContinuation()
        {
            Console.WriteLine("\n\n\t\t1 : Home Menu\n\t\t2 : Logout\n\t\t3 : Exit");
            Console.Write($"\t\t  : ");
            string optionSelected = Console.ReadLine();
            Console.Write("\n\n\t\tProccessing");
            Security.PrintDotAnimation();
            switch (optionSelected)
            {
                case "1":
                    CurrentStage = 1;
                    break;
                case "2":
                    AgentAuthenticationDisplay.LoginScreen();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    break;

            }
            Console.Clear();


        }
    }
}
