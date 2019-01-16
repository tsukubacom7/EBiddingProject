using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EAuctionProj.BL;
using EAuctionProj.DAL;
using EAuctionProj.Utility;

namespace EAuctionProj
{
    public partial class ItemProjectList : System.Web.UI.Page
    { 
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ItemProjectList));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialControl();
            }
        }
        private void InitialControl()
        {
            List<MAS_TEMPLATECOLNAME> lData = GetTemplateProject();

            gvListTemplate.DataSource = lData;
            gvListTemplate.DataBind();
        }

        private List<MAS_TEMPLATECOLNAME> GetTemplateProject()
        {
            List<MAS_TEMPLATECOLNAME> ret = new List<MAS_TEMPLATECOLNAME>();
            try
            {
                Mas_TemplateColName_Manage manage = new Mas_TemplateColName_Manage();
                ret = manage.ListMasTemplateColName();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return ret;
        }

        protected void gvListTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }

        protected void btnAddTemplate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/CreateItemProject.aspx", false);
        }

        protected void gvListTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {              
            string pk = gvListTemplate.DataKeys[e.RowIndex].Value.ToString().Trim();
            MAS_TEMPLATECOLNAME data = new MAS_TEMPLATECOLNAME();
            data.TemplateNo = Int64.Parse(pk);

            Mas_TemplateColName_Manage manage = new Mas_TemplateColName_Manage();
            bool bDel = manage.DeleteMasTemplateColName(data);
            if (bDel)
            {
                InitialControl();
            }
            else
            {
                MessageUtil util = new MessageUtil();
                util.MsgBox("ไม่สามารถลบข้อมูลได้!", this.Page, this);
            }
        }

        protected void gvListTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                int lastCellIndex = e.Row.Cells.Count - 1;

                foreach (LinkButton lbnt in e.Row.Cells[lastCellIndex].Controls.OfType<LinkButton>())
                {
                    if (lbnt.CommandName == "Delete")
                    {
                        lbnt.Attributes["onclick"] = "if(!confirm('คุณต้องการจะลบรายการ: " + item + "?')){ return false; };";
                    }
                }              
            }
        }
    }
}
