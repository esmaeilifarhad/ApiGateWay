using System.Collections.Generic;

namespace APIGateway.ViewModel.Pichak
{

    public class inner_bank_cashing_Holder
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class inner_bank_cashing_ChequeCarrier
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class inner_bank_cashing_Root
    {
        public string sayadId { get; set; }
        public string serialNo { get; set; }
        public string fromIban { get; set; }
        public int cashingAmount { get; set; }
        public string cashingDueDate { get; set; }
        public int chequeType { get; set; }
        public int chequeMedia { get; set; }
        public string requestDate { get; set; }
        public List<inner_bank_cashing_Holder> holders { get; set; }
        public string holderIban { get; set; }
        public int bounceCheque { get; set; }
        public inner_bank_cashing_ChequeCarrier chequeCarrier { get; set; }
        public int bankCashingResult { get; set; }
        public string cashierBankCode { get; set; }
        public string cashierBranchCode { get; set; }
       // public InitialRestRequest initialRestRequest { get; internal set; }
    }
}