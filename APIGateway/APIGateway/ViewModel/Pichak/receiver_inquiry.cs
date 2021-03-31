using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIGateway.ViewModel.Pichak
{
    public class receiver_inquiry_Root
    {
        public string sayadId { get; set; }
        public List<ReceiversId> receiversId { get; set; }
    }
    public class ReceiversId
    {
        public int idType { get; set; }
        public string idCode { get; set; }
    }
}