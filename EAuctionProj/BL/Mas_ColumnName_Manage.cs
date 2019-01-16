using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAuctionProj.DAL;
using System.Data;
using System.Data.SqlClient;

namespace EAuctionProj.BL
{
    public class Mas_ColumnName_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_ColumnName_Manage));

        public List<MAS_COLUMNNAME> ListColumName()
        {
            IDbConnection conn = null;

            List<MAS_COLUMNNAME> lRet = new List<MAS_COLUMNNAME>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;
            
                //OPEN CONNECTION
                conn.Open();

                Mas_ColumnNameBL bl = new Mas_ColumnNameBL(conn);
                lRet = bl.ListColumName();

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

            return lRet;
        }
    }
}