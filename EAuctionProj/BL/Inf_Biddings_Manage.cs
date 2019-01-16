using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAuctionProj.DAL;
using System.Data;
using System.Threading;

namespace EAuctionProj.BL
{
    public class Inf_Biddings_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_Biddings_Manage));
        
        public string InsSubmitBiddings(INF_BIDDINGS data, List<INF_BIDDINGDETAILS> lItemDetail, 
            List<INF_BIDDINGATTACHMENT> lItemAttach)
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

                Inf_BiddingsBL bl = new Inf_BiddingsBL(conn, tran);

                string _biddingNo = string.Empty;
                _biddingNo = bl.InsertBiddings(data);

                if (!string.IsNullOrWhiteSpace(_biddingNo))
                {
                    ret = _biddingNo;
                    bool insResut = false;
                    foreach (INF_BIDDINGDETAILS item in lItemDetail)
                    {
                        INF_BIDDINGDETAILS insItem = new INF_BIDDINGDETAILS();
                        insItem.BiddingsNo = Int64.Parse(_biddingNo);
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

                        insResut = bl.InsertBiddingDetails(insItem);
                    }

                    if (insResut)
                    {
                        if (lItemAttach != null && lItemAttach.Count > 0)
                        {
                            foreach (INF_BIDDINGATTACHMENT itemAttach in lItemAttach)
                            {
                                INF_BIDDINGATTACHMENT insItem = new INF_BIDDINGATTACHMENT();
                                insItem.BiddingsNo = Int64.Parse(_biddingNo);
                                insItem.AttachFilePath = itemAttach.AttachFilePath;
                                insItem.Description = itemAttach.Description;
                                insItem.FileName = itemAttach.FileName;

                                insItem.CreatedBy = data.CreatedBy;
                                insItem.CreatedDate = DateTime.Now;
                                insItem.UpdatedBy = data.UpdatedBy;
                                insItem.UpdatedDate = DateTime.Now;

                                insResut = bl.InsertBiddingAttachment(insItem);
                            }
                        }
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

        public bool DeleteInfBiddings(INF_BIDDINGS data)
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

                Inf_BiddingsBL bl = new Inf_BiddingsBL(conn);
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

        public INF_BIDDINGS GetInfBiddingsByPK(INF_BIDDINGS data)
        {
            IDbConnection conn = null;
            INF_BIDDINGS ret = new INF_BIDDINGS();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                //Inf_BiddingsBL bl = new Inf_BiddingsBL(conn);
                //ret = bl.GetDatByPk(data);

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

        public List<INF_BIDDINGS> ListInfBiddingsByCompany(INF_BIDDINGS data)
        {
            IDbConnection conn = null;
            List<INF_BIDDINGS> ret = new List<INF_BIDDINGS>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                //Inf_BiddingsBL bl = new Inf_BiddingsBL(conn);
                //ret = bl.ListDataByCompanyNo(data);

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