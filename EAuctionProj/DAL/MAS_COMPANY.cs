using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_COMPANY 
    {
        public Int64 CompanyNo { get; set; }
        public string CompanyNameTH { get; set; }
        public string CompanyNameEN { get; set; }
        public string CompanyAddressTH { get; set; }      
    }
}