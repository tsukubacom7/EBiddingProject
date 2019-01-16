using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_COLUMNNAME
    {
        public Int64 ColumnRunNo { get; set; }
        public string ColumnName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CratedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}