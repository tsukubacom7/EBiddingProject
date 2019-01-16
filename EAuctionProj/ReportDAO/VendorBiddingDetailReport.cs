using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FileHelpers;

namespace EAuctionProj.ReportDAO
{
    [DelimitedRecord(",")]
    public class VendorBiddingDetailReport
    {
        [FieldQuoted()]
        public string BiddingCode;

        [FieldQuoted()]
        public string ProjectName;    

        [FieldQuoted()]
        public string CreatedDate;

       [FieldConverter(typeof(MoneyConverter))]
        public Decimal? BiddingPrice;
     
    }   
}