using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class MAS_BIDDINGCOMPANY
    {
        public Int64? CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public string TaxID { get; set; }     
        public string CompanyAddress { get; set; }
        public string ContactName { get; set; }
        public string MobilePhoneNo { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        public string EmailCC { get; set; }
        public string VatRegistrationNoFile { get; set; }
        public string CertificateCompanyFile { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyType { get; set; }
    }
}