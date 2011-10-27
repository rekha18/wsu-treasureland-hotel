using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TreasureLand.Admin
{
    public partial class UserStatusAndApproval : System.Web.UI.UserControl
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if the querystring is empty, send back to ManageUsers
                string userName = Request.QueryString["user"];
                if (string.IsNullOrEmpty(userName))
                {
                    Response.Redirect("ManageUsers.aspx");
                }

                //get the user info
                MembershipUser user = Membership.GetUser(userName);
                //send them back if there's no user by the name in the query string
                if (user == null)
                {
                    Response.Redirect("ManageUsers.aspx");
                }

                //display the user name
                Label_UserName.Text = user.UserName;
                //display the enabled status of the user
                CheckBox_IsEnabled.Checked = user.IsApproved;

                if (user.LastLockoutDate.Year < 1755)
                {
                    Label_LastLockedOutDate.Text = "Never";
                }
                else
                {
                    Label_LastLockedOutDate.Text = user.LastLockoutDate.ToShortDateString();
                }
                //if the user is not locked out, disable the unlock button
                Button_UnlockUser.Enabled = user.IsLockedOut;

                //get the list of user roles
                Bind_CheckBoxList_Roles();
            }
        }
        protected void CheckBox_IsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            //changes the user's approved status
            string userName = Request.QueryString["user"];
            MembershipUser user = Membership.GetUser(userName);
            user.IsApproved = CheckBox_IsEnabled.Checked;
            Membership.UpdateUser(user);

            Label_StatusMsg.Text = (user.IsApproved) ? "The user has been enabled" : "The user has been disabled";

        }

        protected void Button_UnlockUser_Click(object sender, EventArgs e)
        {
            //unlock the account
            string userName = Request.QueryString["user"];
            MembershipUser user = Membership.GetUser(userName);
            user.UnlockUser();

            Button_UnlockUser.Enabled = false;
            Label_StatusMsg.Text = "The user account has been unlocked.";

        }
        #endregion Events
        #region Work methods
        protected void Bind_CheckBoxList_Roles()
        {
            string[] roles = Roles.GetAllRoles();
            CheckBoxList_Roles.DataSource = roles;
            CheckBoxList_Roles.DataBind();

            string userName = Request.QueryString["user"];
            MembershipUser user = Membership.GetUser(userName);

            foreach (ListItem box in CheckBoxList_Roles.Items)
            {
                box.Selected = Roles.GetRolesForUser(userName).Contains(box.Text);
            }
        }
        protected void Button_UpdateRoles_Click(object sender, EventArgs e)
        {
            string userName = Request.QueryString["user"];
            MembershipUser user = Membership.GetUser(userName);
            /*2011/10/27 Ryan Diener: This part of the method is not yet finished.
            foreach (ListItem box in CheckBoxList_Roles.Items)
            {
                if (box.Selected)
                {
                    if (!Roles.IsUserInRole(box.Text))
                        Roles.AddUserToRole(userName, box.Text);
                }
                else
                {
                    if (Roles.IsUserInRole(box.Text))
                        Roles.RemoveUserFromRole(userName, box.Text);
                }
            }
             * */
            Label_StatusMsg.Text = "User roles updated.";
        }
        #endregion Work methods


    }
}