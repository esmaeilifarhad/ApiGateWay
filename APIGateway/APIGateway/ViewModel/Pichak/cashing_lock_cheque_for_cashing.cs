using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class cashing_lock_cheque_Holder
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cashing_lock_cheque_ChequeCarrier
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cashing_lock_cheque_Root
    {
        public string sayadId { get; set; }
        public string holderIban { get; set; }
        public List<cashing_lock_cheque_Holder> holders { get; set; }
        public cashing_lock_cheque_ChequeCarrier chequeCarrier { get; set; }
        public string fromIban { get; set; }
        public int cashingAmount { get; set; }
        public string cashingDueDate { get; set; }
        public string requestDate { get; set; }
        public int bounceCheque { get; set; }
        public string cashierBranchCode { get; set; }
        public string cashierBankCode { get; set; }
        public int chequeType { get; set; }
        public int chequeMedia { get; set; }
        public string serialNo { get; set; }
    }


}