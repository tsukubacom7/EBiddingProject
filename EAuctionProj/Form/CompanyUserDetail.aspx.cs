using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAuctionProj.BL;
using EAuctionProj.DAL;
using EAuctionProj.Utility;
using System.Net;
using System.IO;
using System.Collections;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class CompanyUserDetail : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CompanyUserDetail));
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
                MAS_COMPANYUSER_DTO sessionUserDet = (MAS_COMPANYUSER_DTO)Session["UserLogin"];               
                hdfRoleNo.Value = sessionUserDet.RolesNo.ToString().Trim();
                hdfCompanyNo.Value = sessionUserDet.CompanyNo.ToString().Trim();

                if (!string.IsNullOrWhiteSpace(Request.QueryString["UserName"]))
                {
                    string _strPara = Request.QueryString["UserName"];
                    GlobalFunction fDecrypt = new GlobalFunction();
                    string decPara = fDecrypt.Decrypt(_strPara);

                    hdfUserName.Value = decPara;
                    hdfCompanyNo.Value = null;
                }
                else
                {
                    hdfUserName.Value = sessionUserDet.UserName.ToString().Trim();
                    if (sessionUserDet.RolesNo == 2)
                    {
                        linkQuestionaire.Visible = false;
                        btnCancel.Visible = false;

                        linkChangePass.Attributes["href"] = "~/Account/ChangePassword.aspx";
                        linkChangeProfile.Attributes["href"] = "~/Form/EditUserRegister.aspx";
                    }
                    else if (sessionUserDet.RolesNo ==1)
                    {
                        linkQuestionaire.Visible = true;
                        btnCancel.Visible = true;                       
                    }
                }

                GetUserAccountDetail();

                GetCompanyUserDetail();
            
                GetCompanyUserAttachFile();
            }
        }
        private void GetUserAccountDetail()
        {
            Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
            MAS_COMPANYUSER_DTO ret = new MAS_COMPANYUSER_DTO();
            ret = manage.GetCompanyUserDetail(hdfUserName.Value.ToString());

            if (ret != null && ret.CompanyNo > 0)
            {
                hdfUserNo.Value = ret.UsersNo.ToString().Trim();
                hdfCompanyNo.Value = ret.CompanyNo.ToString().Trim();

               lblUserName.Text = ret.UserName;
                lblProjectName.Text = ret.ProjectName;
                lblStatus.Text = ret.Status;

                if (hdfRoleNo.Value.Trim().Equals("1"))
                {
                    if (!string.IsNullOrWhiteSpace(ret.Status))
                    {
                        if (ret.Status.Trim().Equals("Not Verify"))
                        {
                            btnVerify.Visible = true;
                            btnApprove.Visible = false;
                        }

                        if (ret.Status.Trim().Equals("Verified"))
                        {
                            btnVerify.Visible = false;
                            btnApprove.Visible = true;
                        }
                    }
                }

                /******************** Decrypt Password *******************/
                GlobalFunction func = new GlobalFunction();
                string _password = ret.Password;
                string _decryptPass = func.Decrypt(_password);
                /********************************************************/
                lblPassword.Text = _decryptPass;

                if (string.IsNullOrWhiteSpace(hdfRoleNo.Value) || hdfRoleNo.Value.Trim().Equals("1"))
                {
                    linkChangePass.Attributes["href"] = "~/Account/ChangePassword.aspx?UsersNo=" + ret.UsersNo.ToString().Trim() + "&UserName=" + ret.UserName.Trim();
                    linkChangeProfile.Attributes["href"] = "~/Form/EditUserRegister.aspx?CompanyNo=" + ret.CompanyNo.ToString().Trim();
                    linkQuestionaire.Attributes["href"] = "~/Form/ViewQuestionnaire.aspx?CompanyNo=" + ret.CompanyNo.ToString().Trim() + "&ProjectNo=" + ret.ProjectNo.ToString().Trim();
                }
            }
        }

        private void GetCompanyUserDetail()
        {
            try
            {
                Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                MAS_BIDDINGCOMPANY ret = new MAS_BIDDINGCOMPANY();
                ret = manage.GetBiddingCompany(hdfCompanyNo.Value.ToString());
                if (ret != null && ret.CompanyNo > 0)
                {
                    lblCompanyName.Text = ret.CompanyName;
                    lblTaxID.Text = ret.TaxID;
                                       
                    lblAddress.Text = ret.CompanyAddress;
                    lblContactName.Text = ret.ContactName;
                    lblMobieNo.Text = ret.MobilePhoneNo;
                    lblPhoneNo.Text = ret.TelephoneNo;

                    lblEmail.Text = ret.Email;
                    lblEmailCC.Text = ret.EmailCC;

                    lblWebsite.Text = ret.CompanyWebsite;                 
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }            
        }
        
        private void GetCompanyUserAttachFile()
        {
            Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();

            List<MAS_COMPANYATTACHMENT> ret = new List<MAS_COMPANYATTACHMENT>();
            ret = manage.GetCompanyUserAttachFile(hdfCompanyNo.Value.ToString());

            gvAttachFile.DataSource = ret;
            gvAttachFile.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/CompanyUser.aspx", true);
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
        }      

        protected void gvAttachFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("lnkDownload") as LinkButton;

                string pathFile = lb.CommandArgument;
                pathFile = Page.ResolveClientUrl(pathFile);
                lb.Attributes.Add("href", pathFile);
                lb.Attributes.Add("target", "_blank");              

            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            MAS_COMPANYUSER_DTO sessionUserDet = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
            Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
            MAS_USERS userUpdate = new MAS_USERS();
            userUpdate.UsersNo = Int64.Parse(hdfUserNo.Value.Trim());
            userUpdate.Status = "Verified";
            userUpdate.UpdatedBy = sessionUserDet.UserName;
            userUpdate.UpdatedDate = DateTime.Now;

            bool bReset = manage.UpdateUserStatus(userUpdate);
            if (bReset)
            {

                lblMsgResult1.Text = "บันทึกสถานะ การตรวจสอบข้อมูลสำเร็จ";
                lblMsgResult2.Text = string.Empty;
                lbtnPopup_ModalPopupExtender.Show();

                /************** Send Email Approve***************/
                string _userName = lblUserName.Text.Trim();
                string _companyName = lblCompanyName.Text.Trim();
                string _emailApprove = ConfigurationManager.GetConfiguration().EmailApprove;
                string sBody = GenEmailBodyMailApprove(_userName, _companyName);
                SendMailApprove(sBody, _emailApprove);
                /*************************************************************/
            }
            else
            {
                lblMsgResult1.Text = "บันทึกสถานะ การตรวจสอบข้อมูลไม่สำเร็จ";
                lblMsgResult2.Text = "กรุณาติดต่อผู้ดูแลระบบ";
                lbtnPopup_ModalPopupExtender.Show();
            }
            
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            MAS_COMPANYUSER_DTO sessionUserDet = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
            Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
            MAS_USERS userUpdate = new MAS_USERS();
            userUpdate.UsersNo = Int64.Parse(hdfUserNo.Value.Trim());
            userUpdate.Status = "Approved";
            userUpdate.UpdatedBy = sessionUserDet.UserName;
            userUpdate.UpdatedDate = DateTime.Now;

            bool bReset = manage.UpdateUserStatus(userUpdate);
            if (bReset)
            {
                lblMsgResult1.Text = "บันทึกสถานะ การอนุมัติข้อมูลสำเร็จ";
                lblMsgResult2.Text = string.Empty;
                lbtnPopup_ModalPopupExtender.Show();

                /************** Send Email Notify***************/
                string _userName = lblUserName.Text.Trim();
                string _companyName = lblCompanyName.Text.Trim();
                string _emailNotify = ConfigurationManager.GetConfiguration().EmailNotify;
                string sBody = GenEmailBodyMailNotify(_userName, _companyName);
                SendMailNotify(sBody, _emailNotify);
                /*************************************************************/
            }
            else
            {
                lblMsgResult1.Text = "บันทึกสถานะ การอนุมัติข้อมูลไม่สำเร็จ";
                lblMsgResult2.Text = "กรุณาติดต่อผู้ดูแลระบบ";
                lbtnPopup_ModalPopupExtender.Show();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/CompanyUser.aspx", true);
        }


        private string GenEmailBodyMailApprove(string UserName, string CompanyName)
        {
            string url = ConfigurationManager.GetConfiguration().UrlLogin;
            string strEmailBody = "<HTML><BODY>" +
                "<table style='font-size: 11.0pt; font-family: 'Calibri','sans-serif'; color: black'>" +
                "<tr><td>Dear K.Saowaros Panjapaiboon,</td></tr><tr><td height='10px'></td></tr>" +
                "<tr><td>" +
                "Please process to approve registered company; " +
                "</td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td height='10px'></td></tr>" +
                "<tr><td>Company name: " + CompanyName + "</td></tr>" +
                "<tr><td>User name: " + UserName + "</td></tr>" +
                "<tr><td>Verified by: Kitiya Keeprasit </td></tr>" +               
                "<tr><td></td></tr>" +
                "<tr><td>You can action by login at : " + url + " </td></tr>" +
                "<tr><td height='50px'></td></tr>" +
                "<tr><td>Best regards,</td></tr>" +
                "<tr><td>Gulf E-Bidding</td></tr></table></BODY></HTML>";

            return strEmailBody;
        }

        private void SendMailApprove(string BodyMail, string EmailTo)
        {
            try
            {
                logger.Info("SendMailApprove: Start");

                string sSubjectMail = "[Action Required] Approve Company User";
                string sBodyMail = BodyMail;

                EmailNotification email = new EmailNotification();
                email.EmailSubject = sSubjectMail;

                email.EmailSMTP = ConfigurationManager.GetConfiguration().EmailSMTP;
                email.EmailPort = int.Parse(ConfigurationManager.GetConfiguration().EmailPort);
                email.EmailForm = ConfigurationManager.GetConfiguration().EmailFrom;

                ArrayList aEmailTo = new ArrayList();
                aEmailTo.Add(EmailTo);
            

                email.EmailTo = aEmailTo;
                email.EmailBody = sBodyMail;

                email.SendEmail();

                logger.Info("SendMailApprove: Complete");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private string GenEmailBodyMailNotify(string UserName, string CompanyName)
        {
            string url = ConfigurationManager.GetConfiguration().UrlLogin;
            string strEmailBody = "<HTML><BODY>" +
                "<table style='font-size: 11.0pt; font-family: 'Calibri','sans-serif'; color: black'>" +
                "<tr><td>Dear All,</td></tr><tr><td height='10px'></td></tr>" +
                "<tr><td>" +
                "Please be informed that registered company has been approved; " +
                "</td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td height='10px'></td></tr>" +
                "<tr><td>Company name: " + CompanyName + "</td></tr>" +
                "<tr><td>User name: " + UserName + "</td></tr>" +
                "<tr><td>Approved by: Saowaros Panjapaiboon </td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td>You can wiew by login at : " + url + " </td></tr>" +
                "<tr><td height='50px'></td></tr>" +
                "<tr><td>Best regards,</td></tr>" +
                "<tr><td>Gulf E-Bidding</td></tr></table></BODY></HTML>";

            return strEmailBody;
        }

        private void SendMailNotify(string BodyMail, string EmailTo)
        {
            try
            {
                logger.Info("SendMailNotify: Start");

                string sSubjectMail = "[Inform] Approve Company User";
                string sBodyMail = BodyMail;

                EmailNotification email = new EmailNotification();
                email.EmailSubject = sSubjectMail;

                email.EmailSMTP = ConfigurationManager.GetConfiguration().EmailSMTP;
                email.EmailPort = int.Parse(ConfigurationManager.GetConfiguration().EmailPort);
                email.EmailForm = ConfigurationManager.GetConfiguration().EmailFrom;

                ArrayList aEmailTo = new ArrayList();
                aEmailTo.Add(EmailTo);


                email.EmailTo = aEmailTo;
                email.EmailBody = sBodyMail;

                email.SendEmail();

                logger.Info("SendMailNotify: Complete");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }


    }
}
