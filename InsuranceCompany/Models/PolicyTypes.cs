using System;
using System.Collections.Generic;

namespace InsuranceCompany.Models
{
    public partial class PolicyTypes
    {
        public PolicyTypes()
        {
            Policys = new HashSet<Policys>();
            Risks = new HashSet<Risks>();
        }

        public int Id { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public string PolicyCondition { get; set; }

        public ICollection<Policys> Policys { get; set; }
        public ICollection<Risks> Risks { get; set; }
    }
}
