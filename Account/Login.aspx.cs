using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;

namespace TreasureLand.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }

        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
           
        }

       
    }
}
