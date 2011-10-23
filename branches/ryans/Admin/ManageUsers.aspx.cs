using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TreasureLand.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        public MembershipUserCollection users;
        public string[] s_array_roles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateUsers();
            }
        }

        public void PopulateUsers()
        {
            users = Membership.GetAllUsers();
            ListBox_Users.DataSource = users;
            ListBox_Users.DataBind();
        }


    }
}