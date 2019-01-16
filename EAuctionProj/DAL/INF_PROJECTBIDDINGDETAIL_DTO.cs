using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class INF_PROJECTBIDDINGDETAIL_DTO
    {
        public Int64? ProjectNo { get; set; }
        public Int64? CompanyNo { get; set; }
        public Int64? BiddingsNo { get; set; }
        public string CompanyName { get; set; }
        public string TaxID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Decimal? BiddingPrice { get; set; }
        public Decimal? BiddingTotalPrice { get; set; }
        public string ProjectName { get; set; }
        public string BiddingCode { get; set; }
    }
}