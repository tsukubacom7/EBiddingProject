using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FileHelpers;

namespace EAuctionProj.ReportDAO
{
    [DelimitedRecord(",")]
    public class ProjectBiddingDetailReport
    {
        [FieldQuoted()]
        public string ItemColumn1;

        [FieldQuoted()]
        public string ItemColumn2;

        [FieldQuoted()]
        public string ItemColumn3;

        [FieldQuoted()]
        public string ItemColumn4;

        [FieldQuoted()]
        public string ItemColumn5;

        [FieldQuoted()]
        public string ItemColumn6;

        [FieldQuoted()]
        public string ItemColumn7;

        [FieldQuoted()]
        public string ItemColumn8;
    }
}