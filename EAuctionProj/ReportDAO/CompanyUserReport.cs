using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FileHelpers;

namespace EAuctionProj.ReportDAO
{
    [DelimitedRecord(",")]
    public class CompanyUserReport
    {
        [FieldQuoted()]
        public string CompanyName;

        [FieldQuoted()]
        public string TaxID;

        [FieldQuoted()]
        public string UserName;

        [FieldQuoted()]
        public string Password;
    }
}