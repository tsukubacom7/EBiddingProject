using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using System.IO;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using System.Data;
using System.Net;
using EAuctionProj.Utility;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class ViewQuestionnaire : System.Web.UI.Page
    {
        private string ProjectNo = string.Empty;
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ViewQuestionnaire));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Session.Clear();
                Session.Abandon();
                ViewState.Clear();
                FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);
                Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");

                if (!Page.IsPostBack)
                {
                    GlobalFunction func = new GlobalFunction();
                    string _ProjectNo = Request.QueryString["ProjectNo"];
                    string _CompanyNo = Request.QueryString["CompanyNo"];
                    if (string.IsNullOrEmpty(_ProjectNo) || string.IsNullOrEmpty(_CompanyNo))
                    {
                        Session.Clear();
                        Session.Abandon();
                        ViewState.Clear();
                        FormsAuthentication.SignOut();

                        Response.Redirect("~/Account/Login.aspx");
                    }
                    else
                    {
                        hdfProjectNo.Value = func.Decrypt(_ProjectNo.Trim());
                        hdfCompanyNo.Value = func.Decrypt(_CompanyNo.Trim());
                    }

                    MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                    if (retUser.UsersNo > 0)
                    {
                        string _RoleNo = retUser.RolesNo.ToString();
                        GetCompQuestionaire(_RoleNo);
                    }
                    else
                    {
                        Session.Clear();
                        Session.Abandon();
                        ViewState.Clear();
                        FormsAuthentication.SignOut();

                        logger.Info("Session UserLogin has no data..!");
                        Response.Redirect("~/Account/Login.aspx", true);
                    }
                }
            }
        }

        private void GetCompQuestionaire(string RoleNo)
        {
            try
            {
                MAS_COMPANYUSER_DTO _UserLogin = (MAS_COMPANYUSER_DTO)Session["UserLogin"];

                Inf_Questionnaire_Manage manage = new Inf_Questionnaire_Manage();
                INF_QUESTIONNAIRE retData = new INF_QUESTIONNAIRE();
                retData.ProjectNo = hdfProjectNo.Value.Trim();
                retData.CompanyNo = hdfCompanyNo.Value.Trim();

                retData = manage.GetQuestionaire(retData);
                if (retData.QuestionNo > 0)
                {
                    if ((!retData.CompanyNo.Trim().Equals(_UserLogin.CompanyNo.ToString().Trim()) ||
                        !retData.ProjectNo.Equals(_UserLogin.ProjectNo.ToString().Trim())) &&
                        (RoleNo.Trim().Equals("2")))
                    {
                        Session.Clear();
                        Session.Abandon();
                        ViewState.Clear();
                        FormsAuthentication.SignOut();

                        Response.Redirect("~/Account/Login.aspx", true);
                    }
                    else
                    {
                        Mas_BiddingCompany_Manage cManage = new Mas_BiddingCompany_Manage();
                        MAS_BIDDINGCOMPANY comData = new MAS_BIDDINGCOMPANY();
                        comData = cManage.GetBiddingCompany(retData.CompanyNo);
                        lblCompany.Text = comData.CompanyName;

                        if (retData.AnsQuestion1 != null)
                        {
                            lblQ1.Text = (retData.AnsQuestion1 == 1 ? "ใช่" : "ไม่ใช่");
                        }

                        lblQ2.Text = retData.AnsQuestion2;

                        if (retData.AnsQuestion3 != null)
                        {
                            lblQ3.Text = (retData.AnsQuestion3 == 1 ? "ใช่" : "ไม่ใช่");
                        }

                        if (retData.AnsQuestion4 != null)
                        {
                            lblQ4.Text = (retData.AnsQuestion4 == 1 ? "ใช่" : "ไม่ใช่");
                        }

                        if (retData.AnsQuestion5 != null)
                        {
                            lblQ5.Text = (retData.AnsQuestion5 == 1 ? "ใช่" : "ไม่ใช่");
                        }

                        if (retData.AnsQuestion6 != null)
                        {
                            lblQ6.Text = (retData.AnsQuestion6 == 1 ? "ใช่" : "ไม่ใช่");
                        }

                        if (retData.AnsQuestion7 != null)
                        {
                            lblQ7.Text = (retData.AnsQuestion7 == 1 ? "ใช่" : "ไม่ใช่");
                        }

                        if (!string.IsNullOrEmpty(retData.AnsQuestion8))
                        {
                            lblQ8.Text = retData.AnsQuestion8;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            /* 
             * 
             Function here!! 
             *
             */

        }
    }
}
