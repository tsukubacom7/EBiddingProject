using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAuctionProj.DAL;
using System.Data;

namespace EAuctionProj.BL
{
    public class Inf_Questionnaire_Manage
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Inf_Questionnaire_Manage));

        public bool InsertQuestionnaire(INF_QUESTIONNAIRE data)
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

                Inf_QuestionnaireBL bl = new Inf_QuestionnaireBL(conn);
                ret = bl.InsertData(data);

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
        
        
        public List<INF_QUESTIONNAIRE> ListAll()
        {
            IDbConnection conn = null;
            List<INF_QUESTIONNAIRE> ret = new List<INF_QUESTIONNAIRE>();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Inf_QuestionnaireBL bl = new Inf_QuestionnaireBL(conn);
                ret = bl.ListAllData();

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
        
        public INF_QUESTIONNAIRE GetQuestionaire(INF_QUESTIONNAIRE data)
        {
            IDbConnection conn = null;
            INF_QUESTIONNAIRE ret = new INF_QUESTIONNAIRE();
            try
            {
                //SET CONNECTION
                conn = ConnectionFactory.GetConnection();
                conn.ConnectionString = ConfigurationManager.GetConfiguration().DbConnectionString;

                //OPEN CONNECTION
                conn.Open();

                Inf_QuestionnaireBL bl = new Inf_QuestionnaireBL(conn);
                ret = bl.GetByCompanyNo(data);

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