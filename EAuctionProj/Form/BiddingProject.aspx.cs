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
using System.Web.Security;
using EAuctionProj.Utility;

namespace EAuctionProj
{
    public partial class BiddingProject : System.Web.UI.Page
    {  
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CreateItemProject));

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserLogin"] == null)
            //{
            //    Response.Redirect("~/Account/Login.aspx");
            //}  

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);
            if (!Page.IsPostBack)
            {
                //ViewState["PathFilePDF"] = null;
                if (Session["UserLogin"] != null)
                {
                    MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                    if (retUser.RolesNo > 0)
                    {
                        if (retUser.RolesNo == 1)
                        {
                            btnAccept.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = true;
                        }
                    }
                }

                string _UrlEncrypt = string.IsNullOrWhiteSpace(Request.QueryString["ProjectNo"]) ? "" : Request.QueryString["ProjectNo"];
                if (!string.IsNullOrEmpty(_UrlEncrypt))
                {
                    GlobalFunction fDEcrypt = new GlobalFunction();
                    //_UrlEncrypt = _UrlEncrypt.Replace(" ", "+");
                    string _UrlDecrypt = fDEcrypt.Decrypt(_UrlEncrypt);

                    hdfProjectNo.Value = _UrlDecrypt;

                    ViewState["PathFile"] = null;

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
        }

        private void GetProjectBidding()
        {
            try
            {
                Mas_ProjectBidding_Manage bl = new Mas_ProjectBidding_Manage();
                MAS_PROJECTBIDDING projData = new MAS_PROJECTBIDDING();
                projData.ProjectNo = Int64.Parse(hdfProjectNo.Value.ToString().Trim());
                projData = bl.GetMasProjItemBidding(projData);

                /**************** Retrieve Data ********************/
                lblAddress.Text = projData.CompanyAddress;
                lblBiddingCode.Text = projData.BiddingCode;
                lblContactName.Text = projData.ContactName;
                lblEmail.Text = projData.Email;
                lblEndDate.Text = projData.EndDate.ToString(@"dd\/MM\/yyyy");
                lblPhoneNo.Text = projData.PhoneNo;
                lblProjectName.Text = projData.ProjectName;
                lblStartDate.Text = projData.StartDate.ToString(@"dd\/MM\/yyyy");

                lblDepartment.Text = projData.DepartmentName.Trim();

                ViewState["PathFile"] = projData.AttachFilePath;
                SetDownloadAttachFile(projData.AttachFilePath);

                /**************** Check End Date *******************/
                int _totalDaysStart = (DateTime.Now - projData.StartDate).Days;
                if (_totalDaysStart < 0)
                {
                    btnAccept.Visible = false;
                    btnCancel.Visible = false;
                    btnBack.Visible = true;
                }
                else
                {
                    int _totalDays = (projData.EndDate - DateTime.Now).Days;
                    if (_totalDays < 0)
                    {
                        btnAccept.Visible = false;
                        btnCancel.Visible = false;
                        btnBack.Visible = true;
                    }
                }
             
                /***************************************************/
                BindItemProject(projData.TemplateNo.ToString());
                /***************************************************/
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }           
        }

        private void SetDownloadAttachFile(string PathFile)
        {
            string fileName = Path.GetFileName(PathFile);
            //lbtnAttachFile.Text = fileName;

            lbtnViewPDF.Text = fileName;
            lbtnViewPDF.CommandArgument = PathFile;

            //ViewState["PathFilePDF"] = PathFile;
        }
        
        private void DownloadAttachFile(string pathFile)
        {
            string _fileName = Path.GetFileName(pathFile);

            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            string filePath = pathFile;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            //response.AddHeader("Content-Disposition", "attachment;filename=Filename.extension");
            response.AddHeader("Content-Disposition", "attachment;filename=" + _fileName);
            byte[] data = req.DownloadData(Server.MapPath(filePath));
            response.BinaryWrite(data);
            response.End();
        }

        //protected void lbtnAttachFile_Click(object sender, EventArgs e)
        //{  
        //    if (ViewState["PathFile"] != null)
        //    {
        //        string _pathfile = (string)ViewState["PathFile"];
        //        DownloadAttachFile(_pathfile);
        //    }
        //}

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                /******************************************/
                //// Check Questionnaire ///////////////////
                /******************************************/

                MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                if (retUser.RolesNo > 0)
                {
                    hdfCompanyNo.Value = retUser.CompanyNo.ToString().Trim();
                    hdfUserName.Value = retUser.UserName;
                    hdfUserNo.Value = retUser.UsersNo.ToString().Trim();
                    hdfRoleNo.Value = retUser.RolesNo.ToString();
                }

                Inf_Questionnaire_Manage manage = new Inf_Questionnaire_Manage();
                INF_QUESTIONNAIRE retData = new INF_QUESTIONNAIRE();
                retData.ProjectNo = hdfProjectNo.Value.Trim();
                retData.CompanyNo = hdfCompanyNo.Value.Trim();
                retData = manage.GetQuestionaire(retData);
                if (retData != null && retData.QuestionNo > 0)
                {
                    Response.Redirect("~/Form/BiddingProcess.aspx?ProjectNo=" + hdfProjectNo.Value);
                }
                else
                {
                    //Response.Redirect("~/Form/Questionnaire.aspx?ProjectNo=" + hdfProjectNo.Value);
                    Response.Redirect("~/Form/UserRegister.aspx", true);
                }
                /*******************************************/         
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Default.aspx", true);
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
                        //lbl.Style.Add("Font-Size", "14px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "left");
                    }
                    else if (cellName.Equals("รายละเอียด"))
                    {
                        lbl = new Label();
                        lbl.ID = "lblDetail";
                        lbl.Text = (e.Row.DataItem as DataRowView).Row["ItemColumn2"].ToString();
                        //lbl.Style.Add("Font-Size", "14px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "left");
                    }
                    else if (cellName.Equals("แก้ไข"))
                    {
                        LinkButton lbtnDelete = new LinkButton();
                        lbtnDelete.ID = "lbtnDelete";
                        lbtnDelete.CommandName = "Delete";
                        lbtnDelete.Text = string.IsNullOrWhiteSpace((e.Row.DataItem as DataRowView).Row["ItemColumn1"].ToString()) ? "" : "ลบ";
                        //lbtnDelete.Style.Add("Font-Size", "14px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbtnDelete);
                        e.Row.Cells[i].Attributes.Add("align", "center");
                    }
                    else
                    {
                        _rowName = "ItemColumn" + (i + 1).ToString();
                        lbl = new Label();
                        lbl.ID = "lblItemColumn" + i.ToString();
                        lbl.Text = (e.Row.DataItem as DataRowView).Row[_rowName].ToString();
                        //lbl.Style.Add("Font-Size", "14px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "center");
                    }
                }
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
                /******************************************************/

                /*************** List Item Project ********************************/
                Mas_ProjectITemBidding_Manage projItemBL = new Mas_ProjectITemBidding_Manage();
                List<MAS_PROJECTITEMBIDDING> lItemProj = new List<MAS_PROJECTITEMBIDDING>();
                MAS_PROJECTITEMBIDDING sData = new MAS_PROJECTITEMBIDDING();
                sData.ProjectNo = hdfProjectNo.Value.ToString().Trim();
                lItemProj = projItemBL.ListMasProjItemBiddingByPNo(sData);
                /*****************************************************************/

                DataRow row;
                foreach (MAS_PROJECTITEMBIDDING item in lItemProj)
                {
                    row = dtColumnName.NewRow();

                    row["ProjectItemNo"] = item.ProjectItemNo;
                    row["ProjectNo"] = item.ProjectNo;

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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Default.aspx", true);
        }

        protected void lbtnViewPDF_Click(object sender, EventArgs e)
        {
            string _pathFile = lbtnViewPDF.CommandArgument.ToString().Trim(); 
            _pathFile = Page.ResolveClientUrl(_pathFile);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + _pathFile + "','_blank')", true);
        }
    }
}
