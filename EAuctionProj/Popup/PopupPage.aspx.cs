using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EAuctionProj.Popup
{
    public partial class PopupPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = this.TextBoxName.Text;
            //your code to modify name.......

            ScriptManager.RegisterStartupScript(this, GetType(), "key", "ReturnParentPage('modify success!')", true);
           
        }
    }
}