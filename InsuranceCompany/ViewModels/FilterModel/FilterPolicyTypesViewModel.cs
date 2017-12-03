using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceCompany.ViewModels
{

    public class FilterPolicyTypesViewModel
    {

        public FilterPolicyTypesViewModel(string PolicyName, string PolicyDescription)
        {
            SelectedPolicyName = PolicyName;
            SelectedPolicyDescription = PolicyDescription;
        }

        public string SelectedPolicyName { get; set; }
        public string SelectedPolicyDescription { get; set; }
    }
}
