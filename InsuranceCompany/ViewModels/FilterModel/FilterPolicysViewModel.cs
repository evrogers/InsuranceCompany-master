using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceCompany.ViewModels.FilterModel
{
    public class FilterPolicysViewModel
    {
        public FilterPolicysViewModel(int? PolicyNumber,int? Summ)
        {
            SelectedPolicyNumber = PolicyNumber;
            SelectedSumm = Summ;
        }

        public  int? SelectedPolicyNumber { get; set; }
        public  int? SelectedSumm { get; set; }
    }
}

