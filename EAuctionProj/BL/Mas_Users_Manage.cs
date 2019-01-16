using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAuctionProj.DAL;
using System.Data;
using System.Threading;

namespace EAuctionProj.BL
{
    public class Mas_Users_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Mas_Users_Manage));

        public bool DeleteMasUsers(MAS_USERS data)
        {
            IDbConnection conn = null;
            bool ret = false;
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_UsersBL bl = new Mas_UsersBL(conn);
                ret = bl.DeleteData(data);

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

        public MAS_USERS VerifyUserLogin(string UserName, string Password)
        {
            IDbConnection conn = null;
            MAS_USERS ret = new MAS_USERS();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_UsersBL bl = new Mas_UsersBL(conn);
                ret = bl.VerifyUserLogin(UserName, Password);
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

        public List<MAS_USERS> ListAllUsers()
        {
            IDbConnection conn = null;
            List<MAS_USERS> ret = new List<MAS_USERS>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Mas_UsersBL bl = new Mas_UsersBL(conn);
                ret = bl.ListAllUser();

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