using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceCompany.ViewModels
{
    public class FilterRisksViewModel
    {
        public FilterRisksViewModel(string RiskName, string RiskDescription)
        {
            SelectedRiskName = RiskName;
            SelectedRiskDescription = RiskDescription;
        }

        public string SelectedRiskName { get; set; }
        public string SelectedRiskDescription { get; set; }
    }
}

