using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
   

    public class cheque_block_BlockerAgent
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cheque_block_Blocker
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cheque_block_Root
    {
        public string sayadId { get; set; }
        public string reasonCode { get; set; }
        public cheque_block_BlockerAgent blockerAgent { get; set; }
        public cheque_block_Blocker blocker { get; set; }
        public string requestDate { get; set; }
        public string letterNumber { get; set; }
        public string letterDate { get; set; }
       // public InitialRestRequest initialRestRequest { get; internal set; }
    }
}