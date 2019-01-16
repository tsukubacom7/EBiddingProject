using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EAuctionProj
{
    public partial class BidingCompanyHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }  


            if (!Page.IsPostBack)
            {
                GetCompanyBidding();
            }
        }

        private void GetCompanyBidding()
        {

            DataTable dt = new DataTable("tbCompanyUser");

            dt.Columns.Add("BiddingNo", typeof(string));
            dt.Columns.Add("BiddingName", typeof(string));
            dt.Columns.Add("StartDate", typeof(string));
            dt.Columns.Add("EndDate", typeof(string));

            DataRow dr;
            for (int i = 1; i <= 3; i++)
            {
                dr = dt.NewRow();
                dr["BiddingNo"] = "BD00000" + i;
                dr["BiddingName"] = "งานจ้างรักษาความปลอดภัย ทดสอบ " + i.ToString();
                dr["StartDate"] = "20/06/2017";
                dr["EndDate"] = "25/06/2017";
               
                dt.Rows.Add(dr);
            }
         
            //Session["dtCompanyUser"] = dt;

            gvListCompany.DataSource = dt;
            gvListCompany.DataBind();


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


        protected void gvListCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvListCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvListCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvListCompany_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

    }
}
