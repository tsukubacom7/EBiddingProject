using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using System.Collections;
using EAuctionProj.ReportDAO;
using EAuctionProj.Utility;
using System.Text;

namespace EAuctionProj
{
    public partial class VendorBiddingHistory : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(VendorBiddingHistory));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!Page.IsPostBack)
            {

                Session["VendorBiddingHistoryRPT"] = null;

                ViewState["SortGridview"] = "CompanyName";
                InitialControl();
            }
        }

        private void InitialControl()
        {
            MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
            if (retUser.RolesNo > 0)
            {
                if (retUser.RolesNo == 1)
                {
                    btnExport.Visible = true;
                }
                else
                {
                    btnExport.Visible = false;
                }
            }

            //BindGridview();
            BindCompanyBidding();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //BindGridview();
            BindCompanyBidding();
        }

        private void BindCompanyBidding()
        {
            try
            {
                /*************** List All Company Bidding **************************/
                Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();
                List<INF_PROJECTBIDDINGDETAIL_DTO> lItemProj = new List<INF_PROJECTBIDDINGDETAIL_DTO>();

                /********************* Criteria search ****************************/
                string _companyName = "";
                string _taxID = "";
                if (ddlSearch.SelectedIndex != 0)
                {
                    switch (ddlSearch.SelectedValue)
                    {
                        case "1":
                            _companyName = txtSearch.Text.Trim();
                            break;
                        case "2":
                            _taxID = txtSearch.Text.Trim();
                            break;
                    }
                }

                /******************************************************************/
                lItemProj = manage.ListAllCompanyBiddingHistory(_companyName, _taxID);
                /*****************************************************************/

                /********************** For Sort Gridview ************************/
                string _sortBy = (string)ViewState["SortGridview"];
                switch (_sortBy.Trim())
                {
                    case "CompanyName":
                        lItemProj = lItemProj.OrderBy(x => x.CompanyName).ToList();
                        break;
                    case "CompanyName DESC":
                        lItemProj = lItemProj.OrderByDescending(x => x.CompanyName).ToList();
                        break;
                    case "TaxID":
                        lItemProj = lItemProj.OrderBy(x => x.TaxID).ToList();
                        break;
                    case "TaxID DESC":
                        lItemProj = lItemProj.OrderByDescending(x => x.TaxID).ToList();
                        break;
                }
                /*****************************************************************/

                gvCompanyBidding.DataSource = lItemProj;
                gvCompanyBidding.DataBind();


                Session["VendorBiddingHistoryRPT"] = lItemProj;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        //protected void gvListProject_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //if (e.CommandName.Equals("Select"))
        //    //{
        //    //    int intRowIndex = int.Parse(e.CommandArgument.ToString());
        //    //    string ProjectNo = gvListProject.Rows[intRowIndex].Cells[0].Text;

        //    //    Response.Redirect("~/Form/BiddingProject.aspx?ProjectID=" + ProjectNo.Trim());
        //    //}
        //}      

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;

            BindCompanyBidding();
        }

        protected void gvCompanyBidding_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortGridview"] = GetColumnSirting(e.SortExpression);
            BindCompanyBidding();
        }

        protected void gvCompanyBidding_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompanyBidding.PageIndex = e.NewPageIndex;
            BindCompanyBidding();
        }

        private string GetColumnSirting(string SortBy)
        {
            string retSortColumn = string.Empty;

            if (SortBy.Equals("CompanyName"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("CompanyName"))
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
            if (SortBy.Equals("TaxID"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("TaxID"))
                    {
                        retSortColumn = "TaxID";
                    }
                    else
                    {
                        retSortColumn = "TaxID DESC";
                    }
                }
                else
                {
                    retSortColumn = "TaxID";
                }
            }

            return retSortColumn;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["VendorBiddingHistoryRPT"] != null)
                {
                    string header = string.Empty;
                    for (int i = 0; i <= this.gvCompanyBidding.Columns.Count - 1; i++)
                    {
                        if ((this.gvCompanyBidding.Columns[i]) is System.Web.UI.WebControls.BoundField)
                        {
                            header += this.gvCompanyBidding.Columns[i].HeaderText + ",";
                        }
                    }

                    string _header = header.Substring(0, header.Length - 1);

                    List<INF_PROJECTBIDDINGDETAIL_DTO> lItemRet = new List<INF_PROJECTBIDDINGDETAIL_DTO>();
                    lItemRet = (List<INF_PROJECTBIDDINGDETAIL_DTO>)Session["VendorBiddingHistoryRPT"];

                    ArrayList l = new ArrayList();
                    foreach (INF_PROJECTBIDDINGDETAIL_DTO item in lItemRet)
                    {
                        VendorBiddingHistoryReport expData = new VendorBiddingHistoryReport();
                        expData.CompanyName = item.CompanyName;
                        expData.TaxID = "'" + item.TaxID;

                        l.Add(expData);
                    }

                    GlobalFunction func = new GlobalFunction();
                    string data = func.WriteReport(_header, l);

                    //SEND RESPONSE
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "Attachment;Filename=vender_biddinghistory_report.csv");
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
    }
}
