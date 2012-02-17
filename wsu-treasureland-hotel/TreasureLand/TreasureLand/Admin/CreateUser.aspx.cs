using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TreasureLand.DBM;

namespace TreasureLand.Admin
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }
        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            //FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            //string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            //if (String.IsNullOrEmpty(continueUrl))
            //{
            //    continueUrl = "~/";
            //}

            TextBox user = (TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Username");
            TextBox pin = (TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Pin");


            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            
            aspnet_User u = db.aspnet_Users.Single(us => us.UserName == user.Text);
            aspnet_Membership mem = db.aspnet_Memberships.Single(m => m.UserId == u.UserId);
            mem.Pin = pin.Text;

            db.SubmitChanges();
       
            Response.Redirect("ManageUsers.aspx");
        }

    }
}