using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_PROJECTBIDDING
    {
        public Int64 ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public Int64 TemplateNo { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string AttachFilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string BiddingCode { get; set; }
        public string DepartmentName { get; set; }
    }
}