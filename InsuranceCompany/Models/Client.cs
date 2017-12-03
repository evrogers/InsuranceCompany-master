using System;
using System.Collections.Generic;

namespace InsuranceCompany.Models
{
    public partial class Client
    {
        public Client()
        {
            Policys = new HashSet<Policys>();
        }

        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTime? ClientDateBirth { get; set; }
        public string ClientSex { get; set; }
        public string ClientAddress { get; set; }
        public int? ClientPhone { get; set; }
        public string ClientPassport { get; set; }
        public int? GroupId { get; set; }

        public ClientGroups Group { get; set; }
        public ICollection<Policys> Policys { get; set; }
    }
}