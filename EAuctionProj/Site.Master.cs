using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EAuctionProj.DAL;
using System.Web.Security;

namespace EAuctionProj
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] != null)
            {
                MAS_COMPANYUSER_DTO retUser = (MAS_COMPANYUSER_DTO)Session["UserLogin"];
                if (retUser.RolesNo == 1)
                {
                    MenuItem parent1 = NavigationMenu.FindItem("2");
                    NavigationMenu.Items.Remove(parent1);

                    MenuItem parent = NavigationMenu.FindItem("8");
                    NavigationMenu.Items.Remove(parent);
                }
                else
                {
                    //(retUser.RolesNo == 2)
                    SetVendorMenu();
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                SetDefaultMenu();
            }
        }

        private void SetDefaultMenu()
        {
            MenuItem parent = NavigationMenu.FindItem("3");
            NavigationMenu.Items.Remove(parent);

            MenuItem parent2 = NavigationMenu.FindItem("4");
            NavigationMenu.Items.Remove(parent2);

            MenuItem parent3 = NavigationMenu.FindItem("5");
            NavigationMenu.Items.Remove(parent3);

            MenuItem parent4 = NavigationMenu.FindItem("6");
            NavigationMenu.Items.Remove(parent4);

            MenuItem parent5 = NavigationMenu.FindItem("7");
            NavigationMenu.Items.Remove(parent5);

            MenuItem parent6 = NavigationMenu.FindItem("8");
            NavigationMenu.Items.Remove(parent6);

            MenuItem parent7 = NavigationMenu.FindItem("2");
            NavigationMenu.Items.Remove(parent7);
        }

        private void SetVendorMenu()
        {
            MenuItem parent = NavigationMenu.FindItem("3");
            NavigationMenu.Items.Remove(parent);

            MenuItem parent2 = NavigationMenu.FindItem("4");
            NavigationMenu.Items.Remove(parent2);          

            MenuItem parent4 = NavigationMenu.FindItem("6");
            NavigationMenu.Items.Remove(parent4);

            MenuItem parent5 = NavigationMenu.FindItem("7");
            NavigationMenu.Items.Remove(parent5);
        }

        protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
        }
    }
}
