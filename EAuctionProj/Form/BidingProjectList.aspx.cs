using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using System.Web.Security;
using EAuctionProj.Utility;

namespace EAuctionProj
{
    public partial class BidingProjectList : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BidingProjectList));

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

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (!Page.IsPostBack)
            {
                MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                if (retUser.RolesNo > 0)
                {
                    hdfCompanyNo.Value = retUser.CompanyNo.ToString().Trim();
                    hdfUserName.Value = retUser.UserName;
                    hdfUserNo.Value = retUser.UsersNo.ToString().Trim();
                    hdfRoleNo.Value = retUser.RolesNo.ToString();

                    //Mas_BiddingCompany_Manage cManage = new Mas_BiddingCompany_Manage();
                    //MAS_BIDDINGCOMPANY comData = new MAS_BIDDINGCOMPANY();
                    //comData = cManage.GetBiddingCompany(retUser.CompanyNo.ToString());
                    //lblCompanyName.Text = comData.CompanyName;
                }

                InitialControl();
            }
        }

        private void InitialControl()
        {
            BindGridview();
        }

        private void BindGridview()
        {
            List<MAS_PROJECTBIDDING_DTO> lItemRet = new List<MAS_PROJECTBIDDING_DTO>();
            MAS_PROJECTBIDDING data = new MAS_PROJECTBIDDING();
            Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();

            string BiddingCode = "";
            string ProjectName = "";
            string BiddingMonth = "";

            string UserName = "";
            if (hdfRoleNo.Value.Trim().Equals("2"))
            {
                UserName = hdfUserName.Value.Trim();
            }

            if (ddlSearch.SelectedIndex != 0)
            {
                switch (ddlSearch.SelectedValue)
                {
                    case "1":
                        BiddingCode = txtSearch.Text.Trim();
                        break;
                    case "2":
                        ProjectName = txtSearch.Text.Trim();
                        break;
                }
            }

            if (ddlSelMonth.SelectedIndex != 0)
            {
                BiddingMonth = ddlSelMonth.SelectedValue;
            }

            lItemRet = manage.ListBiddingProject(BiddingCode, ProjectName, BiddingMonth, UserName);

            gvListProject.DataSource = lItemRet;
            gvListProject.DataBind();
        }

        protected void gvListProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName.Equals("Select"))
            //{
            //    int intRowIndex = int.Parse(e.CommandArgument.ToString());
            //    string ProjectNo = gvListProject.Rows[intRowIndex].Cells[0].Text;

            //    Response.Redirect("~/Form/BiddingProject.aspx?ProjectID=" + ProjectNo.Trim());
            //}
        }

        public string ConvertDateFormat(object dt)
        {
            string strDate = string.Empty;
            if (dt != null)
            {
                DateTime _dt = new DateTime();
                _dt = DateTime.Parse(dt.ToString());
                string strDt = _dt.Date.ToString("ddMMyyyy");
                strDate = strDt.Substring(0, 2) + "/" + strDt.Substring(2, 2) + "/" + strDt.Substring(4, 4);
            }

            return strDate;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridview();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlSearch.SelectedIndex = 0;
            ddlSelMonth.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            BindGridview();
        }

        protected void gvListProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListProject.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        protected void gvListProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hplSelectProj = (HyperLink)e.Row.FindControl("hplSelectProj");
                if (hplSelectProj != null)
                {
                    HiddenField hdfEndDate = (HiddenField)e.Row.FindControl("hdfEndDate");
                    HiddenField hdfProjectNo = (HiddenField)e.Row.FindControl("hdfProjectNo");
                    DateTime _endDate = new DateTime();
                    _endDate = Convert.ToDateTime(hdfEndDate.Value);

                    int iCount = (_endDate - DateTime.Now).Days;
                    if (iCount < 0)
                    {
                        hplSelectProj.Visible = false;
                    }
                    else
                    {
                        GlobalFunction fEncrypt = new GlobalFunction();
                        hplSelectProj.NavigateUrl = "~/Form/BiddingProcess.aspx?ProjectNo=" + fEncrypt.Encrypt(hdfProjectNo.Value);
                    }
                }
            }
        }
    }
}
