using System;
using System.Collections.Generic;
using System.Text;

namespace EDSAgentPortal.Interfaces
{
     public interface IAgentAccess
    {
       
        void ViewAgentInformation();
        void ViewCustomersInformation();
        void ViewCustomerSubscriptions();
     
        void UpdateAgentInformation();
        void UpdateCustomerInformation();

        void SubscribeForCustomer();
        void CancelCustomerSubsription();
        void DeleteCustomer();

    }
}
