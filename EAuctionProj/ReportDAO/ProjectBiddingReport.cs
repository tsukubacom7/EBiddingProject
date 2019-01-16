using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FileHelpers;

namespace EAuctionProj.ReportDAO
{
    [DelimitedRecord(",")]
    public class ProjectBiddingReport
    {
        [FieldQuoted()]
        public string BiddingCode;

        [FieldQuoted()]
        public string ProjectName;

        [FieldQuoted()]
        public string StartDate;

        [FieldQuoted()]
        public string EndDate;

        [FieldQuoted()]
        public string Status;
    }
}