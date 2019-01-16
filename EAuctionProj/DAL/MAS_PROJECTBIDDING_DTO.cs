using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_PROJECTBIDDING_DTO
    {
        public Int64 ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public Int64 TemplateNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BiddingCode { get; set; }
        public string ProjectStatus { get; set; }
        public Decimal? BiddingPrice { get; set; }

        public Int64? BiddingsNo { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
    }
}