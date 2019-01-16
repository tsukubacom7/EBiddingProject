using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAuctionProj.DAL;
using System.Data;

namespace EAuctionProj.BL
{
    public class Mas_ProjectITemBidding_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_ProjectITemBidding_Manage));

        public bool InsertMasProjItemBidding(MAS_PROJECTITEMBIDDING data)
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

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
                ret = bl.InsertData(data);

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

        public bool UpdateMasProjItemBidding(MAS_PROJECTITEMBIDDING data)
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

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
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

        public bool DeleteMasProjItemBidding(MAS_PROJECTITEMBIDDING data)
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

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
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

        public MAS_PROJECTITEMBIDDING GetMasProjItemBidding(MAS_PROJECTITEMBIDDING data)
        {
            IDbConnection conn = null;
            MAS_PROJECTITEMBIDDING ret = new MAS_PROJECTITEMBIDDING();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
                ret = bl.GetDatByPk(data);

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

        public List<MAS_PROJECTITEMBIDDING> ListMasProjItemBiddingByPNo(MAS_PROJECTITEMBIDDING data)
        {
            IDbConnection conn = null;
            List<MAS_PROJECTITEMBIDDING> ret = new List<MAS_PROJECTITEMBIDDING>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
                ret = bl.ListDataByProjectNo(data);

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


        public List<MAS_PROJECTITEMBIDDING> ListInfBiddingDetails(string BiddingNo)
        {
            IDbConnection conn = null;
            List<MAS_PROJECTITEMBIDDING> ret = new List<MAS_PROJECTITEMBIDDING>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
                ret = bl.ListInfBiddingDetails(BiddingNo);

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


        public List<INF_BIDDINGATTACHMENT> ListInfBiddingAttachments(string BiddingNo)
        {
            IDbConnection conn = null;
            List<INF_BIDDINGATTACHMENT> ret = new List<INF_BIDDINGATTACHMENT>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
                ret = bl.ListInfBiddingAttachment(BiddingNo);

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


        public INF_BIDDINGS GetInfBidding(string BiddingNo)
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

                Mas_ProjectITemBiddingBL bl = new Mas_ProjectITemBiddingBL(conn);
                ret = bl.GetInfBidding(BiddingNo);

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