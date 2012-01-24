using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Screen hight = 768px
//Screen width = 1024px
//Button size = 72px x 72px
//1024/72 = 14.22
//768/72 = 10.66

namespace TreasureLand
{
    public partial class RestaurantRoomSelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("pageLoad..");
            for (int i = 0; i < 18; i++)
            {
                createButtons(i);
            }
        }

        private void createButtons(int id)
        {
            Button btn = new Button();
            btn.CssClass = "RestaurantRoomSelectionButton";

            btn.Text = "Room " + id.ToString() + "\nRice";
            
            btn.ID = id.ToString();
            
            btn.Click += new System.EventHandler(this.dynamicBtn_Click);
            this.panel_buttons.Controls.Add(btn);
        }

        protected void dynamicBtn_Click(Object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            btn.ForeColor = System.Drawing.Color.Green;

            String id = ((Button)sender).ID;
            System.Diagnostics.Debug.WriteLine("button clicked " + id);
            lbl_testResult.Text = id;
        }
    }
}