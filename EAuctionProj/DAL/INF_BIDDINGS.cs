using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class INF_BIDDINGS
    {
        public Int64? BiddingsNo { get; set; }
        public Int64? CompanyNo { get; set; }
        public Int64? ProjectNo { get; set; }
        public decimal? BiddingPrice { get; set; }
        public decimal? BiddingVat7 { get; set; }
        public decimal? BiddingTotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}