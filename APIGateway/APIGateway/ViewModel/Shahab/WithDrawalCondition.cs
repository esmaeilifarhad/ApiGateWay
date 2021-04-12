using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Shahab
{
    public class WithDrawalCondition
    {
        public string BRANCH_CODE { get; set; }
        public string DEPOSIT_TYPE { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string DEPOSIT_SERIAL { get; set; }
        public string ConditionId { get; set; }
        public string PlaceId { get; set; }
        public string ShareCustomerNo { get; set; }
    }
    public class ParamWithDrawalCondition {
        public string BRANCH_CODE { get; set; }
        public string DEPOSIT_TYPE { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string DEPOSIT_SERIAL { get; set; }
    }
}