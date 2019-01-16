using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using EAuctionProj.BL;
using EAuctionProj.DAL;
using EAuctionProj.Utility;
using System.Collections;
using EAuctionProj.ReportDAO;
using System.Text;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class CompanyUser : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CompanyUser));

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

            if (!Page.IsPostBack)
            {
                Session["CompanyUserRPT"] = null;

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

                ViewState["SortGridview"] = "CompanyNo ";

                BindCompanyUser();
            }
        }

        protected void gvListCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvListCompany.Rows[gvListCompany.SelectedIndex].BackColor = Color.Red;
        }

        protected void gvListCompany_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvListCompany.Rows[e.NewSelectedIndex];          
        }

        protected void gvListCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);              
                string _UserName = gvListCompany.DataKeys[rowIndex].Values[0].ToString().Trim();

                GlobalFunction fDecrypt = new GlobalFunction();
                string encPara = fDecrypt.Encrypt(_UserName);

                Response.Redirect("~/Form/CompanyUserDetail.aspx?UserName=" + encPara);
            }       
        }

        protected void gvListCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {           
            gvListCompany.PageIndex = e.NewPageIndex;
            BindCompanyUser();           
        }

        private void BindCompanyUser()
        {
            string _companyName = "";
            string _taxID = "";
            string _userName = "";
            string _projectName = "";

            if (ddlSearch.SelectedIndex != 0)
            {
                switch (ddlSearch.SelectedValue)
                {
                    case "1":
                        _companyName = txtSearch.Text.Trim();
                        break;
                    case "2":
                        _taxID = txtSearch.Text.Trim();
                        break;
                    case "3":
                        _userName = txtSearch.Text.Trim();
                        break;
                    case "4":
                        _projectName = txtSearch.Text.Trim();
                        break;
                }
            }

            Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
            List<MAS_COMPANYUSER_DTO> lRet = new List<MAS_COMPANYUSER_DTO>();
            lRet = manage.ListCompanyUser(_companyName, _taxID, _userName, _projectName);
            foreach (var item in lRet)
            {
                /******************** Decrypt Password *******************/
                GlobalFunction func = new GlobalFunction();
                string _password = item.Password;
                string _decryptPass = func.Decrypt(_password);
                item.Password = _decryptPass;
                /********************************************************/
            }

            /********************** For Sort Gridview ************************/
            string _sortBy = (string)ViewState["SortGridview"];
            switch (_sortBy.Trim())
            {
                case "CompanyNo":
                    lRet = lRet.OrderBy(x => x.CompanyNo).ToList();
                    break;
                case "CompanyNo DESC":
                    lRet = lRet.OrderByDescending(x => x.CompanyNo).ToList();
                    break;
                case "CompanyName":
                    lRet = lRet.OrderBy(x => x.CompanyName).ToList();
                    break;
                case "CompanyName DESC":
                    lRet = lRet.OrderByDescending(x => x.CompanyName).ToList();
                    break;
                case "TaxID":
                    lRet = lRet.OrderBy(x => x.TaxID).ToList();
                    break;
                case "TaxID DESC":
                    lRet = lRet.OrderByDescending(x => x.TaxID).ToList();
                    break;
                case "UserName":
                    lRet = lRet.OrderBy(x => x.UserName).ToList();
                    break;
                case "UserName DESC":
                    lRet = lRet.OrderByDescending(x => x.UserName).ToList();
                    break;
                case "ProjectName":
                    lRet = lRet.OrderBy(x => x.ProjectName).ToList();
                    break;
                case "ProjectName DESC":
                    lRet = lRet.OrderByDescending(x => x.ProjectName).ToList();
                    break;

                case "Status":
                    lRet = lRet.OrderBy(x => x.Status).ToList();
                    break;
                case "Status DESC":
                    lRet = lRet.OrderByDescending(x => x.Status).ToList();
                    break;
            }
            /*****************************************************************/

            gvListCompany.DataSource = lRet;
            gvListCompany.DataBind();

            Session["CompanyUserRPT"] = lRet;
        }


        private string GetColumnSirting(string SortBy)
        {
            string retSortColumn = string.Empty;           

            if (SortBy.Equals("CompanyName"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("CompanyName"))
                    {
                        retSortColumn = "CompanyName";
                    }
                    else
                    {
                        retSortColumn = "CompanyName DESC";
                    }
                }
                else
                {
                    retSortColumn = "CompanyName";
                }
            }

            if (SortBy.Equals("TaxID"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("TaxID"))
                    {
                        retSortColumn = "TaxID";
                    }
                    else
                    {
                        retSortColumn = "TaxID DESC";
                    }
                }
                else
                {
                    retSortColumn = "TaxID";
                }
            }

            if (SortBy.Equals("UserName"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("UserName"))
                    {
                        retSortColumn = "UserName";
                    }
                    else
                    {
                        retSortColumn = "UserName DESC";
                    }
                }
                else
                {
                    retSortColumn = "UserName";
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

            if (SortBy.Equals("Status"))
            {
                if (ViewState["SortGridview"] != null)
                {
                    if (!ViewState["SortGridview"].ToString().Equals("Status"))
                    {
                        retSortColumn = "Status";
                    }
                    else
                    {
                        retSortColumn = "Status DESC";
                    }
                }
                else
                {
                    retSortColumn = "Status";
                }
            }

            return retSortColumn;
        }

        protected void gvListCompany_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortGridview"] = GetColumnSirting(e.SortExpression);
            BindCompanyUser();     
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindCompanyUser();  
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;

            BindCompanyUser();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyUserRPT"] != null)
                {
                    string header = string.Empty;
                    for (int i = 0; i <= this.gvListCompany.Columns.Count - 1; i++)
                    {
                        if ((this.gvListCompany.Columns[i]) is System.Web.UI.WebControls.BoundField)
                        {
                            header += this.gvListCompany.Columns[i].HeaderText + ",";
                        }
                    }

                    string _header = header.Substring(0, header.Length - 1);

                    List<MAS_COMPANYUSER_DTO> lItemRet = (List<MAS_COMPANYUSER_DTO>)Session["CompanyUserRPT"];
                    ArrayList l = new ArrayList();
                    foreach (MAS_COMPANYUSER_DTO item in lItemRet)
                    {
                        CompanyUserReport expData = new CompanyUserReport();
                        expData.CompanyName = item.CompanyName;
                        expData.TaxID = "'" + item.TaxID;
                        expData.UserName = item.UserName;
                        expData.Password = item.Password;

                        l.Add(expData);
                    }

                    GlobalFunction func = new GlobalFunction();
                    string data = func.WriteReport(_header, l);

                    //SEND RESPONSE
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "Attachment;Filename=company_user_report.csv");
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
