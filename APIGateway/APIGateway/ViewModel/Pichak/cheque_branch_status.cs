using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    public class cheque_branch_status
    {
        public string sayadId { get; set; }
        public string fromIban { get; set; }
        public string branchUserId1 { get; set; }
        public string branchUserId2 { get; set; }
        public int newStatus { get; set; }
        public string description { get; set; }
    }

  

}