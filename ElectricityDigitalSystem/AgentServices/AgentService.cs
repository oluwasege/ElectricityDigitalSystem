using ElectricityDigitalSystem.ClientServices;
using ElectricityDigitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectricityDigitalSystem.AgentServices
{
    public class AgentService:AgentServiceAPI
    {
        CustomerService customerservice = new CustomerService();
        public string RegisterAgent(AgentModel Agent)
        {
            if (Agent == null)
            {
                throw new ArgumentNullException(nameof(Agent));
            }
            //This ill Handle registration of an agent
            else
            {
                fileService.Database.Agents.Add(Agent);
                fileService.SaveChanges();
                return Agent.EmailAddress;
            }


        }
        public AgentModel GetAgentById(string Id)
        {
            AgentModel foundAgent = fileService.Database.Agents.Find(e => e.Id == Id);
            if (foundAgent != null)
            {
                return foundAgent;
            }
            else
                return null;
          
        }

        public string UpdateAgent(AgentModel modifiedAgent)
        {
            AgentModel agents = this.GetAgentById(modifiedAgent.Id);
            if (agents != null)
            {
                int indexOfCustomer = fileService.Database.Agents.IndexOf(agents);
                //fileService.database.Customers.Insert(indexOfCustomer, modifiedCustomer);
                fileService.SaveChanges();
                return "SUCCESSFULLY UPDATED";
            }
            return "Failed, Agent not found";
        }
        public AgentModel GetAgentByEmail(string email )
        {
            AgentModel foundAgent = fileService.Database.Agents.Find(e => e.EmailAddress == email);
            if (foundAgent != null)
            {
                return foundAgent;
            }
            else
                return null;
             

        }
        public string DeleteCustomer(CustomerModel deleteCustomer)
        {
            if (deleteCustomer != null)
            {
                var count = fileService.Database.Customers.Count();
                var indexOfCustomer = fileService.Database.Customers.FindIndex(0, count, d => d.EmailAddress == deleteCustomer.EmailAddress);
                fileService.Database.Customers.RemoveAt(indexOfCustomer);
                fileService.SaveChanges();
                return "Deleted";
            }
            else
                return "Not Deleted";
        }
        public List<CustomerModel> GetAllCustomer()
        {
            var allCustomers = fileService.Database.Customers.ToList();
            return allCustomers;
        }



    }
}
