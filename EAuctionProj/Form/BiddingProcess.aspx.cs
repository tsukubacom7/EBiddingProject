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
using EAuctionProj.Utility;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class BiddingProcess : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BiddingProcess));

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
                    string _projectNo = string.Empty;
                    _projectNo = Request.QueryString["ProjectNo"];

                    if (!string.IsNullOrEmpty(_projectNo))
                    {
                        GlobalFunction fDecrypt = new GlobalFunction();
                        hdfProjectNo.Value = fDecrypt.Decrypt(_projectNo);

                        MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                        if (retUser.RolesNo > 0)
                        {
                            hdfCompanyNo.Value = retUser.CompanyNo.ToString().Trim();
                            hdfUserName.Value = retUser.UserName;
                            hdfUserNo.Value = retUser.UsersNo.ToString().Trim();
                            hdfRoleNo.Value = retUser.RolesNo.ToString();

                            if (retUser.RolesNo == 2)
                            {
                                if (!retUser.ProjectNo.ToString().Trim().Equals(hdfProjectNo.Value.Trim()))
                                {
                                    Session.Clear();
                                    Session.Abandon();
                                    ViewState.Clear();
                                    FormsAuthentication.SignOut();

                                    Response.Redirect("~/Account/Login.aspx");
                                }
                            }
                        }

                        ViewState["PathFile"] = null;
                        ViewState["TemplateNo"] = string.Empty;
                        ViewState["TemplateColName"] = null;
                        ViewState["tbAttachFile"] = null;

                        GetProjectBidding();
                    }
                    else
                    {
                        Session.Clear();
                        Session.Abandon();
                        ViewState.Clear();
                        FormsAuthentication.SignOut();

                        Response.Redirect("~/Account/Login.aspx");
                    }
                }
                else
                {
                    string _templateNo = (string)ViewState["TemplateNo"];
                    BindItemProject(_templateNo);
                }
            }
        }

        private void GetProjectBidding()
        {
            Mas_ProjectBidding_Manage bl = new Mas_ProjectBidding_Manage();
            MAS_PROJECTBIDDING projData = new MAS_PROJECTBIDDING();
            projData.ProjectNo = Int64.Parse(string.IsNullOrWhiteSpace(hdfProjectNo.Value.ToString()) ? "0" : hdfProjectNo.Value.ToString());
            //Convert.ToInt64((string)ViewState["ProjectNo"]);
            projData = bl.GetMasProjItemBidding(projData);

            /**************** Retrieve Data ********************/
            hdfProjectNo.Value = projData.ProjectNo.ToString();

            lblBiddingCode.Text = projData.BiddingCode;
            lblContactName.Text = projData.ContactName;
            lblEmail.Text = projData.Email;
            lblEndDate.Text = projData.EndDate.ToString(@"dd\/MM\/yyyy");
            lblPhoneNo.Text = projData.PhoneNo;
            lblProjectName.Text = projData.ProjectName;
            lblStartDate.Text = projData.StartDate.ToString(@"dd\/MM\/yyyy");

            ViewState["TemplateNo"] = projData.TemplateNo.ToString();

            hdfBiddingCode.Value = projData.BiddingCode;

            BindItemProject(projData.TemplateNo.ToString());
            /***************************************************/
        }

        #region ####  DownloadAttachFile (Old function) ####
        //private void DownloadAttachFile(string pathFile)
        //{
        //    string _fileName = Path.GetFileName(pathFile);

        //    WebClient req = new WebClient();
        //    HttpResponse response = HttpContext.Current.Response;
        //    string filePath = pathFile;
        //    response.Clear();
        //    response.ClearContent();
        //    response.ClearHeaders();
        //    response.Buffer = true;

        //    response.AddHeader("Content-Disposition", "attachment;filename=" + _fileName);
        //    byte[] data = req.DownloadData(Server.MapPath(filePath));
        //    response.BinaryWrite(data);
        //    response.End();
        //}

        //protected void lbtnAttachFile_Click(object sender, EventArgs e)
        //{
        //    if (ViewState["PathFile"] != null)
        //    {
        //        string _pathfile = (string)ViewState["PathFile"];
        //        DownloadAttachFile(_pathfile);
        //    }
        //}
        #endregion

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            string _phoneNo = ConfigurationManager.GetConfiguration().GulfPhoneNo;

            try
            {
                /************** Insert data to database ***************/
                /****************** Bidding Detail ********************/
                INF_BIDDINGS bData = new INF_BIDDINGS();
                bData.CompanyNo = Int64.Parse(hdfCompanyNo.Value);
                bData.ProjectNo = Int64.Parse(hdfProjectNo.Value);
                bData.BiddingPrice = decimal.Parse(string.IsNullOrWhiteSpace(lblSummaryPrice.Text) ? "0" : lblSummaryPrice.Text);
                //bData.BiddingVat7 = decimal.Parse(string.IsNullOrWhiteSpace(lblEstimatedTax.Text) ? "0" : lblEstimatedTax.Text);
                //bData.BiddingTotalPrice = decimal.Parse(string.IsNullOrWhiteSpace(lblTotal.Text) ? "0" : lblTotal.Text);

                bData.CreatedBy = hdfUserName.Value.Trim();
                bData.CreatedDate = DateTime.Now;
                bData.UpdatedBy = hdfUserName.Value.Trim();
                bData.UpdatedDate = DateTime.Now;

                /****************************************************************/
                DataTable dtGVSource = (DataTable)gvItem.DataSource;
                /************************ List Bidding Item Detail **************/

                List<INF_BIDDINGDETAILS> lBidDet = new List<INF_BIDDINGDETAILS>();
                int iRowNo = 0;
                DataTable dtDataSource = new DataTable();
                dtDataSource = dtGVSource;
                //(DataTable)ViewState["TemplateColName"];
                if (dtDataSource != null && dtDataSource.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvItem.Rows)
                    {
                        INF_BIDDINGDETAILS bdItem = new INF_BIDDINGDETAILS();

                        bdItem.CreatedBy = hdfUserName.Value.Trim();
                        bdItem.CreatedDate = DateTime.Now;
                        bdItem.UpdatedBy = hdfUserName.Value.Trim();
                        bdItem.UpdatedDate = DateTime.Now;

                        int totalDTCol = (dtDataSource.Columns.Count - 2);
                        int iTxtDTCol = totalDTCol - 1;

                        Label lblItemColUnit2 = (Label)row.FindControl("lblItemColUnit2");
                        Label lblItemColUnit3 = (Label)row.FindControl("lblItemColUnit3");
                        Label lblItemColUnit4 = (Label)row.FindControl("lblItemColUnit4");
                        Label lblItemColUnit5 = (Label)row.FindControl("lblItemColUnit5");
                        Label lblItemColUnit6 = (Label)row.FindControl("lblItemColUnit6");
                        Label lblItemColUnit7 = (Label)row.FindControl("lblItemColUnit7");
                        Label lblItemColUnit8 = (Label)row.FindControl("lblItemColUnit8");
                        TextBox txtItemColumn = (TextBox)row.FindControl("txtItemColumn");

                        if (dtDataSource.Columns.Contains("ItemColumn1"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn1"].ToString()))
                            {
                                bdItem.ItemColumn1 = dtDataSource.Rows[iRowNo]["ItemColumn1"].ToString();
                            }
                        }

                        if (dtDataSource.Columns.Contains("ItemColumn2"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn2"].ToString()))
                            {
                                bdItem.ItemColumn2 = dtDataSource.Rows[iRowNo]["ItemColumn2"].ToString();
                            }
                        }

                        if (dtDataSource.Columns.Contains("ItemColumn3"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn3"].ToString()))
                            {
                                bdItem.ItemColumn3 = dtDataSource.Rows[iRowNo]["ItemColumn3"].ToString();
                            }
                            else
                            {
                                if (iTxtDTCol.ToString().Equals("3"))
                                {
                                    bdItem.ItemColumn3 = txtItemColumn.Text.Trim();
                                }
                                else if (totalDTCol.ToString().Equals("3"))
                                {
                                    bdItem.ItemColumn3 = lblItemColUnit2.Text;
                                }
                                else
                                {
                                    bdItem.ItemColumn3 = lblItemColUnit2.Text;
                                }
                            }
                        }

                        if (dtDataSource.Columns.Contains("ItemColumn4"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn4"].ToString()))
                            {
                                bdItem.ItemColumn4 = dtDataSource.Rows[iRowNo]["ItemColumn4"].ToString();
                            }
                            else
                            {
                                if (iTxtDTCol.ToString().Equals("4"))
                                {
                                    bdItem.ItemColumn4 = txtItemColumn.Text.Trim();
                                }
                                else if (totalDTCol.ToString().Equals("4"))
                                {
                                    bdItem.ItemColumn4 = lblItemColUnit3.Text;
                                }
                                else
                                {
                                    bdItem.ItemColumn4 = lblItemColUnit3.Text;
                                }
                            }
                        }

                        if (dtDataSource.Columns.Contains("ItemColumn5"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn5"].ToString()))
                            {
                                bdItem.ItemColumn5 = dtDataSource.Rows[iRowNo]["ItemColumn5"].ToString();
                            }
                            else
                            {
                                if (iTxtDTCol.ToString().Equals("5"))
                                {
                                    bdItem.ItemColumn5 = txtItemColumn.Text.Trim();
                                }
                                else if (totalDTCol.ToString().Equals("5"))
                                {
                                    bdItem.ItemColumn5 = lblItemColUnit4.Text;
                                }
                                else
                                {
                                    bdItem.ItemColumn5 = lblItemColUnit4.Text;
                                }
                            }
                        }

                        if (dtDataSource.Columns.Contains("ItemColumn6"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn6"].ToString()))
                            {
                                bdItem.ItemColumn6 = dtDataSource.Rows[iRowNo]["ItemColumn6"].ToString();
                            }
                            else
                            {
                                if (iTxtDTCol.ToString().Equals("6"))
                                {
                                    bdItem.ItemColumn6 = txtItemColumn.Text.Trim();
                                }
                                else if (totalDTCol.ToString().Equals("6"))
                                {
                                    bdItem.ItemColumn6 = lblItemColUnit5.Text;
                                }
                                else
                                {
                                    bdItem.ItemColumn6 = lblItemColUnit5.Text;
                                }
                            }
                        }

                        if (dtDataSource.Columns.Contains("ItemColumn7"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn7"].ToString()))
                            {
                                bdItem.ItemColumn7 = dtDataSource.Rows[iRowNo]["ItemColumn7"].ToString();
                            }
                            else
                            {
                                if (iTxtDTCol.ToString().Equals("7"))
                                {
                                    bdItem.ItemColumn7 = txtItemColumn.Text.Trim();
                                }
                                else if (totalDTCol.ToString().Equals("7"))
                                {
                                    bdItem.ItemColumn7 = lblItemColUnit6.Text;
                                }
                                else
                                {
                                    bdItem.ItemColumn7 = lblItemColUnit6.Text;
                                }
                            }
                        }

                        if (dtDataSource.Columns.Contains("ItemColumn8"))
                        {
                            if (!string.IsNullOrWhiteSpace(dtDataSource.Rows[iRowNo]["ItemColumn8"].ToString()))
                            {
                                bdItem.ItemColumn8 = dtDataSource.Rows[iRowNo]["ItemColumn8"].ToString();
                            }
                            else
                            {
                                if (iTxtDTCol.ToString().Equals("8"))
                                {
                                    bdItem.ItemColumn8 = txtItemColumn.Text.Trim();
                                }
                                else if (totalDTCol.ToString().Equals("8"))
                                {
                                    bdItem.ItemColumn8 = lblItemColUnit7.Text;
                                }
                                else
                                {
                                    bdItem.ItemColumn8 = lblItemColUnit7.Text;
                                }
                            }
                        }

                        lBidDet.Add(bdItem);

                        iRowNo += 1;
                    }
                }

                /**********************************************************************/

                /**************************** List Attach File *************************/
                List<INF_BIDDINGATTACHMENT> lAttach = new List<INF_BIDDINGATTACHMENT>();
                DataTable dtAttachFile = (DataTable)ViewState["tbAttachFile"];
                if (dtAttachFile != null && dtAttachFile.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAttachFile.Rows.Count; i++)
                    {
                        INF_BIDDINGATTACHMENT dataAttach = new INF_BIDDINGATTACHMENT();
                        //dataAttach.BiddingsNo = "";
                        dataAttach.AttachFilePath = dtAttachFile.Rows[i]["AttachFilePath"].ToString();
                        dataAttach.Description = dtAttachFile.Rows[i]["Description"].ToString();
                        dataAttach.FileName = dtAttachFile.Rows[i]["FileName"].ToString();

                        dataAttach.CreatedBy = hdfUserName.Value.Trim();
                        dataAttach.CreatedDate = DateTime.Now;
                        dataAttach.UpdatedBy = hdfUserName.Value.Trim();
                        dataAttach.UpdatedDate = DateTime.Now;

                        lAttach.Add(dataAttach);
                    }
                }
                /***********************************************************************/

                /*******************************************************/
                Inf_Biddings_Manage manage = new Inf_Biddings_Manage();
                string result = manage.InsSubmitBiddings(bData, lBidDet, lAttach);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    //    "alert('ระบบได้รับการเสนอราคาเรียบร้อยแล้ว!');window.location ='BidingProjectList.aspx';", true);                    

                    this.lbtnPopup_ModalPopupExtender.Show();

                    //Response.Redirect("~/Form/BidingProjectList.aspx", true);
                }
                else
                {
                    logger.Info("Can not insert:" + result);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                     "alertMessage", "alert('ไม่สามารถบันทึกข้อมูลได้')", true);
                }
                /*****************************************************/
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "alertMessage", "alert('ไม่สามารถบันทึกข้อมูลได้! กรุณาติดต่อผู้ดูแลระบบที่เบอร์" + _phoneNo + " ')", true);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/BidingProjectList.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/BidingProjectList.aspx");
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
                        lbl.Style.Add("Font-Size", "16px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "left");

                    }
                    else if (cellName.Equals("รายละเอียด"))
                    {
                        lbl = new Label();
                        lbl.ID = "lblDetail";
                        lbl.Text = (e.Row.DataItem as DataRowView).Row["ItemColumn2"].ToString();
                        lbl.Style.Add("Font-Size", "16px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "left");
                    }
                    else if (cellName.Contains("บาท") &&
                        !cellName.Contains("ราคารวม") && !cellName.Contains("ค่าบริการ/สัญญา"))
                    {
                        TextBox txt = new TextBox();
                        txt.ID = "txtItemColumn";
                        txt.Style.Add("Font-Size", "16px");
                        txt.Style.Add("text-align", "right");
                        txt.Style.Add("width", "90%");
                        txt.Width = 100;
                        txt.CssClass = "TextAreaRemark";
                        txt.AutoPostBack = true;
                        txt.TextChanged += new EventHandler(txt_TextChanged);
                        e.Row.Cells[i].Controls.Add(txt);                       
                        e.Row.Cells[i].Attributes.Add("align", "right");
                        e.Row.Cells[i].Attributes.Add("onkeypress", "return isNumberKey(event)");
                    }
                    else if (cellName.Contains("ราคารวม"))
                    {
                        _rowName = "ItemColumn" + (i + 1).ToString();
                        lbl = new Label();
                        lbl.ID = "lblItemColUnit" + i.ToString();
                        lbl.Text = (e.Row.DataItem as DataRowView).Row[_rowName].ToString();
                        lbl.Style.Add("Font-Size", "16px");
                        lbl.CssClass = "lblGridDetail";                       
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "right");
                    }
                    else
                    {
                        _rowName = "ItemColumn" + (i + 1).ToString();
                        lbl = new Label();
                        lbl.ID = "lblItemColUnit" + i.ToString();
                        lbl.Text = (e.Row.DataItem as DataRowView).Row[_rowName].ToString();
                        lbl.Style.Add("Font-Size", "16px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "center");
                    }
                }
            }
        }

        protected void txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                /************ Calculate Price *****************/
                double dPrice = 0.0;

                int iItemColUnit2 = 1;
                int iItemColUnit3 = 1;
                int iItemColUnit4 = 1;
                int iItemColUnit5 = 1;
                int iItemColUnit6 = 1;
                int iItemColUnit7 = 1;

                GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
                TextBox txtItemColumn = (TextBox)row.FindControl("txtItemColumn");
                if (txtItemColumn != null && !string.IsNullOrWhiteSpace(txtItemColumn.Text.Trim()))
                {
                    dPrice = double.Parse(txtItemColumn.Text.Trim());
                }
                txtItemColumn.Text = dPrice.ToString("#,##0.00");

                Label lblItemColUnit2 = (Label)row.FindControl("lblItemColUnit2");
                if (lblItemColUnit2 != null && !string.IsNullOrWhiteSpace(lblItemColUnit2.Text.Trim()))
                {
                    iItemColUnit2 = int.Parse(lblItemColUnit2.Text.Trim());
                }

                Label lblItemColUnit3 = (Label)row.FindControl("lblItemColUnit3");
                if (lblItemColUnit3 != null && !string.IsNullOrWhiteSpace(lblItemColUnit3.Text.Trim()))
                {
                    iItemColUnit3 = int.Parse(lblItemColUnit3.Text.Trim());
                }
                Label lblItemColUnit4 = (Label)row.FindControl("lblItemColUnit4");
                if (lblItemColUnit4 != null && !string.IsNullOrWhiteSpace(lblItemColUnit4.Text.Trim()))
                {
                    iItemColUnit4 = int.Parse(lblItemColUnit4.Text.Trim());
                }
                Label lblItemColUnit5 = (Label)row.FindControl("lblItemColUnit5");
                if (lblItemColUnit5 != null && !string.IsNullOrWhiteSpace(lblItemColUnit5.Text.Trim()))
                {
                    iItemColUnit5 = int.Parse(lblItemColUnit5.Text.Trim());
                }
                Label lblItemColUnit6 = (Label)row.FindControl("lblItemColUnit6");
                if (lblItemColUnit6 != null && !string.IsNullOrWhiteSpace(lblItemColUnit6.Text.Trim()))
                {
                    iItemColUnit6 = int.Parse(lblItemColUnit6.Text.Trim());
                }
                Label lblItemColUnit7 = (Label)row.FindControl("lblItemColUnit7");
                if (lblItemColUnit7 != null && !string.IsNullOrWhiteSpace(lblItemColUnit7.Text.Trim()))
                {
                    iItemColUnit7 = int.Parse(lblItemColUnit7.Text.Trim());
                }

                int totalCell = gvItem.Columns.Count;
                string ctrlName = "lblItemColUnit" + (totalCell - 1).ToString();
                Label lblTotalPrice = (Label)row.FindControl(ctrlName);

                double dTotalPrice = (iItemColUnit2 * iItemColUnit3 * iItemColUnit4 * iItemColUnit5 * iItemColUnit6 * iItemColUnit7 * dPrice);
                lblTotalPrice.Text = dTotalPrice.ToString("#,##0.00");
                /***********************************************************************/

                /**************************** Loop Sum Total Price *********************/
                double _SumPrice = 0.00;
                for (int i = 0; i < gvItem.Rows.Count; i++)
                {
                    double _rPrice = 0.00;

                    Label lblRowTotalPrice = (Label)gvItem.Rows[i].FindControl(ctrlName); //(Label)row.FindControl(ctrlName);
                    if (lblRowTotalPrice != null && !string.IsNullOrWhiteSpace(lblRowTotalPrice.Text.Trim()))
                    {
                        _rPrice = double.Parse(lblRowTotalPrice.Text.Trim());
                    }
                    _SumPrice += _rPrice;
                }

                double _vat7 = 0.00;
                _vat7 = (_SumPrice * (0.07));

                double _totalPrice = 0.00;
                _totalPrice = _SumPrice + _vat7;

                lblSummaryPrice.Text = _SumPrice.ToString("#,##0.00");
                //lblEstimatedTax.Text = _vat7.ToString("#,##0.00");
                //lblTotal.Text = _totalPrice.ToString("#,##0.00");

                /*********************************************************************/

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {


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
                MAS_PROJECTITEMBIDDING sData = new MAS_PROJECTITEMBIDDING();
                sData.ProjectNo = hdfProjectNo.Value.ToString(); //(string)ViewState["ProjectNo"];
                lItemProj = projItemBL.ListMasProjItemBiddingByPNo(sData);
                /*****************************************************************/
                DataRow row;

                string strComName = string.Empty;
                foreach (MAS_PROJECTITEMBIDDING item in lItemProj)
                {
                    row = dtColumnName.NewRow();

                    row["ProjectItemNo"] = item.ProjectItemNo;
                    row["ProjectNo"] = item.ProjectNo;

                    if (row.Table.Columns["ItemColumn1"] != null)
                    {
                        row["ItemColumn1"] = item.ItemColumn1;
                        //item.ItemColumn1.Equals(strComName) ? "" : item.ItemColumn1;
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

                /************ Set Empty Row ************/
                if (dtColumnName.Rows.Count == 0)
                {
                    dtColumnName = SetEmptyRows(dtColumnName);
                }
                /***************************************/

                gvItem.DataSource = dtColumnName;
                gvItem.DataBind();

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
            dtColName.Columns.Add("ProjectItemNo", typeof(string));
            dtColName.Columns.Add("ProjectNo", typeof(string));

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

        private DataTable SetEmptyRows(DataTable TableColName)
        {
            DataTable dtEmptyRow = TableColName;

            DataRow row;
            row = dtEmptyRow.NewRow();

            row["ProjectItemNo"] = string.Empty;
            row["ProjectNo"] = string.Empty;
            if (row.Table.Columns["ItemColumn1"] != null)
            {
                row["ItemColumn1"] = string.Empty;
            }
            if (row.Table.Columns["ItemColumn2"] != null)
            {
                row["ItemColumn2"] = string.Empty;
            }
            if (row.Table.Columns["ItemColumn3"] != null)
            {
                row["ItemColumn3"] = string.Empty;
            }
            if (row.Table.Columns["ItemColumn4"] != null)
            {
                row["ItemColumn4"] = string.Empty;
            }
            if (row.Table.Columns["ItemColumn5"] != null)
            {
                row["ItemColumn5"] = string.Empty;
            }
            if (row.Table.Columns["ItemColumn6"] != null)
            {
                row["ItemColumn6"] = string.Empty;
            }
            if (row.Table.Columns["ItemColumn7"] != null)
            {
                row["ItemColumn7"] = string.Empty;
            }
            if (row.Table.Columns["ItemColumn8"] != null)
            {
                row["ItemColumn8"] = string.Empty;
            }

            dtEmptyRow.Rows.Add(row);

            return dtEmptyRow;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtAttachFile = null;
                DataRow dr;

                string fileName = string.Empty;
                if (IsValid)
                {
                    fileName = fuAttachFile.FileName;

                    string strCompanyNo = "1";
                    string strProjectNo = hdfProjectNo.Value;
                    string strBiddingCode = hdfBiddingCode.Value;

                    string strPathFile = ConfigurationManager.GetConfiguration().AttachFilePath;
                    string strPathDate = DateTime.Now.ToString("ddMMyyyy") + "/";
                    string strBDUploadFolder = ConfigurationManager.GetConfiguration().BiddindUploadFolder;

                    string pathUpload = strPathFile + strPathDate + strBDUploadFolder + "/" + strBiddingCode + "/" + strCompanyNo + "/";
                    String ServerMapPath = Server.MapPath(pathUpload);

                    /**************** Upload File To Server ***********************/
                    if (!System.IO.Directory.Exists(Server.MapPath(pathUpload)))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(pathUpload));
                    }
                    fuAttachFile.PostedFile.SaveAs(ServerMapPath + fuAttachFile.FileName);
                    /**************************************************************/

                    if (ViewState["tbAttachFile"] == null)
                    {
                        dtAttachFile = new DataTable();
                        dtAttachFile.Clear();

                        dtAttachFile.Columns.Add("FileName", typeof(string));
                        dtAttachFile.Columns.Add("Description", typeof(string));
                        dtAttachFile.Columns.Add("AttachFilePath", typeof(string));

                        dr = dtAttachFile.NewRow();

                        dr["FileName"] = fileName;
                        dr["Description"] = txtFileDesc.Text.Trim();
                        dr["AttachFilePath"] = pathUpload + fuAttachFile.FileName;

                    }
                    else
                    {
                        dtAttachFile = (DataTable)ViewState["tbAttachFile"];
                        dr = dtAttachFile.NewRow();

                        dr["FileName"] = fileName;
                        dr["Description"] = txtFileDesc.Text.Trim();
                        dr["AttachFilePath"] = pathUpload + fuAttachFile.FileName;

                    }

                    dtAttachFile.Rows.Add(dr);
                }
                else
                {
                    //MessageUtil util = new MessageUtil();
                    //util.MsgBox("Please select data!", this.Page, this);                
                    return;
                }

                ViewState["tbAttachFile"] = dtAttachFile;

                ResetControl();
                BindGvAttachFile();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void ResetControl()
        {
            txtFileDesc.Text = string.Empty;
            fuAttachFile.Attributes.Clear();
        }

        protected void BindGvAttachFile()
        {
            DataTable dt = (DataTable)ViewState["tbAttachFile"];
            gvAttachFile.DataSource = dt;
            gvAttachFile.DataBind();
        }

        protected void gvAttachFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region #### Not Use ####
            //if (e.CommandName.Equals("Download"))
            //{

            //    //string pathFile = e.CommandArgument.ToString();
            //    //string fileName = Path.GetFileName(pathFile);

            //    //WebClient req = new WebClient();
            //    //HttpResponse response = HttpContext.Current.Response;
            //    //response.Clear();
            //    //response.ClearContent();
            //    //response.ClearHeaders();
            //    //response.Buffer = true;
            //    //response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            //    //byte[] data = req.DownloadData(Server.MapPath(pathFile));
            //    //response.BinaryWrite(data);
            //    //response.End();
            //}
            //else 
            #endregion

            if (e.CommandName.Equals("Delete"))
            {
                string pathFile = e.CommandArgument.ToString();

                //if (System.IO.Directory.Exists(Server.MapPath(pathFile)))
                //{
                //    System.IO.Directory.Delete(Server.MapPath(pathFile));
                //}

                if (!string.IsNullOrWhiteSpace(pathFile))
                {
                    if (System.IO.File.Exists(Server.MapPath(pathFile)))
                    {
                        System.IO.File.Delete(Server.MapPath(pathFile));
                    }
                }
            }
        }

        protected void ValidateTxt_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(txtFileDesc.Text))
            {
                ValidateTxt.Text = "*กรุณาระบุรายละเอียด";
                args.IsValid = false;
            }
            else if (!fuAttachFile.HasFile)
            {
                ValidateTxt.Text = "*กรุณาระบุไฟล์";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void gvAttachFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbAttachFile"];

            int index = Convert.ToInt32(e.RowIndex);
            dt.Rows[index].Delete();
            ViewState["tbAttachFile"] = dt;

            BindGvAttachFile();
        }


        private void UploadFile()
        {
            //string FileName = string.Empty;
            //string FileSize = string.Empty;
            //string extension = string.Empty;
            //string FilePath = string.Empty;

            //if (fuAttachFile.HasFile)
            //{
            //    extension = Path.GetExtension(fuAttachFile.FileName);
            //    FileName = fuAttachFile.PostedFile.FileName;
            //    FileSize = FileName.Length.ToString() + " Bytes";

            //    //strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;  
            //    fuAttachFile.PostedFile.SaveAs(Server.MapPath(@"~/Application/FileUploads/" + FileName.Trim()));
            //    FilePath = @"~/Application/FileUploads/" + FileName.Trim().ToString();
            //}           

            //WarningDialog
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            LinkButton lbtnDownload = (LinkButton)(sender);
            string pathFile = lbtnDownload.CommandArgument;

            string fileName = Path.GetFileName(pathFile);

            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            byte[] data = req.DownloadData(Server.MapPath(pathFile));
            response.BinaryWrite(data);
            response.End();

        }

        protected void gvAttachFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("lnkDownload") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);
            }
        }

      
    }
}
