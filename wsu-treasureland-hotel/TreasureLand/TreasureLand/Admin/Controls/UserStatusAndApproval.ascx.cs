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
                //check off the roles that the user is already a member of
                CheckRolesForSelectedUser();
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
            Repeater_UsersRoleList.DataSource = roles;
            Repeater_UsersRoleList.DataBind();

        }
        
        protected void CheckRolesForSelectedUser()
        {
            //get the user's roles
            string selectedUserName =  Request.QueryString["user"];
            string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);

            //check or uncheck boxes as needed
            foreach (RepeaterItem item in Repeater_UsersRoleList.Items)
            {
                //reference the checkbox
                CheckBox RoleCheckBox = item.FindControl("RoleCheckBox") as CheckBox;
                //see if the rolecheckbox.text is in teh selectedusersroles
                if (selectedUsersRoles.Contains<string>(RoleCheckBox.Text))
                {
                    RoleCheckBox.Checked = true;
                }
                else
                {
                    RoleCheckBox.Checked = false;
                }
            }
        }
        protected void RoleCheckBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox roleCheckBox = sender as CheckBox;
            string selectedUserName = Request.QueryString["user"];

            string roleName = roleCheckBox.Text;

            //check if we need to add or remove the user from the role
            if (roleCheckBox.Checked)
            {
                //add the user to the role
                Roles.AddUserToRole(selectedUserName, roleName);
                Label_StatusMsg.Text = string.Format("User {0} was added to role {1}.", selectedUserName, roleName);
            }
            else
            {
                //remove the user from the role
                Roles.RemoveUserFromRole(selectedUserName, roleName);
                //update the status
                Label_StatusMsg.Text = string.Format("User {0} was removed from role {1}.", selectedUserName, roleName);
            }
        }
        #endregion Work methods


    }
}