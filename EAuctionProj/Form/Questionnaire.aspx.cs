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
using EAuctionProj.Utility;
using System.Web.Security;
using System.Collections;

namespace EAuctionProj
{
    public partial class Questionnaire : System.Web.UI.Page
    {
        private string ProjectNo = string.Empty;
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Questionnaire));

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);

            if (!Page.IsPostBack)
            {
                if (Session["RegDetail"] != null)
                {
                    //InitialParameter();
                    Session["IsAnswer"] = null;

                    string _sRegDetai = (string)Session["RegDetail"];
                    if (!string.IsNullOrEmpty(_sRegDetai))
                    {
                        hdfCompanyNo.Value = _sRegDetai.Trim().Split(':')[0];
                        hdfProjectNo.Value = _sRegDetai.Trim().Split(':')[1];
                    }
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    ViewState.Clear();
                    FormsAuthentication.SignOut();

                    Response.Redirect("~/Account/Login.aspx", true);
                }
            }
        }

        private void InitialParameter()
        {
            try
            {
                string _decryptCompanyNo = Request.QueryString["Company"];
                string _decryptProjectNo = Request.QueryString["Project"];

                GlobalFunction func = new GlobalFunction();
                _decryptCompanyNo = func.Decrypt(Request.QueryString["Company"]);
                _decryptProjectNo = func.Decrypt(Request.QueryString["Project"]);

                hdfCompanyNo.Value = GlobalFunction.DecryptParam(_decryptCompanyNo.Trim());
                hdfProjectNo.Value = GlobalFunction.DecryptParam(_decryptProjectNo.Trim());
              
            }
            catch (Exception ex)
            {                
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                Session.Clear();
                Session.Abandon();
                ViewState.Clear();
                FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", true);
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            logger.Info("btnAccept_Click() - Start");
            /************************ Insert Data *****************/
            bool ret = false;
            try
            {
                string _phoneNo = ConfigurationManager.GetConfiguration().GulfPhoneNo;

                if (!string.IsNullOrWhiteSpace(hdfProjectNo.Value) && !string.IsNullOrWhiteSpace(hdfCompanyNo.Value))
                {
                    Inf_Questionnaire_Manage bl = new Inf_Questionnaire_Manage();
                    INF_QUESTIONNAIRE insData = new INF_QUESTIONNAIRE();

                    insData.ProjectNo = hdfProjectNo.Value.Trim();
                    insData.CompanyNo = hdfCompanyNo.Value.Trim();

                    if (rdoQuestion1.SelectedIndex > -1)
                    {
                        insData.AnsQuestion1 = int.Parse(rdoQuestion1.SelectedValue);
                    }
                    if (rdoQuestion3.SelectedIndex > -1)
                    {
                        insData.AnsQuestion3 = int.Parse(rdoQuestion3.SelectedValue);
                    }
                    if (rdoQuestion4.SelectedIndex > -1)
                    {
                        insData.AnsQuestion4 = int.Parse(rdoQuestion4.SelectedValue);
                    }
                    if (rdoQuestion5.SelectedIndex > -1)
                    {
                        insData.AnsQuestion5 = int.Parse(rdoQuestion5.SelectedValue);
                    }
                    if (rdoQuestion6.SelectedIndex > -1)
                    {
                        insData.AnsQuestion6 = int.Parse(rdoQuestion6.SelectedValue);
                    }
                    if (rdoQuestion7.SelectedIndex > -1)
                    {
                        insData.AnsQuestion7 = int.Parse(rdoQuestion7.SelectedValue);
                    }

                    insData.AnsQuestion2 = "วันที่จดทะเบียน: " + txtRegisterDate.Text.Trim() + " ประสบการณ์: " + lblTotalYear.Text;

                    insData.AnsQuestion8 = txtQuestion8.Text.Trim();

                    insData.CreatedBy = GetIPAddress();
                    insData.CreatedDate = DateTime.Now;
                    insData.UpdatedBy = GetIPAddress();
                    insData.UpdatedDate = DateTime.Now;

                    ret = bl.InsertQuestionnaire(insData);

                    if (ret)
                    {
                        /************** Send Email UserName & Password ***************/
                        if (Session["EmailVendor"] != null)
                        {
                            this.lbtnPopup_ModalPopupExtender.Show();

                            INF_EMIALVENDOR mailData = new INF_EMIALVENDOR();
                            mailData = (INF_EMIALVENDOR)Session["EmailVendor"];

                            this.SendMailUserPassword(mailData.EmailBody, mailData.EmailTo);
                        }
                        /*************************************************************/

                        Session["IsAnswer"] = "true";
                        Session["RegDetail"] = null;

                        //lblMsgResult1.Text = "บันทึกข้อมูลสำเร็จ";
                        lblMsgResult1.Text = "ท่านได้ทำการลงทะเบียนเรียบร้อย ระบบจะจัดส่งข้อมูลผู้ใช้งานให้ทางอีเมล์ที่ได้ลงทะเบียนไว้<br />" +
                            "หากไม่ได้รับอีเมล์ภายใน 1 วันสามารถติดต่อได้ที่เบอร์โทร " + _phoneNo;
                        this.lbtnPopup_ModalPopupExtender.Show();                       
                    }
                    else
                    {
                        Session["IsAnswer"] = "false";
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ไม่สามารถบันทึกข้อมูลได้')", true);
                        //string _phoneNo = ConfigurationManager.GetConfiguration().GulfPhoneNo;

                        lblMsgResult1.Text = "ไม่สามารถบันทึกข้อมูลได้ <br /> กรุณาติดต่อผู้ดูแลระบบที่เบอร์ " + _phoneNo;
                        this.lbtnPopup_ModalPopupExtender.Show();
                    }
                }
                else
                {
                    logger.Info("Session UserLogin has no data..!");
                    Response.Redirect("~/Account/Login.aspx", true);
                }

                logger.Info("btnAccept_Click() - End");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void txtRegisterDate_TextChanged(object sender, EventArgs e)
        {
            string totalReg = "";
            if (!string.IsNullOrWhiteSpace(txtRegisterDate.Text.Trim()))
            {
                //totalReg = (DateTime.Now - 
                DateTime _dtRegDate = Convert.ToDateTime(txtRegisterDate.Text.Trim());
                //string strED = dtStartDate.ToString();
                //_dtEndDate = DateTime.Parse(strED);   

                //int _totalDaysEnd = (DateTime.Now - _dtRegDate).Days;
                //totalReg = _totalDaysEnd.ToString();

                //int _totalReg = TotalRegister(_dtRegDate);
                totalReg = CountDate(_dtRegDate);
            }
            else
            {
                totalReg = "0 ปี 0 เดือน 0 วัน";
            }

            lblTotalYear.Text = totalReg;
        }

        private int TotalRegister(DateTime regDate)
        {
            DateTime end = DateTime.Now;
            return (end.Year - regDate.Year - 1) +
                (((end.Month > regDate.Month) ||
                ((end.Month == regDate.Month) &&
                (end.Day >= regDate.Day))) ? 1 : 0);
        }

        private string CountDate(DateTime RegDate)
        {
            string retCount = "";
            DateTime Cday = DateTime.Now;

            int Years = 0;
            int Months = 0;
            int Days = 0;

            if ((Cday.Year - RegDate.Year) > 0 ||
                (((Cday.Year - RegDate.Year) == 0) && ((RegDate.Month < Cday.Month) ||
                  ((RegDate.Month == Cday.Month) && (RegDate.Day <= Cday.Day)))))
            {
                int DaysInBdayMonth = DateTime.DaysInMonth(RegDate.Year, RegDate.Month);
                int DaysRemain = Cday.Day + (DaysInBdayMonth - RegDate.Day);

                if (Cday.Month > RegDate.Month)
                {
                    Years = Cday.Year - RegDate.Year;
                    Months = Cday.Month - (RegDate.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else if (Cday.Month == RegDate.Month)
                {
                    if (Cday.Day >= RegDate.Day)
                    {
                        Years = Cday.Year - RegDate.Year;
                        Months = 0;
                        Days = Cday.Day - RegDate.Day;
                    }
                    else
                    {
                        Years = (Cday.Year - 1) - RegDate.Year;
                        Months = 11;
                        Days = DateTime.DaysInMonth(RegDate.Year, RegDate.Month) - (RegDate.Day - Cday.Day);
                    }
                }
                else
                {
                    Years = (Cday.Year - 1) - RegDate.Year;
                    Months = Cday.Month + (11 - RegDate.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
            }

            retCount = Years.ToString() + " ปี " + Months.ToString() + " เดือน " + Days.ToString() + " วัน";
            return retCount;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Form/BiddingProcess.aspx?ProjectNo=" + hdfProjectNo.Value);
            string _sISAnswer = (string)Session["IsAnswer"];
            if (_sISAnswer.Trim().Equals("true"))
            {
                Session.Clear();
                Session.Abandon();
                ViewState.Clear();
                FormsAuthentication.SignOut();

                Response.Redirect("~/Account/Login.aspx", true);
            }
            else
            {
                this.lbtnPopup_ModalPopupExtender.Hide();
            }
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

    }
}
