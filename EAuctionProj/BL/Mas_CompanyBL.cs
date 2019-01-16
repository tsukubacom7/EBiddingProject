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
    public class Mas_CompanyBL
    {
        private SqlConnection _conn;
        private SqlTransaction _tran;

        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_CompanyBL));

        public Mas_CompanyBL(IDbConnection conn, IDbTransaction tran = null)
        {
            this._conn = (SqlConnection)conn;
            this._tran = (SqlTransaction)tran;
        }

        public List<MAS_COMPANY> ListCompanyName()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            List<MAS_COMPANY> lRet = new List<MAS_COMPANY>();
            try
            {
                string strQuery = "SELECT [CompanyNo],[CompanyNameTH],[CompanyNameEN] " +
                                  "FROM  [tb_mas_Company] WHERE [IsActive] = 1 ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_COMPANY data = new MAS_COMPANY();

                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            data.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNameTH"]))
                        {
                            data.CompanyNameTH = (string)reader["CompanyNameTH"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNameEN"]))
                        {
                            data.CompanyNameEN = (string)reader["CompanyNameEN"];
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
        
        public MAS_COMPANY GetCompanyByID(string CompanyNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            MAS_COMPANY dataRet = new MAS_COMPANY();
            try
            {
                string strQuery = "SELECT [CompanyNo],[CompanyNameTH],[CompanyNameEN],[CompanyAddressTH] " +
                                  "FROM  [tb_mas_Company] WHERE [IsActive] = 1 and CompanyNo = @CompanyNo ";

                SqlCommand command = new SqlCommand(strQuery, _conn);
                command.Parameters.AddWithValue("@CompanyNo", CompanyNo);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_COMPANY data = new MAS_COMPANY();

                        if (!DBNull.Value.Equals(reader["CompanyNo"]))
                        {
                            data.CompanyNo = (Int64)reader["CompanyNo"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNameTH"]))
                        {
                            data.CompanyNameTH = (string)reader["CompanyNameTH"];
                        }
                        if (!DBNull.Value.Equals(reader["CompanyNameEN"]))
                        {
                            data.CompanyNameEN = (string)reader["CompanyNameEN"];
                        }

                        if (!DBNull.Value.Equals(reader["CompanyAddressTH"]))
                        {
                            data.CompanyAddressTH = (string)reader["CompanyAddressTH"];
                        }

                        dataRet = data;
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

            return dataRet;
        }

        public List<MAS_DEPARTMENT> ListDeprtmentByCompany(string CompanyCode)
        {
            List<MAS_DEPARTMENT> lDept = new List<MAS_DEPARTMENT>();
            try
            {
                string _spName = "spGetDepartment";
                SqlCommand command = new SqlCommand(_spName, _conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CompanyCode", CompanyCode);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAS_DEPARTMENT data = new MAS_DEPARTMENT();

                        if (!DBNull.Value.Equals(reader["CompanyCode"]))
                        {
                            data.CompanyCode = (string)reader["CompanyCode"];
                        }
                        if (!DBNull.Value.Equals(reader["DepartmentName"]))
                        {
                            data.DepartmentName = (string)reader["DepartmentName"];
                        }

                        lDept.Add(data);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                lDept = null;

                logger.Error(sqlEx);
            }
            catch (Exception ex)
            {
                lDept = null;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lDept;
        }

    }
}