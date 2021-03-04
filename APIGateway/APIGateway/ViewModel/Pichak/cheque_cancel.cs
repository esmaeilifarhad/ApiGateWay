using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    public class cheque_cancel_Canceller
    {
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class cheque_cancel_CancellerAgent
    {
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class cheque_cancel_Param
    {
        public string sayadId { get; set; }
        public string cancelDate { get; set; }
        public string cancelDescription { get; set; }
        public cheque_cancel_Canceller canceller { get; set; }
        public cheque_cancel_CancellerAgent cancellerAgent { get; set; }
       // public InitialRestRequest initialRestRequest { get; internal set; }
    }
}