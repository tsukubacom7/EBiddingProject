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
    public class Mas_ProjectITemBiddingBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_ProjectITemBiddingBL));

        public Mas_ProjectITemBiddingBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }

        public bool InsertData(MAS_PROJECTITEMBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_mas_ProjectITemBidding] " +
                                   "([ProjectNo] " +
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
                                   "(@ProjectNo " +
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
                                   ",@UpdatedDate)";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                command.Parameters.AddWithValue("@ProjectNo", Convert.ToInt64(data.ProjectNo));
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

        public bool UpdateData(MAS_PROJECTITEMBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE [dbo].[tb_mas_ProjectITemBidding] " +
                                "SET [ProjectNo] = @ProjectNo " +
                                ",[ItemColumn1] = @ItemColumn1 " +
                                ",[ItemColumn2] = @ItemColumn2 " +
                                ",[ItemColumn3] = @ItemColumn3 " +
                                ",[ItemColumn4] = @ItemColumn4 " +
                                ",[ItemColumn5] = @ItemColumn5 " +
                                ",[ItemColumn6] = @ItemColumn6 " +
                                ",[ItemColumn7] = @ItemColumn7 " +
                                ",[ItemColumn8] = @ItemColumn8 " +
                                ",[CreatedBy] = @CreatedBy " +
                                ",[CreatedDate] = @CreatedDate " +
                                ",[UpdatedBy] = @UpdatedBy " +
                                ",[UpdatedDate] = @UpdatedDate " +
                                "WHERE ProjectItemNo = @ProjectItemNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@ProjectItemNo", string.IsNullOrWhiteSpace(data.ProjectNo) ? null : data.ProjectNo);
                command.Parameters.AddWithValue("@ItemColumn1", string.IsNullOrWhiteSpace(data.ItemColumn1) ? null : data.ItemColumn1);
                command.Parameters.AddWithValue("@ItemColumn2", string.IsNullOrWhiteSpace(data.ItemColumn2) ? null : data.ItemColumn2);
                command.Parameters.AddWithValue("@ItemColumn3", string.IsNullOrWhiteSpace(data.ItemColumn3) ? null : data.ItemColumn3);
                command.Parameters.AddWithValue("@ItemColumn4", string.IsNullOrWhiteSpace(data.ItemColumn4) ? null : data.ItemColumn4);
                command.Parameters.AddWithValue("@ItemColumn5", string.IsNullOrWhiteSpace(data.ItemColumn5) ? null : data.ItemColumn5);
                command.Parameters.AddWithValue("@ItemColumn6", string.IsNullOrWhiteSpace(data.ItemColumn6) ? null : data.ItemColumn6);
                command.Parameters.AddWithValue("@ItemColumn7", string.IsNullOrWhiteSpace(data.ItemColumn7) ? null : data.ItemColumn7);
                command.Parameters.AddWithValue("@ItemColumn8", string.IsNullOrWhiteSpace(data.ItemColumn8) ? null : data.ItemColumn8);

                command.Parameters.AddWithValue("@CreatedBy", string.IsNullOrWhiteSpace(data.CreatedBy) ? null : data.CreatedBy);
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

                command.Parameters.AddWithValue("@UpdatedBy", string.IsNullOrWhiteSpace(data.UpdatedBy) ? null : data.UpdatedBy);
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

                command.Parameters.AddWithValue("@ProjectItemNo", data.ProjectItemNo);
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

        public bool DeleteData(MAS_PROJECTITEMBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;
            try
            {
                string strQuery = " DELETE FROM [dbo].[tb_mas_ProjectITemBidding]  WHERE [ProjectItemNo] = @ProjectItemNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@ProjectItemNo", data.ProjectItemNo);
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

        public MAS_PROJECTITEMBIDDING GetDatByPk(MAS_PROJECTITEMBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            MAS_PROJECTITEMBIDDING retData = new MAS_PROJECTITEMBIDDING();
            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_ProjectITemBidding] WHERE [ProjectItemNo] = @ProjectItemNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@ProjectItemNo", data.ProjectItemNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["ProjectItemNo"]))
                        {
                            retData.ProjectItemNo = (Int64)reader["ProjectItemNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (string)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn1"]))
                        {
                            retData.ItemColumn1 = (string)reader["ItemColumn1"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn2"]))
                        {
                            retData.ItemColumn2 = (string)reader["ItemColumn2"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn3"]))
                        {
                            retData.ItemColumn3 = (string)reader["ItemColumn3"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn4"]))
                        {
                            retData.ItemColumn4 = (string)reader["ItemColumn4"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn5"]))
                        {
                            retData.ItemColumn5 = (string)reader["ItemColumn5"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn6"]))
                        {
                            retData.ItemColumn6 = (string)reader["ItemColumn6"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn7"]))
                        {
                            retData.ItemColumn7 = (string)reader["ItemColumn7"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn8"]))
                        {
                            retData.ItemColumn8 = (string)reader["ItemColumn8"];
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

        public List<MAS_PROJECTITEMBIDDING> ListDataByProjectNo(MAS_PROJECTITEMBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<MAS_PROJECTITEMBIDDING> lRetData = new List<MAS_PROJECTITEMBIDDING>();
            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_ProjectITemBidding] WHERE [ProjectNo] = @ProjectNo ORDER BY [ItemColumn1] ASC ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_PROJECTITEMBIDDING retData = new MAS_PROJECTITEMBIDDING();
                        if (!DBNull.Value.Equals(reader["ProjectItemNo"]))
                        {
                            retData.ProjectItemNo = (Int64)reader["ProjectItemNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = reader["ProjectNo"].ToString();
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn1"]))
                        {
                            retData.ItemColumn1 = (string)reader["ItemColumn1"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn2"]))
                        {
                            retData.ItemColumn2 = (string)reader["ItemColumn2"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn3"]))
                        {
                            retData.ItemColumn3 = (string)reader["ItemColumn3"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn4"]))
                        {
                            retData.ItemColumn4 = (string)reader["ItemColumn4"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn5"]))
                        {
                            retData.ItemColumn5 = (string)reader["ItemColumn5"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn6"]))
                        {
                            retData.ItemColumn6 = (string)reader["ItemColumn6"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn7"]))
                        {
                            retData.ItemColumn7 = (string)reader["ItemColumn7"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn8"]))
                        {
                            retData.ItemColumn8 = (string)reader["ItemColumn8"];
                        }

                        lRetData.Add(retData);
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

            return lRetData;
        }

        public List<MAS_PROJECTITEMBIDDING> ListInfBiddingDetails(string BiddingNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<MAS_PROJECTITEMBIDDING> lRetData = new List<MAS_PROJECTITEMBIDDING>();
            try
            {
                string strQuery = "SELECT * FROM tb_inf_BiddingDetails WHERE BiddingsNo=@BiddingsNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@BiddingsNo", BiddingNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_PROJECTITEMBIDDING retData = new MAS_PROJECTITEMBIDDING();
                        if (!DBNull.Value.Equals(reader["ItemColumn1"]))
                        {
                            retData.ItemColumn1 = (string)reader["ItemColumn1"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn2"]))
                        {
                            retData.ItemColumn2 = (string)reader["ItemColumn2"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn3"]))
                        {
                            retData.ItemColumn3 = (string)reader["ItemColumn3"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn4"]))
                        {
                            retData.ItemColumn4 = (string)reader["ItemColumn4"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn5"]))
                        {
                            retData.ItemColumn5 = (string)reader["ItemColumn5"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn6"]))
                        {
                            retData.ItemColumn6 = (string)reader["ItemColumn6"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn7"]))
                        {
                            retData.ItemColumn7 = (string)reader["ItemColumn7"];
                        }
                        if (!DBNull.Value.Equals(reader["ItemColumn8"]))
                        {
                            retData.ItemColumn8 = (string)reader["ItemColumn8"];
                        }

                        lRetData.Add(retData);
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

            return lRetData;
        }


        public List<INF_BIDDINGATTACHMENT> ListInfBiddingAttachment(string BiddingNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<INF_BIDDINGATTACHMENT> lRetData = new List<INF_BIDDINGATTACHMENT>();
            try
            {
                string strQuery = "SELECT * FROM tb_inf_BiddingAttachment WHERE BiddingsNo = @BiddingsNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@BiddingsNo", BiddingNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        INF_BIDDINGATTACHMENT retData = new INF_BIDDINGATTACHMENT();

                        if (!DBNull.Value.Equals(reader["BiddingsNo"]))
                        {
                            retData.BiddingsNo = (Int64)reader["BiddingsNo"];
                        }
                        if (!DBNull.Value.Equals(reader["FileName"]))
                        {
                            retData.FileName = (string)reader["FileName"];
                        }
                        if (!DBNull.Value.Equals(reader["Description"]))
                        {
                            retData.Description = (string)reader["Description"];
                        }
                        if (!DBNull.Value.Equals(reader["AttachFilePath"]))
                        {
                            retData.AttachFilePath = (string)reader["AttachFilePath"];
                        }

                        lRetData.Add(retData);
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

            return lRetData;
        }


        public INF_BIDDINGS GetInfBidding(string BiddingNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            INF_BIDDINGS retData = new INF_BIDDINGS();
            try
            {
                string strQuery = "SELECT * FROM tb_inf_Biddings WHERE BiddingsNo = @BiddingsNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@BiddingsNo", BiddingNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["BiddingsNo"]))
                        {
                            retData.BiddingsNo = (Int64)reader["BiddingsNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingPrice"]))
                        {
                            retData.BiddingPrice = (Decimal)reader["BiddingPrice"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingVat7"]))
                        {
                            retData.BiddingVat7 = (Decimal)reader["BiddingVat7"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingTotalPrice"]))
                        {
                            retData.BiddingTotalPrice = (Decimal)reader["BiddingTotalPrice"];
                        }
                    }
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

            return retData;
        }

    }
}