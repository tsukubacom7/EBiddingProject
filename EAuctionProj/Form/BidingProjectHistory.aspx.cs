using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using System.Text;
using System.Collections;
using EAuctionProj.Utility;
using EAuctionProj.ReportDAO;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class BidingProjectHistory : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BidingProjectHistory));
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

            if (!Page.IsPostBack)
            {
                ViewState["SortGridview_BidingHistory"] = "BiddingsNo DESC";
                Session["BidingProjectHistoryRPT"] = null;

                MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                if (retUser.RolesNo > 0)
                {
                    hdfCompanyNo.Value = retUser.CompanyNo.ToString().Trim();
                    hdfUserName.Value = retUser.UserName;
                    hdfUserNo.Value = retUser.UsersNo.ToString().Trim();
                    hdfRoleNo.Value = retUser.RolesNo.ToString();

                    if (retUser.RolesNo == 1)
                    {
                        btnExport.Visible = true;
                    }
                    else
                    {
                        btnExport.Visible = false;
                    }
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
            try
            {
                List<MAS_PROJECTBIDDING_DTO> lItemRet = new List<MAS_PROJECTBIDDING_DTO>();
                MAS_PROJECTBIDDING data = new MAS_PROJECTBIDDING();
                Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();

                string BiddingCode = "";
                string ProjectName = "";
                string BiddingMonth = "";
                string CompanyName = "";

                string Username = "";
                if (hdfRoleNo.Value.Trim().Equals("2"))
                {
                    Username = hdfUserName.Value.Trim();
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
                        case "3":
                            CompanyName = txtSearch.Text.Trim();
                            break;
                    }
                }

                if (ddlSelMonth.SelectedIndex != 0)
                {
                    BiddingMonth = ddlSelMonth.SelectedValue;
                }

                lItemRet = manage.ListBiddingProjectHistory(BiddingCode, ProjectName, BiddingMonth, Username, CompanyName);

                /********************** For Sort Gridview ************************/
                string _sortBy = (string)ViewState["SortGridview_BidingHistory"];
                switch (_sortBy.Trim())
                {
                    case "BiddingsNo DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.BiddingsNo).ToList();
                        break;
                    case "BiddingCode":
                        lItemRet = lItemRet.OrderBy(x => x.BiddingCode).ToList();
                        break;
                    case "BiddingCode DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.BiddingCode).ToList();
                        break;
                    case "ProjectName":
                        lItemRet = lItemRet.OrderBy(x => x.ProjectName).ToList();
                        break;
                    case "ProjectName DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.ProjectName).ToList();
                        break;
                    case "EndDate":
                        lItemRet = lItemRet.OrderBy(x => x.EndDate).ToList();
                        break;
                    case "EndDate DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.EndDate).ToList();
                        break;
                    case "BiddingPrice":
                        lItemRet = lItemRet.OrderBy(x => x.BiddingPrice).ToList();
                        break;
                    case "BiddingPrice DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.BiddingPrice).ToList();
                        break;

                    case "CompanyName":
                        lItemRet = lItemRet.OrderBy(x => x.CompanyName).ToList();
                        break;
                    case "CompanyName DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.CompanyName).ToList();
                        break;
                }
                /*****************************************************************/

                gvListProject.DataSource = lItemRet;
                gvListProject.DataBind();

                Session["BidingProjectHistoryRPT"] = lItemRet;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
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

        private string GetColumnSirting(string SortBy)
        {
            string retSortColumn = string.Empty;

            if (SortBy.Equals("BiddingCode"))
            {
                if (ViewState["SortGridview_BidingHistory"] != null)
                {
                    if (!ViewState["SortGridview_BidingHistory"].ToString().Equals("BiddingCode"))
                    {
                        retSortColumn = "BiddingCode";
                    }
                    else
                    {
                        retSortColumn = "BiddingCode DESC";
                    }
                }
                else
                {
                    retSortColumn = "BiddingCode";
                }
            }

            if (SortBy.Equals("ProjectName"))
            {
                if (ViewState["SortGridview_BidingHistory"] != null)
                {
                    if (!ViewState["SortGridview_BidingHistory"].ToString().Equals("ProjectName"))
                    {
                        retSortColumn = "ProjectName";
                    }
                    else
                    {
                        retSortColumn = "ProjectName DESC";
                    }
                }
                else
                {
                    retSortColumn = "ProjectName";
                }
            }

            if (SortBy.Equals("EndDate"))
            {
                if (ViewState["SortGridview_BidingHistory"] != null)
                {
                    if (!ViewState["SortGridview_BidingHistory"].ToString().Equals("EndDate"))
                    {
                        retSortColumn = "EndDate";
                    }
                    else
                    {
                        retSortColumn = "EndDate DESC";
                    }
                }
                else
                {
                    retSortColumn = "EndDate";
                }
            }

            if (SortBy.Equals("BiddingPrice"))
            {
                if (ViewState["SortGridview_BidingHistory"] != null)
                {
                    if (!ViewState["SortGridview_BidingHistory"].ToString().Equals("BiddingPrice"))
                    {
                        retSortColumn = "BiddingPrice";
                    }
                    else
                    {
                        retSortColumn = "BiddingPrice DESC";
                    }
                }
                else
                {
                    retSortColumn = "BiddingPrice";
                }
            }

            if (SortBy.Equals("CompanyName"))
            {
                if (ViewState["SortGridview_BidingHistory"] != null)
                {
                    if (!ViewState["SortGridview_BidingHistory"].ToString().Equals("CompanyName"))
                    {
                        retSortColumn = "CompanyName";
                    }
                    else
                    {
                        retSortColumn = "CompanyName DESC";
                    }
                }
                else
                {
                    retSortColumn = "CompanyName";
                }
            }

            return retSortColumn;
        }

        protected void gvListProject_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortGridview_BidingHistory"] = GetColumnSirting(e.SortExpression);
            BindGridview();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["BidingProjectHistoryRPT"] != null)
                {
                    string header = string.Empty;
                    for (int i = 0; i <= this.gvListProject.Columns.Count - 1; i++)
                    {
                        if ((this.gvListProject.Columns[i]) is System.Web.UI.WebControls.BoundField)
                        {
                            header += this.gvListProject.Columns[i].HeaderText + ",";
                        }
                    }
                 
                    string _header = header.Substring(0, header.Length - 1);

                     List<MAS_PROJECTBIDDING_DTO> lItemRet = new List<MAS_PROJECTBIDDING_DTO>();
                     lItemRet = (List<MAS_PROJECTBIDDING_DTO>)Session["BidingProjectHistoryRPT"];

                     ArrayList l = new ArrayList();
                     foreach (MAS_PROJECTBIDDING_DTO item in lItemRet)
                     {
                         BiddingHistoryReport expData = new BiddingHistoryReport();
                         expData.BiddingCode = item.BiddingCode;
                         expData.ProjectName = item.ProjectName;
                         expData.CompanyName = item.CompanyName;
                         expData.EndDate = item.EndDate.ToString(@"dd\/MM\/yyyy");
                         expData.BiddingPrice = item.BiddingPrice;

                         l.Add(expData);
                     }


                     GlobalFunction func = new GlobalFunction();
                     string data = func.WriteReport(_header, l);

                    //SEND RESPONSE
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "Attachment;Filename=bidding_history_report.csv");
                    Response.ContentType = "application/text";
                    Response.ContentEncoding = Encoding.UTF8;

                    Response.Output.Write(data);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }          
        }

        protected void gvListProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdfProjectNo = e.Row.FindControl("hdfProjectNo") as HiddenField;
                    HiddenField hdfBiddingsNo = e.Row.FindControl("hdfBiddingsNo") as HiddenField;
                    HyperLink hplSelectProj = e.Row.FindControl("hplSelectProj") as HyperLink;
                    if (hplSelectProj != null)
                    {
                        if (hdfProjectNo != null && hdfBiddingsNo != null)
                        {
                            GlobalFunction fEncrypt = new GlobalFunction();
                            hplSelectProj.NavigateUrl = "~/Form/CompanyBiddingDetail.aspx?ProjectNo=" + fEncrypt.Encrypt(hdfProjectNo.Value.Trim()) +
                                "&BiddingNo=" + fEncrypt.Encrypt(hdfBiddingsNo.Value.Trim());
                        }
                        else
                        {
                            hplSelectProj.Visible = false;
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
    }
}
