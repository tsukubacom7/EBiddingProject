using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using EAuctionProj.DAL;

namespace EAuctionProj.BL
{
    public class Mas_ColumnNameBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_ColumnNameBL));

        public Mas_ColumnNameBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }     

        public List<MAS_COLUMNNAME> ListColumName()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<MAS_COLUMNNAME> lRet = null;
            try
            {
                string strQuery = "SELECT * FROM tb_mas_ColumnName; ";
             
                SqlCommand command = new SqlCommand(strQuery, _conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    lRet = new List<MAS_COLUMNNAME>();

                    while (reader.Read())
                    {
                        MAS_COLUMNNAME data = new MAS_COLUMNNAME();

                        if (!DBNull.Value.Equals(reader["ColumnRunNo"]))
                        {
                            data.ColumnRunNo = (Int64)reader["ColumnRunNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName"]))
                        {
                            data.ColumnName = (string)reader["ColumnName"];
                        }

                        lRet.Add(data);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error(sqlEx);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lRet;
        }
    }
}