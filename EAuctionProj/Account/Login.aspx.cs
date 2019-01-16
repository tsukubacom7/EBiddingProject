using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAuctionProj.BL;
using EAuctionProj.DAL;
using EAuctionProj.Utility;
using System.Web.Security;

namespace EAuctionProj.Account
{
    public partial class Login : System.Web.UI.Page
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Login));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserLogin"] = null;
                ClearControl();
                txtUserName.Focus();
            }
        }

        private void ClearControl()
        {
            txtPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;  

            txtPassword.Attributes.Add("onkeypress", "return clickButton(event,'" + LoginButton.ClientID + "')");                    
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Response.Redirect("~/Form/Default.aspx", true);
            }
        }

        protected void ValidatePass_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                string _userName = txtUserName.Text.Trim();
                string _password = txtPassword.Text.Trim();

                Mas_BiddingCompany_Manage manage = new Mas_BiddingCompany_Manage();
                MAS_COMPANYUSER_DTO retUser = new MAS_COMPANYUSER_DTO();
                retUser = manage.GetUserLogin(_userName);

                /***************** Verify Username *******************/
                if (retUser.UsersNo == null)
                {
                    logger.Info("User is not Exist [UserName:" + _userName + "]");
                    args.IsValid = false;
                    return;
                }
                /*****************************************************/

                /******************** Varify Password *******************/
                GlobalFunction func = new GlobalFunction();
                string _encryptPass = func.Encrypt(_password);
                if (!retUser.Password.Equals(_encryptPass))
                {
                    logger.Info("Password is Incorrect [UserName:" + _userName + "]& [Password:" + _password + "]");
                    args.IsValid = false;
                    return;
                }
                /********************************************************/

                FormsAuthentication.SetAuthCookie(_userName, true);
                Session["UserLogin"] = retUser;
            }
            catch (Exception ex)
            {
                args.IsValid = false;

                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }
    }
}
