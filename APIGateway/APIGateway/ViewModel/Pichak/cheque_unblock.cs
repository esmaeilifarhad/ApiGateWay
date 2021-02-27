using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
   
    public class cheque_unblock_UnblockerAgent
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cheque_unblock_Unblocker
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cheque_unblock_Root
    {
        public string sayadId { get; set; }
        public cheque_unblock_UnblockerAgent unblockerAgent { get; set; }
        public cheque_unblock_Unblocker unblocker { get; set; }
        public string requestDate { get; set; }
        public string letterNumber { get; set; }
        public string letterDate { get; set; }
    }
}