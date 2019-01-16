using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAuctionProj.DAL;
using EAuctionProj.BL;
using EAuctionProj.Utility;
using System.Data;
using System.IO;
using System.Collections;

namespace EAuctionProj
{
    public partial class UserRegister : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(UserRegister));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBDProject();
                ClearControl();

                Session["RegDetail"] = null;
                Session["EmailVendor"] = null;
            }
        }
        private void ListBDProject()
        {
            List<MAS_PROJECTBIDDING_DTO> lItemRet = new List<MAS_PROJECTBIDDING_DTO>();
            MAS_PROJECTBIDDING data = new MAS_PROJECTBIDDING();
            Mas_ProjectBidding_Manage manage = new Mas_ProjectBidding_Manage();
            lItemRet = manage.ListAllProjBidingActive();

            ddlBDProject.DataSource = lItemRet;
            ddlBDProject.DataBind();

            ddlBDProject.Items.Insert(0, new ListItem("== เลือกรายการ ==", "0"));

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Default.aspx", true);
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            logger.Info("btnRegister_Click (Start)");
            string _phoneNo = ConfigurationManager.GetConfiguration().GulfPhoneNo;

            try
            {
                if (IsValid)
                {
                    Int64? _iCompanyNo = null;
                    string _sCompanyNo = hdfRetCompanyNo.Value.Trim();
                    if (!string.IsNullOrEmpty(_sCompanyNo))
                    {
                        _iCompanyNo = Convert.ToInt64(_sCompanyNo);
                    }

                    MAS_BIDDINGCOMPANY data = new MAS_BIDDINGCOMPANY();
                    data.CompanyName = txtCompanyName.Text.Trim();
                    data.TaxID = txtTaxID.Text.Trim();
                    data.CompanyAddress = txtCompanyAdd.Text.Trim();
                    data.ContactName = txtContactPerson.Text.Trim();
                    data.MobilePhoneNo = txtMobilePhone.Text.Trim();
                    data.TelephoneNo = txtPhoneNo.Text.Trim();
                    data.Email = txtEmail.Text.Trim();
                    data.EmailCC = txtEmailCC.Text.Trim();
                    data.CreatedDate = DateTime.Now;
                    data.CreatedBy = GetIPAddress();
                    data.UpdatedDate = DateTime.Now;
                    data.UpdatedBy = GetIPAddress();

                    data.CompanyWebsite = txtCompanyWebsite.Text.Trim();
                    data.CompanyType = ddlUserType.SelectedValue.Trim();
                    data.CompanyNo = _iCompanyNo;

                    /************************** For Set PAssword ***********************/
                    MAS_USERS userData = new MAS_USERS();
                    int _length = int.Parse(ConfigurationManager.GetConfiguration().PasswordLength);
                    GlobalFunction func = new GlobalFunction();
                    string _password = func.RandomDefaultPass(_length);
                    string _encryptPass = func.Encrypt(_password);

                    userData.Password = _encryptPass;
                    //userData.UserName = txtEmail.Text.Trim();
                    userData.RolesNo = 2; //Role for Company or Vendor!!
                    userData.CreatedDate = DateTime.Now;
                    userData.CreatedBy = GetIPAddress();
                    userData.UpdatedDate = DateTime.Now;
                    userData.UpdatedBy = GetIPAddress();

                    userData.Status = "Not Verify";

                    userData.ProjectNo = Convert.ToInt64(ddlBDProject.SelectedValue);
                    hdfProjectNo.Value = ddlBDProject.SelectedValue;
                    /*******************************************************************/
                    Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                    string strRetInsUser = manage.RegisterCompany(data, userData);

                    if (!string.IsNullOrWhiteSpace(strRetInsUser))
                    {
                        hdfCompanyNo.Value = strRetInsUser.Split(';')[0].ToString().Trim();

                        /************ Insert Attach File **************/
                        string _companyNo = strRetInsUser.Split(';')[0].ToString().Trim();
                        List<MAS_COMPANYATTACHMENT> lAttach = new List<MAS_COMPANYATTACHMENT>();
                        lAttach = SetCompanyFileUpload(_companyNo);
                        bool result = manage.InsertCompanyAttach(lAttach);
                        /**********************************************/
                        if (result)
                        {
                            /***************************** Old Version *********************************/                          
                            /************** Send Email UserName & Password ***************/
                            string _userName = strRetInsUser.Split(';')[1].ToString().Trim();
                            string sBody = GenEmailBody(_userName, _password);

                            //SendMailUserPassword(sBody, data.Email);
                            INF_EMIALVENDOR mailData = new INF_EMIALVENDOR();
                            mailData.EmailTo = data.Email;
                            mailData.EmailBody = sBody;
                            Session["EmailVendor"] = mailData;
                            /*************************************************************/

                            //******************* Case Crate Success ******************//
                            //lblMsgResult1.Text = "ท่านได้ทำการลงทะเบียนเรียบร้อย ระบบจะจัดส่งข้อมูลผู้ใช้งานให้ทางอีเมล์ที่ได้ลงทะเบียนไว้";
                            //lblMsgResult2.Text = "หากไม่ได้รับอีเมล์ภายใน 1 วันสามารถติดต่อได้ที่เบอร์โทร " + _phoneNo;
                            //lbtnPopup_ModalPopupExtender.Show();

                            string _encryCompanyNo = "";
                            string _encryProjectNo = "";
                            _encryCompanyNo = hdfCompanyNo.Value.Trim();
                            _encryProjectNo = hdfProjectNo.Value.Trim();
                            //_encryCompanyNo = GlobalFunction.EncryptParam(hdfCompanyNo.Value.Trim());
                            //_encryProjectNo = GlobalFunction.EncryptParam(hdfProjectNo.Value.Trim());

                            Session["RegDetail"] = _encryCompanyNo + ":" + _encryProjectNo;

                            /***************************** Old Version *********************************/

                            //string _urlDestination = "~/Form/Questionnaire.aspx?Company=" + _encryCompanyNo + "&Project=" + _encryProjectNo;
                            string _urlDestination = "~/Form/Questionnaire.aspx";
                            Response.Redirect(_urlDestination);
                            //*********************************************************//
                        }
                        else
                        {
                            lblMsgResult1.Text = "ไม่สามารถแก้ไขรหัสผ่านได้! ";
                            lblMsgResult2.Text = "กรุณาติดต่อผู้ดูแลระบบที่เบอร์โทร " + _phoneNo;
                            this.lbtnPopup_ModalPopupExtender.Show();
                        }
                    }
                    else
                    {
                        lblMsgResult1.Text = "ไม่สามารถบันทึกข้อมูลได้! ";
                        lblMsgResult2.Text = "กรุณาติดต่อผู้ดูแลระบบที่เบอร์โทร " + _phoneNo;
                        this.lbtnPopup_ModalPopupExtender.Show();
                    }
                }

                logger.Info("btnRegister_Click (End)");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                lblMsgResult1.Text = "ไม่สามารถแก้ไขรหัสผ่านได้! ";
                lblMsgResult2.Text = "กรุณาติดต่อผู้ดูแลระบบที่เบอร์โทร " + _phoneNo;

                this.lbtnPopup_ModalPopupExtender.Show();
            }
        }
        private void ClearControl()
        {
            txtCompanyName.Text = string.Empty;
            txtTaxID.Text = string.Empty;
            txtCompanyAdd.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtMobilePhone.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEmailCC.Text = string.Empty;

            fuCompanyCert.Controls.Clear();
            fuIDCard.Controls.Clear();
            fuVat20.Controls.Clear();
        }
        private List<MAS_COMPANYATTACHMENT> SetCompanyFileUpload(string CompanyNo)
        {
            List<MAS_COMPANYATTACHMENT> lCompanyAttach = new List<MAS_COMPANYATTACHMENT>();
            try
            {
                string strPathFile = ConfigurationManager.GetConfiguration().AttachFilePath;
                string strPathDate = DateTime.Now.ToString("ddMMyyyy");
                string strUploadFolder = ConfigurationManager.GetConfiguration().CompanyUploadFolder;

                string pathUpload = strPathFile + strUploadFolder + "/" + strPathDate + "/" + CompanyNo + "/";
                //String ServerMapPath = Server.MapPath(pathUpload);

                /**************** Upload File To Server ***********************/
                if (!System.IO.Directory.Exists(Server.MapPath(pathUpload)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(pathUpload));
                }
                if (fuCompanyCert.HasFile)
                {
                    //string fileName = fuCompanyCert.FileName;
                    //String ServerMapPath = Server.MapPath(pathUpload);
                    //fuCompanyCert.PostedFile.SaveAs(ServerMapPath + fuCompanyCert.FileName);

                    string fileName = Path.GetFileName(fuCompanyCert.PostedFile.FileName);
                    //Save files to disk
                    fuCompanyCert.SaveAs(Server.MapPath(pathUpload + fileName));


                    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                    data.CompanyNo = Int64.Parse(CompanyNo);
                    data.AttachFilePath = pathUpload + fileName;
                    data.FileName = fileName;
                    data.Description = "หนังสือรับรองบริษัท";
                    data.CreatedBy = GetIPAddress();
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = GetIPAddress();
                    data.UpdatedDate = DateTime.Now;

                    lCompanyAttach.Add(data);
                }

                if (fuVat20.HasFile)
                {
                    //string fileName = fuVat20.FileName;
                    ////pathUpload = pathUpload + fileName;
                    //String ServerMapPath = Server.MapPath(pathUpload);
                    //fuCompanyCert.PostedFile.SaveAs(ServerMapPath + fuVat20.FileName);

                    string fileName = Path.GetFileName(fuVat20.PostedFile.FileName);
                    //Save files to disk
                    fuVat20.SaveAs(Server.MapPath(pathUpload + fileName));

                    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                    data.CompanyNo = Int64.Parse(CompanyNo);
                    data.AttachFilePath = pathUpload + fileName;
                    data.FileName = fileName;
                    data.Description = "เอกสาร ภพ.20";
                    data.CreatedBy = GetIPAddress();
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = GetIPAddress();
                    data.UpdatedDate = DateTime.Now;

                    lCompanyAttach.Add(data);
                }

                if (fuIDCard.HasFile)
                {
                    //string fileName = fuIDCard.FileName;
                    ////pathUpload = pathUpload + fileName;
                    //String ServerMapPath = Server.MapPath(pathUpload);
                    //fuCompanyCert.PostedFile.SaveAs(ServerMapPath + fuIDCard.FileName);

                    string fileName = Path.GetFileName(fuIDCard.PostedFile.FileName);
                    //Save files to disk
                    fuIDCard.SaveAs(Server.MapPath(pathUpload + fileName));


                    MAS_COMPANYATTACHMENT data = new MAS_COMPANYATTACHMENT();
                    data.CompanyNo = Int64.Parse(CompanyNo);
                    data.AttachFilePath = pathUpload + fileName;
                    data.FileName = fileName;
                    data.Description = "บัตรประจำตัวประชาชน";
                    data.CreatedBy = GetIPAddress();
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = GetIPAddress();
                    data.UpdatedDate = DateTime.Now;

                    lCompanyAttach.Add(data);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                throw ex;
            }

            return lCompanyAttach;
        }
        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        protected void ValidateTxt_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (ddlBDProject.SelectedIndex == 0)
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุรายการที่เข้าร่วมประมูล";
                    args.IsValid = false;
                    return;
                }


                if (ddlUserType.SelectedIndex == 0)
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุประเภทผู้เข้าร่วมประมูล";
                    args.IsValid = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล";
                    txtCompanyName.Focus();
                    args.IsValid = false;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTaxID.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุเลขที่ผู้เสียภาษี";
                    txtTaxID.Focus();
                    args.IsValid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtCompanyAdd.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุที่อยู่บริษัท";
                    txtCompanyAdd.Focus();
                    args.IsValid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุชื่อผุ้ติดต่อ";
                    txtContactPerson.Focus();
                    args.IsValid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMobilePhone.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุเบอร์โทรศัพท์มือถือ";
                    txtMobilePhone.Focus();
                    args.IsValid = false;
                    return;
                }

                if (ddlUserType.SelectedValue.Equals("1"))
                {
                    if (string.IsNullOrWhiteSpace(txtPhoneNo.Text))
                    {
                        ValidateTxt.ErrorMessage = "กรุณาระบุเบอร์โทรศัพท์สำนักงาน";
                        txtPhoneNo.Focus();
                        args.IsValid = false;
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    ValidateTxt.ErrorMessage = "กรุณาระบุอีเมล์";
                    txtEmail.Focus();
                    args.IsValid = false;
                    return;
                }

                /********************Validate Attach File *********************/
                if (ddlUserType.SelectedValue.ToString().Trim().Equals("1"))
                {
                    if (!fuCompanyCert.HasFile || !fuVat20.HasFile)
                    {
                        ValidateTxt.ErrorMessage = "กรุณาแนบไฟล์หนังสือรับรองบริษัท และ ภพ.20";
                        args.IsValid = false;
                        return;
                    }
                }
                else
                {
                    if (!fuIDCard.HasFile)
                    {
                        ValidateTxt.ErrorMessage = "กรุณาแนบไฟล์บัตรประจำตัวประชาชน";
                        args.IsValid = false;
                        return;
                    }
                }
                /**************************************************************/

                /********************Validate is Exist USer ******************/
                string _projectNo = ddlBDProject.SelectedValue;
                string _taxID = txtTaxID.Text.Trim();
                Mas_BiddingCompany_Manage compBL = new Mas_BiddingCompany_Manage();
                bool isExistComp = compBL.IsExistCompany(_taxID, _projectNo);
                if (isExistComp)
                {
                    ValidateTxt.ErrorMessage = "บริษัท/บุคคลผู้เข้าร่วมประมูลนี้ลงทะเบียนในระบบแล้ว";
                    args.IsValid = false;
                    return;
                }
                /**************************************************************/

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private void SendMailUserPassword(string BodyMail, string EmailTo)
        {
            try
            {
                logger.Info("SendMailUserPassword: Start");

                string sSubjectMail = "Thank you for you register at Gulf E-Bidding";
                string sBodyMail = BodyMail;

                EmailNotification email = new EmailNotification();
                email.EmailSubject = sSubjectMail;

                email.EmailSMTP = ConfigurationManager.GetConfiguration().EmailSMTP;
                email.EmailPort = int.Parse(ConfigurationManager.GetConfiguration().EmailPort);
                email.EmailForm = ConfigurationManager.GetConfiguration().EmailFrom;

                ArrayList aEmailTo = new ArrayList();
                aEmailTo.Add(EmailTo);

                //Dim aEmailTo As New ArrayList;
                //Dim EmailTo() As String;
                //Dim sEmailTo As String;
                //EmailTo = Split(config.EmailTo, ";")
                //For Each sEmailTo In EmailTo
                //    aEmailTo.Add(sEmailTo)
                //Next

                email.EmailTo = aEmailTo;
                email.EmailBody = sBodyMail;

                email.SendEmail();

                logger.Info("SendMailUserPassword: Complete");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        private string GenEmailBody(string UserName, string Password)
        {
            string url = ConfigurationManager.GetConfiguration().UrlLogin;
            string strEmailBody = "<HTML><BODY>" +
                "<table style='font-size: 11.0pt; font-family: 'Calibri','sans-serif'; color: black'>" +
                "<tr><td>Dear Bidder,</td></tr><tr><td height='10px'></td></tr>" +
                "<tr><td>" +
                "Thank you for your registration. Please find username and password below to login our system; " +
                "</td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td height='10px'></td></tr>" +
                "<tr><td>User name: " + UserName + "</td></tr>" +
                "<tr><td>Password: " + Password + "</td></tr>" +
                "<tr><td></td></tr>" +
                "<tr><td>You may login at : " + url + " </td></tr>" +
                "<tr><td height='50px'></td></tr>" +
                "<tr><td>Best regards,</td></tr>" +
                "<tr><td>Gulf E-Bidding</td></tr></table></BODY></HTML>";

            return strEmailBody;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            lbtnPopup_ModalPopupExtender.Hide();
            Response.Redirect("~/Form/Default.aspx", true);

            #region #### Old Version ####
            //string _encryCompanyNo = "";
            //string _encryProjectNo = "";      

            //_encryCompanyNo = GlobalFunction.EncryptParam(hdfCompanyNo.Value.Trim());
            //_encryProjectNo = GlobalFunction.EncryptParam(hdfProjectNo.Value.Trim());

            //string _urlDestination = "~/Form/Questionnaire.aspx?Company=" + _encryCompanyNo + "&Project=" + _encryProjectNo;
            //Response.Redirect(_urlDestination, true);
            #endregion 
        }
        protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTaxID.Text.Trim()))
                {
                    string _taxID = txtTaxID.Text.Trim();
                    Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                    MAS_BIDDINGCOMPANY data = new MAS_BIDDINGCOMPANY();
                    data = manage.GetCompanyByTaxID(_taxID);

                    if (data != null && data.CompanyNo > 0)
                    {
                        hdfRetCompanyNo.Value = data.CompanyNo.ToString();
                        txtCompanyName.Text = data.CompanyName;
                        txtCompanyAdd.Text = data.CompanyAddress;
                        txtContactPerson.Text = data.ContactName;
                        txtMobilePhone.Text = data.MobilePhoneNo;
                        txtPhoneNo.Text = data.TelephoneNo;
                        txtEmail.Text = data.Email;
                        txtEmailCC.Text = data.EmailCC;
                        txtCompanyWebsite.Text = data.CompanyWebsite;
                    }
                    else
                    {
                        hdfRetCompanyNo.Value = null;
                        txtCompanyName.Text = string.Empty;
                        txtCompanyAdd.Text = string.Empty;
                        txtContactPerson.Text = string.Empty;
                        txtMobilePhone.Text = string.Empty;
                        txtPhoneNo.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtEmailCC.Text = string.Empty;
                        txtCompanyWebsite.Text = string.Empty;
                    }
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
