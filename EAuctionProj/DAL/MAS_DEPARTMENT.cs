using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_DEPARTMENT
    {
        public string CompanyCode { get; set; }
        public string DepartmentName { get; set; }
    }
}