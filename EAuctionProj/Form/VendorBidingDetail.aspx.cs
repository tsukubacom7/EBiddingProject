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
using System.Web.UI.HtmlControls;
using System.IO;

namespace EAuctionProj
{
    public partial class VendorBidingDetail : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(VendorBidingDetail));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }  


            if (!Page.IsPostBack)
            {
                Session["VendorBiddingDetailRPT"] = null;

                hdfCompanyNo.Value = Request.QueryString["CompanyNo"];
                ViewState["SortGridview"] = "CreatedDate DESC";

                GetCompanyDetail(hdfCompanyNo.Value);
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


            BindGridview();
        }

        private void GetCompanyDetail(string CompanyNo)
        {
            try
            {
                string _companyNo = CompanyNo;
                Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                MAS_BIDDINGCOMPANY company = new MAS_BIDDINGCOMPANY();
                company = manage.GetBiddingCompany(_companyNo);

                lblCompany.Text = company.CompanyName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void BindGridview()
        {
            try
            {
                string _biddingCode = "";
                string _projectName = "";               

                if (ddlSearch.SelectedIndex != 0)
                {
                    switch (ddlSearch.SelectedValue)
                    {
                        case "1":
                            _biddingCode = txtSearch.Text.Trim();
                            break;
                        case "2":
                            _projectName = txtSearch.Text.Trim();
                            break;
                    }
                }
            

                List<INF_PROJECTBIDDINGDETAIL_DTO> lItemRet = new List<INF_PROJECTBIDDINGDETAIL_DTO>();               
                Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();

                string _companyNo = hdfCompanyNo.Value.ToString();
                lItemRet = manage.ListBiddingVendorProject(_companyNo, _projectName, _biddingCode);

                /********************** For Sort Gridview ************************/
                string _sortBy = (string)ViewState["SortGridview"];
                switch (_sortBy.Trim())
                {
                    case "BiddingCode":
                        lItemRet = lItemRet.OrderBy(x => x.BiddingCode).ToList();
                        break;
                    case "BiddingCode DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.CompanyName).ToList();
                        break;
                    case "ProjectName":
                        lItemRet = lItemRet.OrderBy(x => x.ProjectName).ToList();
                        break;
                    case "ProjectName DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.ProjectName).ToList();
                        break;
                    case "CreatedDate":
                        lItemRet = lItemRet.OrderBy(x => x.CreatedDate).ToList();
                        break;
                    case "CreatedDate DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    case "BiddingPrice":
                        lItemRet = lItemRet.OrderBy(x => x.BiddingPrice).ToList();
                        break;
                    case "BiddingPrice DESC":
                        lItemRet = lItemRet.OrderByDescending(x => x.BiddingPrice).ToList();
                        break;
                }
                /*****************************************************************/

                gvListProject.DataSource = lItemRet;
                gvListProject.DataBind();

                Session["VendorBiddingDetailRPT"] = lItemRet;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridview();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            BindGridview();
        }

        protected void gvListProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListProject.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        protected void gvListProject_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortGridview"] = GetColumnSirting(e.SortExpression);
            BindGridview(); 
        }

        private string GetColumnSirting(string SortBy)
        {
            string retSortColumn = string.Empty;

            if (SortBy.Equals("BiddingCode"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("BiddingCode"))
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
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("ProjectName"))
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
            Response.Redirect("~/Form/VendorBiddingHistory.aspx", false);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
            //ExportGridToExcel();

            //try
            //{
            //    if (Session["VendorBiddingDetailRPT"] != null)
            //    {
            //        string header = string.Empty;
            //        for (int i = 0; i <= this.gvListProject.Columns.Count - 1; i++)
            //        {
            //            if ((this.gvListProject.Columns[i]) is System.Web.UI.WebControls.BoundField)
            //            {
            //                header += this.gvListProject.Columns[i].HeaderText + ",";
            //            }
            //        }

            //        string _header = header.Substring(0, header.Length - 1);

            //        List<INF_PROJECTBIDDINGDETAIL_DTO> lItemRet = new List<INF_PROJECTBIDDINGDETAIL_DTO>();
            //        lItemRet = (List<INF_PROJECTBIDDINGDETAIL_DTO>)Session["VendorBiddingDetailRPT"];

            //        ArrayList l = new ArrayList();
            //        foreach (INF_PROJECTBIDDINGDETAIL_DTO item in lItemRet)
            //        {
            //            VendorBiddingDetailReport expData = new VendorBiddingDetailReport();
            //            expData.BiddingCode = item.BiddingCode;
            //            expData.ProjectName = item.ProjectName;
            //            expData.CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy");
            //            expData.BiddingPrice = item.BiddingPrice;

            //            l.Add(expData);
            //        }

            //        GlobalFunction func = new GlobalFunction();
            //        string data = func.WriteReport(_header, l);

            //        //******SEND RESPONSE**********//
            //        Response.ClearContent();
            //        Response.AddHeader("Content-Disposition", "Attachment;Filename=vender_biddingdetail_report.csv");
            //        Response.ContentType = "application/text";

            //        Response.Charset = "TIS-620";
            //        Response.ContentEncoding = System.Text.Encoding.UTF8;
            //        //Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            //        Response.Output.Write(data);
            //        Response.Flush();
            //        Response.End();
            //        //****************************//
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //    logger.Error(ex.StackTrace);
            //}    
        }


        private void ExportGridToCSV()
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "Attachment;Filename=vender_biddingdetail_report.csv");                
                Response.Charset = "TIS-620";
                Response.ContentType = "application/text";

                StringBuilder columnbind = new StringBuilder();   
                for (int k = 0; k < gvListProject.Columns.Count; k++)
                {
                    columnbind.Append(gvListProject.Columns[k].HeaderText + ',');
                }

                columnbind.Append("\r\n");
                for (int i = 0; i < gvListProject.Rows.Count; i++)
                {
                    for (int k = 0; k < gvListProject.Columns.Count; k++)
                    {
                        columnbind.Append(gvListProject.Rows[i].Cells[k].Text + ',');
                    }

                    columnbind.Append("\r\n");
                }

                Response.ContentEncoding = Encoding.Default;
                //Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

                Response.Output.Write(columnbind.ToString());
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {                
               logger.Error(ex.Message);
               logger.Error(ex.StackTrace);
            }           

        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Export1.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvListProject.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
            return;
        }

    }
}
