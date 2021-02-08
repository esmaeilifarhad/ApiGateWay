using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    public class Cheque_accept_Acceptor
    {
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class Cheque_accept_AcceptorAgent
    {
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class Cheque_accept_Param
    {
        public string sayadId { get; set; }
        public int accept { get; set; }
        public string acceptDescription { get; set; }
        public Cheque_accept_Acceptor acceptor { get; set; }
        public Cheque_accept_AcceptorAgent acceptorAgent { get; set; }
        public string acceptDate { get; set; }
    }
}