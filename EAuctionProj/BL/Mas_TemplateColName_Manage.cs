using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EAuctionProj.DAL;

namespace EAuctionProj.BL
{
    public class Mas_TemplateColName_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_TemplateColName_Manage));

        public bool InsertMasTemplateColName(MAS_TEMPLATECOLNAME data)
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

                Mas_TemplateColNameBL bl = new Mas_TemplateColNameBL(conn);
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

        public bool UpdateMasTemplateColName(MAS_TEMPLATECOLNAME data)
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

                Mas_TemplateColNameBL bl = new Mas_TemplateColNameBL(conn);
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

        public bool DeleteMasTemplateColName(MAS_TEMPLATECOLNAME data)
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

                Mas_TemplateColNameBL bl = new Mas_TemplateColNameBL(conn);
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

        public MAS_TEMPLATECOLNAME GetMasTemplateColNameByName(MAS_TEMPLATECOLNAME data)
        {
            IDbConnection conn = null;
            MAS_TEMPLATECOLNAME ret = new MAS_TEMPLATECOLNAME();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_TemplateColNameBL bl = new Mas_TemplateColNameBL(conn);
                ret = bl.GetDataByName(data);

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


        public MAS_TEMPLATECOLNAME GetMasTemplateColNameByKey(MAS_TEMPLATECOLNAME data)
        {
            IDbConnection conn = null;
            MAS_TEMPLATECOLNAME ret = new MAS_TEMPLATECOLNAME();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_TemplateColNameBL bl = new Mas_TemplateColNameBL(conn);
                ret = bl.GetDataByKey(data);

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


        public List<MAS_TEMPLATECOLNAME> ListMasTemplateColName()
        {
            IDbConnection conn = null;
            List<MAS_TEMPLATECOLNAME> ret = new List<MAS_TEMPLATECOLNAME>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_TemplateColNameBL bl = new Mas_TemplateColNameBL(conn);
                ret = bl.ListData();

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


        public List<MAS_TEMPLATECOLNAME> ListMasTemplateName()
        {
            IDbConnection conn = null;
            List<MAS_TEMPLATECOLNAME> ret = new List<MAS_TEMPLATECOLNAME>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_TemplateColNameBL bl = new Mas_TemplateColNameBL(conn);
                ret = bl.ListTemplateName();

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