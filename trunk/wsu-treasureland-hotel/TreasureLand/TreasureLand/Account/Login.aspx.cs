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
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            RedirectLogin(LoginUser.UserName);
        }

        /// <summary>
        /// Redirect the user to a specific URL, as specified in the web.config, depending on their role.
        /// If a user belongs to multiple roles, the first matching role in the web.config is used.
        /// Prioritize the role list by listing higher-level roles at the top.
        /// </summary>
        /// <param name="username">Username to check the roles for</param>
        private void RedirectLogin(string username)
        {
            LoginRedirectByRoleSection roleRedirectSection = (LoginRedirectByRoleSection)ConfigurationManager.GetSection("loginRedirectByRole");
            foreach (RoleRedirect roleRedirect in roleRedirectSection.RoleRedirects)
            {
                if (Roles.IsUserInRole(username, roleRedirect.Role))
                {
                    Response.Redirect(roleRedirect.Url);
                }
            }
        }
    }
}
