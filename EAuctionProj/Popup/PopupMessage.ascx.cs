using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EAuctionProj.Popup
{
    public partial class PopupMessage : System.Web.UI.UserControl
    {
        private string _messages;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Clear()
        {
            this.lMessage.Items.Clear();
        }

        public ListItemCollection Messages
        {
            get { return this.lMessage.Items; }
        }

        public void Show()
        {
            try
            {
                this.lnkDummy_ModalPopupExtender.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkClose_Click(object sender, System.EventArgs e)
        {
            this.lnkDummy_ModalPopupExtender.Hide();
            Response.Redirect("~/Form/BidingProjectList.aspx", true);
        }    
     
    }
}