using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using System.Threading;
using EAuctionProj.Utility;
using System.Text;
using EAuctionProj.ReportDAO;
using System.Collections;
using System.IO;
using System.Net;

namespace EAuctionProj
{
    public partial class _Default : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(_Default));

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (!Page.IsPostBack)
            {
                Session["ProjectBiddingRPT"] = null;

                InitialControl();
            }
        }

        private void InitialControl()
        {
            if (Session["UserLogin"] != null)
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
            }

            BindGridview();
        }
        private void BindGridview()
        {
            try
            {
                List<MAS_PROJECTBIDDING_DTO> lItemRet = new List<MAS_PROJECTBIDDING_DTO>();
                MAS_PROJECTBIDDING data = new MAS_PROJECTBIDDING();
                Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();

                string BiddingCode = "";
                string ProjectName = "";
                string BiddingMonth = "";
                string Department = "";

                if (ddlSearch.SelectedIndex != 0)
                {
                    switch (ddlSearch.SelectedValue)
                    {
                        case "1":
                            BiddingCode = txtSearch.Text.Trim();
                            break;
                        case "2":
                            ProjectName = txtSearch.Text.Trim();
                            break;
                        case "3":
                            Department = txtSearch.Text.Trim();
                            break;
                    }
                }

                if (ddlSelMonth.SelectedIndex != 0)
                {
                    BiddingMonth = ddlSelMonth.SelectedValue;
                }

                lItemRet = manage.ListProjectDefault(BiddingCode, ProjectName, BiddingMonth, Department);

                gvListProject.DataSource = lItemRet;
                gvListProject.DataBind();

                Session["ProjectBiddingRPT"] = lItemRet;
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

        public string CheckProjectStatus(object dtEndDate, object dtStartDate)
        {
            string strRet = string.Empty;
            if (dtStartDate != null && dtEndDate != null)
            {
                DateTime _dtStartDate = Convert.ToDateTime(dtStartDate);

                //string strSD = dtStartDate.ToString();
                //_dtStartDate = DateTime.Parse(strSD);

                int _totalDaysStart = (DateTime.Now - _dtStartDate).Days;
                if (_totalDaysStart < 0)
                {
                    strRet = "ยังไม่เริ่มประมูล";
                }
                else
                {
                    DateTime _dtEndDate = Convert.ToDateTime(dtEndDate);

                    //string strED = dtStartDate.ToString();
                    //_dtEndDate = DateTime.Parse(strED);   

                    int _totalDaysEnd = (_dtEndDate - DateTime.Now).Days;
                    if (_totalDaysEnd < 0)
                    {
                        strRet = "สิ้นสุดการประมูล";
                    }
                    else
                    {
                        strRet = "อยู่ระหว่างการประมูล";
                    }
                }
            }
            else
            {
                strRet = "-";
            }
            return strRet;
        }

        //public string CheckProjectStatus(object dt)
        //{
        //    string strDate = string.Empty;
        //    if (dt != null)
        //    {
        //        DateTime _dt = new DateTime();
        //        _dt = DateTime.Parse(dt.ToString());
        //        string strDt = _dt.Date.ToString("ddMMyyyy");
        //        strDate = strDt.Substring(0, 2) + "/" + strDt.Substring(2, 2) + "/" + strDt.Substring(4, 4);
        //    }

        //    return strDate;
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridview();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlSearch.SelectedIndex = 0;
            ddlSelMonth.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            BindGridview();
        }

        protected void gvListProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListProject.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        protected void gvListProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfProjectNo = e.Row.FindControl("hdfProjectNo") as HiddenField;
                if (hdfProjectNo != null)
                {
                    GlobalFunction fEncrypt = new GlobalFunction();
                    HyperLink hplClick = e.Row.FindControl("hplClick") as HyperLink;
                    hplClick.NavigateUrl = "~/Form/BiddingProject.aspx?ProjectNo=" + fEncrypt.Encrypt(hdfProjectNo.Value);
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            #region #### Old Code Not Use ####
            //try
            //{
            //    if (Session["ProjectBiddingRPT"] != null)
            //    {
            //        string header = string.Empty;
            //        for (int i = 0; i <= this.gvListProject.Columns.Count - 1; i++)
            //        {
            //            if ((this.gvListProject.Columns[i]) is System.Web.UI.WebControls.BoundField)
            //            {
            //                header += this.gvListProject.Columns[i].HeaderText + ",";
            //            }
            //        }

            //        if (!string.IsNullOrWhiteSpace(header))
            //        {
            //            header += "สถานะ,";
            //        }


            //        string _header = header.Substring(0, header.Length - 1);

            //        List<MAS_PROJECTBIDDING_DTO> lItemRet = (List<MAS_PROJECTBIDDING_DTO>)Session["ProjectBiddingRPT"];

            //        ArrayList l = new ArrayList();
            //        foreach (MAS_PROJECTBIDDING_DTO item in lItemRet)
            //        {
            //            ProjectBiddingReport expData = new ProjectBiddingReport();
            //            expData.BiddingCode = item.BiddingCode;
            //            expData.ProjectName = item.ProjectName;
            //            expData.StartDate = item.StartDate.ToString(@"dd\/MM\/yyyy");
            //            expData.EndDate = item.EndDate.ToString(@"dd\/MM\/yyyy");
            //            expData.Status = CheckProjectStatus(item.EndDate, item.StartDate);

            //            l.Add(expData);
            //        }

            //        GlobalFunction func = new GlobalFunction();
            //        string data = func.WriteReport(_header, l);

            //        //SEND RESPONSE
            //        Response.ClearContent();
            //        Response.AddHeader("Content-Disposition", "Attachment;Filename=project_bidding_report.csv");
            //        Response.ContentType = "application/text";
            //        Response.ContentEncoding = Encoding.UTF8;

            //        Response.Output.Write(data);
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex.Message);
            //    logger.Error(ex.StackTrace);
            //}  

            //ExportGridToCSV();
            //ExportGridToExcel();
            #endregion

            DataTable dtGrid = GetDataTable(gvListProject);
            if (dtGrid != null)
            {
                string _str = ToCSV(dtGrid);
                if (dtGrid.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;

                    Response.AddHeader("content-disposition", "attachment; filename=project_bidding_report.xlsx");
                    Response.ContentType = "text/ms-excel";

                    Response.Write(_str);
                    Response.End();
                }
            }
        }

        public static string ToCSV(DataTable dataTable)
        {
            //create the stringbuilder that would hold the data
            StringBuilder sb = new StringBuilder();
            //check if there are columns in the datatable
            if (dataTable.Columns.Count != 0)
            {
                //loop thru each of the columns for headers
                foreach (DataColumn column in dataTable.Columns)
                {
                    //append the column name followed by the separator
                    sb.Append(column.ColumnName + ",");
                }
                //append a carriage return
                sb.Append("\r\n");

                //loop thru each row of the datatable
                foreach (DataRow row in dataTable.Rows)
                {
                    //loop thru each column in the datatable
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        //get the value for the row on the specified column
                        // and append the separator
                        sb.Append("\"" + row[column].ToString() + "\"" + ",");
                    }
                    //append a carriage return
                    sb.Append("\r\n");
                }
            }
            return (sb.ToString());
        }

        private void ExportGridToCSV()
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "Attachment;Filename=project_bidding_report.csv");
                Response.ContentType = "application/text";
                Response.Charset = "windows-874";

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

                Response.ContentEncoding = Encoding.Unicode;
                Response.Output.Write(columnbind.ToString());

                //Response.Write(columnbind.ToString());
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

        private DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
