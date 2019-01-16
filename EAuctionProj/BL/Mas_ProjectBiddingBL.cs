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
    public class Mas_ProjectBiddingBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_ProjectITemBiddingBL));

        public Mas_ProjectBiddingBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }

        public string InsertData(MAS_PROJECTBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string sProjectNo = string.Empty;

            try
            {
                string strQuery = "INSERT INTO [dbo].[tb_mas_ProjectBidding] " +
                                    "([ProjectName] " +
                                    ",[TemplateNo] " +
                                    ",[CompanyAddress] " +
                                    ",[StartDate] " +
                                    ",[EndDate] " +
                                    ",[ContactName] " +
                                    ",[Email] " +
                                    ",[PhoneNo] " +
                                    ",[AttachFilePath] " +
                                    ",[CreatedBy] " +
                                    ",[CreatedDate] " +
                                    ",[UpdatedBy] " +
                                    ",[UpdatedDate] " +
                                    ",[DepartmentName]) " +
                                    " VALUES " +
                                    "(@ProjectName " +
                                    ",@TemplateNo " +
                                    ",@CompanyAddress " +
                                    ",@StartDate " +
                                    ",@EndDate " +
                                    ",@ContactName " +
                                    ",@Email " +
                                    ",@PhoneNo " +
                                    ",@AttachFilePath " +
                                    ",@CreatedBy " +
                                    ",@CreatedDate " +
                                    ",@UpdatedBy " +
                                    ",@UpdatedDate " +
                                    ",@DepartmentName); " +
                                    "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Transaction = _tran;

                command.Parameters.AddWithValue("@ProjectName", data.ProjectName);
                command.Parameters.AddWithValue("@TemplateNo", data.TemplateNo);
                command.Parameters.AddWithValue("@CompanyAddress", data.CompanyAddress);

                if (!string.IsNullOrEmpty(data.StartDate.ToString()))
                {
                    command.Parameters.AddWithValue("@StartDate", data.StartDate);

                    //DateTime dtNew = (DateTime)data.StartDate;
                    //string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    //command.Parameters.AddWithValue("@StartDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@StartDate", DBNull.Value);
                }

                if (!string.IsNullOrEmpty(data.EndDate.ToString()))
                {
                    command.Parameters.AddWithValue("@EndDate", data.EndDate);

                    //DateTime dtNew = (DateTime)data.EndDate;
                    //string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    //command.Parameters.AddWithValue("@EndDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@EndDate", DBNull.Value);
                }

                command.Parameters.AddWithValue("@ContactName", data.ContactName);
                command.Parameters.AddWithValue("@Email", data.Email);
                command.Parameters.AddWithValue("@PhoneNo", data.PhoneNo);

                if (!string.IsNullOrWhiteSpace(data.AttachFilePath))
                {
                    command.Parameters.AddWithValue("@AttachFilePath", data.AttachFilePath);
                }
                else
                {
                    command.Parameters.AddWithValue("@AttachFilePath", DBNull.Value);
                }

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
                command.Parameters.AddWithValue("@DepartmentName", data.DepartmentName);

                object projectNo = command.ExecuteScalar();
                if (projectNo != null)
                {
                    sProjectNo = projectNo.ToString();
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

            return sProjectNo;
        }

        public bool UpdateData(MAS_PROJECTBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE [dbo].[tb_mas_ProjectBidding] " +
                                    "SET [ProjectName] = @ProjectName  " +
                                    ",[TemplateNo] = @TemplateNo " +
                                    ",[CompanyAddress] = @CompanyAddress  " +
                                    ",[StartDate] = @StartDate " +
                                    ",[EndDate] = @EndDate " +
                                    ",[ContactName] = @ContactName " +
                                    ",[Email] = @Email " +
                                    ",[PhoneNo] = @PhoneNo " +
                                    ",[AttachFilePath] = @AttachFilePath " +
                                    ",[CreatedBy] = @CreatedBy " +
                                    ",[CreatedDate] = @CreatedDate " +
                                    ",[UpdatedBy] = @UpdatedBy " +
                                    ",[UpdatedDate] = @UpdatedDate " +
                                    ",[BiddingCode]= @BiddingCode " +
                                    ",[DepartmentName]= @DepartmentName " +
                                    "WHERE [ProjectNo] = @ProjectNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@ProjectName", string.IsNullOrWhiteSpace(data.ProjectName) ? "" : data.ProjectName);
                command.Parameters.AddWithValue("@TemplateNo", data.TemplateNo);
                command.Parameters.AddWithValue("@CompanyAddress", string.IsNullOrWhiteSpace(data.CompanyAddress) ? "" : data.CompanyAddress);

                if (!string.IsNullOrEmpty(data.StartDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.StartDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@StartDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@StartDate", null);
                }

                if (!string.IsNullOrEmpty(data.EndDate.ToString()))
                {
                    DateTime dtNew = (DateTime)data.EndDate;
                    string dateString = dtNew.ToString("MM/dd/yyyy HH:mm:ss");
                    command.Parameters.AddWithValue("@EndDate", dateString);
                }
                else
                {
                    command.Parameters.AddWithValue("@EndDate", null);
                }

                command.Parameters.AddWithValue("@ContactName", string.IsNullOrWhiteSpace(data.ContactName) ? "" : data.ContactName);
                command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(data.Email) ? "" : data.Email);
                command.Parameters.AddWithValue("@PhoneNo", string.IsNullOrWhiteSpace(data.PhoneNo) ? "" : data.PhoneNo);
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

                command.Parameters.AddWithValue("@BiddingCode", string.IsNullOrWhiteSpace(data.BiddingCode) ? "" : data.BiddingCode);
                command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);
                command.Parameters.AddWithValue("@DepartmentName", string.IsNullOrWhiteSpace(data.DepartmentName) ? "" : data.DepartmentName);
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

        public bool DeleteData(MAS_PROJECTBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;
            try
            {
                string strQuery = "DELETE FROM [dbo].[tb_mas_ProjectBidding] WHERE [ProjectNo] = @ProjectNo";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);

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

        public MAS_PROJECTBIDDING GetData(MAS_PROJECTBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            MAS_PROJECTBIDDING retData = new MAS_PROJECTBIDDING();
            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_ProjectBidding] WHERE [ProjectNo] = @ProjectNo ";
                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            retData.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyAddress"]))
                        {
                            retData.CompanyAddress = (string)reader["CompanyAddress"];
                        }
                        if (!DBNull.Value.Equals(reader["StartDate"]))
                        {
                            retData.StartDate = (DateTime)reader["StartDate"];
                        }
                        if (!DBNull.Value.Equals(reader["EndDate"]))
                        {
                            retData.EndDate = (DateTime)reader["EndDate"];
                        }
                        if (!DBNull.Value.Equals(reader["ContactName"]))
                        {
                            retData.ContactName = (string)reader["ContactName"];
                        }
                        if (!DBNull.Value.Equals(reader["Email"]))
                        {
                            retData.Email = (string)reader["Email"];
                        }
                        if (!DBNull.Value.Equals(reader["PhoneNo"]))
                        {
                            retData.PhoneNo = (string)reader["PhoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["AttachFilePath"]))
                        {
                            retData.AttachFilePath = (string)reader["AttachFilePath"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingCode"]))
                        {
                            retData.BiddingCode = (string)reader["BiddingCode"];
                        }
                        if (!DBNull.Value.Equals(reader["DepartmentName"]))
                        {
                            retData.DepartmentName = (string)reader["DepartmentName"];
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
        
        public bool UpdateBiddingCode(MAS_PROJECTBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            bool bRet = false;

            try
            {
                string strQuery = "UPDATE [dbo].[tb_mas_ProjectBidding] SET [BiddingCode]=@BiddingCode,[AttachFilePath]=@AttachFilePath WHERE [ProjectNo]=@ProjectNo;";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                command.Parameters.AddWithValue("@BiddingCode", data.BiddingCode);
                command.Parameters.AddWithValue("@AttachFilePath", data.AttachFilePath);
                command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);
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

        public List<MAS_PROJECTBIDDING> ListAllData(MAS_PROJECTBIDDING data)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<MAS_PROJECTBIDDING> lRetData = new List<MAS_PROJECTBIDDING>();
            try
            {
                string strQuery = "SELECT * FROM [dbo].[tb_mas_ProjectBidding] ";
                SqlCommand command = new SqlCommand(strQuery, _conn);
                //command.Parameters.AddWithValue("@ProjectNo", data.ProjectNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_PROJECTBIDDING retData = new MAS_PROJECTBIDDING();

                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            retData.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyAddress"]))
                        {
                            retData.CompanyAddress = (string)reader["CompanyAddress"];
                        }
                        if (!DBNull.Value.Equals(reader["StartDate"]))
                        {
                            retData.StartDate = (DateTime)reader["StartDate"];
                        }
                        if (!DBNull.Value.Equals(reader["EndDate"]))
                        {
                            retData.EndDate = (DateTime)reader["EndDate"];
                        }
                        if (!DBNull.Value.Equals(reader["ContactName"]))
                        {
                            retData.ContactName = (string)reader["ContactName"];
                        }
                        if (!DBNull.Value.Equals(reader["Email"]))
                        {
                            retData.Email = (string)reader["Email"];
                        }
                        if (!DBNull.Value.Equals(reader["PhoneNo"]))
                        {
                            retData.PhoneNo = (string)reader["PhoneNo"];
                        }
                        if (!DBNull.Value.Equals(reader["AttachFilePath"]))
                        {
                            retData.AttachFilePath = (string)reader["AttachFilePath"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingCode"]))
                        {
                            retData.BiddingCode = (string)reader["BiddingCode"];
                        }
                        if (!DBNull.Value.Equals(reader["DepartmentName"]))
                        {
                            retData.DepartmentName = (string)reader["DepartmentName"];
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
        public List<MAS_PROJECTBIDDING_DTO> ListProjectBiding(string BiddingCode, string ProjectName, string BiddingMonth, string UserName)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_PROJECTBIDDING_DTO> lRetData = new List<MAS_PROJECTBIDDING_DTO>();
            try
            {
                #region #### Old Function ####
                //string strQuery = "SELECT [ProjectNo],[ProjectName],[TemplateNo] " +
                //                    ",Convert(varchar, [StartDate], 103) AS StartDate " +
                //                    ",Convert(varchar, [EndDate], 103) AS EndDate " +
                //                    ",[BiddingCode] " +
                //                    "FROM tb_mas_ProjectBidding " +
                //                    "WHERE ([BiddingCode]  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
                //                    "([ProjectName]  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                //                    "(MONTH([StartDate]) = @MONTH OR @MONTH IS NULL) ";              

                //########################################################################################//

                //string strQuery = "SELECT Distinct  p.ProjectNo, p.ProjectName, p.TemplateNo, CONVERT(varchar, p.StartDate, 103) AS StartDate, " +
                //                    "CONVERT(varchar, p.EndDate, 103) AS EndDate, p.BiddingCode, c.CompanyNo " +
                //                    "FROM  tb_mas_ProjectBidding AS p Left Join tb_inf_Questionnaire c On p.ProjectNo = c.ProjectNo " +

                //string strQuery = "SELECT c.CompanyNo , p.ProjectNo, p.ProjectName, " +
                //                   "p.TemplateNo, p.StartDate, " +
                //                   "p.EndDate, p.BiddingCode, p.DepartmentName " +
                //                   "FROM tb_mas_ProjectBidding p Left Join  tb_inf_Questionnaire c   On p.ProjectNo = c.ProjectNo " +
                //                   "WHERE (c.CompanyNo = @CompanyNo OR @CompanyNo Is NULL) AND " +
                //                   "(p.BiddingCode  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
                //                   "(p.ProjectName  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                //                   "(MONTH(p.StartDate) = @MONTH OR @MONTH IS NULL) ORDER BY p.CreatedDate DESC ";
                #endregion
                
                string strQuery = "SELECT distinct c.CompanyNo , p.ProjectNo, p.ProjectName, " +
                                    "p.TemplateNo, p.StartDate, " +
                                    "p.EndDate, p.BiddingCode, p.DepartmentName, u.UserName " +
                                    "FROM tb_mas_ProjectBidding p inner Join " +
                                    "tb_inf_Questionnaire c On p.ProjectNo = c.ProjectNo inner join " +
                                    "tb_mas_Users u on c.CompanyNo = u.CompanyNo and u.ProjectNo = p.ProjectNo " +
                                    "WHERE (u.UserName = @UserName or @UserName is null) AND " +
                                    "(p.BiddingCode  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
                                    "(p.ProjectName  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                                    "(MONTH(p.StartDate) = @MONTH OR @MONTH IS NULL); ";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                }
                else
                {
                    command.Parameters.AddWithValue("@UserName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(BiddingCode))
                {
                    command.Parameters.AddWithValue("@BiddingCode", BiddingCode);
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingCode", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(ProjectName))
                {
                    command.Parameters.AddWithValue("@ProjectName", ProjectName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(BiddingMonth))
                {
                    command.Parameters.AddWithValue("@MONTH", BiddingMonth);
                }
                else
                {
                    command.Parameters.AddWithValue("@MONTH", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_PROJECTBIDDING_DTO retData = new MAS_PROJECTBIDDING_DTO();

                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            retData.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["StartDate"]))
                        {
                            retData.StartDate = (DateTime)reader["StartDate"];
                        }
                        if (!DBNull.Value.Equals(reader["EndDate"]))
                        {
                            retData.EndDate = (DateTime)reader["EndDate"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingCode"]))
                        {
                            retData.BiddingCode = (string)reader["BiddingCode"];
                        }
                        if (!DBNull.Value.Equals(reader["DepartmentName"]))
                        {
                            retData.DepartmentName = (string)reader["DepartmentName"];
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


        public List<MAS_PROJECTBIDDING_DTO> ListProjBidingDefault(string BiddingCode, string ProjectName, string BiddingMonth, string Department)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_PROJECTBIDDING_DTO> lRetData = new List<MAS_PROJECTBIDDING_DTO>();
            try
            {
                string strQuery = "SELECT p.ProjectNo, p.ProjectName, " +
                                   "p.TemplateNo, p.StartDate, " +
                                   "p.EndDate, p.BiddingCode, p.DepartmentName " + 
                                   "FROM  tb_mas_ProjectBidding p " + 
                                   "WHERE (p.BiddingCode  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
                                   "(p.ProjectName  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                                   "(MONTH(p.StartDate) = @MONTH OR @MONTH IS NULL) AND " +
                                   "(p.DepartmentName  LIKE '%' + @Department + '%' OR @Department IS NULL) " + 
                                   "ORDER BY p.CreatedDate DESC ";

                SqlCommand command = new SqlCommand(strQuery, _conn);             

                if (!string.IsNullOrWhiteSpace(BiddingCode))
                {
                    command.Parameters.AddWithValue("@BiddingCode", BiddingCode);
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingCode", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(ProjectName))
                {
                    command.Parameters.AddWithValue("@ProjectName", ProjectName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(BiddingMonth))
                {
                    command.Parameters.AddWithValue("@MONTH", BiddingMonth);
                }
                else
                {
                    command.Parameters.AddWithValue("@MONTH", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(Department))
                {
                    command.Parameters.AddWithValue("@Department", Department);
                }
                else
                {
                    command.Parameters.AddWithValue("@Department", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_PROJECTBIDDING_DTO retData = new MAS_PROJECTBIDDING_DTO();

                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            retData.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["StartDate"]))
                        {
                            retData.StartDate = (DateTime)reader["StartDate"];
                        }
                        if (!DBNull.Value.Equals(reader["EndDate"]))
                        {
                            retData.EndDate = (DateTime)reader["EndDate"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingCode"]))
                        {
                            retData.BiddingCode = (string)reader["BiddingCode"];
                        }
                        if (!DBNull.Value.Equals(reader["DepartmentName"]))
                        {
                            retData.DepartmentName = (string)reader["DepartmentName"];
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


        public List<MAS_PROJECTBIDDING_DTO> ListBidingProjectHis(string BiddingCode, string ProjectName,
            string BiddingMonth, string Username, string CompanyName)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_PROJECTBIDDING_DTO> lRetData = new List<MAS_PROJECTBIDDING_DTO>();
            try
            {
                #region #### Old Query ####
                //string strQuery = "SELECT p.BiddingCode, p.ProjectNo, p.ProjectName, " +
                //                  "Convert(varchar,p.StartDate, 103) AS StartDate, " +
                //                  "Convert(varchar,p.EndDate, 103) AS EndDate " +
                //                  "FROM tb_inf_Biddings AS b INNER JOIN tb_mas_ProjectBidding AS p ON b.ProjectNo = p.ProjectNo " +
                //"WHERE (p.[BiddingCode]  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
                //"(p.[ProjectName]  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                //"(MONTH(p.[StartDate]) = @MONTH OR @MONTH IS NULL) AND " +
                //" (b.CompanyNo = @CompanyNo OR @CompanyNo IS NULL)";

                //string strQuery = "SELECT p.BiddingCode, p.ProjectNo, p.ProjectName, " +
                //                    "p.StartDate, " +
                //                    "p.EndDate, " +
                //                    "b.CreatedDate, b.BiddingPrice, b.BiddingsNo " +  
                //                    "FROM tb_inf_Biddings AS b INNER JOIN tb_mas_ProjectBidding AS p ON b.ProjectNo = p.ProjectNo " +
                //                    "WHERE (p.[BiddingCode]  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
                //                    "(p.[ProjectName]  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                //                    "(MONTH(p.[StartDate]) = @MONTH OR @MONTH IS NULL) AND " +
                //                    "(b.CompanyNo = @CompanyNo OR @CompanyNo IS NULL)";             

                //***********************************************************************//
                //string strQuery = "SELECT p.BiddingCode, p.ProjectNo, p.ProjectName, " +
                //                    "p.StartDate, p.EndDate, " +
                //                    "b.CreatedDate, b.BiddingPrice, b.BiddingsNo, b.CompanyNo, c.CompanyName " +
                //                    "FROM tb_inf_Biddings AS b INNER JOIN " +
                //                    "tb_mas_ProjectBidding AS p ON b.ProjectNo = p.ProjectNo  INNER JOIN " +
                //                    "tb_mas_BiddingCompany AS c  ON b.CompanyNo = c.CompanyNo " +
                //                    "WHERE (p.[BiddingCode]  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND  " +
                //                    "(p.[ProjectName]  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                //                    "(MONTH(p.[StartDate]) = @MONTH OR @MONTH IS NULL) AND " +
                //                    "(b.CompanyNo = @CompanyNo OR @CompanyNo IS NULL) AND " +
                //                    "(c.CompanyName  LIKE '%' + @CompanyName + '%' OR @CompanyName IS NULL)";
                #endregion

                string strQuery = "SELECT distinct p.BiddingCode, p.ProjectNo, p.ProjectName,p.StartDate, p.EndDate, " +
                                    "b.CreatedDate, b.BiddingPrice, b.BiddingsNo, b.CompanyNo, c.CompanyName " +
                                    "FROM tb_inf_Biddings AS b INNER JOIN " +
                                    "tb_mas_ProjectBidding AS p ON b.ProjectNo = p.ProjectNo  INNER JOIN " +
                                    "tb_mas_BiddingCompany AS c ON b.CompanyNo = c.CompanyNo LEFT JOIN " +
                                    "tb_mas_Users u on c.CompanyNo = u.CompanyNo and u.ProjectNo = p.ProjectNo " +
                                    "WHERE (p.[BiddingCode]  LIKE '%' + @BiddingCode + '%' OR @BiddingCode IS NULL) AND " +
                                    "(p.[ProjectName]  LIKE '%' + @ProjectName + '%' OR @ProjectName IS NULL) AND " +
                                    "(MONTH(p.[StartDate]) = @MONTH OR @MONTH IS NULL) AND " +
                                    "(u.UserName = @Username OR @Username IS NULL) AND " +
                                    "(c.CompanyName  LIKE '%' + @CompanyName + '%' OR @CompanyName IS NULL)";


                SqlCommand command = new SqlCommand(strQuery, _conn);
                if (!string.IsNullOrWhiteSpace(BiddingCode))
                {
                    command.Parameters.AddWithValue("@BiddingCode", BiddingCode);
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingCode", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(ProjectName))
                {
                    command.Parameters.AddWithValue("@ProjectName", ProjectName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(BiddingMonth))
                {
                    command.Parameters.AddWithValue("@MONTH", BiddingMonth);
                }
                else
                {
                    command.Parameters.AddWithValue("@MONTH", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(Username))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                }
                else
                {
                    command.Parameters.AddWithValue("@Username", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(CompanyName))
                {
                    command.Parameters.AddWithValue("@CompanyName", CompanyName);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_PROJECTBIDDING_DTO retData = new MAS_PROJECTBIDDING_DTO();

                        if (!DBNull.Value.Equals(reader["BiddingCode"]))
                        {
                            retData.BiddingCode = (string)reader["BiddingCode"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }

                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            retData.EndDate = (DateTime)reader["CreatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingPrice"]))
                        {
                            retData.BiddingPrice = (Decimal)reader["BiddingPrice"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingsNo"]))
                        {
                            retData.BiddingsNo = (Int64)reader["BiddingsNo"];
                        }

                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
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

        
        public List<INF_PROJECTBIDDINGDETAIL_DTO> ListBidingProjectHisDet(string ProjectNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<INF_PROJECTBIDDINGDETAIL_DTO> lRetData = new List<INF_PROJECTBIDDINGDETAIL_DTO>();
            try
            {
                string strQuery = "SELECT tb1.ProjectNo,tb1.CompanyNo,tb1.CompanyName,tb1.BiddingPrice, " +
                    "tb1.BiddingTotalPrice,tb1.CreatedDate,tb1.TaxID,tb1.BiddingsNo " +
                    "FROM (SELECT b.ProjectNo, c.CompanyNo, c.CompanyName,b.CreatedDate,b.BiddingPrice, b.BiddingTotalPrice,c.TaxID,b.BiddingsNo " +
                    "FROM tb_mas_BiddingCompany c INNER JOIN tb_inf_Biddings b on c.CompanyNo = b.CompanyNo) tb1 " +
                    "INNER JOIN (SELECT ProjectNo, MAX(CreatedDate) AS MaxDateTime " +
                    "FROM tb_inf_Biddings GROUP BY ProjectNo) tb2 ON tb1.ProjectNo = tb2.ProjectNo AND tb1.CreatedDate = tb2.MaxDateTime " +
                    "WHERE (tb1.ProjectNo = @ProjectNo OR @ProjectNo IS NULL)";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrWhiteSpace(ProjectNo))
                {
                    command.Parameters.AddWithValue("@ProjectNo", ProjectNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectNo", DBNull.Value);
                }
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        INF_PROJECTBIDDINGDETAIL_DTO retData = new INF_PROJECTBIDDINGDETAIL_DTO();
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingPrice"]))
                        {
                            retData.BiddingPrice = (Decimal)reader["BiddingPrice"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingTotalPrice"]))
                        {
                            retData.BiddingTotalPrice = (Decimal)reader["BiddingTotalPrice"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            retData.CreatedDate = (DateTime)reader["CreatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["TaxID"]))
                        {
                            retData.TaxID = (string)reader["TaxID"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingsNo"]))
                        {
                            retData.BiddingsNo = (Int64)reader["BiddingsNo"];
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


        public List<INF_PROJECTBIDDINGDETAIL_DTO> ListAllCompanyBidingHisDet(string CompanyName, string TaxID)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<INF_PROJECTBIDDINGDETAIL_DTO> lRetData = new List<INF_PROJECTBIDDINGDETAIL_DTO>();
            try
            {
                string strQuery = "SELECT DISTINCT c.CompanyNo, c.CompanyName,c.TaxID " +
                                  "FROM tb_mas_BiddingCompany c INNER JOIN tb_inf_Biddings b on c.CompanyNo = b.CompanyNo " +
                                  "WHERE (c.CompanyName like '%'+@CompanyName+'%' OR @CompanyName IS NULL) AND " +
                                  "(c.TaxID LIKE '%'+ @TaxID + '%' OR @TaxID IS NULL)";
               
                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrWhiteSpace(CompanyName))
                {
                    command.Parameters.AddWithValue("@CompanyName", CompanyName);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(TaxID))
                {
                    command.Parameters.AddWithValue("@TaxID", TaxID);
                }
                else
                {
                    command.Parameters.AddWithValue("@TaxID", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        INF_PROJECTBIDDINGDETAIL_DTO retData = new INF_PROJECTBIDDINGDETAIL_DTO();
                       
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyName"]))
                        {
                            retData.CompanyName = (string)reader["CompanyName"];
                        }                        
                        if (!DBNull.Value.Equals(reader["TaxID"]))
                        {
                            retData.TaxID = (string)reader["TaxID"];
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
        public List<INF_PROJECTBIDDINGDETAIL_DTO> ListBidingVendorDetail(string CompanyNo, string ProjectName, string BiddingCode)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<INF_PROJECTBIDDINGDETAIL_DTO> lRetData = new List<INF_PROJECTBIDDINGDETAIL_DTO>();
            try
            {
                //string strQuery = "SELECT tb1.ProjectNo,tb1.ProjectName,tb1.BiddingPrice, " +
                //        "tb1.BiddingTotalPrice,Convert(varchar,tb1.CreatedDate, 103) AS CreatedDate,tb1.BiddingsNo, tb1.CompanyNo,tb1.BiddingCode " +
                //        "FROM (SELECT p.ProjectNo,p.ProjectName,b.CreatedDate,b.BiddingPrice, b.BiddingTotalPrice,b.BiddingsNo,b.CompanyNo,p.BiddingCode " + 
                //        "FROM tb_mas_ProjectBidding p INNER JOIN tb_inf_Biddings b on p.ProjectNo = b.ProjectNo) tb1 " +
                //        "INNER JOIN (SELECT ProjectNo, MAX(CreatedDate) AS MaxDateTime " +
                //        "FROM tb_inf_Biddings GROUP BY ProjectNo) tb2 ON tb1.ProjectNo = tb2.ProjectNo AND tb1.CreatedDate = tb2.MaxDateTime " +
                //        "WHERE (tb1.CompanyNo = @CompanyNo OR @CompanyNo IS NULL) AND " +
                //        "(tb1.ProjectName like '%'+@ProjectName+'%' OR @ProjectName IS NULL) AND " +
                //        "(tb1.BiddingCode LIKE '%'+ @BiddingCode + '%' OR @BiddingCode IS NULL)";

                string strQuery = "SELECT p.ProjectNo,p.ProjectName,b.CreatedDate, " +
                            "b.BiddingPrice, b.BiddingTotalPrice,b.BiddingsNo,b.CompanyNo,p.BiddingCode " +
                            "FROM tb_mas_ProjectBidding p INNER JOIN tb_inf_Biddings b on p.ProjectNo = b.ProjectNo " +
                            "WHERE (b.CompanyNo = @CompanyNo OR @CompanyNo IS NULL) AND " +
                            "(p.ProjectName like '%'+@ProjectName+'%' OR @ProjectName IS NULL) AND " +
                            "(p.BiddingCode LIKE '%'+ @BiddingCode + '%' OR @BiddingCode IS NULL) ";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                if (!string.IsNullOrWhiteSpace(CompanyNo))
                {
                    command.Parameters.AddWithValue("@CompanyNo", CompanyNo);
                }
                else
                {
                    command.Parameters.AddWithValue("@CompanyNo", DBNull.Value);
                }
                if (!string.IsNullOrWhiteSpace(ProjectName))
                {
                    command.Parameters.AddWithValue("@ProjectName", ProjectName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                }
                if (!string.IsNullOrWhiteSpace(BiddingCode))
                {
                    command.Parameters.AddWithValue("@BiddingCode", BiddingCode);
                }
                else
                {
                    command.Parameters.AddWithValue("@BiddingCode", DBNull.Value);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        INF_PROJECTBIDDINGDETAIL_DTO retData = new INF_PROJECTBIDDINGDETAIL_DTO();
                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            retData.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingPrice"]))
                        {
                            retData.BiddingPrice = (Decimal)reader["BiddingPrice"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingTotalPrice"]))
                        {
                            retData.BiddingTotalPrice = (Decimal)reader["BiddingTotalPrice"];
                        }
                        if (!DBNull.Value.Equals(reader["CreatedDate"]))
                        {
                            retData.CreatedDate = (DateTime)reader["CreatedDate"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingsNo"]))
                        {
                            retData.BiddingsNo = (Int64)reader["BiddingsNo"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingCode"]))
                        {
                            retData.BiddingCode = (string)reader["BiddingCode"];
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
        public List<MAS_PROJECTBIDDING_DTO> ListAllProjBidingActive()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<MAS_PROJECTBIDDING_DTO> lRetData = new List<MAS_PROJECTBIDDING_DTO>();
            try
            {
                string strQuery = "SELECT * FROM tb_mas_ProjectBidding " + 
                                  "WHERE CAST(StartDate AS DATE) <= CAST(SYSDATETIME() AS DATE) AND " +  
                                  "CAST(EndDate AS DATE) >= CAST(SYSDATETIME() AS DATE)";

                SqlCommand command = new SqlCommand(strQuery, _conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_PROJECTBIDDING_DTO retData = new MAS_PROJECTBIDDING_DTO();

                        if (!DBNull.Value.Equals(reader["ProjectNo"]))
                        {
                            retData.ProjectNo = (Int64)reader["ProjectNo"];
                        }
                        if (!DBNull.Value.Equals(reader["ProjectName"]))
                        {
                            retData.ProjectName = (string)reader["ProjectName"];
                        }
                        if (!DBNull.Value.Equals(reader["TemplateNo"]))
                        {
                            retData.TemplateNo = (Int64)reader["TemplateNo"];
                        }
                        if (!DBNull.Value.Equals(reader["StartDate"]))
                        {
                            retData.StartDate = (DateTime)reader["StartDate"];
                        }
                        if (!DBNull.Value.Equals(reader["EndDate"]))
                        {
                            retData.EndDate = (DateTime)reader["EndDate"];
                        }
                        if (!DBNull.Value.Equals(reader["BiddingCode"]))
                        {
                            retData.BiddingCode = (string)reader["BiddingCode"];
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
    }
}