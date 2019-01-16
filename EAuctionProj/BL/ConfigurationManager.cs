using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAuctionProj.BL
{
    public class ConfigurationManager
    {
        private static ConfigurationItem _item;
        public static ConfigurationItem GetConfiguration()
        {
            if (_item == null)
            {
                _item = new ConfigurationItem();
            }

            return _item;
        }
    }

    public class ConfigurationItem
    {
        private string _dbcs;
        private string _dbProvider;
        private string _companyNo;
        private string _attachFilePath;
        private string _biddingULFolder;
        private string _passLength;
        private string _encryptionKey;
        private string _companyULFolder;
        
        private string _emailSMTP;
        private string _emailPort;        
        private string _emailUser;
        private string _emailPassword;
        private string _emailFrom;

        private string _urlLogin;

        private string _dateFormat;
        private string _gulfPhoneNo;

        private string _emailApprove;
        private string _emailNotify;

        public ConfigurationItem()
        {
            _dbcs = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
            _dbProvider = System.Configuration.ConfigurationManager.AppSettings["db.provider_name"];
            _companyNo = System.Configuration.ConfigurationManager.AppSettings["DefaultCompanyID"];
            _attachFilePath = System.Configuration.ConfigurationManager.AppSettings["AttachFilePath"];
            _biddingULFolder = System.Configuration.ConfigurationManager.AppSettings["BiddingUploadFolder"];
            _passLength = System.Configuration.ConfigurationManager.AppSettings["PasswordLength"];
            _encryptionKey = System.Configuration.ConfigurationManager.AppSettings["EncryptionKey"];

            _companyULFolder = System.Configuration.ConfigurationManager.AppSettings["CompanyUploadFolder"];

            _emailSMTP = System.Configuration.ConfigurationManager.AppSettings["EmailSMTP"];
            _emailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"];
            _emailUser = System.Configuration.ConfigurationManager.AppSettings["EmailUser"];
            _emailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"];
            _emailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailFrom"];

            _urlLogin = System.Configuration.ConfigurationManager.AppSettings["UrlLogin"];

            _dateFormat = System.Configuration.ConfigurationManager.AppSettings["DateFormat"];

            _gulfPhoneNo = System.Configuration.ConfigurationManager.AppSettings["GulfPhoneNo"];

            _emailApprove = System.Configuration.ConfigurationManager.AppSettings["EmailApprove"];

            _emailNotify = System.Configuration.ConfigurationManager.AppSettings["EmailNotify"];

        }

        public string DbConnectionString
        {
            get { return _dbcs; }
        }
        public string DbProviderName
        {
            get { return _dbProvider; }
        }
        public string CompanyNo
        {
            get { return _companyNo; }
        }
        public string AttachFilePath
        {
            get { return _attachFilePath; }
        }

        public string BiddindUploadFolder
        {
            get { return _biddingULFolder; }
        }

        public string PasswordLength
        {
            get { return _passLength; }
        }

        public string EncryptionKey
        {
            get { return _encryptionKey; }
        }

        public string CompanyUploadFolder
        {
            get { return _companyULFolder; }
        }

        /************* For Email **************************/
        public string EmailSMTP
        {
            get { return _emailSMTP; }
        }

        public string EmailPort
        {
            get { return _emailPort; }
        }

        public string EmailUser
        {
            get { return _emailUser; }
        }

        public string EmailPassword
        {
            get { return _emailPassword; }
        }

        public string EmailFrom
        {
            get { return _emailFrom; }
        }
        /***********************************************/

        public string UrlLogin
        {
            get { return _urlLogin; }
        }

        public string DateFormat
        {
            get { return _dateFormat; }
        }

        public string GulfPhoneNo
        {
            get { return _gulfPhoneNo; }
        }

        public string EmailApprove
        {
            get { return _emailApprove; }
        }

        public string EmailNotify
        {
            get { return _emailNotify; }
        }
    }
}
