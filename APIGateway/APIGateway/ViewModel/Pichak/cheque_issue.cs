using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    //public class InitialRestRequest {
    //    public string TerminalName { get; set; }
    //    public string BranchUserName { get; set; }
    //    public string BranchCode { get; set; }
    //    public string AuthStatus { get; set; }

    //}
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

    public class cheque_issue_SignGrantor
    {
        public string name { get; set; }
        public string idCode { get; set; }
        public string shahabId { get; set; }
        public int idType { get; set; }
    }

    public class cheque_issue_Signer
    {
        public cheque_issue_Signer2 signer { get; set; }
        public cheque_issue_SignGrantor signGrantor { get; set; }
        public int legalStamp { get; set; }
    }

    public class cheque_issue_Root
    {
        /// <summary>
        /// این پراپرتی برای داینامیک کردن استفاده شده است
        /// </summary>
       // public InitialRestRequest  initialRestRequest { get; set; }
        public List<cheque_issue_AccountOwner> accountOwners { get; set; }
        public List<cheque_issue_Receiver> receivers { get; set; }
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




}