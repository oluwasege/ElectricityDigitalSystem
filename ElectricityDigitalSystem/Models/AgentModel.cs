using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityDigitalSystem.Models
{
    public class AgentModel : BaseUserModel
    {
        public AgentModel()
        {
            this.Id = "AGT-" + Guid.NewGuid().ToString();
        }
        public string Password { get; set; }

    }
}
