using ElectricityDigitalSystem.Models;
using ElectricityDigitalSystem.AgentServices;
using System;
using System.Collections.Generic;
using System.Text;
using ElectricityDigitalSystem.Data;
using EDSAgentPortal.AppData;
using EDSAgentPortal.Menu;

namespace EDSAgentPortal.Services
{
    public class AgentAuthenticationService
    {

        private static AgentService agentService = new AgentService();
        private static AgentModel AttemptingUser = new AgentModel();
        private static AgentModel LoggedInAgent = new AgentModel();

        private static string RegisterAgent(AgentModel agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }
            else
            {
                string email = agentService.RegisterAgent(agent);
                return email == null ? "Failed" : "Success";

            }
        }

        public static void RegisterationForAgents()
        {
            Dictionary<string, string> navItemDIc = new Dictionary<string, string>();
            List<string> navigationItems = new List<string>
            {
                "FirstName", "LastName", "Email", "Password", "PhoneNumber"
            };
            Console.Clear();
            Console.WriteLine("\t\tPlease Provide your Details");

            for (int i = 0; i < navigationItems.Count; i++)
            {
                Console.Write($"\t\t{navigationItems[i]} : ");
                var value = Console.ReadLine();

                navItemDIc.Add(navigationItems[i], value);
            }

            string FirstName, LastName, Email, Password, PhoneNumber;

            FirstName = navItemDIc["FirstName"];
            LastName = navItemDIc["LastName"];
            Email = navItemDIc["Email"];
            Password = navItemDIc["Password"];
            PhoneNumber = navItemDIc["PhoneNumber"];
            while (string.IsNullOrEmpty(FirstName))
            {
                Console.WriteLine("\n\n\t\tFirst name cannot be left blank");
                Console.Write("\t\tFirst Name : ");
                FirstName = Console.ReadLine();
            }

            while (string.IsNullOrEmpty(LastName))
            {
                Console.WriteLine("\n\t\tLast name cannot be left blank");
                Console.Write("\t\tLast Name : ");
                LastName = Console.ReadLine();
            }

            while (string.IsNullOrEmpty(Email))
            {
                Console.WriteLine("\n\t\tEmail cannot be left blank");
                Console.Write("\t\tEmail : ");
                Email = Console.ReadLine();
            }

            while (string.IsNullOrEmpty(Password))
            {
                Console.WriteLine("\n\t\tPassword cannot be left blank");
                Console.Write("\t\tPassword : ");
                Password = Console.ReadLine();
            }

            ulong number;
            while (!ulong.TryParse(PhoneNumber, out number))
            {
                Console.WriteLine("\n\t\tPlease enter an 11 digit number");
                Console.Write("\t\tPhone Number : ");
                PhoneNumber = Console.ReadLine();
            }


            navItemDIc.Add("Firstname", FirstName);
            navItemDIc.TryAdd("LastName", LastName);
            navItemDIc.TryAdd("LastName", LastName);
            navItemDIc.TryAdd("Email", Email);
            navItemDIc.TryAdd("Password", Password);
            navItemDIc.TryAdd("PhoneNumber", number.ToString("00000000000"));


            //public int SeatNumber => r.Next(1, 19);
            AgentModel model = new AgentModel
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = FirstName,
                LastName = LastName,

                EmailAddress = Email,
                Password = Password,
                
                PhoneNumber = number.ToString("00000000000"),
            };

            string registrationResponds = RegisterAgent(model);
            if (registrationResponds == "Success")
            {
                Console.WriteLine("\t\tRegistration Successful");
                Console.Write("\t\tRedirecting you to Home Page");
                Security.PrintDotAnimation();
                AgentAuthenticationDisplay.LoginScreen();

            }
            else
            {

                Console.WriteLine("An Error occured While Trying to Create your Account Please try Again");
                Security.PrintDotAnimation();

                AgentAuthenticationDisplay.LoginScreen();
            }

        }
        public static void AgentLogin()
        {
            bool IsLoggedIn = false;
            while (!IsLoggedIn)
            {
                Console.WriteLine("\t\t\t\t\t\tWelcome To EPMS Agent PORTAL.\n\n");
                Console.WriteLine("\t\t\t\t\tLogin with your Email and Password");
                Console.Write("\t\tEmail : ");
                string Emailvalue = Console.ReadLine();
                Console.WriteLine();
                Console.Write("\t\tPassword : ");
                string PasswordValue = Security.ReadPassword();
                Console.Write("\n\n\t\tProccessing");
                Security.PrintDotAnimation();
                Console.Clear();

                AttemptingUser = agentService.GetAgentByEmail(Emailvalue);
                if (AttemptingUser != null)
                {
                    if (PasswordValue == AttemptingUser.Password)
                    {
                        LoggedInAgent = AttemptingUser;

                        AgentAppData.CurrentAgentId = LoggedInAgent.Id;
                        AgentHomeMenu.HomeMenuDisplay(LoggedInAgent);
                        //HomeMenu.CurrentStage = 2;
                        
                        IsLoggedIn = true;
                    }
                    else
                    {
                        IsLoggedIn = false;
                        Console.Clear();
                        Console.WriteLine("\t\tUsername or Password does not exist\n\t\tCreate an account");
                        Console.Write($"\n\t\tProccessing");
                        Security.LongerPrintDotAnimation();
                        AgentAuthenticationDisplay.LoginScreen();
                    }

                }
                else
                {
                    IsLoggedIn = false;
                    Console.Clear();
                    Console.WriteLine("\t\tUsername or Password does not exist\n\t\tCreate an account");
                    Console.Write($"\n\t\tProccessing");
                    Security.LongerPrintDotAnimation();

                    AgentAuthenticationDisplay.LoginScreen();

                }



            }



        }

    }
}
