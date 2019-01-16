using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class INF_BIDDINGATTACHMENT
    {
        public Int64? AttachmentNo { get; set; }
        public Int64? BiddingsNo { get; set; }
        public string FileName { get; set; }
        public string AttachFilePath { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
             
    }
}