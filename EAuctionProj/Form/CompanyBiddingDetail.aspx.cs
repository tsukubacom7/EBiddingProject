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
using EAuctionProj.Utility;
using System.Text;
using EAuctionProj.ReportDAO;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class CompanyBiddingDetail : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CompanyBiddingDetail));

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
                if (!IsPostBack)
                {
                    GlobalFunction fDecrypt = new GlobalFunction();

                    string _ProjectNo = Request.QueryString["ProjectNo"];
                    string _BiddingNo = Request.QueryString["BiddingNo"];

                    if (string.IsNullOrEmpty(_ProjectNo) || string.IsNullOrEmpty(_BiddingNo))
                    {
                        Session.Clear();
                        Session.Abandon();
                        ViewState.Clear();
                        FormsAuthentication.SignOut();

                        Response.Redirect("~/Account/Login.aspx");
                    }
                    else
                    {                        
                        hdfProjectNo.Value = fDecrypt.Decrypt(_ProjectNo);
                        hdfBiddingNo.Value = fDecrypt.Decrypt(_BiddingNo);
                    }                 

                    Session["BiddingDetailRPT"] = null;

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
                            //********************* Authen User **********************************// 
                            if (!retUser.ProjectNo.ToString().Trim().Equals(hdfProjectNo.Value.Trim()))
                            {
                                Session.Clear();
                                Session.Abandon();
                                ViewState.Clear();
                                FormsAuthentication.SignOut();

                                Response.Redirect("~/Account/Login.aspx");
                            }

                            Mas_ProjectITemBidding_Manage manage = new Mas_ProjectITemBidding_Manage();
                            INF_BIDDINGS bData = new INF_BIDDINGS();
                            bData = manage.GetInfBidding(hdfBiddingNo.Value.Trim());
                            if (bData.BiddingsNo == null)
                            {
                                Session.Clear();
                                Session.Abandon();
                                ViewState.Clear();
                                FormsAuthentication.SignOut();

                                Response.Redirect("~/Account/Login.aspx");
                            }
                            else
                            {
                                if (bData.CompanyNo != retUser.CompanyNo && bData.ProjectNo != retUser.ProjectNo)
                                {
                                    Session.Clear();
                                    Session.Abandon();
                                    ViewState.Clear();
                                    FormsAuthentication.SignOut();

                                    Response.Redirect("~/Account/Login.aspx");
                                }
                            }
                            //******************************************************//

                        }
                    }

                    ViewState["TemplateNo"] = string.Empty;
                    ViewState["TemplateColName"] = null;

                    GetProjectBidding();
                    BindGvAttachFile();
                    GetPriceDetail();

                    linkViewQuestionaire.Attributes["href"] = "~/Form/ViewQuestionnaire.aspx?ProjectNo=" + fDecrypt.Encrypt(hdfProjectNo.Value.Trim()) +
                        "&CompanyNo=" + fDecrypt.Encrypt(hdfCompanyNo.Value.Trim());
                }
            }
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

            ViewState["TemplateNo"] = projData.TemplateNo.ToString();
            hdfBiddingCode.Value = projData.BiddingCode;

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
                    lblSummaryPrice.Text = Convert.ToDouble(data.BiddingPrice).ToString("#,##0.00");                   

                    //lblEstimatedTax.Text = Convert.ToDouble(data.BiddingVat7).ToString("#,##0.00");
                    hdfCompanyNo.Value = data.CompanyNo.ToString();
                    //lblTotal.Text = Convert.ToDouble(data.BiddingTotalPrice).ToString("#,##0.00");

                    GetBiddingCompany(data.CompanyNo.ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void GetBiddingCompany(string CompanyNo)
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
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "left");

                    }
                    else if (cellName.Equals("รายละเอียด"))
                    {
                        lbl = new Label();
                        lbl.ID = "lblDetail";
                        lbl.Text = (e.Row.DataItem as DataRowView).Row["ItemColumn2"].ToString();
                        //lbl.Style.Add("Font-Size", "11px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "left");
                    }
                    else
                    {
                        _rowName = "ItemColumn" + (i + 1).ToString();
                        lbl = new Label();
                        lbl.ID = "lblItemColUnit" + i.ToString();
                        lbl.Text = (e.Row.DataItem as DataRowView).Row[_rowName].ToString();
                        //lbl.Style.Add("Font-Size", "11px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "right");
                    }
                    //else if (cellName.Contains("บาท") && !cellName.Contains("ราคารวม") && !cellName.Contains("ค่าบริการ/สัญญา"))
                    //{
                    //    lbl = new Label();
                    //    lbl.ID = "lblItemColumn";
                    //    lbl.Style.Add("Font-Size", "11px");
                    //    lbl.Style.Add("text-align", "right");
                    //    lbl.Width = 100;
                    //    e.Row.Cells[i].Controls.Add(lbl);
                    //    e.Row.Cells[i].Attributes.Add("align", "center");
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

                Session["BiddingDetailRPT"] = dtColumnName;

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
                LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;
                //ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDownload);

                string pathFile = lnkDownload.CommandArgument;
                pathFile = Page.ResolveClientUrl(pathFile);
                lnkDownload.Attributes.Add("href", pathFile);
                lnkDownload.Attributes.Add("target", "_blank");
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
            Response.Redirect("~/Form/BidingProjectHistory.aspx", true);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            CultureInfo _cultureTHInfo = new CultureInfo("th-TH");
            try
            {
                ExportDTtoExcel();

                #region #### Export Old Function ####
                //if (Session["BiddingDetailRPT"] != null)
                //{
                //    string header = string.Empty;
                //    for (int i = 0; i <= this.gvItem.Columns.Count - 1; i++)
                //    {
                //        if ((this.gvItem.Columns[i]) is System.Web.UI.WebControls.TemplateField)
                //        {
                //            header += this.gvItem.Columns[i].HeaderText + ",";
                //        }
                //    }

                //    string _header = header.Substring(0, header.Length - 1);

                //    DataTable dt = (DataTable)Session["BiddingDetailRPT"];
                //    ArrayList lData = new ArrayList();

                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        ProjectBiddingDetailReport expData = new ProjectBiddingDetailReport();
                //        if (dt.Columns["ItemColumn1"] != null)
                //        {
                //            expData.ItemColumn1 = dt.Rows[i]["ItemColumn1"].ToString();
                //        }
                //        if (dt.Columns["ItemColumn2"] != null)
                //        {
                //            expData.ItemColumn2 = dt.Rows[i]["ItemColumn2"].ToString();
                //        }
                //        if (dt.Columns["ItemColumn3"] != null)
                //        {
                //            expData.ItemColumn3 = dt.Rows[i]["ItemColumn3"].ToString();
                //        }
                //        if (dt.Columns["ItemColumn4"] != null)
                //        {
                //            expData.ItemColumn4 = dt.Rows[i]["ItemColumn4"].ToString();
                //        }
                //        if (dt.Columns["ItemColumn5"] != null)
                //        {
                //            expData.ItemColumn5 = dt.Rows[i]["ItemColumn5"].ToString();
                //        }
                //        if (dt.Columns["ItemColumn6"] != null)
                //        {
                //            expData.ItemColumn6 = dt.Rows[i]["ItemColumn6"].ToString();
                //        }
                //        if (dt.Columns["ItemColumn7"] != null)
                //        {
                //            expData.ItemColumn7 = dt.Rows[i]["ItemColumn7"].ToString();
                //        }
                //        if (dt.Columns["ItemColumn8"] != null)
                //        {
                //            expData.ItemColumn8 = dt.Rows[i]["ItemColumn8"].ToString();
                //        }

                //        lData.Add(expData);
                //    }

                //    GlobalFunction func = new GlobalFunction();
                //    string data = func.WriteReport(_header, lData);

                //    //SEND RESPONSE
                //    HttpContext.Current.Response.ClearContent();
                //    HttpContext.Current.Response.ContentType = "application/ms-excel";
                //    HttpContext.Current.Response.AddHeader("Content-Disposition", "Attachment;Filename=company_bidding_detail.csv");
                //    HttpContext.Current.Response.Charset = "windows-874";
                //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-874");

                //    //Response.ContentType = "application/text";
                //    //Response.ContentEncoding = Encoding.UTF8;

                //    HttpContext.Current.Response.Output.Write(data);
                //    HttpContext.Current.Response.Flush();
                //    HttpContext.Current.Response.End();
                //}
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void ExportDTtoExcel()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=windows-874'>");

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=FleetExport.xls");
            HttpContext.Current.Response.Charset = "windows-874";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-874");

            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Tahoma;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");

            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " + "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
            "style='font-size:10.0pt; font-family:Tahoma; background:white;'> <TR>");

            //am getting my grid's column headers
            int columnscount = gvItem.Columns.Count;
            for (int j = 0; j < columnscount; j++)
            {
                //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(gvItem.Columns[j].HeaderText.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");

            DataTable dtBiddingDetail = (DataTable)Session["BiddingDetailRPT"];
            foreach (DataRow row in dtBiddingDetail.Rows)
            {
                //write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < dtBiddingDetail.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }
    }
}
