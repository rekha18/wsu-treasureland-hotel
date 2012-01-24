using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;

//Screen hight = 768px
//Screen width = 1024px
//Button size = 72px x 72px

namespace TreasureLand
{
    public partial class RestaurantRoomSelection : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Page_Load..");
            addButtonsToPage();
        }

        /// <summary>
        /// adds the buttons to a panel
        /// </summary>
        private void addButtonsToPage()
        {
            int page = 0;
            if (Session["PageNumber"] == null)
            {
                System.Diagnostics.Debug.WriteLine("session was null ");
                Session["PageNumber"] = page;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("session was NOT null ");
                page = getPageNumber();
            }

            System.Diagnostics.Debug.WriteLine("pageNumber = " + page);
            lbl_pageNumber.Text = (page + 1).ToString();

            //connect to the database
            //TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            //Room room = new Room();
            //IEnumerable<Room> rooms =
            //    from r in db.Rooms
            //    where r.RoomStatus == 'C'
            //    select r;

            //int count = rooms.Count();
            //System.Diagnostics.Debug.WriteLine("room count = " + count);










            if (page > 0)
            {
                page = page * 24;
            }
            for (int i = page; i < page + 24; i++)
            {
                createButtons(i);
            }
        }

        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="id">the button ID</param>
        private void createButtons(int id)
        {
            Button btn = new Button();
            btn.CssClass = "roomButtons";

            btn.Text = "Room " + id.ToString() + "\nRice";
            btn.ID = id.ToString();


            btn.Click += new System.EventHandler(this.dynamicBtn_Click);
            this.panel_buttons.Controls.Add(btn);
        }

        protected void dynamicBtn_Click(Object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("dynamicBtn_Click..");
            Button btn = sender as Button;
            String id = ((Button)sender).ID;
            System.Diagnostics.Debug.WriteLine("button clicked " + id);
            Response.Redirect("RestaurantMenu.aspx");
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("previous clicked");
            int page = getPageNumber();
            page = --page;
            if (page < 0)
            {
                page = 0;
            }
            setPageNumber(page);
            //Must Redirect because of button click issues with dynamically created buttons
            Response.Redirect("RestaurantRoomSelection.aspx");
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("next clicked");
            int page = getPageNumber();
            page = ++page;
            setPageNumber(page);
            //Must Redirect because of button click issues with dynamically created buttons
            Response.Redirect("RestaurantRoomSelection.aspx");
        }

        protected void setPageNumber(int n)
        {
            Session["PageNumber"] = n;
        }

        protected int getPageNumber()
        {
            return (int)Session["PageNumber"];
        }
    }
}