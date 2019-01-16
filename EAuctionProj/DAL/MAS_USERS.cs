using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_USERS
    {
        public Int64? UsersNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int64? RolesNo { get; set; }
        public Int64? CompanyNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Int64? ProjectNo { get; set; }
        public string Status { get; set; }
    }
}