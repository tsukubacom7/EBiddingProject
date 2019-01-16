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
    public class Inf_BiddingsBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_BiddingsBL));

        public Inf_BiddingsBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }

        public string InsertBiddings(INF_BIDDINGS data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string strRetPK = string.Empty;

            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_inf_Biddings] " +
                                       "([CompanyNo] " +
                                       ",[ProjectNo] " +
                                       ",[BiddingPrice] " +
                                       ",[BiddingVat7] " +
                                       ",[BiddingTotalPrice] " +
                                       ",[CreatedBy] " +
                                       ",[CreatedDate] " +
                                       ",[UpdatedBy] " +
                                       ",[UpdatedDate]) " +
                                  "VALUES " +
                                       "(@CompanyNo " +
                                       ",@ProjectNo " +
                                       ",@BiddingPrice " +
                                       ",@BiddingVat7 " +
                                       ",@BiddingTotalPrice " +
                                       ",@CreatedBy " +
                                       ",@CreatedDate " +
                                       ",@UpdatedBy " +
                                       ",@UpdatedDate); " +
                                       "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                command.Parameters.AddWithValue("@CompanyNo", Convert.ToInt64(data.CompanyNo));
                command.Parameters.AddWithValue("@ProjectNo", Convert.ToInt64(data.ProjectNo));

                if (data.BiddingPrice != null)
                {
                    command.Parameters.AddWithValue("@BiddingPrice", data.BiddingPrice);
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingPrice", DBNull.Value);
                }

                if (data.BiddingVat7 != null)
                {
                    command.Parameters.AddWithValue("@BiddingVat7", data.BiddingVat7);
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingVat7", DBNull.Value);
                }

                if (data.BiddingTotalPrice != null)
                {
                    command.Parameters.AddWithValue("@BiddingTotalPrice", data.BiddingTotalPrice);
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingTotalPrice", DBNull.Value);
                }
               
                command.Parameters.AddWithValue("@CreatedBy", string.IsNullOrWhiteSpace(data.CreatedBy) ? "" : data.CreatedBy);
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

                command.Parameters.AddWithValue("@UpdatedBy", string.IsNullOrWhiteSpace(data.UpdatedBy) ? "" : data.UpdatedBy);
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

                object biddingNo = command.ExecuteScalar();
                if (biddingNo != null)
                {
                    strRetPK = biddingNo.ToString();
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

            return strRetPK;
        }

        public bool InsertBiddingDetails(INF_BIDDINGDETAILS data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_inf_BiddingDetails] " +
                                       "([BiddingsNo] " +
                                       ",[ItemColumn1] " +
                                       ",[ItemColumn2] " +
                                       ",[ItemColumn3] " +
                                       ",[ItemColumn4] " + 
                                       ",[ItemColumn5] " +
                                       ",[ItemColumn6] " +
                                       ",[ItemColumn7] " +
                                       ",[ItemColumn8] " +
                                       ",[CreatedBy] " +
                                       ",[CreatedDate] " +
                                       ",[UpdatedBy] " +
                                       ",[UpdatedDate]) " +
                                  "VALUES " +
                                       "(@BiddingsNo " +
                                       ",@ItemColumn1 " +
                                       ",@ItemColumn2 " +
                                       ",@ItemColumn3 " +
                                       ",@ItemColumn4 " +
                                       ",@ItemColumn5 " +
                                       ",@ItemColumn6 " +
                                       ",@ItemColumn7 " +
                                       ",@ItemColumn8 " +
                                       ",@CreatedBy " +
                                       ",@CreatedDate " +
                                       ",@UpdatedBy " +
                                       ",@UpdatedDate) ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                command.Parameters.AddWithValue("@BiddingsNo", Convert.ToInt64(data.BiddingsNo));
                command.Parameters.AddWithValue("@ItemColumn1", string.IsNullOrWhiteSpace(data.ItemColumn1) ? "" : data.ItemColumn1);
                command.Parameters.AddWithValue("@ItemColumn2", string.IsNullOrWhiteSpace(data.ItemColumn2) ? "" : data.ItemColumn2);
                command.Parameters.AddWithValue("@ItemColumn3", string.IsNullOrWhiteSpace(data.ItemColumn3) ? "" : data.ItemColumn3);
                command.Parameters.AddWithValue("@ItemColumn4", string.IsNullOrWhiteSpace(data.ItemColumn4) ? "" : data.ItemColumn4);
                command.Parameters.AddWithValue("@ItemColumn5", string.IsNullOrWhiteSpace(data.ItemColumn5) ? "" : data.ItemColumn5);
                command.Parameters.AddWithValue("@ItemColumn6", string.IsNullOrWhiteSpace(data.ItemColumn6) ? "" : data.ItemColumn6);
                command.Parameters.AddWithValue("@ItemColumn7", string.IsNullOrWhiteSpace(data.ItemColumn7) ? "" : data.ItemColumn7);
                command.Parameters.AddWithValue("@ItemColumn8", string.IsNullOrWhiteSpace(data.ItemColumn8) ? "" : data.ItemColumn8);

                command.Parameters.AddWithValue("@CreatedBy", string.IsNullOrWhiteSpace(data.CreatedBy) ? "" : data.CreatedBy);
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

                command.Parameters.AddWithValue("@UpdatedBy", string.IsNullOrWhiteSpace(data.UpdatedBy) ? "" : data.UpdatedBy);
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

        public bool InsertBiddingAttachment(INF_BIDDINGATTACHMENT data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_inf_BiddingAttachment] " +
                                    "([BiddingsNo] " +
                                    ",[FileName] " +
                                    ",[Description] " +
                                    ",[AttachFilePath] " +
                                    ",[CreatedBy] " +
                                    ",[CreatedDate] " +
                                    ",[UpdatedBy] " +
                                    ",[UpdatedDate]) " +
                                    "VALUES " +
                                    "(@BiddingsNo" +
                                    ",@FileName " +
                                    ",@Description " +
                                    ",@AttachFilePath " +
                                    ",@CreatedBy " +
                                    ",@CreatedDate " +
                                    ",@UpdatedBy " +
                                    ",@UpdatedDate)";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                if (data.BiddingsNo != null || data.BiddingsNo > 0)
                {
                    command.Parameters.AddWithValue("@BiddingsNo", Convert.ToInt64(data.BiddingsNo));
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingsNo", DBNull.Value);
                }

                command.Parameters.AddWithValue("@FileName", string.IsNullOrWhiteSpace(data.FileName) ? "" : data.FileName);
                command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(data.Description) ? "" : data.Description);
                command.Parameters.AddWithValue("@AttachFilePath", string.IsNullOrWhiteSpace(data.AttachFilePath) ? "" : data.AttachFilePath);                  

                command.Parameters.AddWithValue("@CreatedBy", string.IsNullOrWhiteSpace(data.CreatedBy) ? "" : data.CreatedBy);
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

                command.Parameters.AddWithValue("@UpdatedBy", string.IsNullOrWhiteSpace(data.UpdatedBy) ? "" : data.UpdatedBy);
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
        
        public bool UpdateBiddingAttachment(INF_BIDDINGATTACHMENT data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE [dbo].[tb_inf_BiddingAttachment] " +
                                  "SET [BiddingsNo] = @BiddingsNo " +
                                        ",[FileName] = @FileName " +
                                        ",[AttachFilePath] = @AttachFilePath " +
                                        ",[Description] = @Description " +                                       
                                        ",[UpdatedBy] = @UpdatedBy " +
                                        ",[UpdatedDate] = @UpdatedDate " +
                                   "WHERE [AttachmentNo] = @AttachmentNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                if (data.BiddingsNo != null)
                {
                    command.Parameters.AddWithValue("@BiddingsNo", Convert.ToInt64(data.BiddingsNo));
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingsNo", DBNull.Value);
                }

                command.Parameters.AddWithValue("@FileName", string.IsNullOrWhiteSpace(data.FileName) ? "" : data.FileName);
                command.Parameters.AddWithValue("@AttachFilePath", string.IsNullOrWhiteSpace(data.AttachFilePath) ? "" : data.AttachFilePath);
                command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(data.Description) ? "" : data.Description);
                
                command.Parameters.AddWithValue("@UpdatedBy", string.IsNullOrWhiteSpace(data.UpdatedBy) ? "" : data.UpdatedBy);
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

                command.Parameters.AddWithValue("@AttachmentNo", data.AttachmentNo);

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

        public bool DeleteData(INF_BIDDINGS data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;
            try
            {
                string strQuery = " DELETE FROM [dbo].[tb_inf_Biddings]  WHERE [BiddingsNo] = @BiddingsNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@BiddingsNo", data.BiddingsNo);
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

        #region #### Function Not Use ####

        //public INF_BIDDINGS GetDatByPk(INF_BIDDINGS data)
        //{
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        //    INF_BIDDINGS retData = new INF_BIDDINGS();
        //    try
        //    {
        //        string strQuery = "SELECT * FROM [dbo].[tb_inf_Biddings] WHERE [BiddingNo] = @BiddingNo ";

        //        SqlCommand command = new SqlCommand(strQuery, _conn);
        //        command.Parameters.AddWithValue("@BiddingNo", data.BiddingNo);

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {                       
        //                if (!DBNull.Value.Equals(reader["BiddingNo"]))
        //                {
        //                    retData.BiddingNo = (Int64)reader["BiddingNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["CompanyNo"]))
        //                {
        //                    retData.CompanyNo = (Int64)reader["CompanyNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ProjectItemNo"]))
        //                {
        //                    retData.ProjectItemNo = (Int64)reader["ProjectItemNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ProjectNo"]))
        //                {
        //                    retData.ProjectNo = (Int64)reader["ProjectNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn1"]))
        //                {
        //                    retData.ItemColumn1 = (string)reader["ItemColumn1"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn2"]))
        //                {
        //                    retData.ItemColumn2 = (string)reader["ItemColumn2"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn3"]))
        //                {
        //                    retData.ItemColumn3 = (string)reader["ItemColumn3"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn4"]))
        //                {
        //                    retData.ItemColumn4 = (string)reader["ItemColumn4"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn5"]))
        //                {
        //                    retData.ItemColumn5 = (string)reader["ItemColumn5"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn6"]))
        //                {
        //                    retData.ItemColumn6 = (string)reader["ItemColumn6"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn7"]))
        //                {
        //                    retData.ItemColumn7 = (string)reader["ItemColumn7"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn8"]))
        //                {
        //                    retData.ItemColumn8 = (string)reader["ItemColumn8"];
        //                }
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

        //    return retData;
        //}

        //public List<INF_BIDDINGS> ListDataByCompanyNo(INF_BIDDINGS data)
        //{
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        //    List<INF_BIDDINGS> lRetData = new List<INF_BIDDINGS>();
        //    try
        //    {
        //        string strQuery = "SELECT * FROM [dbo].[tb_inf_Biddings] WHERE [CompanyNo] = @CompanyNo ";

        //        SqlCommand command = new SqlCommand(strQuery, _conn);
        //        command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                INF_BIDDINGS retData = new INF_BIDDINGS();

        //                if (!DBNull.Value.Equals(reader["BiddingNo"]))
        //                {
        //                    retData.BiddingNo = (Int64)reader["BiddingNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["CompanyNo"]))
        //                {
        //                    retData.CompanyNo = (Int64)reader["CompanyNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ProjectItemNo"]))
        //                {
        //                    retData.ProjectItemNo = (Int64)reader["ProjectItemNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ProjectNo"]))
        //                {
        //                    retData.ProjectNo = (Int64)reader["ProjectNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn1"]))
        //                {
        //                    retData.ItemColumn1 = (string)reader["ItemColumn1"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn2"]))
        //                {
        //                    retData.ItemColumn2 = (string)reader["ItemColumn2"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn3"]))
        //                {
        //                    retData.ItemColumn3 = (string)reader["ItemColumn3"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn4"]))
        //                {
        //                    retData.ItemColumn4 = (string)reader["ItemColumn4"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn5"]))
        //                {
        //                    retData.ItemColumn5 = (string)reader["ItemColumn5"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn6"]))
        //                {
        //                    retData.ItemColumn6 = (string)reader["ItemColumn6"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn7"]))
        //                {
        //                    retData.ItemColumn7 = (string)reader["ItemColumn7"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ItemColumn8"]))
        //                {
        //                    retData.ItemColumn8 = (string)reader["ItemColumn8"];
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

        #endregion

    }
}