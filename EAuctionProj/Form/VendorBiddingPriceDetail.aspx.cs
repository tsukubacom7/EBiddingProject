using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using EAuctionProj.BL;
using EAuctionProj.DAL;
using System.Net;
using System.IO;
using System.Collections;
using EAuctionProj.ReportDAO;
using EAuctionProj.Utility;
using System.Text;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class VendorBiddingPriceDetail : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(VendorBiddingPriceDetail));

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

            if (!IsPostBack)
            {
                Session["VendorBiddingDetailRPT"] = null;

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

                hdfProjectNo.Value = Request.QueryString["ProjectNo"];
                hdfBiddingNo.Value = Request.QueryString["BiddingNo"];

                ViewState["TemplateNo"] = string.Empty;
                ViewState["TemplateColName"] = null;

                GetProjectBidding();
                BindGvAttachFile();
                GetPriceDetail();
            }

            //else
            //{              
            //    string _templateNo = (string)ViewState["TemplateNo"];
            //    BindItemProject(_templateNo);
            //    BindGvAttachFile();
            //}
        }

        private void GetProjectBidding()
        {
            Mas_ProjectBidding_Manage bl = new Mas_ProjectBidding_Manage();
            MAS_PROJECTBIDDING projData = new MAS_PROJECTBIDDING();
            projData.ProjectNo = Int64.Parse(string.IsNullOrWhiteSpace(hdfProjectNo.Value.ToString()) ? "0" : hdfProjectNo.Value.ToString());
            projData = bl.GetMasProjItemBidding(projData);

            /**************** Retrieve Data ********************/
            hdfProjectNo.Value = projData.ProjectNo.ToString();

            lblBiddingCode.Text = projData.BiddingCode;
            lblProjectName.Text = projData.ProjectName;

            lblStartDate.Text = projData.StartDate.ToString(@"dd\/MM\/yyyy");
            lblEndDate.Text = projData.EndDate.ToString(@"dd\/MM\/yyyy");

            ViewState["TemplateNo"] = projData.TemplateNo.ToString();

            BindItemProject(projData.TemplateNo.ToString());
            /***************************************************/
        }

        protected void BindGvAttachFile()
        {
            try
            {
                string _biddingNo = hdfBiddingNo.Value;
                Mas_ProjectITemBidding_Manage manage = new Mas_ProjectITemBidding_Manage();
                List<INF_BIDDINGATTACHMENT> lAttach = new List<INF_BIDDINGATTACHMENT>();

                lAttach = manage.ListInfBiddingAttachments(_biddingNo);

                gvAttachFile.DataSource = lAttach;
                gvAttachFile.DataBind();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void GetPriceDetail()
        {
            try
            {
                string _biddingNo = hdfBiddingNo.Value;
                Mas_ProjectITemBidding_Manage manage = new Mas_ProjectITemBidding_Manage();
                INF_BIDDINGS data = new INF_BIDDINGS();

                data = manage.GetInfBidding(_biddingNo);
                if (data.BiddingsNo != null)
                {
                    hdfCompanyNo.Value = data.CompanyNo.ToString();
                    lblSummaryPrice.Text = Convert.ToDouble(data.BiddingPrice).ToString("#,##0.00");

                    //lblEstimatedTax.Text = Convert.ToDouble(data.BiddingVat7).ToString("#,##0.00");
                    //lblTotal.Text = Convert.ToDouble(data.BiddingTotalPrice).ToString("#,##0.00");                  

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }     

        protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string cellName = string.Empty;
                TemplateField fieldDataRow = null;
                Label lbl = null;

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string _rowName = "";
                    fieldDataRow = (TemplateField)((DataControlFieldCell)e.Row.Cells[i]).ContainingField;
                    cellName = fieldDataRow.HeaderText;

                    if (cellName.Equals("บริษัท"))
                    {
                        lbl = new Label();
                        lbl.ID = "lblCompamy";
                        lbl.Text = (e.Row.DataItem as DataRowView).Row["ItemColumn1"].ToString();
                        //lbl.Style.Add("Font-Size", "11px");
                        lbl.CssClass = "lblGridDetailLeft";
                        e.Row.Cells[i].Controls.Add(lbl);
                        //e.Row.Cells[i].Attributes.Add("align", "left");

                    }
                    else if (cellName.Equals("รายละเอียด"))
                    {
                        lbl = new Label();
                        lbl.ID = "lblDetail";
                        lbl.Text = (e.Row.DataItem as DataRowView).Row["ItemColumn2"].ToString();
                        //lbl.Style.Add("Font-Size", "11px");
                        lbl.CssClass = "lblGridDetailLeft";
                        e.Row.Cells[i].Controls.Add(lbl);
                        //e.Row.Cells[i].Attributes.Add("align", "left");
                    }
                    else if (cellName.Equals("ราคารวม(บาท)"))
                    {
                        _rowName = "ItemColumn" + (i + 1).ToString();
                        lbl = new Label();
                        lbl.ID = "lblTotalAmount";
                        lbl.Text = (e.Row.DataItem as DataRowView).Row[_rowName].ToString();
                        lbl.CssClass = "lblGridAmount";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "right");
                    }
                    else
                    {
                        _rowName = "ItemColumn" + (i + 1).ToString();
                        lbl = new Label();
                        lbl.ID = "lblItemColUnit" + i.ToString();
                        lbl.Text = (e.Row.DataItem as DataRowView).Row[_rowName].ToString();
                        //lbl.Style.Add("Font-Size", "11px");
                        lbl.CssClass = "lblGridAmount";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "center");
                    }    
                    
                    
                    //else if (cellName.Contains("บาท") ||
                    //    cellName.Contains("ราคารวม") ||
                    //    cellName.Contains("ค่าบริการ/สัญญา"))
                    //{
                    //    lbl = new Label();
                    //    lbl.ID = "lblItemColumn";
                    //    lbl.CssClass = "lblGridAmount";
                    //    e.Row.Cells[i].Controls.Add(lbl);
                    //    //e.Row.Cells[i].Attributes.Add("align", "right");
                    //}
                }
            }
        }     
     
        private void BindItemProject(string TemplateNo)
        {
            try
            {
                /******************* Get Column Item Project Name ********/
                MAS_TEMPLATECOLNAME value = new MAS_TEMPLATECOLNAME();
                Int64 pkItemCol = Int64.Parse(TemplateNo);
                Mas_TemplateColName_Manage manageCol = new Mas_TemplateColName_Manage();
                MAS_TEMPLATECOLNAME para = new MAS_TEMPLATECOLNAME();
                para.TemplateNo = pkItemCol;
                value = manageCol.GetMasTemplateColNameByKey(para);
                /*******************************************************/

                /*************** Gen Table & Gridview Column **********/
                DataTable dtColumnName = new DataTable();
                dtColumnName = CreateTableItemColumn(value);
                ViewState["TemplateColName"] = dtColumnName;
                /******************************************************/

                /*************** List Item Project ********************************/
                Mas_ProjectITemBidding_Manage projItemBL = new Mas_ProjectITemBidding_Manage();
                List<MAS_PROJECTITEMBIDDING> lItemProj = new List<MAS_PROJECTITEMBIDDING>();

                string _biddingNo = hdfBiddingNo.Value;
                lItemProj = projItemBL.ListInfBiddingDetails(_biddingNo);
                /*****************************************************************/

                DataRow row;

                string strComName = string.Empty;
                foreach (MAS_PROJECTITEMBIDDING item in lItemProj)
                {
                    row = dtColumnName.NewRow();

                    //row["ProjectItemNo"] = item.ProjectItemNo;
                    //row["ProjectNo"] = item.ProjectNo;

                    if (row.Table.Columns["ItemColumn1"] != null)
                    {
                        row["ItemColumn1"] = item.ItemColumn1;
                    }
                    if (row.Table.Columns["ItemColumn2"] != null)
                    {
                        row["ItemColumn2"] = item.ItemColumn2;
                    }
                    if (row.Table.Columns["ItemColumn3"] != null)
                    {
                        row["ItemColumn3"] = item.ItemColumn3;
                    }
                    if (row.Table.Columns["ItemColumn4"] != null)
                    {
                        row["ItemColumn4"] = item.ItemColumn4;
                    }
                    if (row.Table.Columns["ItemColumn5"] != null)
                    {
                        row["ItemColumn5"] = item.ItemColumn5;
                    }
                    if (row.Table.Columns["ItemColumn6"] != null)
                    {
                        row["ItemColumn6"] = item.ItemColumn6;
                    }
                    if (row.Table.Columns["ItemColumn7"] != null)
                    {
                        row["ItemColumn7"] = item.ItemColumn7;
                    }
                    if (row.Table.Columns["ItemColumn8"] != null)
                    {
                        row["ItemColumn8"] = item.ItemColumn8;
                    }

                    strComName = item.ItemColumn1;

                    dtColumnName.Rows.Add(row);
                }          

                gvItem.DataSource = dtColumnName;
                gvItem.DataBind();

                Session["VendorBiddingDetailRPT"] = dtColumnName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private DataTable CreateTableItemColumn(MAS_TEMPLATECOLNAME colName)
        {
            /*********** For Create DataTable and Gridview Column ****************/
            DataTable dtColName = null;
            dtColName = new DataTable();
            dtColName.Clear();

            //dtColName.Columns.Add("ProjectItemNo", typeof(string));
            //dtColName.Columns.Add("ProjectNo", typeof(string));

            /**************** Set Gridview Property ***************/
            gvItem.Columns.Clear();
            gvItem.AllowSorting = false;
            gvItem.AllowPaging = false;
            gvItem.ShowFooter = true;
            /******************************************************/

            TemplateField tfield;
            if (!string.IsNullOrWhiteSpace(colName.ColumnName1))
            {
                dtColName.Columns.Add("ItemColumn1", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName1;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName2))
            {
                dtColName.Columns.Add("ItemColumn2", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName2;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName3))
            {
                dtColName.Columns.Add("ItemColumn3", typeof(string));


                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName3;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName4))
            {
                dtColName.Columns.Add("ItemColumn4", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName4;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName5))
            {
                dtColName.Columns.Add("ItemColumn5", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName5;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName6))
            {
                dtColName.Columns.Add("ItemColumn6", typeof(string));


                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName6;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName7))
            {
                dtColName.Columns.Add("ItemColumn7", typeof(string));


                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName7;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName8))
            {
                dtColName.Columns.Add("ItemColumn8", typeof(string));

                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName8;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvItem.Columns.Add(tfield);
            }

            return dtColName;
        }
       

        protected void gvAttachFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("lnkDownload") as LinkButton;
                //ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);

                string pathFile = lb.CommandArgument;
                pathFile = Page.ResolveClientUrl(pathFile);
                lb.Attributes.Add("href", pathFile);
                lb.Attributes.Add("target", "_blank");

            }
        }  

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            //LinkButton lbtnDownload = (LinkButton)(sender);
            //string pathFile = lbtnDownload.CommandArgument;

            //string fileName = Path.GetFileName(pathFile);

            //WebClient req = new WebClient();
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.ClearContent();
            //response.ClearHeaders();
            //response.Buffer = true;
            //response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            //byte[] data = req.DownloadData(Server.MapPath(pathFile));
            //response.BinaryWrite(data);
            //response.End();

            //pathFile = Page.ResolveClientUrl(pathFile);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + pathFile + "','_blank')", true);

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/VendorBidingDetail.aspx?CompanyNo=" + hdfCompanyNo.Value);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["VendorBiddingDetailRPT"] != null)
                {
                    string header = string.Empty;
                    for (int i = 0; i <= this.gvItem.Columns.Count - 1; i++)
                    {
                        if ((this.gvItem.Columns[i]) is System.Web.UI.WebControls.TemplateField)
                        {
                            header += this.gvItem.Columns[i].HeaderText + ",";
                        }
                    }

                    string _header = header.Substring(0, header.Length - 1);

                    DataTable dt = (DataTable)Session["VendorBiddingDetailRPT"];
                    ArrayList l = new ArrayList();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProjectBiddingDetailReport expData = new ProjectBiddingDetailReport();
                        if (dt.Columns["ItemColumn1"] != null)
                        {
                            expData.ItemColumn1 = dt.Rows[i]["ItemColumn1"].ToString();
                        }
                        if (dt.Columns["ItemColumn2"] != null)
                        {
                            expData.ItemColumn2 = dt.Rows[i]["ItemColumn2"].ToString();
                        }
                        if (dt.Columns["ItemColumn3"] != null)
                        {
                            expData.ItemColumn3 = dt.Rows[i]["ItemColumn3"].ToString();
                        }
                        if (dt.Columns["ItemColumn4"] != null)
                        {
                            expData.ItemColumn4 = dt.Rows[i]["ItemColumn4"].ToString();
                        }
                        if (dt.Columns["ItemColumn5"] != null)
                        {
                            expData.ItemColumn5 = dt.Rows[i]["ItemColumn5"].ToString();
                        }
                        if (dt.Columns["ItemColumn6"] != null)
                        {
                            expData.ItemColumn6 = dt.Rows[i]["ItemColumn6"].ToString();
                        }
                        if (dt.Columns["ItemColumn7"] != null)
                        {
                            expData.ItemColumn7 = dt.Rows[i]["ItemColumn7"].ToString();
                        }
                        if (dt.Columns["ItemColumn8"] != null)
                        {
                            expData.ItemColumn8 = dt.Rows[i]["ItemColumn8"].ToString();
                        }

                        l.Add(expData);
                    }

                    GlobalFunction func = new GlobalFunction();
                    string data = func.WriteReport(_header, l);

                    //SEND RESPONSE
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "Attachment;Filename=vendor_biddingprice_detail.csv");
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
