using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using EAuctionProj.Utility;
using System.Threading;


namespace EAuctionProj
{
    public partial class CreateProject : System.Web.UI.Page
    {
        private string _projectNo = string.Empty;
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CreateProject));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }  


            if (!IsPostBack)
            {
                MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                if (retUser.RolesNo > 0)
                {                  
                    hdfUserName.Value = retUser.UserName;                 
                }

                ViewState["AddNewItem"] = null;
                Session["CreateResult"] = null;

                InitialControl();
                InitialDDLDepartment();
            }
            else
            {
                //pnListItem.Visible = true;
                GetItemTemplateNSetGridview(ddlItemTemplate.SelectedValue);

                //if (ddlItemTemplate.SelectedIndex != 0)
                //{
                //    pnListItem.Visible = true;
                //    GetItemTemplateNSetGridview(ddlItemTemplate.SelectedValue);
                //}
                //else
                //{
                //    pnListItem.Visible = false;
                //}
            }
        }

        private void InitialControl()
        {
            InitialDropDownList();
          
            pnListItem.Visible = false;

            GetCompanyAddress();

        }

        private void GetCompanyAddress()
        {
            string _companyNo = ConfigurationManager.GetConfiguration().CompanyNo;
            MAS_COMPANY cData = new MAS_COMPANY();
            Mas_Company_Manage manage = new Mas_Company_Manage();

            cData = manage.GetMasCompanyByID(_companyNo);

            txtContactAdd.Text = cData.CompanyAddressTH;
        }

        private void InitialDropDownList()
        {
            List<MAS_TEMPLATECOLNAME> lData = new List<MAS_TEMPLATECOLNAME>();
            Mas_TemplateColName_Manage manage = new Mas_TemplateColName_Manage();

            lData = manage.ListMasTemplateName();

            ddlItemTemplate.DataSource = lData;
            ddlItemTemplate.DataBind();

            ddlItemTemplate.Items.Insert(0, new ListItem("== เลือกรายการ Template ==", "0"));
        }

        private void InitialDDLDepartment()
        {
            string _CompanyCode = "2000"; //Default Company Code 2000 [GED]
            List<MAS_DEPARTMENT> lData = new List<MAS_DEPARTMENT>();
            Mas_Company_Manage manage = new Mas_Company_Manage();
            lData = manage.ListDepartmentByComCode(_CompanyCode);

            ddlDepartment.DataSource = lData;
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, new ListItem("== เลือกแผนก ==", "0"));
        }

        protected void ddlItemTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["AddNewItem"] = null;

            string _TemplateNo = ddlItemTemplate.SelectedValue.ToString().Trim();
            if (ddlItemTemplate.SelectedIndex != 0)
            {
                pnListItem.Visible = true;
            
                GetItemTemplateNSetGridview(_TemplateNo);              
            }
            else
            {
                pnListItem.Visible = false;
            }
        }

        private void GetItemTemplateNSetGridview(string TemplateNo)
        {
            MAS_TEMPLATECOLNAME value = new MAS_TEMPLATECOLNAME();
            try
            {
                Int64 pkItemCol = Int64.Parse(TemplateNo);
                Mas_TemplateColName_Manage manage = new Mas_TemplateColName_Manage();
                MAS_TEMPLATECOLNAME para = new MAS_TEMPLATECOLNAME();
                para.TemplateNo = pkItemCol;

                value = manage.GetMasTemplateColNameByKey(para);

                ViewState["ColNameByPk"] = value;

                if (value != null && !string.IsNullOrWhiteSpace(value.TemplateNo.ToString()))
                {
                    BindItemProjectData(value);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void BindItemProjectData(MAS_TEMPLATECOLNAME ItemColName)
        {
            try
            {
                DataTable dtColumnName = new DataTable();
                dtColumnName = CreateTableItemColumn(ItemColName);

                List<MAS_PROJECTITEMBIDDING> lItemRet = new List<MAS_PROJECTITEMBIDDING>();
                lItemRet = (List<MAS_PROJECTITEMBIDDING>)ViewState["AddNewItem"];
                if (lItemRet == null || lItemRet.Count == 0)
                {
                    lItemRet = GetListItemProject();
                    ViewState["AddNewItem"] = lItemRet;
                }

                DataRow row;
                foreach (MAS_PROJECTITEMBIDDING item in lItemRet)
                {
                    row = dtColumnName.NewRow();

                    row["ProjectItemNo"] = string.Empty;
                    row["ProjectNo"] = string.Empty;

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
               
                gvListItem.DataSource = dtColumnName;
                gvListItem.DataBind();
                gvListItem.ShowFooter = true;


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
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
      

        private List<MAS_PROJECTITEMBIDDING> GetListItemProject()
        {         
            List<MAS_PROJECTITEMBIDDING> lRet = new List<MAS_PROJECTITEMBIDDING>();
            try
            {
                Mas_ProjectITemBidding_Manage piManage = new Mas_ProjectITemBidding_Manage();
                MAS_PROJECTITEMBIDDING para = new MAS_PROJECTITEMBIDDING();
                para.ProjectNo = _projectNo;
                lRet = piManage.ListMasProjItemBiddingByPNo(para);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return lRet;
        }

        #region #### Gridview Event ####

        protected void gvListItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvListItem.EditIndex = e.NewEditIndex;

            //MAS_TEMPLATECOLNAME vsColName = (MAS_TEMPLATECOLNAME)ViewState["ColNameByPk"];
            //this.BindItemProjectData(vsColName);
        }

        protected void gvListItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvListItem.EditIndex = -1;

            //MAS_TEMPLATECOLNAME vsColName = (MAS_TEMPLATECOLNAME)ViewState["ColNameByPk"];
            //this.BindItemProjectData(vsColName);
        }

        protected void gvListItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvListItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvListItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                int totalCell = e.Row.Cells.Count;
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string cellName = "";
                    TemplateField fieldFooter = (TemplateField)((DataControlFieldCell)e.Row.Cells[i]).ContainingField;
                    cellName = fieldFooter.HeaderText;

                    if (cellName.Equals("บริษัท"))
                    {
                        DropDownList ddlCompany = new DropDownList();
                        ddlCompany.ID = "ddlCompany";
                        ddlCompany.Font.Size = 10;

                        ddlCompany.DataTextField = "CompanyNameTH";
                        ddlCompany.DataValueField = "CompanyNo";

                        Mas_Company_Manage manage = new Mas_Company_Manage();
                        List<MAS_COMPANY> lRet = new List<MAS_COMPANY>();
                        lRet = manage.ListMasCompany();

                        ddlCompany.DataSource = lRet;
                        ddlCompany.DataBind();
                        //ddlCompany.Style.Add("Font-Size", "11px");
                        ddlCompany.CssClass = "lblGridDetail";
                        ddlCompany.Width = 250;
                        e.Row.Cells[i].Controls.Add(ddlCompany);
                        e.Row.Cells[i].Attributes.Add("align", "left");

                    }
                    else if (cellName.Equals("รายละเอียด"))
                    {
                        TextBox txt = new TextBox();
                        txt.ID = "TxtDetail";
                        //txt.Style.Add("Font-Size", "11px");
                        //txt.Width = 150;
                        txt.CssClass = "TxtGridDetail";                       
                        e.Row.Cells[i].Controls.Add(txt);
                        e.Row.Cells[i].Attributes.Add("align", "left");
                    }
                    else if (cellName.Equals("แก้ไข"))
                    {
                        LinkButton lbtnAdd = new LinkButton();
                        lbtnAdd.ID = "lbtnAdd";
                        lbtnAdd.CommandName = "AddNew";
                        lbtnAdd.Text = "เพิ่ม";
                        //lbtnAdd.Style.Add("Font-Size", "11px");
                        lbtnAdd.CssClass = "lblGridLink";
                        e.Row.Cells[i].Controls.Add(lbtnAdd);
                        e.Row.Cells[i].Attributes.Add("align", "center");
                    }
                    else
                    {
                        if (!cellName.Contains("บาท"))
                        {
                            TextBox txt = new TextBox();
                            txt.ID = "TxtColumn" + i.ToString();
                            //txt.Font.Size = 10;
                            //txt.Width = 90;
                            //txt.Style.Add("Font-Size", "11px");
                            txt.CssClass = "TxtGridDetail";
                            e.Row.Cells[i].Controls.Add(txt);
                            e.Row.Cells[i].Attributes.Add("align", "center");
                        }                       
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
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
                    else if (cellName.Equals("แก้ไข"))
                    {
                        LinkButton lbtnDelete = new LinkButton();
                        lbtnDelete.ID = "lbtnDelete";
                        lbtnDelete.CommandName = "Delete";
                        lbtnDelete.Text = string.IsNullOrWhiteSpace((e.Row.DataItem as DataRowView).Row["ItemColumn1"].ToString()) ? "" : "ลบ";
                        //lbtnDelete.Style.Add("Font-Size", "11px");
                        lbtnDelete.CssClass = "lblGridLink";
                        e.Row.Cells[i].Controls.Add(lbtnDelete);
                        e.Row.Cells[i].Attributes.Add("align", "center");
                    }
                    else
                    {
                        _rowName = "ItemColumn" + (i + 1).ToString();
                        lbl = new Label();
                        lbl.ID = "lblItemColumn" + i.ToString();
                        lbl.Text = (e.Row.DataItem as DataRowView).Row[_rowName].ToString();
                        //lbl.Style.Add("Font-Size", "11px");
                        lbl.CssClass = "lblGridDetail";
                        e.Row.Cells[i].Controls.Add(lbl);
                        e.Row.Cells[i].Attributes.Add("align", "center");
                    }
                }
            }
        }       

        protected void gvListItem_RowCreated(object sender, GridViewRowEventArgs e)
        {
            /******************* Test Add Row Data **********************/
           
            /************************************************************/
        }    

        protected void gvListItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                List<MAS_PROJECTITEMBIDDING> lRet = new List<MAS_PROJECTITEMBIDDING>();
                lRet = (List<MAS_PROJECTITEMBIDDING>)ViewState["AddNewItem"];

                if (e.CommandName.Equals("AddNew"))
                {
                    MAS_PROJECTITEMBIDDING addItem = new MAS_PROJECTITEMBIDDING();
                    DropDownList ddlCompany = (DropDownList)gvListItem.FooterRow.FindControl("ddlCompany");
                    if (ddlCompany != null)
                    {
                        addItem.ItemColumn1 = ddlCompany.SelectedItem.Text;
                    }

                    TextBox TxtDetail = (TextBox)gvListItem.FooterRow.FindControl("TxtDetail");
                    if (TxtDetail != null)
                    {
                        addItem.ItemColumn2 = TxtDetail.Text;
                    }

                    TextBox TxtColumn2 = (TextBox)gvListItem.FooterRow.FindControl("TxtColumn2");
                    if (TxtColumn2 != null)
                    {
                        addItem.ItemColumn3 = TxtColumn2.Text;
                    }

                    TextBox TxtColumn3 = (TextBox)gvListItem.FooterRow.FindControl("TxtColumn3");
                    if (TxtColumn3 != null)
                    {
                        addItem.ItemColumn4 = TxtColumn3.Text;
                    }

                    TextBox TxtColumn4 = (TextBox)gvListItem.FooterRow.FindControl("TxtColumn4");
                    if (TxtColumn4 != null)
                    {
                        addItem.ItemColumn5 = TxtColumn4.Text;
                    }

                    TextBox TxtColumn5 = (TextBox)gvListItem.FooterRow.FindControl("TxtColumn5");
                    if (TxtColumn5 != null)
                    {
                        addItem.ItemColumn6 = TxtColumn5.Text;
                    }

                    TextBox TxtColumn6 = (TextBox)gvListItem.FooterRow.FindControl("TxtColumn6");
                    if (TxtColumn6 != null)
                    {
                        addItem.ItemColumn7 = TxtColumn6.Text;
                    }

                    TextBox TxtColumn7 = (TextBox)gvListItem.FooterRow.FindControl("TxtColumn7");
                    if (TxtColumn7 != null)
                    {
                        addItem.ItemColumn8 = TxtColumn7.Text;
                    }

                    lRet.Add(addItem);

                    ViewState["AddNewItem"] = lRet;
                   
                }
                else if (e.CommandName.Equals("Delete"))
                {

                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;

                    lRet.RemoveAt(RowIndex);
                    ViewState["AddNewItem"] = lRet;
                }

                MAS_TEMPLATECOLNAME vsColName = (MAS_TEMPLATECOLNAME)ViewState["ColNameByPk"];
                BindItemProjectData(vsColName);
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
            gvListItem.Columns.Clear();
            gvListItem.AllowSorting = false;
            gvListItem.AllowPaging = false;
            gvListItem.ShowFooter = true;
            /******************************************************/

            TemplateField tfield;
            if (!string.IsNullOrWhiteSpace(colName.ColumnName1))
            {
                dtColName.Columns.Add("ItemColumn1", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName1;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName2))
            {
                dtColName.Columns.Add("ItemColumn2", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName2;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName3))
            {
                dtColName.Columns.Add("ItemColumn3", typeof(string));


                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName3;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName4))
            {
                dtColName.Columns.Add("ItemColumn4", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName4;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName5))
            {
                dtColName.Columns.Add("ItemColumn5", typeof(string));
                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName5;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName6))
            {
                dtColName.Columns.Add("ItemColumn6", typeof(string));


                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName6;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName7))
            {
                dtColName.Columns.Add("ItemColumn7", typeof(string));


                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName7;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }
            if (!string.IsNullOrWhiteSpace(colName.ColumnName8))
            {
                dtColName.Columns.Add("ItemColumn8", typeof(string));

                tfield = new TemplateField();
                tfield.HeaderText = colName.ColumnName8;
                tfield.ItemStyle.HorizontalAlign.Equals("Center");
                tfield.ItemStyle.VerticalAlign.Equals("Middle");

                gvListItem.Columns.Add(tfield);
            }

            tfield = new TemplateField();
            tfield.HeaderText = "แก้ไข";
            gvListItem.Columns.Add(tfield);

            return dtColName;
        }

        #endregion

        #region ### Button Event ####

        protected void btnSave_Click(object sender, EventArgs e)
        {
            logger.Info("btnSave_Click-[Start]");
            try
            {
                if (IsValid)
                {
                    /****************** Insert to tb MAS_PROJECTBIDDING ************************/
                    Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();
                    MAS_PROJECTBIDDING insData = new MAS_PROJECTBIDDING();
                    insData.ProjectName = txtProjectName.Text.Trim();
                    insData.TemplateNo = Int64.Parse(ddlItemTemplate.SelectedValue.ToString().Trim());
                    insData.CompanyAddress = txtContactAdd.Text;

                    //insData.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                    //insData.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());

                    string format = ConfigurationManager.GetConfiguration().DateFormat;
                    IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

                    string _startDate = txtStartDate.Text.Trim();
                    insData.StartDate = DateTime.ParseExact(_startDate, format, culture);

                    string _endDate = txtEndDate.Text.Trim();
                    insData.EndDate = DateTime.ParseExact(_endDate, format, culture);

                    insData.ContactName = txtContactPers.Text.Trim();
                    insData.Email = txtContactEmail.Text.Trim();
                    insData.PhoneNo = txtContactPhone.Text.Trim();

                    insData.CreatedBy = hdfUserName.Value.Trim();
                    insData.CreatedDate = DateTime.Now;
                    insData.UpdatedBy = hdfUserName.Value.Trim();
                    insData.UpdatedDate = DateTime.Now;

                    //*********** AddBy Preecha J. 2018-10-08***********//
                    insData.DepartmentName = ddlDepartment.SelectedValue.Trim();
                    //**************************************************//

                    List<MAS_PROJECTITEMBIDDING> lItemProj = new List<MAS_PROJECTITEMBIDDING>();
                    lItemProj = (List<MAS_PROJECTITEMBIDDING>)ViewState["AddNewItem"];

                    logger.Info("InsertMasProjtBidding-[Start]");

                    string strBiddNo = manage.InsertMasProjtBidding(insData, lItemProj);

                    logger.Info("InsertMasProjtBidding-[End]");

                    if (!string.IsNullOrWhiteSpace(strBiddNo))
                    {
                        string strPathFile = ConfigurationManager.GetConfiguration().AttachFilePath;
                        string strPathDate = DateTime.Now.ToString("ddMMyyyy") + "/";
                        string bdCode = GenBiddingCode(strBiddNo);

                        string pathUpload = strPathFile + strPathDate + bdCode + "/";
                        String ServerMapPath = Server.MapPath(pathUpload);

                        /******************* Update BiddingCode ****************/
                        MAS_PROJECTBIDDING updData = new MAS_PROJECTBIDDING();
                        updData.ProjectNo = Convert.ToInt64(strBiddNo);
                        updData.BiddingCode = bdCode;
                        updData.AttachFilePath = pathUpload + fuTOR.FileName;
                        /*******************************************************/

                        /***************** Upload File  ************************/
                        if (manage.UpdateBiddingCode(updData))
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath(pathUpload)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath(pathUpload));
                            }

                            fuTOR.PostedFile.SaveAs(ServerMapPath + fuTOR.FileName);

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            // "alert('สร้างรายการเรียบร้อย..');window.location ='Default.aspx';", true);

                            lblMsgResult.Text = "สร้างรายการ จัดซื้อ/จัดจ้าง สำเร็จ";
                            Session["CreateResult"] = "y";
                        }
                        else
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                            //    "alertMessage", "alert('ไม่สามารถบันทึกข้อมูลได้! กรุณาติดต่อผู้ดูแลระบบ')", true);
                            lblMsgResult.Text = "ไม่สามารถสร้างรายการได้! กรุณาติดต่อผู้ดูแลระบบ";
                            Session["CreateResult"] = "n";
                        }
                    }
                    /***************************************************************/

                    lbtnPopup_ModalPopupExtender.Show();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ไม่สามารถบันทึกข้อมูลได้! กรุณาติดต่อผู้ดูแลระบบ')", true);  
                lblMsgResult.Text = "ไม่สามารถบันทึกข้อมูลได้! กรุณาติดต่อผู้ดูแลระบบ";
                Session["CreateResult"] = "n";

                lbtnPopup_ModalPopupExtender.Show();
            }

            logger.Info("btnSave_Click-[End]");          
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Default.aspx");
        }

        #endregion

        #region #### UploadFile (Not Use) ####
        //private void UploadFile(string BiddingCode)
        //{
        //    try
        //    {
        //        //Boolean fileOK = false;
        //        //~/BiddingProj/AttachFile/20170627/BD17000001/testfile.pdf
        //        string strDate = DateTime.Now.ToString("ddMMyyyy");

        //        /***************** Upload File  ********************************************/
        //        if (!System.IO.Directory.Exists(Server.MapPath(@"~/AttachFile/" + projectName + "/")))
        //        {
        //            System.IO.Directory.CreateDirectory(Server.MapPath(@"~/AttachFile/" + projectName + "/"));
        //        }
        //        String ServerMapPath = Server.MapPath("~/AttachFile/" + projectName + "/");
        //        fuTOR.PostedFile.SaveAs(ServerMapPath + fuTOR.FileName);

        //        //String fileExtension =
        //        //    System.IO.Path.GetExtension(fuTOR.FileName).ToLower();
        //        //String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".pdf" };
        //        //for (int i = 0; i < allowedExtensions.Length; i++)
        //        //{
        //        //    if (fileExtension == allowedExtensions[i])
        //        //    {
        //        //        fileOK = true;
        //        //    }
        //        //}
        //        /**************************************************************************/

        //    }
        //    catch (Exception ex)
        //    {

        //        logger.Error(ex.Message);
        //        logger.Error(ex.StackTrace);
        //    }
        //}
        #endregion

        private string GenBiddingCode(string ProjRunNo)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string strBDCode = "";

            /************************** BD17000001 ****************************/
            string strPrefix = "BD" + DateTime.Now.Year.ToString().Substring(2, 2);
            string strCodeRunNo = "00000" + ProjRunNo;
            GlobalFunction func = new GlobalFunction();
            string runnNo = func.RightFunction(strCodeRunNo, 6);

            strBDCode = strPrefix + runnNo;
            /****************************************************************/

            return strBDCode;
        }
        protected void ValidateProj_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (ddlDepartment.SelectedIndex == 0)
                {
                    ValidateProj.ErrorMessage = "กรุณาระบุแผนก";
                    ddlDepartment.Focus();
                    args.IsValid = false;
                    return;
                }

                if (!fuTOR.HasFile)
                {
                    ValidateProj.ErrorMessage = "กรุณาระบุไฟล์แนบ";
                    fuTOR.Focus();
                    args.IsValid = false;
                    return;
                }

                if (ddlItemTemplate.SelectedIndex == 0)
                {
                    ValidateProj.ErrorMessage = "กรุณาเลือก Template รายการ";
                    ddlItemTemplate.Focus();
                    args.IsValid = false;
                    return;
                }               
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string sResult = (string)Session["CreateResult"];
                if (sResult.Trim().Equals("y"))
                {
                    Response.Redirect("~/Form/Default.aspx");
                }
                else
                {
                    lbtnPopup_ModalPopupExtender.Hide();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }           
        }

        protected void ValidateDll_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (ddlDepartment.SelectedIndex == 0)
                {
                    ValidateProj.ErrorMessage = "กรุณาระบุแผนก";
                    ddlDepartment.Focus();
                    args.IsValid = false;
                    return;
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
