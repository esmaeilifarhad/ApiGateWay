using System.Collections.Generic;

namespace APIGateway.ViewModel.Pichak
{
   
    public class cashing_inquiry_Holder
    {
        public string shahabId { get; set; }
        public string idCode { get; set; }
        public int idType { get; set; }
    }

    public class cashing_inquiry_Root
    {
        public string sayadId { get; set; }
        public string serialNo { get; set; }
        public string fromIban { get; set; }
        public long cashingAmount { get; set; }
        public string cashingDueDate { get; set; }
        public int chequeType { get; set; }
        public int chequeMedia { get; set; }
        public string requestDate { get; set; }
        public List<cashing_inquiry_Holder> holders { get; set; }
        public string cashierBankCode { get; set; }
        public string cashierBranchCode { get; set; }
       // public InitialRestRequest initialRestRequest { get; internal set; }
    }
}