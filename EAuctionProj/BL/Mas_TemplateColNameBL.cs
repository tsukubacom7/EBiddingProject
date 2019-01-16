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
    public class Mas_TemplateColNameBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_TemplateColNameBL));    

        public Mas_TemplateColNameBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }

        public bool InsertData(MAS_TEMPLATECOLNAME data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_mas_TemplateColName] " +
                                   "([TemplateName] " +
                                   ",[ColumnName1] " +
                                   ",[ColumnName2] " +
                                   ",[ColumnName3] " +
                                   ",[ColumnName4] " +
                                   ",[ColumnName5] " +
                                   ",[ColumnName6] " +
                                   ",[ColumnName7] " +
                                   ",[ColumnName8] " +
                                   ",[CreatedBy] " +
                                   ",[CreatedDate] " +
                                   ",[UpdatedBy] " +
                                   ",[UpdatedDate]) " +
                                   "VALUES " +
                                   "(@TemplateName " +
                                   ",@ColumnName1 " +
                                   ",@ColumnName2 " +
                                   ",@ColumnName3 " +
                                   ",@ColumnName4 " +
                                   ",@ColumnName5 " +
                                   ",@ColumnName6 " +
                                   ",@ColumnName7 " +
                                   ",@ColumnName8 " +
                                   ",@CreatedBy " +
                                   ",@CreatedDate " +
                                   ",@UpdatedBy " +
                                   ",@UpdatedDate)";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@TemplateName", (string.IsNullOrWhiteSpace(data.TemplateName) ? "" : data.TemplateName));
                command.Parameters.AddWithValue("@ColumnName1", (string.IsNullOrWhiteSpace(data.ColumnName1) ? "" : data.ColumnName1));
                command.Parameters.AddWithValue("@ColumnName2", (string.IsNullOrWhiteSpace(data.ColumnName2) ? "" : data.ColumnName2));
                command.Parameters.AddWithValue("@ColumnName3", (string.IsNullOrWhiteSpace(data.ColumnName3) ? "" : data.ColumnName3));
                command.Parameters.AddWithValue("@ColumnName4", (string.IsNullOrWhiteSpace(data.ColumnName4) ? "" : data.ColumnName4));
                command.Parameters.AddWithValue("@ColumnName5", (string.IsNullOrWhiteSpace(data.ColumnName5) ? "" : data.ColumnName5));
                command.Parameters.AddWithValue("@ColumnName6", (string.IsNullOrWhiteSpace(data.ColumnName6) ? "" : data.ColumnName6));
                command.Parameters.AddWithValue("@ColumnName7", (string.IsNullOrWhiteSpace(data.ColumnName7) ? "" : data.ColumnName7));
                command.Parameters.AddWithValue("@ColumnName8", (string.IsNullOrWhiteSpace(data.ColumnName8) ? "" : data.ColumnName8));

                
                command.Parameters.AddWithValue("@CreatedBy", (string.IsNullOrWhiteSpace(data.CreatedBy) ? "" : data.CreatedBy));
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
                
                command.Parameters.AddWithValue("@UpdatedBy", (string.IsNullOrWhiteSpace(data.UpdatedBy) ? "" : data.UpdatedBy));
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

                if (command.ExecuteNonQuery() == 1)
                {
                    bRet = true;
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

            return bRet;
        }

        public bool UpdateData(MAS_TEMPLATECOLNAME data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE [dbo].[tb_mas_TemplateColName] " +
                                  "SET [TemplateName] = @TemplateName " +
                                  ",[ColumnName1] = @ColumnName1 " +
                                  ",[ColumnName2] = @ColumnName2 " +
                                  ",[ColumnName3] = @ColumnName3 " +
                                  ",[ColumnName4] = @ColumnName4 " +
                                  ",[ColumnName5] = @ColumnName5 " +
                                  ",[ColumnName6] = @ColumnName6 " +
                                  ",[ColumnName7] = @ColumnName7 " +
                                  ",[ColumnName8] = @ColumnName8 " +
                                  ",[CreatedBy] = @CreatedBy " +
                                  ",[CreatedDate] = @CreatedDate " +
                                  ",[UpdatedBy] = @UpdatedBy " +
                                  ",[UpdatedDate] = @UpdatedDate " +
                                  " WHERE [TemplateNo] = @TemplateNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@TemplateName", (string.IsNullOrWhiteSpace(data.TemplateName) ? "" : data.TemplateName));
                command.Parameters.AddWithValue("@ColumnName1", (string.IsNullOrWhiteSpace(data.ColumnName1) ? "" : data.ColumnName1));
                command.Parameters.AddWithValue("@ColumnName2", (string.IsNullOrWhiteSpace(data.ColumnName2) ? "" : data.ColumnName2));
                command.Parameters.AddWithValue("@ColumnName3", (string.IsNullOrWhiteSpace(data.ColumnName3) ? "" : data.ColumnName3));
                command.Parameters.AddWithValue("@ColumnName4", (string.IsNullOrWhiteSpace(data.ColumnName4) ? "" : data.ColumnName4));
                command.Parameters.AddWithValue("@ColumnName5", (string.IsNullOrWhiteSpace(data.ColumnName5) ? "" : data.ColumnName5));
                command.Parameters.AddWithValue("@ColumnName6", (string.IsNullOrWhiteSpace(data.ColumnName6) ? "" : data.ColumnName6));
                command.Parameters.AddWithValue("@ColumnName7", (string.IsNullOrWhiteSpace(data.ColumnName7) ? "" : data.ColumnName7));
                command.Parameters.AddWithValue("@ColumnName8", (string.IsNullOrWhiteSpace(data.ColumnName8) ? "" : data.ColumnName8));
                                
                command.Parameters.AddWithValue("@CreatedBy", (string.IsNullOrWhiteSpace(data.CreatedBy) ? "" : data.CreatedBy));
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
                
                command.Parameters.AddWithValue("@UpdatedBy", (string.IsNullOrWhiteSpace(data.UpdatedBy) ? "" : data.UpdatedBy));
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

                command.Parameters.AddWithValue("@TemplateNo", data.TemplateNo);

                if (command.ExecuteNonQuery() == 1)
                {
                    bRet = true;
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

            return bRet;
        }

        public bool DeleteData(MAS_TEMPLATECOLNAME data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;
            try
            {

                string strQuery = "DELETE FROM [dbo].[tb_mas_TemplateColName] WHERE TemplateNo = @TemplateNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@TemplateNo", data.TemplateNo);
                command.ExecuteNonQuery();

                bRet = true;
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

            return bRet;
        }

        public MAS_TEMPLATECOLNAME GetDataByName(MAS_TEMPLATECOLNAME data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_TEMPLATECOLNAME retData = new MAS_TEMPLATECOLNAME();

            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_TemplateColName] WHERE [TemplateName] = @TemplateName ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@TemplateName", data.TemplateName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            retData.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateName"]))
                        {
                            retData.TemplateName = (string)reader["TemplateName"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName1"]))
                        {
                            retData.ColumnName1 = (string)reader["ColumnName1"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName2"]))
                        {
                            retData.ColumnName2 = (string)reader["ColumnName2"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName3"]))
                        {
                            retData.ColumnName3 = (string)reader["ColumnName3"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName4"]))
                        {
                            retData.ColumnName4 = (string)reader["ColumnName4"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName5"]))
                        {
                            retData.ColumnName5 = (string)reader["ColumnName5"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName6"]))
                        {
                            retData.ColumnName6 = (string)reader["ColumnName6"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName7"]))
                        {
                            retData.ColumnName7 = (string)reader["ColumnName7"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName8"]))
                        {
                            retData.ColumnName8 = (string)reader["ColumnName8"];
                        }
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

            return retData;
        }

        public MAS_TEMPLATECOLNAME GetDataByKey(MAS_TEMPLATECOLNAME data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_TEMPLATECOLNAME retData = new MAS_TEMPLATECOLNAME();

            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_TemplateColName] WHERE [TemplateNo] = @TemplateNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@TemplateNo", data.TemplateNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            retData.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateName"]))
                        {
                            retData.TemplateName = (string)reader["TemplateName"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName1"]))
                        {
                            retData.ColumnName1 = (string)reader["ColumnName1"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName2"]))
                        {
                            retData.ColumnName2 = (string)reader["ColumnName2"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName3"]))
                        {
                            retData.ColumnName3 = (string)reader["ColumnName3"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName4"]))
                        {
                            retData.ColumnName4 = (string)reader["ColumnName4"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName5"]))
                        {
                            retData.ColumnName5 = (string)reader["ColumnName5"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName6"]))
                        {
                            retData.ColumnName6 = (string)reader["ColumnName6"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName7"]))
                        {
                            retData.ColumnName7 = (string)reader["ColumnName7"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName8"]))
                        {
                            retData.ColumnName8 = (string)reader["ColumnName8"];
                        }
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

            return retData;
        }

        public List<MAS_TEMPLATECOLNAME> ListData()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<MAS_TEMPLATECOLNAME> lRet = new List<MAS_TEMPLATECOLNAME>();

            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_TemplateColName] ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_TEMPLATECOLNAME data = new MAS_TEMPLATECOLNAME();

                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            data.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateName"]))
                        {
                            data.TemplateName = (string)reader["TemplateName"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName1"]))
                        {
                            data.ColumnName1 = (string)reader["ColumnName1"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName2"]))
                        {
                            data.ColumnName2 = (string)reader["ColumnName2"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName3"]))
                        {
                            data.ColumnName3 = (string)reader["ColumnName3"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName4"]))
                        {
                            data.ColumnName4 = (string)reader["ColumnName4"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName5"]))
                        {
                            data.ColumnName5 = (string)reader["ColumnName5"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName6"]))
                        {
                            data.ColumnName6 = (string)reader["ColumnName6"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName7"]))
                        {
                            data.ColumnName7 = (string)reader["ColumnName7"];
                        }
                        if (!DBNull.Value.Equals(reader["ColumnName8"]))
                        {
                            data.ColumnName8 = (string)reader["ColumnName8"];
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
        
        public List<MAS_TEMPLATECOLNAME> ListTemplateName()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<MAS_TEMPLATECOLNAME> lRet = new List<MAS_TEMPLATECOLNAME>();
            try
            {
                string strQuery = "SELECT DISTINCT TemplateNo,TemplateName FROM  tb_mas_TemplateColName ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_TEMPLATECOLNAME data = new MAS_TEMPLATECOLNAME();

                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            data.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateName"]))
                        {
                            data.TemplateName = (string)reader["TemplateName"];
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