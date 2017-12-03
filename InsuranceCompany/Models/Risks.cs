using System;
using System.Collections.Generic;

namespace InsuranceCompany.Models
{
    public partial class Risks
    {
        public int Id { get; set; }
        public string RiskName { get; set; }
        public string RiskDescription { get; set; }
        public string AverageProbability { get; set; }
        public int? TypeId { get; set; }

        public PolicyTypes Type { get; set; }
    }
}
