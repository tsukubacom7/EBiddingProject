using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAuctionProj.DAL;
using System.Data;

namespace EAuctionProj.BL
{
    public class Mas_Company_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_Company_Manage));

        public List<MAS_COMPANY> ListMasCompany()
        {
            IDbConnection conn = null;
            List<MAS_COMPANY> ret = new List<MAS_COMPANY>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_CompanyBL bl = new Mas_CompanyBL(conn);
                ret = bl.ListCompanyName();

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
        
        public MAS_COMPANY GetMasCompanyByID(string CompanyNo)
        {
            IDbConnection conn = null;
            MAS_COMPANY ret = new MAS_COMPANY();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_CompanyBL bl = new Mas_CompanyBL(conn);
                ret = bl.GetCompanyByID(CompanyNo);

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
        
        public List<MAS_DEPARTMENT> ListDepartmentByComCode(string CompanyNo)
        {
            IDbConnection conn = null;
            List<MAS_DEPARTMENT> ret = new List<MAS_DEPARTMENT>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_CompanyBL bl = new Mas_CompanyBL(conn);
                ret = bl.ListDeprtmentByCompany(CompanyNo);
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