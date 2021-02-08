using System.Collections.Generic;

namespace APIGateway.ViewModel.Pichak
{
    public class cashing_unlock_cheque_Root
    {
        public string sayadId { get; set; }
        public List<cashing_unlock_cheque_Holder> holders { get; set; }
        public cashing_unlock_cheque_ChequeCarrier chequeCarrier { get; set; }
        public string requestDate { get; set; }
        public string cashierBranchCode { get; set; }
        public string cashierBankCode { get; set; }
    }
    public class cashing_unlock_cheque_Holder
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cashing_unlock_cheque_ChequeCarrier
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }



}