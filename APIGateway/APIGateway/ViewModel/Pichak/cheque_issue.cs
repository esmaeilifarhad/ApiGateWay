using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class cheque_issue_Specification
    {
        public string name { get; set; }
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class cheque_issue_AccountOwner
    {
        public string name { get; set; }
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class cheque_issue_Receiver
    {
        public string name { get; set; }
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class cheque_issue_Signer2
    {
        public string name { get; set; }
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class cheque_issue_Signer
    {
        public List<cheque_issue_Specification> signer { get; set; }
        public int legalStamp { get; set; }
    }

    public class cheque_issue_Root
    {
        public List<cheque_issue_Specification> accountOwners { get; set; }
        public List<cheque_issue_Specification> receivers { get; set; }
        public List<cheque_issue_Signer> signers { get; set; }
        public string sayadId { get; set; }
        public string seriesNo { get; set; }
        public string serialNo { get; set; }
        public string fromIban { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string dueDate { get; set; }
        public string toIban { get; set; }
        public int currency { get; set; }
        public string bankCode { get; set; }
        public string branchCode { get; set; }
        public int chequeType { get; set; }
        public int chequeMedia { get; set; }
    }

    public class Error
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }


}