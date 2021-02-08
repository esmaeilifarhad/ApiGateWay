using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    public class cheque_revoke_RevokerAgent
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cheque_revoke_Revoker
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cheque_revoke_Root
    {
        public string sayadId { get; set; }
        public cheque_revoke_RevokerAgent revokerAgent { get; set; }
        public cheque_revoke_Revoker revoker { get; set; }
        public string revokeDate { get; set; }
    }
}