using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class cheque_transfer_Holder
        {
            public string idCode { get; set; }
            public string shahabId { get; set; }
            public int idType { get; set; }
        }

        public class cheque_transfer_Receiver
        {
            public string name { get; set; }
            public string idCode { get; set; }
            public string shahabId { get; set; }
            public int idType { get; set; }
        }

        public class cheque_transfer_Signer2
        {
            public string idCode { get; set; }
            public string shahabId { get; set; }
            public int idType { get; set; }
        }

        public class cheque_transfer_Signer
        {
            public cheque_transfer_Signer2 signer { get; set; }
            public int legalStamp { get; set; }
        }

        public class cheque_transfer_root
        {
            public string sayadId { get; set; }
            public cheque_transfer_Holder holder { get; set; }
            public List<cheque_transfer_Receiver> receivers { get; set; }
            public List<cheque_transfer_Signer> signers { get; set; }
            public string toIban { get; set; }
            public string transferDate { get; set; }
            public int acceptTransfer { get; set; }
            public string description { get; set; }
       // public InitialRestRequest initialRestRequest { get; internal set; }
    }
}