using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FileHelpers;

namespace EAuctionProj.ReportDAO
{
    [DelimitedRecord(",")]
    public class BiddingHistoryReport
    {
        [FieldQuoted()]
        public string BiddingCode;

        [FieldQuoted()]
        public string ProjectName;

        [FieldQuoted()]
        public string CompanyName;

        [FieldQuoted()]
        public string EndDate;

       [FieldConverter(typeof(MoneyConverter))]
        public Decimal? BiddingPrice;
     
    }


    public class MoneyConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return Decimal.Parse(from);
        }

        public override string FieldToString(object fieldValue)
        {
            string ret = "";
            if (fieldValue == null)
            {
                ret = "0";
            }
            else
            {
                ret = ((Decimal)fieldValue).ToString("#.##");
            }

            return ret;
        }
    }
}