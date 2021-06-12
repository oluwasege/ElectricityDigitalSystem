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
        static AgentService agentService = new AgentService();
        CustomerService customerService = new CustomerService();
        List<CustomerModel> allCustomers = agentService.GetAllCustomer();
        SubscriptionService subscriptionService = new SubscriptionService();
        TariffService tariffService = new TariffService();
        decimal t1, t2, t3, t4 = default;
        string a1, a2, a3, a4 = default;
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
        public void RemoveCustomer()
        {
            Console.Clear();
            Console.Write("\t\tEnter Customer Email to be deleted : ");
            string customerEmail = Console.ReadLine();
            var customer = customerService.GetCustomerByEmail(customerEmail);
            var customerToBeDeleted = agentService.DeleteCustomer(customer);

            if (customerToBeDeleted == true)
            {
                Console.WriteLine($"\n\t\tYou have Succesfully deleted {customer.FirstName} {customer.LastName} from the Available Customers");
                Security.LongerPrintDotAnimation();
                AgentHomeMenu.AgentContinuation();

            }
            else
            {
                Console.WriteLine("Customer not Found");
                Security.LongerPrintDotAnimation();
                AgentHomeMenu.AgentContinuation();
            }
                

           

        } 
        public void ViewCustomersInformation()
        {
            
            if (allCustomers.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("\t\tThere are no Customers Available");
                AgentHomeMenu.AgentContinuation();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t\tCustomers Information\n\n");
                Console.WriteLine("FirstName\tLastName\tEmailAddress\tPhoneNumber\tMeterNumber\tCreatedDateTime\n");

               
                foreach (var customer in allCustomers)
                {
                  Console.WriteLine($"{customer.FirstName}\t\t{customer.LastName}  \t{customer.EmailAddress}\t\t{customer.PhoneNumber}\t{customer.MeterNumber}\t{customer.CreatedDateTime}\n");
                }
                AgentHomeMenu.AgentContinuation();
                //    Console.WriteLine("\t\tDo you want to remove any customer?\n\t\t1 : Yes\n\t\t2 : No");
                //    Console.Write($"\t\t  : ");
                //    string entry = Console.ReadLine();
                //    switch (entry)
                //    {
                //        case "1":
                //            Console.WriteLine("\n\t\tPlease provide the Customer's email address");
                //            Console.Write($"\t\t  : ");
                //            string customerEmail = Console.ReadLine();
                //            RemoveCustomer();
                //            AgentHomeMenu.AgentContinuation();
                //            break;
                //        case "2":
                //            AgentHomeMenu.AgentContinuation();
                //            break;
                //        default:
                //            ViewCustomersInformation();
                //            break;
                //    }

            }
           
        }

        public void ViewCustomerSubscriptionHistory()
        {
            Console.Clear();
            Console.Write("\t\tEnter Customer email address : ");
            string customerEmail = Console.ReadLine();
            Console.Write("\t\tProcessing");
            Security.LongerPrintDotAnimation();
            Console.Clear();
            //string tariffId = default;
            var foundCustomer = customerService.GetCustomerByEmail(customerEmail);
            var subscriptions = subscriptionService.GetCustomerSubscription(foundCustomer.Id);
            if(foundCustomer!=null)
            {
                if (subscriptions.Count==0)
                {
                    Console.WriteLine($"\t\t{foundCustomer.FirstName} has not made any subscription");
                    AgentHomeMenu.AgentContinuation();

                }
                else
                {
                    Console.WriteLine($"\t\t{foundCustomer.FirstName}'s subscription history\n\n");
                    Console.WriteLine("\t\tTariff Name\t\t   Amount\t\tSubscription Date\t\tSubscription Status\n\n");
                    foreach (var subscription in subscriptions)
                    {
                        string tariffId = subscription.TariffId;
                        var customerTariff = tariffService.GetTarriffById(tariffId);
                        Console.Write($"\t\t{customerTariff.Name}\t   #{subscription.Amount}\t\t{subscription.SubcriptionDateTime}\t\t{subscription.SubscriptionStatus}\n\n");
                    }
                    AgentHomeMenu.AgentContinuation();
                }

            }
            else
            {
                Console.WriteLine("\t\tCustomer does not exist");
                AgentHomeMenu.AgentContinuation();
            }
            
        }

        public void SubscribeForCustomer()
        {
            Console.Clear();
            Console.Write("\t\tEnter Customer email address : ");
            string customerEmail = Console.ReadLine();
            Console.Write("\t\tProcessing");
            Security.LongerPrintDotAnimation();
            Console.Clear();
            
            var customer = customerService.GetCustomerByEmail(customerEmail);
            var activeSub = subscriptionService.CheckActiveSubscription(customer.Id);

            if (activeSub.Count != 0)
            {
                Console.WriteLine($"\t\t{customer.FirstName} currently has an active subscription \n\t\tBuying a new Subscription will deactivate previous subscription");
                Console.WriteLine("\n\t\t1 : Continue\n\t\t2 : Back to Home ");
                Console.Write($"\t\t  : ");
                string entry = Console.ReadLine();
                Console.Write($"\n\t\tProccessing");
                Security.PrintDotAnimation();
                switch (entry)
                {
                    case "1":
                        MakeSubscriptionPaymentForCustomer(customer);
                        break;
                    case "2":
                        AgentHomeMenu.CurrentStage = 1;
                        break;
                    default:
                        SubscribeForCustomer();
                        break;
                }
            }
            else
            {
                MakeSubscriptionPaymentForCustomer(customer);
            }
        }

        public void UpdateCustomerInformation()
        { 
            Console.Write("\t\tEnter Customer email :");
            string customerEmail = Console.ReadLine();
            var customer = customerService.GetCustomerByEmail(customerEmail);
            if(customer !=null)
            {
                Console.WriteLine("\t\tWhat would like to update?\n\n");
                Console.WriteLine("\t\t1 : First Name \n\t\t2 : Last Name\n\t\t3 : Email Address\n\t\t4 : Phone Number");
                Console.Write($"\t\t  : ");
                string entry = Console.ReadLine();
                switch (entry)
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
                        customer.FirstName = newFirstName;
                        customer.ModifiedDateTime = DateTime.Now;
                        customerService.UpdateCustomer(customer);
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
                        customer.LastName = newLastName;
                        customer.ModifiedDateTime = DateTime.Now;
                        customerService.UpdateCustomer(customer);
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
                        customer.EmailAddress = newEmail;
                        customer.ModifiedDateTime = DateTime.Now;
                        customerService.UpdateCustomer(customer);
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
                        customer.PhoneNumber = NewPhoneNumber;
                        customer.ModifiedDateTime = DateTime.Now;
                        customerService.UpdateCustomer(customer);
                        Console.Write("\n\nChanged Successfully");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("\t\tCustomer does not exist");
                AgentHomeMenu.AgentContinuation();
            }
            
        }

        public void CancelCustomerSubscription()
        {
            Console.Clear();
            Console.Write("\t\tEnter Customer email address : ");
            string customerEmail = Console.ReadLine();
            Console.Write("\t\tProcessing");
            Security.LongerPrintDotAnimation();
            Console.Clear();
            var customer = customerService.GetCustomerByEmail(customerEmail);
            Console.WriteLine($"\t\tDo you want to cancel {customer.FirstName}'s active subscription?\n\t\t1 : Yes\n\t\t2 : No");
            Console.Write($"\t\t  : ");
            var entry = Console.ReadLine();
            switch (entry)
            {
                case "1":
                    CancelCustomerSubscription(customer);
                    break;
                case "2":
                    AgentHomeMenu.CurrentStage = 1;
                    break;
                default:
                    CancelCustomerSubscription();
                    break;
            }
        }

        private void CancelCustomerSubscription(CustomerModel customer)
        {
            
            var active = subscriptionService.CheckActiveSubscription(customer.Id);
            if (active.Count == 0)
            {
                Console.WriteLine($"\n\n\t\t{customer.FirstName}does not have an Active subscription");
                AgentHomeMenu.AgentContinuation();
            }
            else
            {
                foreach (var item in active)
                {
                    item.SubscriptionStatus = "Inactive";
                    subscriptionService.UpdateSubscription(item);
                    Console.WriteLine("\n\n\t\tSubscription cancelled successfully");

                }
                AgentHomeMenu.AgentContinuation();
            }
        }

  


        private void MakeSubscriptionPaymentForCustomer(CustomerModel customer)
        {
            Console.Clear();
            var tariffs=tariffService.GetAllTariff();
            Console.WriteLine("\t\tWe have four Tariffs");
            Console.WriteLine("\n\n\t\t\tTariff Name \t\tTariff Price");
            for (var i = 0; i < tariffs.Count; i++)
            {
                Console.Write($"\t\t{i + 1} : {tariffs[i].Name}\t\t#{tariffs[i].PricePerUnit} \n");
                if (i == 0)
                {
                    t1 = tariffs[i].PricePerUnit;
                    a1 = tariffs[i].Id;

                }
                else if (i == 1)
                {
                    t2 = tariffs[i].PricePerUnit;
                    a2 = tariffs[i].Id;
                }
                else if (i == 2)
                {
                    t3 = tariffs[i].PricePerUnit;
                    a3 = tariffs[i].Id;

                }
                else if (i == 3)
                {
                    t4 = tariffs[i].PricePerUnit; ;
                    a4 = tariffs[i].Id;
                }
            }
            Console.Write($"\t\t  : ");
            string userInput = Console.ReadLine();
            Console.Write($"\n\t\tProccessing");
            Security.PrintDotAnimation();
            Console.Clear();
            int amountToBuy = default;
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string firstInput = Console.ReadLine();
                    Console.Write($"\n\t\tProccessing");
                    Security.PrintDotAnimation();
                    while (!int.TryParse(firstInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        firstInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t1, a1,customer);
                    break;
                case "2":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string secondInput = Console.ReadLine();
                    while (!int.TryParse(secondInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        secondInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t2, a2,customer);
                    break;
                case "3":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string thirdInput = Console.ReadLine();
                    while (!int.TryParse(thirdInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        thirdInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t3, a3,customer);
                    break;
                case "4":
                    Console.WriteLine("\t\tHow many units would you like to purchase?");
                    Console.Write($"\t\t  : ");
                    string fourthInput = Console.ReadLine();
                    while (!int.TryParse(fourthInput, out amountToBuy))
                    {
                        Console.WriteLine("\t\tInvalid Input\n\t\tHow many units would you like to purchase?");
                        Console.Write($"\t\t  : ");
                        fourthInput = Console.ReadLine();
                    }
                    CalculateTotalUnit(amountToBuy, t4, a4,customer);
                    break;
                default:
                    MakeSubscriptionPaymentForCustomer(customer);
                    break;
            }
        }
        private void CalculateTotalUnit(int unitToBePurchased, decimal pricePerUnit, string tariffId,CustomerModel customer)
        {
            decimal totalAmountPurchased = Convert.ToDecimal(unitToBePurchased) * pricePerUnit;
            Console.WriteLine($"\t\tYou are about to pay N{totalAmountPurchased} \n\t\tDo you want to proceed? \n\t\t1 : Yes\n\t\t2 : No  ");
            Console.Write($"\t\t  : ");
            string entry = Console.ReadLine();
            Console.Write($"\n\t\tProccessing");
            Security.PrintDotAnimation();
            switch (entry)
            {
                case "1":
                    Console.WriteLine($"\t\tYou have Successfully purchased {unitToBePurchased} Units");

                    //var currentCustomer = customerService.GetCustomerById(customer.Id);
                    var sub = subscriptionService.CheckActiveSubscription(customer.Id);
                    foreach (var item in sub)
                    {
                        item.SubscriptionStatus = "Inactive";
                        subscriptionService.UpdateSubscription(item);
                    }

                    CustomerSubcription customerSubcription = new CustomerSubcription
                    {
                        Id = Guid.NewGuid().ToString(),
                        SubscriptionStatus = "Active",
                        CustomerId = customer.Id,
                        TariffId = tariffId,
                        SubcriptionDateTime = DateTime.Now,
                        Amount = totalAmountPurchased,
                        AgentId = AgentAppData.CurrentAgentId
                    };
                    subscriptionService.MakeSubscription(customerSubcription);
                    AgentHomeMenu.AgentContinuation();
                    break;
                case "2":
                    AgentHomeMenu.CurrentStage = 1;
                    break;
                default:

                    break;
            }

        }
    }
}

        
