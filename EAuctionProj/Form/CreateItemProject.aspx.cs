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
using EAuctionProj.Utility;

namespace EAuctionProj
{
    public partial class CreateItemProject : System.Web.UI.Page
    {
        private int _PageSize = 20;
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CreateItemProject));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                Session["tbColumnName"] = null;
                InitialControl();
            }
        }

        protected void InitialControl()
        {
            InitialDDLColName();

            ddlHeaderName.Items.Insert(0, new ListItem("== ระบุชื่อ ==", "0"));

            pnSubmitBtn.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    DataTable dt = (DataTable)Session["tbColumnName"];
                    if (dt != null && dt.Rows.Count > 0)
                    {                        
                        //=== Sort Column Grid ===== //
                        DataTable dtSort = null;
                        dt.DefaultView.Sort = "ColumnNo asc";
                        dtSort = dt.DefaultView.ToTable();
                        //===========================//

                        MAS_TEMPLATECOLNAME data = new MAS_TEMPLATECOLNAME();

                        data.TemplateName = txtProjectName.Text.Trim();
                        data.ColumnName1 = dtSort.Rows.Count > 0 ? dtSort.Rows[0]["ColumnName"].ToString().Trim() : "";
                        data.ColumnName2 = dtSort.Rows.Count > 1 ? dtSort.Rows[1]["ColumnName"].ToString().Trim() : "";
                        data.ColumnName3 = dtSort.Rows.Count > 2 ? dtSort.Rows[2]["ColumnName"].ToString().Trim() : "";
                        data.ColumnName4 = dtSort.Rows.Count > 3 ? dtSort.Rows[3]["ColumnName"].ToString().Trim() : "";
                        data.ColumnName5 = dtSort.Rows.Count > 4 ? dtSort.Rows[4]["ColumnName"].ToString().Trim() : "";
                        data.ColumnName6 = dtSort.Rows.Count > 5 ? dtSort.Rows[5]["ColumnName"].ToString().Trim() : "";
                        data.ColumnName7 = dtSort.Rows.Count > 6 ? dtSort.Rows[6]["ColumnName"].ToString().Trim() : "";
                        data.ColumnName8 = dtSort.Rows.Count > 7 ? dtSort.Rows[7]["ColumnName"].ToString().Trim() : "";

                        data.CreatedBy = "System Admin";
                        data.CreatedDate = DateTime.Now;
                        data.UpdatedBy = "System Admin";
                        data.UpdatedDate = DateTime.Now;

                        Mas_TemplateColName_Manage ins = new Mas_TemplateColName_Manage();
                        bool bIns = ins.InsertMasTemplateColName(data);
                        if (bIns)
                        {
                            MessageUtil util = new MessageUtil();
                            util.MsgBox("บันทึกข้อมูลสำเร็จ", this.Page, this);
                            Response.Redirect("~/Form/ItemProjectList.aspx");
                        }
                        else
                        {
                            MessageUtil util = new MessageUtil();
                            util.MsgBox("เกิดปัญหา ไม่สามารถบันทึกข้อมูลได้!", this.Page, this);
                        }
                    }
                    else
                    {
                        MessageUtil util = new MessageUtil();
                        util.MsgBox("กรุณาระบุข้อมูล ลำดับและชื่อคอลัมน์", this.Page, this);
                        return;
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                MessageUtil util = new MessageUtil();
                util.MsgBox("เกิดปัญหา ไม่สามารถบันทึกข้อมูลได้!", this.Page, this);               
            }      
        
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/ItemProjectList.aspx");
        }

        protected void gvTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                BindItemColumnData(1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BindItemColumnData(int pageNumber)
        {
            try
            {
                DataTable dtSetItemColumn = new DataTable();
                dtSetItemColumn = CreateGridViewItemData(gvTemplate);

                gvTemplate.DataSource = dtSetItemColumn;
                gvTemplate.PageSize = _PageSize;
                gvTemplate.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private DataTable CreateGridViewItemData(GridView gridView)
        {
            DataTable dtColumnName = new DataTable();
            dtColumnName = (DataTable)Session["tbColumnName"];

            //=== Sort Column Grid ===== //
            DataTable dtOut = null;
            dtColumnName.DefaultView.Sort = "ColumnNo asc";
            dtOut = dtColumnName.DefaultView.ToTable();
            //===========================//

            DataTable dt = new DataTable();
            dt.Clear();

            gridView.Columns.Clear();
            gridView.AllowSorting = true;
            gridView.AllowPaging = true;

            TemplateField tfield;
            foreach (DataRow dr in dtOut.Rows)
            {
                dt.Columns.Add(dr["ColumnName"].ToString());

                tfield = new TemplateField();
                tfield.HeaderText = dr["ColumnName"].ToString();
                gridView.Columns.Add(tfield);
            }

            return dt;
        }

        protected void InitialDDLColName()
        {
            List<MAS_COLUMNNAME> lData = new List<MAS_COLUMNNAME>();
            Mas_ColumnName_Manage manage = new Mas_ColumnName_Manage();

            lData = manage.ListColumName();

            ddlHeaderName.DataSource = lData;
            ddlHeaderName.DataBind();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dtColName = null;
            DataRow dr;

            if (IsValid)
            {
                if (Session["tbColumnName"] == null)
                {
                    dtColName = new DataTable();
                    dtColName.Clear();

                    dtColName.Columns.Add("ColumnNo", typeof(string));
                    dtColName.Columns.Add("ColumnName", typeof(string));

                    dr = dtColName.NewRow();
                    dr["ColumnNo"] = ddlColunnNo.SelectedValue.ToString();
                    dr["ColumnName"] = ddlHeaderName.SelectedItem.Text;
                }
                else
                {
                    dtColName = (DataTable)Session["tbColumnName"];

                    dr = dtColName.NewRow();
                    dr["ColumnNo"] = ddlColunnNo.SelectedValue.ToString();
                    dr["ColumnName"] = ddlHeaderName.SelectedItem.Text;
                }

                dtColName.Rows.Add(dr);


                Session["tbColumnName"] = dtColName;

                if (dtColName != null && dtColName.Rows.Count > 0)
                {
                    pnSubmitBtn.Visible = true;
                }

                ddlColunnNo.SelectedIndex = 0;
                ddlHeaderName.SelectedIndex = 0;

                BindGvColumnName();
            }
            else
            {
                //MessageUtil util = new MessageUtil();
                //util.MsgBox("Please select data!", this.Page, this);
                return;
            }
        }

        protected void BindGvColumnName()
        {
            DataTable dt = (DataTable)Session["tbColumnName"];

            gvAddColumn.DataSource = dt;
            gvAddColumn.DataBind();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            DataTable dtColumnName = new DataTable();
            dtColumnName = (DataTable)Session["tbColumnName"];

            lblPreview.Visible = true;
            if (dtColumnName != null && dtColumnName.Rows.Count > 0)
            {               
                BindItemColumnData(1);
            }
            else
            {
                gvTemplate.Visible = false;
            }
        }

        #region ### Gridview gvAddColumn Event ###

        protected void gvAddColumn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvAddColumn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void gvAddColumn_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
        }
        protected void gvAddColumn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tbColumnName"];
            int index = Convert.ToInt32(e.RowIndex);
            dt.Rows[index].Delete();

            Session["tbColumnName"] = dt;

            BindGvColumnName();
        }

        #endregion     

        protected void ValidateDll_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text.Trim()))
            {
                args.IsValid = false;
                ValidateDll.Text = "* กรุณาระบุชื่อรายการ";
                return;
            }

            if (ddlColunnNo.SelectedValue.Equals("0"))
            {
                args.IsValid = false;
                ValidateDll.Text = "* กรุณาระบุลำดับ";
                return;
            }

            if (ddlHeaderName.SelectedValue.Equals("0"))
            {
                args.IsValid = false;
                ValidateDll.Text = "* กรุณาระบุชื่อหัวข้อ";
                return;
            }
        }
    }
}
