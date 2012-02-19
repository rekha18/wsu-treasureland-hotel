using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Xml;
using System.IO;
using TreasureLand.DBM;

namespace TreasureLand.Manager
{
    public partial class ManagerReports : System.Web.UI.Page
    {
        private string m_rdlcPath = "";
        private string m_reportName = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                txtBeginDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate_CalendarExtender.StartDate = DateTime.Now;
                txtEndDate.Text = txtEndDate_CalendarExtender.StartDate.Value.ToShortDateString();
            }
        }

        public void showdate(bool value)
        {
            txtBeginDate.Visible = value;
            txtEndDate.Visible = value;
            lblBeginDate.Visible = value;
            lblEndDate.Visible = value;
        }
        /// <summary>
        /// Chooses the type of report to create, Housekeeping, Restaurant, Rooms,
        /// Repair, and Financial
        /// Depending on the choice, only the options for the selected report type will be shown
        /// all others will be hidden
        /// </summary>
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Hides all the drop down lists
            ddlFinancial.Visible = false;
            ddlHousekeeping.Visible = false;
            ddlRepair.Visible = false;
            ddlRooms.Visible = false;
            ddlRestaurant.Visible = false;
            ddlFirstParameter.Visible = false;
            ddlSecondParameter.Visible = false;
            ddlThirdParameter.Visible = false;
            showdate(false);
            //Make the ddl associated with the report type visible
            //and populates any needed parameters
            switch (Convert.ToInt32(ddlReportType.SelectedValue))
            {
                case 1:
                    ddlFinancial.Visible = true;
                    showdate(true);
                    break;
                case 2:
                    ddlRooms.Visible = true;
                    
                    break;
                case 3:
                    ddlRestaurant.Visible = true;
                    getIngredients();
                    break;
                case 4:
                    ddlHousekeeping.Visible = true;
                    break;
                case 5:
                    ddlRepair.Visible = true;
                    break;                    
            }
        }

        /// <summary>
        /// Sets the name of the report to be generated.
        /// This name is taken from the ddl and saved
        /// </summary>
        private void getReportName()
        {
            switch (Convert.ToInt32(ddlReportType.SelectedValue))
            {
                case 1:
                    ddlFinancial.Visible = true;
                    m_reportName = ddlFinancial.SelectedValue;
                    showdate(true);
                    break;
                case 2:
                    ddlRooms.Visible = true;
                    m_reportName = ddlRooms.SelectedValue;
                    showdate(false);
                    break;
                case 3:
                    ddlRestaurant.Visible = true;
                    m_reportName = ddlRestaurant.SelectedValue;
                    showdate(false);
                    break;
                case 4:
                    ddlHousekeeping.Visible = true;
                    m_reportName = ddlHousekeeping.SelectedValue;
                    showdate(false);
                    break;
                case 5:
                    ddlRepair.Visible = true;
                    showdate(false);
                    break;
            }            
        }

        /// <summary>
        /// Loads the report and binds the data for the selected report
        /// </summary>
        protected void btnCreateReport_Click(object sender, EventArgs e)
        {
            getReportName();

            //Gets the location of the RDLC file to load
            m_rdlcPath = HttpContext.Current.Request.MapPath(".") + "\\" + "Reports" + "\\" + m_reportName + ".rdlc"; 
            
            //creates the ReportDataSource and populates it based on the selected values
            ReportDataSource rds = getDatasource();
                

            //Resets the ReportViewer, adds the new datasource, and changes the name of the report
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DisplayName = "reportDatasource";

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(m_rdlcPath);
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmldoc.Save(xmlStream);
                xmlStream.Flush();
                xmlStream.Position = 0;
                ReportViewer1.LocalReport.LoadReportDefinition(xmlStream);
            }

            //binds the data to the reportviewer and refreshes the display
            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.Visible = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns datasource to be bound to the reportviewer</returns>
        private ReportDataSource getDatasource()
        {
            string select;
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            switch (Convert.ToInt32(ddlReportType.SelectedValue))
            {
                case 1:
                    m_reportName = ddlFinancial.SelectedValue;
                    IEnumerable<Revenue> ingredient = getFinancialSelect();
                    return new ReportDataSource("reportDatasource", ingredient);
                    
                case 2:
                    m_reportName = ddlRooms.SelectedValue;
                    var Rooms = getRoomsSelect();
                    return new ReportDataSource("reportDatasource", Rooms);
                    
                case 3:
                    m_reportName = ddlRestaurant.SelectedValue;
                    var menuitem = getRestaurantSelect();
                    return new ReportDataSource("reportDatasource", menuitem);
                   
                case 4:
                    m_reportName = ddlHousekeeping.SelectedValue;
                    select = getHousekeepingSelect();
                    break;
                case 5:
                    m_reportName = ddlRepair.SelectedValue;
                    select = getRepairSelect();
                    break;
            }
            ReportDataSource data= new ReportDataSource();
            return data;
        }

        /// <summary>
        /// Creates the select statement for the repair reports
        /// </summary>
        /// <returns></returns>
        private string getRepairSelect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the select statement for housekeeping reports
        /// </summary>
        /// <returns></returns>
        private string getHousekeepingSelect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the selects statements for restaurant reports
        /// </summary>
        /// <returns></returns>
        private IQueryable getRestaurantSelect()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            if (m_reportName == "Dishes containing ingredient")
            {
                if (ddlFirstParameter.SelectedIndex == 1)
                {
                    return from m in db.MenuItems
                           join i in db.MenuItemIngredients
                           on m.MenuItemID equals i.MenuItemID
                           where i.IngredientID !=null
                           select m;
                }
                else
                {
                    return from m in db.MenuItems
                           join i in db.MenuItemIngredients
                           on m.MenuItemID equals i.MenuItemID
                           where i.IngredientID == Convert.ToSByte(ddlFirstParameter.SelectedValue)
                           select m;
                }
            }
            else if (m_reportName == "FoodSales")
            {
                if (ddlFirstParameter.SelectedIndex == 1)
                {
                    return from l in db.LineItems
                           join r in db.ReservationDetailBillings
                           on l.ReservationDetailBillingID equals r.ReservationDetailBillingID
                           join f in db.MenuItems
                           on l.MenuItemID equals f.MenuItemID
                           where(r.BillingItemDate > Convert.ToDateTime(txtBeginDate.Text) &
                           r.BillingItemDate < Convert.ToDateTime(txtEndDate.Text))
                           select new { l.LineItemTransactionID, l.LineItemAmount, f.MenuItemName };
                }
                else
                {
                    return from l in db.LineItems
                           join r in db.ReservationDetailBillings
                           on l.ReservationDetailBillingID equals r.ReservationDetailBillingID
                           join f in db.MenuItems
                           on l.MenuItemID equals f.MenuItemID
                           where f.MenuItemName == ddlFirstParameter.SelectedValue &
                           (r.BillingItemDate > Convert.ToDateTime(txtBeginDate.Text) &
                           r.BillingItemDate < Convert.ToDateTime(txtEndDate.Text))
                           select new { l.LineItemTransactionID, l.LineItemAmount, f.MenuItemName };
                }
            }
            else 
            {
                if (ddlFirstParameter.SelectedIndex == 1)
                {
                    return from l in db.LineItems
                           join fd in db.MenuItems
                           on l.MenuItemID equals fd.MenuItemID
                           select new { l.LineItemTransactionID, l.LineItemAmount, fd.MenuItemName };
                }
                else
                {
                    return from l in db.LineItems
                           join fd in db.MenuItems
                           on l.MenuItemID equals fd.MenuItemID
                           where fd.MenuItemName == ddlFirstParameter.SelectedValue
                           select new { l.LineItemTransactionID, l.LineItemAmount, fd.MenuItemName };
                }
            }
        }

        /// <summary>
        /// Creates the select statements for the room reports
        /// </summary>
        /// <returns></returns>
        private IQueryable getRoomsSelect()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            if (m_reportName == "RoomsOccupied")
            {
                return from r in db.Rooms
                       join rd in db.ReservationDetails
                       on r.RoomID equals rd.RoomID
                       join res in db.Reservations
                       on rd.ReservationID equals res.ReservationID
                       join g in db.Guests
                       on res.GuestID equals g.GuestID
                       where r.RoomStatus == 'A'
                       select new { r.RoomNumbers, g.GuestFirstName, g.GuestSurName, rd.CheckinDate, rd.Nights };

            }
            else if (m_reportName == "Payments")
            {
                return from r in db.Rooms
                       join rd in db.ReservationDetails
                       on r.RoomID equals rd.RoomID
                       join res in db.Reservations
                       on rd.ReservationID equals res.ReservationID
                       join g in db.Guests
                       on res.GuestID equals g.GuestID
                       where res.ReservationStatus == 'C'
                       select new { r.RoomNumbers, g.GuestFirstName, g.GuestSurName, rd.CheckinDate, rd.Nights };

            }
            else
            {
                return from r in db.Rooms
                      
                       select r;
            }
        }

        /// <summary>
        /// Creates the select statements for the financial reports
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Revenue> getFinancialSelect()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            if (m_reportName=="Deposits")
            {
            return from r in db.Revenues
                   where r.RevenueCategoryID == 1 &
                   (r.RevenueDate > Convert.ToDateTime(txtBeginDate.Text) &
                   r.RevenueDate < Convert.ToDateTime(txtEndDate.Text))
                   select r;                
            }
            else if(m_reportName=="Payments")
            {
                return from r in db.Revenues
                       where r.RevenueCategoryID == 2 &
                       (r.RevenueDate > Convert.ToDateTime(txtBeginDate.Text) &
                       r.RevenueDate < Convert.ToDateTime(txtEndDate.Text))
                       select r;
            }
            else
            {
                return from r in db.Revenues
                       where r.RevenueCategoryID == 2 &
                       (r.RevenueDate > Convert.ToDateTime(txtBeginDate.Text) &
                   r.RevenueDate < Convert.ToDateTime(txtEndDate.Text))
                       select r;
            }
        }

        /// <summary>
        /// If the restaurant ddl is changed to ingredients, the ddl that show the ingredients is shown
        /// 
        /// </summary>
        protected void ddl_SelectedIndexChangedRestaurant(object sender, EventArgs e)
        {
            if (ddlRestaurant.SelectedValue == "Dishes containing ingredient")
            {
                getIngredients();
                showdate(false);
            }
            else if(ddlRestaurant.SelectedValue == "FoodSales")
            {
                getMenuItems();
                showdate(true);
            }
            else
            {
                getDrinks();
                showdate(true);
            }          

        }

        private void getDrinks()
        {
            ddlFirstParameter.Items.Clear();
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            IEnumerable<DBM.MenuItem> drink =
                from d in db.MenuItems
                select d;
            ddlFirstParameter.DataSource = drink;
            ddlFirstParameter.DataValueField = "DrinkID";
            ddlFirstParameter.DataTextField = "DrinkName";

            ddlFirstParameter.Items.Insert(0, new ListItem("All Drinks", "0"));
            ddlFirstParameter.DataBind();

            ddlFirstParameter.Visible = true;
        }


        protected void ddl_SelectedIndexChangedHousekeeping(object sender, EventArgs e)
        {
            m_reportName = (sender as DropDownList).SelectedValue;
        }

        protected void ddl_SelectedIndexChangedRepairs(object sender, EventArgs e)
        {
            m_reportName = (sender as DropDownList).SelectedValue;
        }
        
        private void getMenuItems()
        {
            ddlFirstParameter.Items.Clear();
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            IEnumerable<DBM.MenuItem> menuItem =
                from m in db.MenuItems
                select m;
            ddlFirstParameter.DataSource = menuItem;
            ddlFirstParameter.DataValueField = "MenuItemID";
            ddlFirstParameter.DataTextField = "MenuItemName";

            ddlFirstParameter.Items.Insert(0, new ListItem("All Menu Items", "0"));
            ddlFirstParameter.DataBind();

            ddlFirstParameter.Visible = true;
        }

        /// <summary>
        /// Populates the first dropdown list with all the ingredients
        /// </summary>
        private void getIngredients()
        {
            ddlFirstParameter.Items.Clear();
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            IEnumerable<Ingredient> ingredient =
                from i in db.Ingredients
                select i;
            ddlFirstParameter.DataSource = ingredient;
            ddlFirstParameter.DataValueField = "IngredientID";
            ddlFirstParameter.DataTextField = "IngredientName";
 
            ddlFirstParameter.Items.Insert(0, new ListItem("All Ingredients", "0"));
            ddlFirstParameter.DataBind();
            
            ddlFirstParameter.Visible = true;
        }

        /// <summary>
        /// Populates the first dropdown list with all the rooms in the hotel
        /// </summary>
        private void getRooms()
        {            
            ddlFirstParameter.Items.Clear();
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            IEnumerable<Room> room =
                from r in db.Rooms
                select r;

            ddlFirstParameter.DataSource = LinqDataSource1;
            ddlFirstParameter.DataValueField = "RoomID";
            ddlFirstParameter.DataTextField = "RoomNumbers";
            ddlFirstParameter.Items.Insert(0, new ListItem("All Rooms", "0"));
            ddlFirstParameter.DataBind();
            ddlFirstParameter.Visible = true;
        }

        /// <summary>
        /// Displays the paramaters used for creating room reports
        /// If the report is future reservations, the choice of all rooms or individual room numbers
        /// will be displayed in the first ddl.
        /// Any other room report will result in the ddl being hidden
        /// </summary>
        protected void ddlRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_reportName = (sender as DropDownList).SelectedValue;
            if (ddlRooms.SelectedIndex == 1)
            {
                getRooms();
            }
            else
            {
                ddlFirstParameter.DataSource = null;
                ddlFirstParameter.Items.Clear();
                ddlFirstParameter.Visible = false;
            }

        }
    }
}