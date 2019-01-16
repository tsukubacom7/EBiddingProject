using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EAuctionProj.DAL;
using EAuctionProj.Utility;
using System.Threading;

namespace EAuctionProj.BL
{
    public class Mas_ProjectBidding_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_ProjectBidding_Manage));

        public string InsertMasProjtBidding(MAS_PROJECTBIDDING data, List<MAS_PROJECTITEMBIDDING> lItemData)
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

                Mas_ProjectBiddingBL bidBL = new Mas_ProjectBiddingBL(conn, tran);
                Mas_ProjectITemBiddingBL itemBl = new Mas_ProjectITemBiddingBL(conn, tran);

                string pkProjectBD = string.Empty;
                pkProjectBD = bidBL.InsertData(data);
                if (!string.IsNullOrWhiteSpace(pkProjectBD))
                {
                    ret = pkProjectBD;

                    foreach (MAS_PROJECTITEMBIDDING item in lItemData)
                    {
                        MAS_PROJECTITEMBIDDING insItem = new MAS_PROJECTITEMBIDDING();
                        insItem.ProjectNo = pkProjectBD;
                        insItem.ItemColumn1 = item.ItemColumn1;
                        insItem.ItemColumn2 = item.ItemColumn2;
                        insItem.ItemColumn3 = item.ItemColumn3;
                        insItem.ItemColumn4 = item.ItemColumn4;
                        insItem.ItemColumn5 = item.ItemColumn5;
                        insItem.ItemColumn6 = item.ItemColumn6;
                        insItem.ItemColumn7 = item.ItemColumn7;
                        insItem.ItemColumn8 = item.ItemColumn8;

                        insItem.CreatedBy = data.CreatedBy;
                        insItem.CreatedDate = DateTime.Now;
                        insItem.UpdatedBy = data.UpdatedBy;
                        insItem.UpdatedDate = DateTime.Now;

                        itemBl.InsertData(insItem);
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
        
        public bool UpdateMasProjBidding(MAS_PROJECTBIDDING data)
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

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
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

        public bool DeleteMasProjBidding(MAS_PROJECTBIDDING data)
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

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
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

        public MAS_PROJECTBIDDING GetMasProjItemBidding(MAS_PROJECTBIDDING data)
        {
            IDbConnection conn = null;
            MAS_PROJECTBIDDING ret = new MAS_PROJECTBIDDING();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.GetData(data);

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

        public bool UpdateBiddingCode(MAS_PROJECTBIDDING data)
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

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.UpdateBiddingCode(data);

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

        public List<MAS_PROJECTBIDDING_DTO> ListBiddingProject(string BiddingCode, string ProjectName, string BiddingMonth, string UserName)
        {
            IDbConnection conn = null;
            List<MAS_PROJECTBIDDING_DTO> ret = null;

            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<MAS_PROJECTBIDDING_DTO>();
            
                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListProjectBiding(BiddingCode, ProjectName, BiddingMonth, UserName);
                
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

        public List<MAS_PROJECTBIDDING_DTO> ListProjectDefault(string BiddingCode, string ProjectName, string BiddingMonth, string Department)
        {
            IDbConnection conn = null;
            List<MAS_PROJECTBIDDING_DTO> ret = null;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<MAS_PROJECTBIDDING_DTO>();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListProjBidingDefault(BiddingCode, ProjectName, BiddingMonth, Department);

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


        public List<MAS_PROJECTBIDDING_DTO> ListBiddingProjectHistory(string BiddingCode, string ProjectName,
            string BiddingMonth, string Username, string CompanyName)
        {
            IDbConnection conn = null;
            List<MAS_PROJECTBIDDING_DTO> ret = null;

            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<MAS_PROJECTBIDDING_DTO>();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListBidingProjectHis(BiddingCode, ProjectName, BiddingMonth, Username, CompanyName);

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

        public List<MAS_PROJECTBIDDING> ListALLProject(MAS_PROJECTBIDDING data)
        {
            IDbConnection conn = null;
            List<MAS_PROJECTBIDDING> ret = null;

            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<MAS_PROJECTBIDDING>();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListAllData(data);

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
        
        public List<INF_PROJECTBIDDINGDETAIL_DTO> ListBiddingProjectHistoryDet(string ProjectNo)
        {
            IDbConnection conn = null;
            List<INF_PROJECTBIDDINGDETAIL_DTO> ret = null;

            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<INF_PROJECTBIDDINGDETAIL_DTO>();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListBidingProjectHisDet(ProjectNo);

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

        public List<INF_PROJECTBIDDINGDETAIL_DTO> ListAllCompanyBiddingHistory(string CompanyName, string TaxID)
        {
            IDbConnection conn = null;
            List<INF_PROJECTBIDDINGDETAIL_DTO> ret = null;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<INF_PROJECTBIDDINGDETAIL_DTO>();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListAllCompanyBidingHisDet(CompanyName, TaxID);
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


        public List<INF_PROJECTBIDDINGDETAIL_DTO> ListBiddingVendorProject(string CompanyNo, string ProjectName, string BiddingCode)
        {
            IDbConnection conn = null;
            List<INF_PROJECTBIDDINGDETAIL_DTO> ret = null;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<INF_PROJECTBIDDINGDETAIL_DTO>();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListBidingVendorDetail(CompanyNo, ProjectName, BiddingCode);
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


        public List<MAS_PROJECTBIDDING_DTO> ListAllProjBidingActive()
        {
            IDbConnection conn = null;
            List<MAS_PROJECTBIDDING_DTO> ret = null;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();
                ret = new List<MAS_PROJECTBIDDING_DTO>();

                Mas_ProjectBiddingBL bl = new Mas_ProjectBiddingBL(conn);
                ret = bl.ListAllProjBidingActive();

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