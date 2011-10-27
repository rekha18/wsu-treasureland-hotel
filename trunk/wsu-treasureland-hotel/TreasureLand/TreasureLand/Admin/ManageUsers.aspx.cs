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
        #region Properties

        public string[] s_array_roles;

        /// <summary>
        /// Stores the search filter's command name for filtering the gridview
        /// </summary>
        private string UsernameToMatch
        {
            get
            {
                object o = ViewState["UsernameToMatch"];
                if (o == null)
                    return string.Empty;
                else
                    return (string)o;
            }
            set
            {
                ViewState["UsernameToMatch"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                object o = ViewState["PageIndex"];
                if (o == null)
                    return 0;
                else
                    return (int)o;
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }

        private int PageSize
        {
            get
            {
                return 10;
            }
        }
        #endregion Properties
        #region Work Methods
        protected void BindUserAccounts()
        {
            int totalRecords;
            GridView_UserAccounts.DataSource = Membership.FindUsersByName(this.UsernameToMatch + "%", this.PageIndex, this.PageSize, out totalRecords);
            GridView_UserAccounts.DataBind();

            //Enable or disable the paging interface
            bool visitingFirstPage = (this.PageIndex == 0);
            lnkFirst.Enabled = !visitingFirstPage;
            lnkPrev.Enabled = !visitingFirstPage;

            int lastPageIndex = (totalRecords - 1) / this.PageSize;
            bool visitingLastPage = (this.PageIndex >= lastPageIndex);
            lnkNext.Enabled = !visitingLastPage;
            lnkLast.Enabled = !visitingLastPage;

        }
        /// <summary>
        /// creates and binds a dynamic list of filters to list users.
        /// </summary>
        protected void Bind_Repeater_FilteringUI()
        {
            List<string> List_UserNames = new List<string>();
            foreach (MembershipUser user in Membership.GetAllUsers())
            {                List_UserNames.Add(user.UserName); 
            }

            //get the first letter of all the names in the database
            var filterOptions = from u in List_UserNames select u.Substring(0, 1).ToUpper();
            List<string> List_UserFirstLetters = filterOptions.ToList<string>();
            List_UserFirstLetters.Insert(0, "ALL");
            //add the "All option"            

            Repeater_FilteringUI.DataSource = List_UserFirstLetters;
            Repeater_FilteringUI.DataBind();
        }
        #endregion Work Methods

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserAccounts();
                Bind_Repeater_FilteringUI();
            }
        }
        protected void Repeater_FilteringUI_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ALL")
            {
                this.UsernameToMatch = string.Empty;
                BindUserAccounts();
            }
            else
            {
                this.UsernameToMatch = e.CommandName;
                BindUserAccounts();
            }
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            this.PageIndex = 0;
            BindUserAccounts();
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            this.PageIndex -= 1;
            BindUserAccounts();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            this.PageIndex += 1;
            BindUserAccounts();
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            // Determine the total number of records
            int totalRecords;
            Membership.FindUsersByName(this.UsernameToMatch + "%", this.PageIndex, this.PageSize, out totalRecords);
            // Navigate to the last page index
            this.PageIndex = (totalRecords - 1) / this.PageSize;
            BindUserAccounts();
        }
        #endregion Events
    }
}