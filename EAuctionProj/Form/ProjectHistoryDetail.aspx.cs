using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EAuctionProj.BL;
using EAuctionProj.DAL;

namespace EAuctionProj
{
    public partial class ProjectHistoryDetail : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ProjectHistoryDetail));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdfProjectNo.Value = Request.QueryString["ProjectNo"];

                ViewState["SortGridview"] = "CreatedDate DESC";

                GetProjectBidding();
                BindCompanyBidding();
            }
        }

        private void GetProjectBidding()
        {
            Mas_ProjectBidding_Manage bl = new Mas_ProjectBidding_Manage();
            MAS_PROJECTBIDDING projData = new MAS_PROJECTBIDDING();
            projData.ProjectNo = Convert.ToInt64(string.IsNullOrWhiteSpace(hdfProjectNo.Value) ? "0" : hdfProjectNo.Value.ToString());
            projData = bl.GetMasProjItemBidding(projData);

            /**************** Retrieve Data ********************/
            lblBiddingCode.Text = projData.BiddingCode;
            lblProjectName.Text = projData.ProjectName;
            lblStartDate.Text = projData.StartDate.ToString(@"dd\/MM\/yyyy");            
            lblEndDate.Text = projData.EndDate.ToString(@"dd\/MM\/yyyy");

            //lblContactName.Text = projData.ContactName;
            //lblEmail.Text = projData.Email;          
            //lblPhoneNo.Text = projData.PhoneNo;          
            /***************************************************/
        }

        private void BindCompanyBidding()
        {
            try
            {              
                /*************** List Item Project ********************************/
                Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();
                List<INF_PROJECTBIDDINGDETAIL_DTO> lItemProj = new List<INF_PROJECTBIDDINGDETAIL_DTO>();
                string _projectNo = string.IsNullOrWhiteSpace(hdfProjectNo.Value) ? "0" : hdfProjectNo.Value.ToString();
                lItemProj = manage.ListBiddingProjectHistoryDet(_projectNo);
                /*****************************************************************/

                /********************** For Sort Gridview ************************/
                string _sortBy = (string)ViewState["SortGridview"];
                switch (_sortBy.Trim())
                {
                    case "CreatedDate":
                        lItemProj = lItemProj.OrderBy(x => x.CreatedDate).ToList();
                        break;
                    case "CreatedDate DESC":
                        lItemProj = lItemProj.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
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
                    case "BiddingPrice":
                        lItemProj = lItemProj.OrderBy(x => x.BiddingPrice).ToList();
                        break;
                    case "BiddingPrice DESC":
                        lItemProj = lItemProj.OrderByDescending(x => x.BiddingPrice).ToList();
                        break;
                }
                /*****************************************************************/

                gvListProject.DataSource = lItemProj;
                gvListProject.DataBind();

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

        protected void gvListProject_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortGridview"] = GetColumnSirting(e.SortExpression);
            BindCompanyBidding();            
        }

        private string GetColumnSirting(string SortBy)
        {
            string retSortColumn = string.Empty;

            if (SortBy.Equals("CreatedDate"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("CreatedDate"))
                    {
                        retSortColumn = "CreatedDate";
                    }
                    else
                    {
                        retSortColumn = "CreatedDate DESC";
                    }
                }
                else
                {
                    retSortColumn = "CreatedDate";
                }
            }

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

            if (SortBy.Equals("BiddingPrice"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("BiddingPrice"))
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

            return retSortColumn;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/BidingProjectHistory.aspx", false);
        }

    }
}
