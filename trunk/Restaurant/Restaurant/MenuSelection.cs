using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restaurant
{
    //System.Diagnostics.Debug.WriteLine("");
    public partial class MenuSelection : Form
    {
        public MenuSelection()
        {
            InitializeComponent();
        }

        public MenuSelection(int selectedRoom)
        {
            InitializeComponent();
            lbl_selectedRoom.Text = "Room: " + selectedRoom.ToString();
            //Load Categories
            //Load Menu Items (Drinks if exist) else (nothing)
            
        }

        private void btn_submit_order_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_cancel_order_Click(object sender, EventArgs e)
        {
            Close();
        }






        //-----------------------------------------------------//
        //Drinks Category
        //-----------------------------------------------------//

        /// <summary>
        /// Non-Alcoholic Drinks Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_non_alcoholic_drinks_Click(object sender, EventArgs e)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var query = from d in db.Drinks
                        select d;//new { d.DrinkID, d.FoodDrinkCategoryID, d.DrinkName};

            if (query.Any())
            {
                foreach (var item in query)
                {
                    System.Diagnostics.Debug.WriteLine("Drink: " + item.DrinkName);
                }
            }
            

        }
        /// <summary>
        /// Alcoholic Drinks Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_alcoholic_drinks_Click(object sender, EventArgs e)
        {

        }


        //DataClassesDataContext db = new DataClassesDataContext();

        //    var query = from r in db.Rooms
        //                join rd in db.ReservationDetails
        //                on r.RoomID equals rd.RoomID
        //                join rv in db.Reservations
        //                on rd.ReservationID equals rv.ReservationID
        //                join g in db.Guests
        //                on rv.GuestID equals g.GuestID
        //                where r.RoomStatus == 'C' & rv.ReservationStatus == 'A'
        //                select new { r.RoomID, g.GuestSurName };

        //    int count = 0;
        //    foreach (var q in query)
        //    {
        //        buttonInfoDict.Add(Convert.ToInt32(q.RoomID), q.GuestSurName.ToString());
        //        count++;
        //    }

        //    int num = 0;
        //    foreach (int key in buttonDict.Keys)
        //    {
        //        Button b = buttonDict[key];
        //        if (num < buttonInfoDict.Count)
        //        {
        //            String element = buttonInfoDict.ElementAt(num).ToString();
        //            element = element.Replace("[", "");
        //            element = element.Replace("]", "");
        //            String[] arr = element.Split(',');
        //            b.Text = arr[0] + "\n" + arr[1];
        //        }
        //        num++;
        //    }

    }
}
