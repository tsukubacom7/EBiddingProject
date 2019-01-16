using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAuctionProj.BL;
using EAuctionProj.DAL;
using EAuctionProj.Utility;

namespace EAuctionProj.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ChangePassword));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            {
                Response.Redirect("~/Account/Login.aspx", true);
            }

            if (!IsPostBack)
            {
                string _userNo = Request.QueryString["UsersNo"];
                string _userName = Request.QueryString["UserName"];

                MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                hdfUserName.Value = string.IsNullOrWhiteSpace(_userName) ? retUser.UserName.ToString().Trim() : _userName.Trim();
                hdfUsersNo.Value = string.IsNullOrWhiteSpace(_userNo) ? retUser.UsersNo.ToString().Trim() : _userNo.Trim();
                hdfRoleNo.Value = retUser.RolesNo.ToString().Trim(); 
            }
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    string _confirmPass = txtConfirmNewPassword.Text.Trim();
                    GlobalFunction func = new GlobalFunction();
                    string newPass = func.Encrypt(_confirmPass);
                    string userNo = hdfUsersNo.Value.ToString().Trim();

                    Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();

                    MAS_USERS userUpdate = new MAS_USERS();
                    userUpdate.UsersNo = Int64.Parse(userNo);
                    userUpdate.Password = newPass;
                    userUpdate.UpdatedBy = hdfUserName.Value.ToString().Trim();
                    userUpdate.UpdatedDate = DateTime.Now;

                    bool bReset = manage.ResetPassword(userUpdate);
                    if (bReset)
                    {
                        if (hdfRoleNo.Value.Trim().Equals("1"))
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            // "alert('แก้ไขรหัสผ่านสำเร็จ..');window.location ='../Form/CompanyUser.aspx';", true);
                            lblMsgResult1.Text = "แก้ไขรหัสผ่านสำเร็จ";
                            lbtnPopup_ModalPopupExtender.Show();
                        }
                        else
                        {
                            lblMsgResult1.Text = "แก้ไขรหัสผ่านสำเร็จ";
                            lbtnPopup_ModalPopupExtender.Show();
                        }
                    }
                    else
                    {
                        logger.Info("ChangePasswordPushButton_Click(): bReset=false!");
                        //Can not update
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        //       "alertMessage", "alert('ไม่สามารถแก้ไขรหัสผ่านได้!')", true);

                        lblMsgResult1.Text = "ไม่สามารถแก้ไขรหัสผ่านได้!";
                        lblMsgResult2.Text = "กรุณาติดต่อผู้ดูแลระบบ";

                        lbtnPopup_ModalPopupExtender.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                              "alertMessage", "alert('ไม่สามารถแก้ไขรหัสผ่านได้! กรุณาติดต่อผู้ดูแลระบบ')", true);
            }           
        }

        protected void ValidatePass_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                int _passLength = int.Parse(ConfigurationManager.GetConfiguration().PasswordLength);
                if (txtNewPassword.Text.Trim().Length < _passLength)
                {
                    //Minimum 6 characters required.
                    //logger.Info("Minimum 6 characters required. [UserName:" + hdfUserName.Value + "]");

                    ValidatePass.ErrorMessage = "กรุณาระบุรหัสผ่านใหม่อย่างน้อย 6 ตัวอักษร.";
                    args.IsValid = false;
                    return;
                }

                Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                MAS_COMPANYUSER_DTO retUser = new MAS_COMPANYUSER_DTO();


                retUser = manage.GetUserLogin(hdfUserName.Value);

                if (retUser.UsersNo != null)
                {
                    GlobalFunction func = new GlobalFunction();
                    string _currentPass = func.Encrypt(txtCurrentPassword.Text.Trim());
                    string _newPass = txtNewPassword.Text.Trim();
                    string _confirmPass = txtConfirmNewPassword.Text.Trim();

                    string _oldPass = retUser.Password.Trim();
                    if (!_oldPass.Equals(_currentPass))
                    {
                        //รหัสผ่านเก่าไม่ถูกต้อง
                        //logger.Info("Old password is incorrect. [UserName:" + hdfUserName.Value + "]");

                        ValidatePass.ErrorMessage = "รหัสผ่านเก่าไม่ถูกต้อง";
                        args.IsValid = false;
                        return;
                    }

                    if (!_newPass.Equals(_confirmPass))
                    {
                        //ยืนยันรหัสผ่านไม่ถูกต้อง
                        //logger.Info("The Confirm New Password must match the New Password entry. [UserName:" + hdfUserName.Value + "]");

                        ValidatePass.ErrorMessage = "ยืนยันรหัสผ่านไม่ถูกต้อง";
                        args.IsValid = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }

        protected void CancelPushButton_Click(object sender, EventArgs e)
        {
            if (hdfRoleNo.Value.Trim().Equals("1"))
            {
                Response.Redirect("~/Form/CompanyUser.aspx", true);
            }
            else
            {
                Response.Redirect("~/Form/CompanyUserDetail.aspx", true);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (hdfRoleNo.Value.Trim().Equals("1"))
            {
                Response.Redirect("~/Form/CompanyUser.aspx", true);
            }
            else
            {
                Response.Redirect("~/Form/CompanyUserDetail.aspx", true);
            }
        }       
    }
}
