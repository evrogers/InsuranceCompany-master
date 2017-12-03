using System;
using System.Collections.Generic;

namespace InsuranceCompany.Models
{
    public partial class ClientGroups
    {
        public ClientGroups()
        {
            Clients = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
