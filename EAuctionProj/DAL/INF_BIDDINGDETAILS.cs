using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class INF_BIDDINGDETAILS
    {
        public Int64 DetailsNo { get; set; }
        public Int64 BiddingsNo { get; set; }
        public string ItemColumn1 { get; set; }
        public string ItemColumn2 { get; set; }
        public string ItemColumn3 { get; set; }
        public string ItemColumn4 { get; set; }
        public string ItemColumn5 { get; set; }
        public string ItemColumn6 { get; set; }
        public string ItemColumn7 { get; set; }
        public string ItemColumn8 { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }     
    }
}