using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EAuctionProj.DAL
{
    [Serializable()]
    public class INF_EMIALVENDOR
    {
        public string EmailTo { get; set; }
        public string EmailBody { get; set; }
    }
}