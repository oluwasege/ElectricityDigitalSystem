using EDSAgentPortal.AppData;
using EDSAgentPortal.Interfaces;
using EDSAgentPortal.Menu; 
using ElectricityDigitalSystem.AgentServices;
using ElectricityDigitalSystem.ClientServices;
using ElectricityDigitalSystem.Data;
using ElectricityDigitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDSAgentPortal.Services
{
    public class AgentAccess : IAgentAccess
    {
        AgentService agentService = new AgentService();
        CustomerService customerService = new CustomerService();
        //CustomerAccess customerAccess = new CustomerAccess();

       

        private void UpdateDetails(AgentModel agent)
        {
            Console.WriteLine("\t\tWhat would like to update?\n\n");
            Console.WriteLine("\t\t1 : First Name \n\t\t2 : Last Name\n\t\t3 : Email Address\n\t\t4 : Phone Number");
            Console.Write($"\t\t  : ");
            string entry = Console.ReadLine();
            switch(entry)
            {
                case "1":
                    Console.Write("\t\tNew First Name : ");
                    string newFirstName = Console.ReadLine();
                    while (string.IsNullOrEmpty(newFirstName))
                    {
                        Console.WriteLine("\n\n\t\tFirst name cannot be left blank");
                        Console.Write("\t\tFirst Name : ");
                        newFirstName = Console.ReadLine();
                    }
                    agent.FirstName = newFirstName;
                    agent.ModifiedDateTime = DateTime.Now;
                    agentService.UpdateAgent(agent);
                    Console.Write("\n\n\t\tChanged Successfully");
                    break;
                case "2":
                    Console.Write("\t\tNew Last Name : ");
                    string newLastName = Console.ReadLine();
                    while (string.IsNullOrEmpty(newLastName))
                    {
                        Console.WriteLine("\n\t\tLast name cannot be left blank");
                        Console.Write("\t\tLast Name : ");
                        newLastName = Console.ReadLine();
                    }
                    agent.LastName = newLastName;
                    agent.ModifiedDateTime = DateTime.Now;
                    agentService.UpdateAgent(agent);
                    Console.Write("\n\n\t\tChanged Successfully");
                    break;
                case "3":
                    Console.Write("\t\tNew Email Address : ");
                    string newEmail = Console.ReadLine();
                    while (string.IsNullOrEmpty(newEmail))
                    {
                        Console.WriteLine("\n\t\tEmail cannot be left blank");
                        Console.Write("\t\tEmail : ");
                        newEmail = Console.ReadLine();
                    }
                    agent.EmailAddress = newEmail;
                    agent.ModifiedDateTime = DateTime.Now;
                    agentService.UpdateAgent(agent);
                    Console.Write("\n\n\t\tChanged Successfully");
                    break;

                case "4":
                    Console.Write("\t\tNew Phone Number : ");
                    string NewPhoneNumber = Console.ReadLine();
                    ulong number;
                    while (!ulong.TryParse(NewPhoneNumber, out number))
                    {
                        Console.WriteLine("\n\t\tPlease enter an 11 digit number");
                        Console.Write("\t\tPhone Number : ");
                        NewPhoneNumber = Console.ReadLine();
                    }
                    agent.PhoneNumber = NewPhoneNumber;
                    agent.ModifiedDateTime = DateTime.Now;
                    agentService.UpdateAgent(agent);
                    Console.Write("\n\nChanged Successfully");
                    break;
                default:
                    break;
            }
        }
        public void UpdateAgentInformation()
        {
            var foundCustomer = agentService.GetAgentById(AgentAppData.CurrentAgentId);
            UpdateDetails(foundCustomer);
        }

        public void ViewAgentInformation()
        {

            Console.Clear();
            var foundAgent = agentService.GetAgentById(AgentAppData.CurrentAgentId);
            Console.WriteLine($"\t\t\t\t\t\tWelcome {foundAgent.FirstName} {foundAgent.LastName}\n\n\t\t\t\t\t\tPortal Dashboard");
            Console.WriteLine($"\t\tFirst Name: {foundAgent.FirstName}\n");
            Console.WriteLine($"\t\tLast Name: {foundAgent.LastName}\n");
            Console.WriteLine($"\t\tEmail Address: {foundAgent.EmailAddress}\n");
            Console.WriteLine($"\t\tPhone Number: {foundAgent.PhoneNumber}\n");
           

            Console.WriteLine("\n\t\tDo you want to update your information?\n\t\t1 : Yes\n\t\t2 : No");
            Console.Write($"\t\t  : ");
            string entry = Console.ReadLine();
            switch (entry)
            {
                case "1":
                    UpdateAgentInformation();
                    AgentHomeMenu.AgentContinuation();
                    break;
                case "2":
                    AgentHomeMenu.AgentContinuation();
                    break;
                default:
                    ViewAgentInformation();
                    break;
            }
        }
        public void RemoveCustomer(string IdNumber)
        {
            var customer = customerService.GetCustomerById(IdNumber);
            var customerToBeDeleted = agentService.DeleteCustomer(customer);

            if (customerToBeDeleted == true)
            {
                Console.WriteLine($"\n\t\tYou have Succesfully deleted {customer.FirstName} {customer.LastName} from the Available Customers");
                Security.LongerPrintDotAnimation();

            }
            else
            {
                Console.WriteLine("Customer not Found");
                Security.LongerPrintDotAnimation();
            }
                

           

        } 
        public void ViewCustomersInformation()
        {
            List<CustomerModel> allCustomers = agentService.GetAllCustomer();
            if (allCustomers.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\tThere are no Customers Available");
                AgentHomeMenu.AgentContinuation();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t\t\t\t\t\tCustomers Information\n\n");
                Console.WriteLine("CustomerId\t\t\t\tFirstName\t\tLastName\t\tEmailAddress\t\tPhoneNumber\t\tMeterNumber\tCreatedDateTime\n");
                foreach (var customer in allCustomers)
                {
                    Console.WriteLine($"{customer.Id}\t{customer.FirstName}\t\t\t{customer.LastName}\t\t\t{customer.EmailAddress}\t\t\t{customer.PhoneNumber}\t\t{customer.MeterNumber}\t{customer.CreatedDateTime}\n");

                }

                Console.WriteLine("\t\tDo you want to remove any customer?\n\t\t1 : Yes\n\t\t2 : No");
                Console.Write($"\t\t  : ");
                string entry = Console.ReadLine();
                switch (entry)
                {
                    case "1":
                        Console.WriteLine("\n\t\tPlease provide the Customer's Identification Number");
                        Console.Write($"\t\t  : ");
                        string idNumber = Console.ReadLine();
                        RemoveCustomer(idNumber);
                        AgentHomeMenu.AgentContinuation();
                        break;
                    case "2":
                        AgentHomeMenu.AgentContinuation();
                        break;
                    default:
                        ViewCustomersInformation();
                        break;
                }

            }
           


        }

        public void ViewCustomerSubscriptions()
        {
            Console.Clear();
            Console.WriteLine();
        }

        public void SubscribeForCustomer()
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomerInformation()
        {
            throw new NotImplementedException();
        }

        public void CancelCustomerSubsription()
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
