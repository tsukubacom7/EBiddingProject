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
    public class Inf_QuestionnaireBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_QuestionnaireBL));

        public Inf_QuestionnaireBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }


        public bool InsertData(INF_QUESTIONNAIRE data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_inf_Questionnaire] " +
                                   "([ProjectNo] " +
                                   ",[CompanyNo] " +
                                   ",[AnsQuestion1] " +
                                   ",[AnsQuestion2] " +
                                   ",[AnsQuestion3] " +
                                   ",[AnsQuestion4] " +
                                   ",[AnsQuestion5] " +
                                   ",[AnsQuestion6] " +
                                   ",[AnsQuestion7] " +
                                   ",[AnsQuestion8] " +
                                   ",[CreatedDate] " +
                                   ",[CreatedBy] " +
                                   ",[UpdatedDate] " +
                                   ",[UpdatedBy]) " +
                                   "VALUES " +
                                   "(@ProjectNo " +
                                   ",@CompanyNo " +
                                   ",@AnsQuestion1 " +
                                   ",@AnsQuestion2 " +
                                   ",@AnsQuestion3 " +
                                   ",@AnsQuestion4 " +
                                   ",@AnsQuestion5 " +
                                   ",@AnsQuestion6 " +
                                   ",@AnsQuestion7 " +
                                   ",@AnsQuestion8 " +
                                   ",@CreatedDate " +
                                   ",@CreatedBy " +
                                   ",@UpdatedDate " +
                                   ",@UpdatedBy)";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrWhiteSpace(data.ProjectNo))
                {
                    command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectNo", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(data.CompanyNo))
                {
                    command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyNo", DBNull.Value);
                }

                if (data.AnsQuestion1 != null)
                {
                    command.Parameters.AddWithValue("@AnsQuestion1", data.AnsQuestion1);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion1", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(data.AnsQuestion2))
                {
                    command.Parameters.AddWithValue("@AnsQuestion2", data.AnsQuestion2);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion2", DBNull.Value);
                }

                if (data.AnsQuestion3 != null)
                {
                    command.Parameters.AddWithValue("@AnsQuestion3", data.AnsQuestion3);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion3", DBNull.Value);
                }

                if (data.AnsQuestion4 != null)
                {
                    command.Parameters.AddWithValue("@AnsQuestion4", data.AnsQuestion4);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion4", DBNull.Value);
                }

                if (data.AnsQuestion5 != null)
                {
                    command.Parameters.AddWithValue("@AnsQuestion5", data.AnsQuestion5);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion5", DBNull.Value);
                }

                if (data.AnsQuestion6 != null)
                {
                    command.Parameters.AddWithValue("@AnsQuestion6", data.AnsQuestion6);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion6", DBNull.Value);
                }

                if (data.AnsQuestion7 != null)
                {
                    command.Parameters.AddWithValue("@AnsQuestion7", data.AnsQuestion7);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion7", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(data.AnsQuestion8))
                {
                    command.Parameters.AddWithValue("@AnsQuestion8", data.AnsQuestion8);
                }
                else
                {
                    command.Parameters.AddWithValue("@AnsQuestion8", DBNull.Value);
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

        public List<INF_QUESTIONNAIRE> ListAllData()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<INF_QUESTIONNAIRE> lRet = new List<INF_QUESTIONNAIRE>();
            try
            {
                string strQuery = "SELECT [QuestionNo] " +
                                    ",[ProjectNo] " +
                                    ",[CompanyNo] " +
                                    ",[AnsQuestion1] " +
                                    ",[AnsQuestion2] " +
                                    ",[AnsQuestion3] " +
                                    ",[AnsQuestion4] " +
                                    ",[AnsQuestion5] " +
                                    ",[AnsQuestion6] " +
                                    ",[AnsQuestion7] " +
                                    ",[AnsQuestion8] " +
                                    ",[CreatedDate] " +
                                    ",[CreatedBy] " +
                                    ",[UpdatedDate] " +
                                    ",[UpdatedBy] " +
                                    "FROM [tb_inf_Questionnaire]";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        INF_QUESTIONNAIRE data = new INF_QUESTIONNAIRE();

                        if (!DBNull.Value.Equals(reader["QuestionNo"]))
                        {
                            data.QuestionNo = (Int64)reader["QuestionNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            data.ProjectNo = (string)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            data.CompanyNo = (string)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["AnsQuestion1"]))
                        {
                            data.AnsQuestion1 = (int)reader["AnsQuestion1"];
                        }
                        if (!DBNull.Value.Equals(reader["AnsQuestion2"]))
                        {
                            data.AnsQuestion2 = (string)reader["AnsQuestion2"];
                        }
                        if (!DBNull.Value.Equals(reader["AnsQuestion3"]))
                        {
                            data.AnsQuestion3 = (int)reader["AnsQuestion3"];
                        }
                        if (!DBNull.Value.Equals(reader["AnsQuestion4"]))
                        {
                            data.AnsQuestion4 = (int)reader["AnsQuestion4"];
                        }
                        if (!DBNull.Value.Equals(reader["AnsQuestion5"]))
                        {
                            data.AnsQuestion5 = (int)reader["AnsQuestion5"];
                        }
                        if (!DBNull.Value.Equals(reader["AnsQuestion6"]))
                        {
                            data.AnsQuestion6 = (int)reader["AnsQuestion6"];
                        }
                        if (!DBNull.Value.Equals(reader["AnsQuestion7"]))
                        {
                            data.AnsQuestion7 = (int)reader["AnsQuestion7"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion8"]))
                        {
                            data.AnsQuestion8 = (string)reader["AnsQuestion8"];
                        }

                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            data.CreatedDate = (DateTime)reader["CreatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedBy"]))
                        {
                            data.CreatedBy = (string)reader["CreatedBy"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedDate"]))
                        {
                            data.UpdatedDate = (DateTime)reader["UpdatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["UpdatedBy"]))
                        {
                            data.UpdatedBy = (string)reader["UpdatedBy"];
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

        public INF_QUESTIONNAIRE GetByCompanyNo(INF_QUESTIONNAIRE data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            INF_QUESTIONNAIRE ret = null;
            try
            {
                string strQuery = "SELECT * FROM tb_inf_Questionnaire WHERE ProjectNo=@ProjectNo AND CompanyNo=@CompanyNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);
                command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = new INF_QUESTIONNAIRE();

                        if (!DBNull.Value.Equals(reader["QuestionNo"]))
                        {
                            ret.QuestionNo = (Int64)reader["QuestionNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            ret.ProjectNo = (string)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            ret.CompanyNo = (string)reader["CompanyNo"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion1"]))
                        {
                            ret.AnsQuestion1 = (int)reader["AnsQuestion1"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion2"]))
                        {
                            ret.AnsQuestion2 = (string)reader["AnsQuestion2"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion3"]))
                        {
                            ret.AnsQuestion3 = (int)reader["AnsQuestion3"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion4"]))
                        {
                            ret.AnsQuestion4 = (int)reader["AnsQuestion4"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion5"]))
                        {
                            ret.AnsQuestion5 = (int)reader["AnsQuestion5"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion6"]))
                        {
                            ret.AnsQuestion6 = (int)reader["AnsQuestion6"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion7"]))
                        {
                            ret.AnsQuestion7 = (int)reader["AnsQuestion7"];
                        }  
                                             
                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            ret.CreatedDate = (DateTime)reader["CreatedDate"];
                        }

                        if (!DBNull.Value.Equals(reader["CreatedBy"]))
                        {
                            ret.CreatedBy = (string)reader["CreatedBy"];
                        }

                        if (!DBNull.Value.Equals(reader["UpdatedDate"]))
                        {
                            ret.UpdatedDate = (DateTime)reader["UpdatedDate"];
                        }

                        if (!DBNull.Value.Equals(reader["UpdatedBy"]))
                        {
                            ret.UpdatedBy = (string)reader["UpdatedBy"];
                        }

                        if (!DBNull.Value.Equals(reader["AnsQuestion8"]))
                        {
                            ret.AnsQuestion8 = (string)reader["AnsQuestion8"];
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

            return ret;
        }
    }
}