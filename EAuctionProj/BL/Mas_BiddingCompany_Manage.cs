using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAuctionProj.DAL;
using System.Data;
using System.Threading;
using EAuctionProj.Utility;

namespace EAuctionProj.BL
{
    public class Mas_BiddingCompany_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_BiddingCompany_Manage));

        #region #### Not Use ####
        //public string InsertBiddingCompany(MAS_BIDDINGCOMPANY data)
        //{
        //    IDbConnection conn = null;
        //    string ret = string.Empty;
        //    try
        //    {
        //        //SET CONNECTION
        //        conn = ConnectionFactory.GetConnection();
        //        conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

        //        //OPEN CONNECTION
        //        conn.Open();

        //        Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
        //        ret = bl.InsertData(data);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        logger.Error(ex.StackTrace);
        //    }
        //    finally
        //    {
        //        if (conn != null)
        //        {
        //            if (conn.State == ConnectionState.Open)
        //            {
        //                conn.Close();
        //            }
        //            conn.Dispose();
        //        }
        //    }

        //    return ret;
        //}
        #endregion

        public string RegisterCompany(MAS_BIDDINGCOMPANY companyData, MAS_USERS userData)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            IDbConnection conn = null;
            IDbTransaction tran = null;

            string ret = string.Empty;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                tran = conn.BeginTransaction(IsolationLevel.Serializable);

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn, tran);
                string _companyNo = string.Empty;

                if (companyData.CompanyNo != null)
                {
                    //************* For Update Company Register *************//
                    _companyNo = companyData.CompanyNo.ToString().Trim();
                    bool bRet = bl.UpdateData(companyData);
                }
                else
                {
                    _companyNo = bl.InsertData(companyData);
                }

                if (!string.IsNullOrWhiteSpace(_companyNo))
                {
                    userData.CompanyNo = Int64.Parse(_companyNo);
                    /************ Generate UserName *********************/
                    string _userName = GenUserName(_companyNo);
                    userData.UserName = _userName;
                    /****************************************************/
                    string userNo = bl.InsertUsersData(userData);
                    if (!string.IsNullOrWhiteSpace(userNo))
                    {
                        ret = _companyNo + ";" + _userName;

                        #region #### Insert Attach (Not Use) ####
                        /******* Inset table [tb_mas_CompanyAttachment] *******/
                        //foreach (var item in lCompanyAttach)
                        //{
                        //    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                        //    data.CompanyNo = Int64.Parse(_companyNo);
                        //    data.AttachFilePath = item.AttachFilePath;
                        //    data.FileName = item.FileName;
                        //    data.Description = item.Description;
                        //    data.CreatedBy = item.CreatedBy;
                        //    data.CreatedDate = item.CreatedDate;
                        //    data.UpdatedBy = item.UpdatedBy;
                        //    data.UpdatedDate = item.UpdatedDate;

                        //    bl.InsertCompanyAttachData(data);
                        //}
                        /******************************************************/
                        #endregion
                    }
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                ret = string.Empty;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }

                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }        


        public bool UpdateInfBiddingCompany(MAS_BIDDINGCOMPANY data)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.UpdateData(data);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public bool DeleteInfBiddingCompany(MAS_BIDDINGCOMPANY data)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.DeleteData(data);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }


        public List<MAS_BIDDINGCOMPANY> ListBiddingCompany()
        {
            IDbConnection conn = null;
            List<MAS_BIDDINGCOMPANY> ret = new List<MAS_BIDDINGCOMPANY>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.ListBiddingCompany();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public MAS_BIDDINGCOMPANY GetBiddingCompany(string CompanyNo)
        {
            IDbConnection conn = null;
            MAS_BIDDINGCOMPANY ret = new MAS_BIDDINGCOMPANY();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.GetData(CompanyNo);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public bool InsertCompanyAttach(List<MAS_COMPANYATTACHMENT> lData)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                /******* Inset table [tb_mas_CompanyAttachment] *******/
                foreach (var item in lData)
                {
                    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                    data = item;
                    ret = bl.InsertCompanyAttachData(data);
                }
               
                /******************************************************/
            }
            catch (Exception ex)
            {
                ret = false;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public List<MAS_COMPANYUSER_DTO> ListCompanyUser(string CompanyName, string TaxID, string UserName, string ProjectName)
        {
            IDbConnection conn = null;
            List<MAS_COMPANYUSER_DTO> ret = new List<MAS_COMPANYUSER_DTO>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.ListCompanyUser(CompanyName, TaxID, UserName, ProjectName);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public MAS_COMPANYUSER_DTO GetCompanyUserDetail(string UserName)
        {
            IDbConnection conn = null;
            MAS_COMPANYUSER_DTO ret = new MAS_COMPANYUSER_DTO();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.GetCompanyUserDetail(UserName);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }
        public List<MAS_COMPANYATTACHMENT> GetCompanyUserAttachFile(string CompanyNo)
        {
            IDbConnection conn = null;
            List<MAS_COMPANYATTACHMENT> ret = new List<MAS_COMPANYATTACHMENT>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.GetCompanyUserAttachFile(CompanyNo);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public MAS_COMPANYUSER_DTO GetUserLogin(string UserName)
        {
            IDbConnection conn = null;
            MAS_COMPANYUSER_DTO ret = new MAS_COMPANYUSER_DTO();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.GetUserByUserName(UserName);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public bool ResetPassword(MAS_USERS userData)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.UpdatePassword(userData);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }
        
        public bool IsExistCompany(string TaxID, string ProjectNo)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                MAS_BIDDINGCOMPANY retData = new MAS_BIDDINGCOMPANY();
                retData = bl.GetCompanyByTaxIDNProjectNo(TaxID, ProjectNo);
                if (retData != null && retData.CompanyNo > 0)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public MAS_BIDDINGCOMPANY GetCompanyByTaxID(string TaxID)
        {
            IDbConnection conn = null;
            MAS_BIDDINGCOMPANY retData = new MAS_BIDDINGCOMPANY();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);               
                retData = bl.GetCompanyByTaxID(TaxID);
             
            }
            catch (Exception ex)
            {
                retData = null;
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return retData;
        }

        private string GenUserName(string CompanyNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string strUserNo = "";

            /************************** GBD0001 ****************************/
            string strPrefix = "GBD";
            string strCodeRunNo = "000" + CompanyNo;
            GlobalFunction func = new GlobalFunction();
            string runnNo = func.RightFunction(strCodeRunNo, 4);

            strUserNo = strPrefix + runnNo;
            /****************************************************************/
            //************* Check Exist User And Get New *******************//
            strUserNo = GetNextUserName(strUserNo);
            //*************************************************************//

            return strUserNo;
        }
        
        public bool UpdateCompanyAttach(List<MAS_COMPANYATTACHMENT> lData, string CompanyNo)
        {
            IDbConnection conn = null;
            IDbTransaction tran = null;

            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                tran = conn.BeginTransaction(IsolationLevel.Serializable);

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn, tran);
                /********** Delete CompanyAttach before Insert **********/
                bool retDel = bl.DeleteCompanyAttachData(CompanyNo);
                /********************************************************/
                /******* Inset table [tb_mas_CompanyAttachment]  After delete *******/
                if (retDel)
                {
                    foreach (MAS_COMPANYATTACHMENT item in lData)
                    {
                        MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                        data = item;
                        ret = bl.InsertCompanyAttachData(data);
                    }
                }
                /******************************************************/

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                ret = false;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }

                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }

        public bool UpdateUserStatus(MAS_USERS userData)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.UpdateUserStatus(userData);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }


        public string GetNextUserName(string DefaultUSerName)
        {
            IDbConnection conn = null;
            string ret = string.Empty;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_BiddingCompanyBL bl = new Mas_BiddingCompanyBL(conn);
                ret = bl.GenUserName(DefaultUSerName);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }

            return ret;
        }


    }
}