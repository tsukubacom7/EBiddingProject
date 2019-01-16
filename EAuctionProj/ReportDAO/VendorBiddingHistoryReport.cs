using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FileHelpers;

namespace EAuctionProj.ReportDAO
{
    [DelimitedRecord(",")]
    public class VendorBiddingHistoryReport
    {
        [FieldQuoted()]
        public string CompanyName;

        [FieldQuoted()]
        public string TaxID;
    }
}