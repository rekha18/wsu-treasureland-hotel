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
        internal class RoleInfo
        {
            public string SelectedUserName;
            public string RoleName;
            public bool Checked;

            public RoleInfo(string sun, string rn, bool c)
            {
                SelectedUserName = sun;
                RoleName = rn;
                Checked = c;
            }
        }
        
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["RoleChanges"] == null)
                    Session.Add("RoleChanges", new LinkedList<RoleInfo>());
                else
                    Session["RoleChanges"] = new LinkedList<RoleInfo>();
                
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

            if (Page.User.Identity.Name.ToLower() == Label_UserName.Text.ToLower()) //If the admin is editing himself
                ((CheckBox)Repeater_UsersRoleList.Items[0].FindControl("RoleCheckBox")).Enabled = false;
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

            LinkedList<RoleInfo> rc = (LinkedList<RoleInfo>)Session["RoleChanges"];
            LinkedList<RoleInfo> roleDeletes = new LinkedList<RoleInfo>();
            LinkedList<RoleInfo> roleInserts = new LinkedList<RoleInfo>();

            if (rc.Count == 0)
                rc.AddFirst(new RoleInfo(selectedUserName, roleName, roleCheckBox.Checked));
            else
            {
                bool foundMatch = false;
                foreach (RoleInfo ri in rc)
                {
                    //If it repeats, that means a change was reverted
                    if (ri.SelectedUserName == selectedUserName && ri.RoleName == roleName)
                    {
                        roleDeletes.AddFirst(ri);
                        foundMatch = true;
                        break;
                    }
                }
                if(!foundMatch)
                    roleInserts.AddFirst(new RoleInfo(selectedUserName, roleName, roleCheckBox.Checked));
            }

            foreach (RoleInfo remi in roleDeletes)
                rc.Remove(remi);

            foreach (RoleInfo remi in roleInserts)
                rc.AddFirst(remi);

            if (rc.Count > 0)
                btnSubmitRoleChanges.Enabled = true;
            else
                btnSubmitRoleChanges.Enabled = false;
        }
        #endregion Work methods

        /// <summary>
        /// Iterates through the repeater display and adds or removes roles in one block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitRoleChanges_Click(object sender, EventArgs e)
        {
            LinkedList<RoleInfo> rc = (LinkedList<RoleInfo>)Session["RoleChanges"];
            Label_StatusMsg.Text = String.Empty;

            foreach (RoleInfo ri in rc)
            {
                //check if we need to add or remove the user from the role
                if (ri.Checked)
                {
                    //add the user to the role
                    Roles.AddUserToRole(ri.SelectedUserName, ri.RoleName);
                    Label_StatusMsg.Text += string.Format("User {0} was added to role {1}.<br />", ri.SelectedUserName, ri.RoleName);
                }
                else
                {
                    //remove the user from the role
                    Roles.RemoveUserFromRole(ri.SelectedUserName, ri.RoleName);
                    //update the status
                    Label_StatusMsg.Text += string.Format("User {0} was removed from role {1}.<br />", ri.SelectedUserName, ri.RoleName);
                }
            }

            while (rc.Count > 0)
                rc.RemoveFirst();

            btnSubmitRoleChanges.Enabled = false;
        }


    }
}