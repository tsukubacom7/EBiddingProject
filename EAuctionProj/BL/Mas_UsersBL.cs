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
    public class Mas_UsersBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_UsersBL));

        public Mas_UsersBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }

        public string InsertData(MAS_USERS data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string retUserNo = string.Empty;
            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_mas_Users] " +
                                        "([UserName] " +
                                        ",[Password] " +
                                        ",[RolesNo] " +
                                        ",[CompanyNo] " +
                                        ",[CreatedBy] " +
                                        ",[CreatedDate] " +
                                        ",[UpdatedBy] " +
                                        ",[UpdatedDate]) " +
                                    "VALUES " +
                                        "(@UserName " +
                                        ",@Password " +
                                        ",@RolesNo " +
                                        ",@CompanyNo " +
                                        ",@CreatedBy " +
                                        ",@CreatedDate " +
                                        ",@UpdatedBy " +
                                        ",@UpdatedDate); " +
                                        " SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;
                command.Parameters.AddWithValue("@UserName", data.UserName);
                command.Parameters.AddWithValue("@Password", data.Password);
                command.Parameters.AddWithValue("@RolesNo", data.RolesNo);
                command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);

                command.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
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

                command.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
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

                object _CompanyNo = command.ExecuteScalar();
                if (_CompanyNo != null)
                {
                    retUserNo = _CompanyNo.ToString();
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

            return retUserNo;
        }

        public bool UpdateData(MAS_USERS data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE [dbo].[tb_mas_Users] " +
                                  "SET [Password] = @Password " +
                                      ",[RolesNo] = @RolesNo " +
                                      ",[CompanyNo] = @CompanyNo " +
                                      ",[UpdatedBy] = @UpdatedBy " +
                                      ",[UpdatedDate] = @UpdatedDate " +
                                  "WHERE UserName = @UserName";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@Password", data.Password);
                command.Parameters.AddWithValue("@RolesNo", data.RolesNo);
                command.Parameters.AddWithValue("@CompanyNo", data.CompanyNo);

                command.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
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

                command.Parameters.AddWithValue("@UserName", data.UserName);

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

        public bool DeleteData(MAS_USERS data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;
            try
            {
                string strQuery = "DELETE FROM tb_mas_Users WHERE [UserName] = @UserName";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@UserName", data.UserName);

                command.ExecuteNonQuery();

                bRet = true;
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

        public MAS_USERS VerifyUserLogin(string UserName, string Password)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            MAS_USERS retData = new MAS_USERS();
            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_Users] WHERE UserName = @UserName AND Password = @Password";
                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Password", Password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["UserName"]))
                        {
                            retData.UserName = (string)reader["UserName"];
                        }
                        if (!DBNull.Value.Equals(reader["Password"]))
                        {
                            retData.Password = (string)reader["Password"];
                        }
                        if (!DBNull.Value.Equals(reader["RolesNo"]))
                        {
                            retData.RolesNo = (Int64)reader["RolesNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
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

        public List<MAS_USERS> ListAllUser()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_USERS> lRetData = new List<MAS_USERS>();
            try
            {
                string strQuery = "SELECT * FROM tb_mas_Users";
                SqlCommand command = new SqlCommand(strQuery, _conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_USERS retData = new MAS_USERS();

                        if (!DBNull.Value.Equals(reader["UserName"]))
                        {
                            retData.UserName = (string)reader["UserName"];
                        }
                        if (!DBNull.Value.Equals(reader["Password"]))
                        {
                            retData.Password = (string)reader["Password"];
                        }
                        if (!DBNull.Value.Equals(reader["RolesNo"]))
                        {
                            retData.RolesNo = (Int64)reader["RolesNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }

                        lRetData.Add(retData);
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

            return lRetData;
        }

        #region #### Not Use ####

        //public List<MAS_PROJECTBIDDING_DTO> ListProjectBiding(string BiddingCode, string ProjectName, string BiddingMonth)
        //{
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        //    List<MAS_PROJECTBIDDING_DTO> lRetData = new List<MAS_PROJECTBIDDING_DTO>();
        //    try
        //    {
        //        string strQuery = "SELECT [ProjectNo],[ProjectName],[TemplateNo] " +
        //                            ",Convert(varchar, [StartDate], 103) AS StartDate " +
        //                            ",Convert(varchar, [EndDate], 103) AS EndDate " +
        //                            ",[BiddingCode] " +
        //                            "FROM tb_mas_ProjectBidding " +
        //                            "WHERE ([BiddingCode]  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
        //                            "([ProjectName]  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
        //                            "(MONTH([StartDate]) = @MONTH OR @MONTH IS NULL) ";              

        //        SqlCommand command = new SqlCommand(strQuery, _conn);

        //        if (!string.IsNullOrWhiteSpace(BiddingCode))
        //        {
        //            command.Parameters.AddWithValue("@BiddingCode", BiddingCode);
        //        }
        //        else
        //        {
        //            command.Parameters.AddWithValue("@BiddingCode", DBNull.Value);
        //        }

        //        if (!string.IsNullOrWhiteSpace(ProjectName))
        //        {
        //            command.Parameters.AddWithValue("@ProjectName", ProjectName);
        //        }
        //        else
        //        {
        //            command.Parameters.AddWithValue("@ProjectName", DBNull.Value);
        //        }

        //        if (!string.IsNullOrWhiteSpace(BiddingMonth))
        //        {
        //            command.Parameters.AddWithValue("@MONTH", BiddingMonth);
        //        }
        //        else
        //        {
        //            command.Parameters.AddWithValue("@MONTH", DBNull.Value);
        //        }

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                MAS_PROJECTBIDDING_DTO retData = new MAS_PROJECTBIDDING_DTO();

        //                if (!DBNull.Value.Equals(reader["ProjectNo"]))
        //                {
        //                    retData.ProjectNo = (Int64)reader["ProjectNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["ProjectName"]))
        //                {
        //                    retData.ProjectName = (string)reader["ProjectName"];
        //                }
        //                if (!DBNull.Value.Equals(reader["TemplateNo"]))
        //                {
        //                    retData.TemplateNo = (Int64)reader["TemplateNo"];
        //                }
        //                if (!DBNull.Value.Equals(reader["StartDate"]))
        //                {
        //                    retData.StartDate = (string)reader["StartDate"];
        //                }
        //                if (!DBNull.Value.Equals(reader["EndDate"]))
        //                {
        //                    retData.EndDate = (string)reader["EndDate"];
        //                }
        //                if (!DBNull.Value.Equals(reader["BiddingCode"]))
        //                {
        //                    retData.BiddingCode = (string)reader["BiddingCode"];
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
        
        //public bool UpdateBiddingCode(MAS_PROJECTBIDDING data)
        //{
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //    bool bRet = false;

        //    try
        //    {
        //        string strQuery = "UPDATE [dbo].[tb_mas_ProjectBidding] SET [BiddingCode]=@BiddingCode,[AttachFilePath]=@AttachFilePath WHERE [ProjectNo]=@ProjectNo;";

        //        SqlCommand command = new SqlCommand(strQuery, _conn);

        //        command.Parameters.AddWithValue("@BiddingCode", data.BiddingCode);
        //        command.Parameters.AddWithValue("@AttachFilePath", data.AttachFilePath);
        //        command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);
        //        if (command.ExecuteNonQuery() == 1)
        //        {
        //            bRet = true;
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

        //    return bRet;
        //}

        #endregion
    }
}