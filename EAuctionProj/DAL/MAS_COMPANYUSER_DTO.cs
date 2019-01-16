using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_COMPANYUSER_DTO
    {
        public Int64? CompanyNo { get; set; }
        public Int64? UsersNo { get; set; }
        public Int64? RolesNo { get; set; }
        public string CompanyName { get; set; }
        public string TaxID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public Int64? ProjectNo { get; set; }
    }
}