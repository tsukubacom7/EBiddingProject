using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class INF_QUESTIONNAIRE
    {
        public Int64? QuestionNo { get; set; }
        public string ProjectNo { get; set; }
        public string CompanyNo { get; set; }
        public int? AnsQuestion1 { get; set; }
        public string AnsQuestion2 { get; set; }
        public int? AnsQuestion3 { get; set; }
        public int? AnsQuestion4 { get; set; }
        public int? AnsQuestion5 { get; set; }
        public int? AnsQuestion6 { get; set; }
        public int? AnsQuestion7 { get; set; }
        public string AnsQuestion8 { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}