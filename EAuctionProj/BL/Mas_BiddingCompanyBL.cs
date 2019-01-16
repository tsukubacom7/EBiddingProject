using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using EAuctionProj.DAL;
using System.Threading;

namespace EAuctionProj.BL
{
    public class Mas_BiddingCompanyBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_BiddingCompanyBL));

        public Mas_BiddingCompanyBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }

        public string InsertData(MAS_BIDDINGCOMPANY data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string retCompanyNo = string.Empty;
            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_mas_BiddingCompany] " +
                                    "([CompanyName] " +
                                    ",[TaxID] " +                                  
                                    ",[CompanyAddress] " +
                                    ",[ContactName] " +
                                    ",[MobilePhoneNo] " +
                                    ",[TelephoneNo] " +
                                    ",[Email] " +
                                    ",[EmailCC] " +
                                    ",[VatRegistrationNoFile] " +
                                    ",[CertificateCompanyFile] " +
                                    ",[CreatedBy] " +
                                    ",[CreatedDate] " +
                                    ",[UpdatedBy] " +
                                    ",[UpdatedDate], [CompanyWebsite],[CompanyType]) " +
                                  "VALUES " +
                                    "(@CompanyName " +
                                    ",@TaxID " +                                 
                                    ",@CompanyAddress " +
                                    ",@ContactName " +
                                    ",@MobilePhoneNo " +
                                    ",@TelephoneNo " +
                                    ",@Email " +
                                    ",@EmailCC " +
                                    ",@VatRegistrationNoFile " +
                                    ",@CertificateCompanyFile " +
                                    ",@CreatedBy " +
                                    ",@CreatedDate " +
                                    ",@UpdatedBy " +
                                    ",@UpdatedDate, @CompanyWebsite, @CompanyType); " +
                                    " SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                command.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                command.Parameters.AddWithValue("@TaxID", data.TaxID);
                command.Parameters.AddWithValue("@CompanyAddress", string.IsNullOrWhiteSpace(data.CompanyAddress) ? "" : data.CompanyAddress);
                command.Parameters.AddWithValue("@ContactName", string.IsNullOrWhiteSpace(data.ContactName) ? "" : data.ContactName);
                command.Parameters.AddWithValue("@MobilePhoneNo", string.IsNullOrWhiteSpace(data.MobilePhoneNo) ? "" : data.MobilePhoneNo);
                command.Parameters.AddWithValue("@TelephoneNo", string.IsNullOrWhiteSpace(data.TelephoneNo) ? "" : data.TelephoneNo);
                command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(data.Email) ? "" : data.Email);
                command.Parameters.AddWithValue("@EmailCC", string.IsNullOrWhiteSpace(data.EmailCC) ? "" : data.EmailCC);
                command.Parameters.AddWithValue("@VatRegistrationNoFile", string.IsNullOrWhiteSpace(data.VatRegistrationNoFile) ? "" : data.VatRegistrationNoFile);
                command.Parameters.AddWithValue("@CertificateCompanyFile", string.IsNullOrWhiteSpace(data.CertificateCompanyFile) ? "" : data.CertificateCompanyFile); 
     
                command.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
                if (!string.IsNullOrEmpty(data.CreatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.CreatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@CreatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
                if (!string.IsNullOrEmpty(data.UpdatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.UpdatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@UpdatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@CompanyWebsite", string.IsNullOrWhiteSpace(data.CompanyWebsite) ? "" : data.CompanyWebsite);
                command.Parameters.AddWithValue("@CompanyType", string.IsNullOrWhiteSpace(data.CompanyType) ? "" : data.CompanyType);
                //
                object _CompanyNo = command.ExecuteScalar();
                if (_CompanyNo != null)
                {
                    retCompanyNo = _CompanyNo.ToString();
                }

            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return retCompanyNo;
        }
        
        public string InsertUsersData(MAS_USERS data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string retUserNo = string.Empty;
            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_mas_Users] " +
                                        "([UserName] " +
                                        ",[Password] " +
                                        ",[RolesNo] " +
                                        ",[CompanyNo] " +
                                        ",[CreatedBy] " +
                                        ",[CreatedDate] " +
                                        ",[UpdatedBy] " +
                                        ",[UpdatedDate],[ProjectNo],[Status]) " +
                                    "VALUES " +
                                        "(@UserName " +
                                        ",@Password " +
                                        ",@RolesNo " +
                                        ",@CompanyNo " +
                                        ",@CreatedBy " +
                                        ",@CreatedDate " +
                                        ",@UpdatedBy " +
                                        ",@UpdatedDate, @ProjectNo,@Status); " +
                                        " SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                command.Parameters.AddWithValue("@UserName", data.UserName);
                command.Parameters.AddWithValue("@Password", data.Password);
                command.Parameters.AddWithValue("@RolesNo", data.RolesNo);
                command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);

                command.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
                if (!string.IsNullOrEmpty(data.CreatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.CreatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@CreatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
                if (!string.IsNullOrEmpty(data.UpdatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.UpdatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@UpdatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);
                command.Parameters.AddWithValue("@Status", data.Status);

                object _CompanyNo = command.ExecuteScalar();
                if (_CompanyNo != null)
                {
                    retUserNo = _CompanyNo.ToString();
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return retUserNo;
        }

        public bool UpdateData(MAS_BIDDINGCOMPANY data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE tb_mas_BiddingCompany " +
                                    "SET [CompanyName] = @CompanyName " +
                                        ",[TaxID] = @TaxID " +
                                        ",[CompanyAddress] = @CompanyAddress " +
                                        ",[ContactName] = @ContactName " +
                                        ",[MobilePhoneNo] = @MobilePhoneNo " +
                                        ",[TelephoneNo] = @TelephoneNo " +
                                        ",[Email] = @Email " +
                                        ",[EmailCC] = @EmailCC " +                                                                     
                                        ",[UpdatedBy] = @UpdatedBy " +
                                        ",[UpdatedDate] = @UpdatedDate " +
                                        ",[CompanyWebsite] = @CompanyWebsite " +
                                        ",[CompanyType] = @CompanyType " +
                                    "WHERE [CompanyNo] = @CompanyNo";

                SqlCommand command = new SqlCommand(strQuery, _conn, _tran);

                command.Parameters.AddWithValue("@CompanyName", string.IsNullOrWhiteSpace(data.CompanyName) ? "" : data.CompanyName);
                command.Parameters.AddWithValue("@TaxID", string.IsNullOrWhiteSpace(data.TaxID) ? "" : data.TaxID);
                command.Parameters.AddWithValue("@CompanyAddress", string.IsNullOrWhiteSpace(data.CompanyAddress) ? "" : data.CompanyAddress);
                command.Parameters.AddWithValue("@ContactName", string.IsNullOrWhiteSpace(data.ContactName) ? "" : data.ContactName);
                command.Parameters.AddWithValue("@MobilePhoneNo", string.IsNullOrWhiteSpace(data.MobilePhoneNo) ? "" : data.MobilePhoneNo);
                command.Parameters.AddWithValue("@TelephoneNo", string.IsNullOrWhiteSpace(data.TelephoneNo) ? "" : data.TelephoneNo);
                command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(data.Email) ? "" : data.Email);
                command.Parameters.AddWithValue("@EmailCC", string.IsNullOrWhiteSpace(data.EmailCC) ? "" : data.EmailCC);

                //command.Parameters.AddWithValue("@VatRegistrationNoFile", data.VatRegistrationNoFile);
                //command.Parameters.AddWithValue("@CertificateCompanyFile", data.CertificateCompanyFile);             

                command.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
                if (!string.IsNullOrEmpty(data.UpdatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.UpdatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@UpdatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@CompanyWebsite", string.IsNullOrWhiteSpace(data.CompanyWebsite) ? "" : data.CompanyWebsite);
                command.Parameters.AddWithValue("@CompanyType", string.IsNullOrWhiteSpace(data.CompanyType) ? "" : data.CompanyType);
                command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);

                if (command.ExecuteNonQuery() == 1)
                {
                    bRet = true;
                }

            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }

            return bRet;
        }

        public bool DeleteData(MAS_BIDDINGCOMPANY data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;
            try
            {
                string strQuery = "DELETE FROM [dbo].[tb_mas_BiddingCompany] WHERE [CompanyNo] = @CompanyNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);

                command.ExecuteNonQuery();

                bRet = true;
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }

            return bRet;
        }

        public MAS_BIDDINGCOMPANY GetData(string CompanyNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_BIDDINGCOMPANY retData = new MAS_BIDDINGCOMPANY();

            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_BiddingCompany] WHERE [CompanyNo] = @CompanyNo";
                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@CompanyNo", CompanyNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
                        }
                        if (!DBNull.Value.Equals(reader["TaxID"]))
                        {
                            retData.TaxID = (string)reader["TaxID"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyAddress"]))
                        {
                            retData.CompanyAddress = (string)reader["CompanyAddress"];
                        }
                        if (!DBNull.Value.Equals(reader["ContactName"]))
                        {
                            retData.ContactName = (string)reader["ContactName"];
                        }
                        if (!DBNull.Value.Equals(reader["MobilePhoneNo"]))
                        {
                            retData.MobilePhoneNo = (string)reader["MobilePhoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["TelephoneNo"]))
                        {
                            retData.TelephoneNo = (string)reader["TelephoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["Email"]))
                        {
                            retData.Email = (string)reader["Email"];
                        }
                        if (!DBNull.Value.Equals(reader["EmailCC"]))
                        {
                            retData.EmailCC = (string)reader["EmailCC"];
                        }
                        if (!DBNull.Value.Equals(reader["VatRegistrationNoFile"]))
                        {
                            retData.VatRegistrationNoFile = (string)reader["VatRegistrationNoFile"];
                        }
                        if (!DBNull.Value.Equals(reader["CertificateCompanyFile"]))
                        {
                            retData.CertificateCompanyFile = (string)reader["CertificateCompanyFile"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedBy"]))
                        {
                            retData.CreatedBy = (string)reader["CreatedBy"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            retData.CreatedDate = (DateTime)reader["CreatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedBy"]))
                        {
                            retData.UpdatedBy = (string)reader["UpdatedBy"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedDate"]))
                        {
                            retData.UpdatedDate = (DateTime)reader["UpdatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyWebsite"]))
                        {
                            retData.CompanyWebsite = (string)reader["CompanyWebsite"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyType"]))
                        {
                            retData.CompanyType = (string)reader["CompanyType"];
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return retData;
        }

        public MAS_BIDDINGCOMPANY GetCompanyByTaxIDNProjectNo(string TaxID, string ProjectNo)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_BIDDINGCOMPANY retData = new MAS_BIDDINGCOMPANY();

            try
            {
                //string strQuery = "SELECT * FROM [dbo].[tb_mas_BiddingCompany] WHERE [TaxID] = @TaxID";

                string strQuery = "SELECT c.* FROM tb_mas_BiddingCompany c INNER JOIN " +
                                  "tb_mas_Users u ON c.CompanyNo = u.CompanyNo " +
                                  "WHERE c.TaxID = @TaxID AND u.ProjectNo = @ProjectNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrWhiteSpace(TaxID))
                {
                    command.Parameters.AddWithValue("@TaxID", TaxID);
                }
                else
                {
                    command.Parameters.AddWithValue("@TaxID", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(ProjectNo))
                {
                    command.Parameters.AddWithValue("@ProjectNo", ProjectNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectNo", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
                        }
                        if (!DBNull.Value.Equals(reader["TaxID"]))
                        {
                            retData.TaxID = (string)reader["TaxID"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyAddress"]))
                        {
                            retData.CompanyAddress = (string)reader["CompanyAddress"];
                        }
                        if (!DBNull.Value.Equals(reader["ContactName"]))
                        {
                            retData.ContactName = (string)reader["ContactName"];
                        }
                        if (!DBNull.Value.Equals(reader["MobilePhoneNo"]))
                        {
                            retData.MobilePhoneNo = (string)reader["MobilePhoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["TelephoneNo"]))
                        {
                            retData.TelephoneNo = (string)reader["TelephoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["Email"]))
                        {
                            retData.Email = (string)reader["Email"];
                        }
                        if (!DBNull.Value.Equals(reader["EmailCC"]))
                        {
                            retData.EmailCC = (string)reader["EmailCC"];
                        }
                        if (!DBNull.Value.Equals(reader["VatRegistrationNoFile"]))
                        {
                            retData.VatRegistrationNoFile = (string)reader["VatRegistrationNoFile"];
                        }
                        if (!DBNull.Value.Equals(reader["CertificateCompanyFile"]))
                        {
                            retData.CertificateCompanyFile = (string)reader["CertificateCompanyFile"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedBy"]))
                        {
                            retData.CreatedBy = (string)reader["CreatedBy"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            retData.CreatedDate = (DateTime)reader["CreatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedBy"]))
                        {
                            retData.UpdatedBy = (string)reader["UpdatedBy"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedDate"]))
                        {
                            retData.UpdatedDate = (DateTime)reader["UpdatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyWebsite"]))
                        {
                            retData.CompanyWebsite = (string)reader["CompanyWebsite"];
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return retData;
        }

        public MAS_BIDDINGCOMPANY GetCompanyByTaxID(string TaxID)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_BIDDINGCOMPANY retData = new MAS_BIDDINGCOMPANY();

            try
            {               
                string strQuery = "SELECT c.* FROM tb_mas_BiddingCompany c INNER JOIN " +
                                  "tb_mas_Users u ON c.CompanyNo = u.CompanyNo " +
                                  "WHERE c.CompanyType != 'HO' AND c.TaxID = @TaxID; ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@TaxID", TaxID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
                        }
                        if (!DBNull.Value.Equals(reader["TaxID"]))
                        {
                            retData.TaxID = (string)reader["TaxID"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyAddress"]))
                        {
                            retData.CompanyAddress = (string)reader["CompanyAddress"];
                        }
                        if (!DBNull.Value.Equals(reader["ContactName"]))
                        {
                            retData.ContactName = (string)reader["ContactName"];
                        }
                        if (!DBNull.Value.Equals(reader["MobilePhoneNo"]))
                        {
                            retData.MobilePhoneNo = (string)reader["MobilePhoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["TelephoneNo"]))
                        {
                            retData.TelephoneNo = (string)reader["TelephoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["Email"]))
                        {
                            retData.Email = (string)reader["Email"];
                        }
                        if (!DBNull.Value.Equals(reader["EmailCC"]))
                        {
                            retData.EmailCC = (string)reader["EmailCC"];
                        }
                        if (!DBNull.Value.Equals(reader["VatRegistrationNoFile"]))
                        {
                            retData.VatRegistrationNoFile = (string)reader["VatRegistrationNoFile"];
                        }
                        if (!DBNull.Value.Equals(reader["CertificateCompanyFile"]))
                        {
                            retData.CertificateCompanyFile = (string)reader["CertificateCompanyFile"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedBy"]))
                        {
                            retData.CreatedBy = (string)reader["CreatedBy"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            retData.CreatedDate = (DateTime)reader["CreatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedBy"]))
                        {
                            retData.UpdatedBy = (string)reader["UpdatedBy"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedDate"]))
                        {
                            retData.UpdatedDate = (DateTime)reader["UpdatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyWebsite"]))
                        {
                            retData.CompanyWebsite = (string)reader["CompanyWebsite"];
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return retData;
        }

        public List<MAS_BIDDINGCOMPANY> ListBiddingCompany()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_BIDDINGCOMPANY> lRetData = new List<MAS_BIDDINGCOMPANY>();
            try
            {
                string strQuery = "SELECT distinct  bc.CompanyNo, bc.CompanyName, bc.TaxID " +
                                  "FROM tb_mas_BiddingCompany AS bc INNER JOIN tb_inf_Biddings AS b ON bc.CompanyNo = b.CompanyNo";
                SqlCommand command = new SqlCommand(strQuery, _conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_BIDDINGCOMPANY retData = new MAS_BIDDINGCOMPANY();

                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
                        }
                        if (!DBNull.Value.Equals(reader["TaxID"]))
                        {
                            retData.TaxID = (string)reader["TaxID"];
                        }
                        
                        lRetData.Add(retData);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return lRetData;
        }

        public bool InsertCompanyAttachData(MAS_COMPANYATTACHMENT data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool ret = false;
            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_mas_CompanyAttachment] " +
                                        "([CompanyNo] " +
                                        ",[FileName] " +
                                        ",[Description] " +
                                        ",[AttachFilePath] " +
                                        ",[CreatedBy] " +
                                        ",[CreatedDate] " +
                                        ",[UpdatedBy] " +
                                        ",[UpdatedDate]) " +
                                    "VALUES " +
                                        "(@CompanyNo " +
                                        ",@FileName " +
                                        ",@Description " +
                                        ",@AttachFilePath " +
                                        ",@CreatedBy " +
                                        ",@CreatedDate " +
                                        ",@UpdatedBy " +
                                        ",@UpdatedDate)";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                if (data.CompanyNo != null)
                {
                    command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyNo", DBNull.Value);
                }

                command.Parameters.AddWithValue("@FileName", string.IsNullOrWhiteSpace(data.FileName) ? "" : data.FileName);
                command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(data.Description) ? "" : data.Description);
                command.Parameters.AddWithValue("@AttachFilePath", data.AttachFilePath);

                command.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
                if (!string.IsNullOrEmpty(data.CreatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.CreatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@CreatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                }

                command.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
                if (!string.IsNullOrEmpty(data.UpdatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.UpdatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@UpdatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                }

                command.ExecuteNonQuery();

                ret = true;
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return ret;
        }

        public List<MAS_COMPANYUSER_DTO> ListCompanyUser(string CompanyName, string TaxID, string UserName, string ProjectName)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_COMPANYUSER_DTO> lRetData = new List<MAS_COMPANYUSER_DTO>();
            try
            {
                //string strQuery = "SELECT c.CompanyNo,c.CompanyName,c.TaxID,u.UserName,u.[Password] " +
                //                  "FROM tb_mas_BiddingCompany c INNER JOIN tb_mas_Users u ON c.CompanyNo = u.CompanyNo " +
                //                  "WHERE (u.RolesNo <> 1) AND (c.CompanyName LIKE '%' + @CompanyName+ '%' OR @CompanyName IS NULL) AND " +
                //                  "(c.TaxID LIKE '%' + @TaxID+ '%' OR @TaxID IS NULL) AND " +
                //                  "(u.UserName LIKE '%' + @UserName+ '%' OR @UserName IS NULL)";

                string strQuery = "SELECT DISTINCT c.CompanyNo,c.CompanyName,c.TaxID,u.UserName,u.[Password], b.ProjectName, u.[Status] " +
                                  "FROM tb_mas_BiddingCompany c INNER JOIN tb_mas_Users u ON c.CompanyNo = u.CompanyNo INNER JOIN " +
                                  "tb_mas_ProjectBidding b ON u.ProjectNo = b.ProjectNo " +
                                  "WHERE (u.RolesNo <> 1) AND (c.CompanyName LIKE '%' + @CompanyName+ '%' OR @CompanyName IS NULL) AND " +
                                  "(c.TaxID LIKE '%' + @TaxID+ '%' OR @TaxID IS NULL) AND " +
                                  "(u.UserName LIKE '%' + @UserName+ '%' OR @UserName IS NULL) AND " +
                                  "(b.ProjectName LIKE '%' + @ProjectName+ '%' OR @ProjectName IS NULL)";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                if (!string.IsNullOrWhiteSpace(CompanyName))
                {
                    command.Parameters.AddWithValue("@CompanyName", CompanyName);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(TaxID))
                {
                    command.Parameters.AddWithValue("@TaxID", TaxID);
                }
                else
                {
                    command.Parameters.AddWithValue("@TaxID", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                }
                else
                {
                    command.Parameters.AddWithValue("@UserName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(ProjectName))
                {
                    command.Parameters.AddWithValue("@ProjectName", ProjectName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_COMPANYUSER_DTO retData = new MAS_COMPANYUSER_DTO();

                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
                        }
                        if (!DBNull.Value.Equals(reader["TaxID"]))
                        {
                            retData.TaxID = (string)reader["TaxID"];
                        }
                        if (!DBNull.Value.Equals(reader["UserName"]))
                        {
                            retData.UserName = (string)reader["UserName"];
                        }
                        if (!DBNull.Value.Equals(reader["Password"]))
                        {
                            retData.Password = (string)reader["Password"];
                        }

                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }

                        if (!DBNull.Value.Equals(reader["Status"]))
                        {
                            retData.Status = (string)reader["Status"];
                        }

                        lRetData.Add(retData);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return lRetData;
        }

        public MAS_COMPANYUSER_DTO GetCompanyUserDetail(string UserName)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_COMPANYUSER_DTO retData = new MAS_COMPANYUSER_DTO();
            try
            {
                //string strQuery = "Select * From tb_mas_Users Where CompanyNo = @CompanyNo";
                string strQuery = "Select u.CompanyNo, u.UserName,u.[Password],u.UsersNo,b.ProjectNo,b.ProjectName,u.[Status] From tb_mas_Users u INNER JOIN " +
                                   "tb_mas_ProjectBidding b ON u.ProjectNo = b.ProjectNo Where u.UserName = @UserName ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                }
                else
                {
                    command.Parameters.AddWithValue("@UserName", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                     
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }                      
                        if (!DBNull.Value.Equals(reader["UserName"]))
                        {
                            retData.UserName = (string)reader["UserName"];
                        }
                        if (!DBNull.Value.Equals(reader["Password"]))
                        {
                            retData.Password = (string)reader["Password"];
                        }
                        if (!DBNull.Value.Equals(reader["UsersNo"]))
                        {
                            retData.UsersNo = (Int64)reader["UsersNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["Status"]))
                        {
                            retData.Status = (string)reader["Status"];
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return retData;
        }
        
        public List<MAS_COMPANYATTACHMENT> GetCompanyUserAttachFile(string CompanyNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_COMPANYATTACHMENT> lRetData = new List<MAS_COMPANYATTACHMENT>();
            try
            {
                string strQuery = "Select distinct CompanyNo, FileName, Description, AttachFilePath, CreatedDate " + 
                                  "From tb_mas_CompanyAttachment Where CompanyNo = @CompanyNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                if (!string.IsNullOrWhiteSpace(CompanyNo))
                {
                    command.Parameters.AddWithValue("@CompanyNo", CompanyNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyNo", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_COMPANYATTACHMENT readData = new MAS_COMPANYATTACHMENT();
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            readData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["FileName"]))
                        {
                            readData.FileName = (string)reader["FileName"];
                        }
                        if (!DBNull.Value.Equals(reader["Description"]))
                        {
                            readData.Description = (string)reader["Description"];
                        }
                        if (!DBNull.Value.Equals(reader["AttachFilePath"]))
                        {
                            readData.AttachFilePath = (string)reader["AttachFilePath"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            readData.CreatedDate = (DateTime)reader["CreatedDate"];
                        }

                        lRetData.Add(readData);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return lRetData;
        }

        public MAS_COMPANYUSER_DTO GetUserByUserName(string UserName)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_COMPANYUSER_DTO retData = new MAS_COMPANYUSER_DTO();
            try
            {
                string strQuery = "Select * From tb_mas_Users Where UserName = @UserName";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyNo", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["UsersNo"]))
                        {
                            retData.UsersNo = (Int64)reader["UsersNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["UserName"]))
                        {
                            retData.UserName = (string)reader["UserName"];
                        }
                        if (!DBNull.Value.Equals(reader["Password"]))
                        {
                            retData.Password = (string)reader["Password"];
                        }
                        if (!DBNull.Value.Equals(reader["RolesNo"]))
                        {
                            retData.RolesNo = (Int64)reader["RolesNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return retData;
        }
        
        public bool UpdatePassword(MAS_USERS userData)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "Update tb_mas_Users Set [Password] = @Password, UpdatedBy = @UpdatedBy, UpdatedDate = @UpdatedDate Where UsersNo = @UsersNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@Password", userData.Password);
                command.Parameters.AddWithValue("@UpdatedBy", userData.UpdatedBy);
                if (!string.IsNullOrEmpty(userData.UpdatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)userData.UpdatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@UpdatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                }
                command.Parameters.AddWithValue("@UsersNo", userData.UsersNo);

                command.ExecuteNonQuery();

                bRet = true;
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }

            return bRet;
        }
        
        public bool DeleteCompanyAttachData(string CompanyNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool ret = false;
            try
            {
                string strQuery = "DELETE FROM [dbo].[tb_mas_CompanyAttachment]  WHERE CompanyNo = @CompanyNo ";
                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                command.Parameters.AddWithValue("@CompanyNo", CompanyNo);
                command.ExecuteNonQuery();

                ret = true;
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);

                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return ret;
        }

        public bool UpdateUserStatus(MAS_USERS userData)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE tb_mas_Users SET [Status] = @Status, UpdatedBy = @UpdatedBy, UpdatedDate = @UpdatedDate Where UsersNo = @UsersNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@Status", userData.Status);
                command.Parameters.AddWithValue("@UpdatedBy", userData.UpdatedBy);
                if (!string.IsNullOrEmpty(userData.UpdatedDate.ToString()))
                {
                    DateTime dtNew = (DateTime)userData.UpdatedDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@UpdatedDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                }
                command.Parameters.AddWithValue("@UsersNo", userData.UsersNo);

                command.ExecuteNonQuery();

                bRet = true;
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }

            return bRet;
        }


        public string GenUserName(string DefaultUserName)
        {
            string strUseName = "";
            try
            {
                string _spName = "spGetUserName";
                SqlCommand command = new SqlCommand(_spName, _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserName", DefaultUserName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["UserName"]))
                        {
                            strUseName = (string)reader["UserName"];
                        }                       
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                strUseName = "";

                logger.Error(sqlEx);
            }
            catch (Exception ex)
            {
                strUseName = "";

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return strUseName;
        }

        #region #### Not Use ####

        //public List<MAS_PROJECTBIDDING_DTO> ListProjectBiding(string BiddingCode, string ProjectName, string BiddingMonth)
        //{
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        //    List<MAS_PROJECTBIDDING_DTO> lRetData = new List<MAS_PROJECTBIDDING_DTO>();
        //    try
        //    {
        //        string strQuery = "SELECT [ProjectNo],[ProjectName],[TemplateNo] " +
        //                            ",Convert(varchar, [StartDate], 103) AS StartDate " +
        //                            ",Convert(varchar, [EndDate], 103) AS EndDate " +
        //                            ",[BiddingCode] " +
        //                            "FROM tb_mas_ProjectBidding " +
        //                            "WHERE ([BiddingCode]  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
        //                            "([ProjectName]  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
        //                            "(MONTH([StartDate]) = @MONTH OR @MONTH IS NULL) ";              

        //        SqlCommand command = new SqlCommand(strQuery, _conn);

        //        if (!string.IsNullOrWhiteSpace(BiddingCode))
        //        {
        //            command.Parameters.AddWithValue("@BiddingCode", BiddingCode);
        //        }
        //        else
        //        {
        //            command.Parameters.AddWithValue("@BiddingCode", DBNull.Value);
        //        }

        //        if (!string.IsNullOrWhiteSpace(ProjectName))
        //        {
        //            command.Parameters.AddWithValue("@ProjectName", ProjectName);
        //        }
        //        else
        //        {
        //            command.Parameters.AddWithValue("@ProjectName", DBNull.Value);
        //        }

        //        if (!string.IsNullOrWhiteSpace(BiddingMonth))
        //        {
        //            command.Parameters.AddWithValue("@MONTH", BiddingMonth);
        //        }
        //        else
        //        {
        //            command.Parameters.AddWithValue("@MONTH", DBNull.Value);
        //        }

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                MAS_PROJECTBIDDING_DTO retData = new MAS_PROJECTBIDDING_DTO();

        //                if (!DBNull.Value.Equals(reader["ProjectNo"]))
        //                {
        //                    retData.ProjectNo = (Int64)reader["ProjectNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ProjectName"]))
        //                {
        //                    retData.ProjectName = (string)reader["ProjectName"];
        //                }
        //                if (!DBNull.Value.Equals(reader["TemplateNo"]))
        //                {
        //                    retData.TemplateNo = (Int64)reader["TemplateNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["StartDate"]))
        //                {
        //                    retData.StartDate = (string)reader["StartDate"];
        //                }
        //                if (!DBNull.Value.Equals(reader["EndDate"]))
        //                {
        //                    retData.EndDate = (string)reader["EndDate"];
        //                }
        //                if (!DBNull.Value.Equals(reader["BiddingCode"]))
        //                {
        //                    retData.BiddingCode = (string)reader["BiddingCode"];
        //                }

        //                lRetData.Add(retData);
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        logger.Error(sqlEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        logger.Error(ex.StackTrace);
        //    }

        //    return lRetData;
        //}

        //public bool UpdateBiddingCode(MAS_PROJECTBIDDING data)
        //{
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //    bool bRet = false;

        //    try
        //    {
        //        string strQuery = "UPDATE [dbo].[tb_mas_ProjectBidding] SET [BiddingCode]=@BiddingCode,[AttachFilePath]=@AttachFilePath WHERE [ProjectNo]=@ProjectNo;";

        //        SqlCommand command = new SqlCommand(strQuery, _conn);

        //        command.Parameters.AddWithValue("@BiddingCode", data.BiddingCode);
        //        command.Parameters.AddWithValue("@AttachFilePath", data.AttachFilePath);
        //        command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);
        //        if (command.ExecuteNonQuery() == 1)
        //        {
        //            bRet = true;
        //        }

        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        logger.Error(sqlEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        logger.Error(ex.StackTrace);
        //    }

        //    return bRet;
        //}

        #endregion
    }
}