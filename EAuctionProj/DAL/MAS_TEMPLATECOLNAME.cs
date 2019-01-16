using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_TEMPLATECOLNAME
    {
        public Int64 TemplateNo { get; set; }
        public string TemplateName { get; set; }
        public string ColumnName1 { get; set; }
        public string ColumnName2 { get; set; }
        public string ColumnName3 { get; set; }
        public string ColumnName4 { get; set; }
        public string ColumnName5 { get; set; }
        public string ColumnName6 { get; set; }
        public string ColumnName7 { get; set; }
        public string ColumnName8 { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}