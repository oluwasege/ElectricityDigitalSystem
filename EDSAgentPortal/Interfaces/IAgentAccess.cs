using ElectricityDigitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDSAgentPortal.Interfaces
{
     public interface IAgentAccess
    {
       
        void ViewAgentInformation();
        void ViewCustomersInformation();
        void ViewCustomerSubscriptionHistory();
     
        void UpdateAgentInformation();
        void UpdateCustomerInformation();

        void SubscribeForCustomer();
        void CancelCustomerSubscription();
        void RemoveCustomer();

    }
}
