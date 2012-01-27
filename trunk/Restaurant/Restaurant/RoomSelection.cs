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
    public partial class RoomSelectionForm : Form
    {
        int numberOfRooms = 88;
        int pageNumber = 1;
        int maxPageNumber = 1;
        SortedDictionary<int, Button> buttonDict = new SortedDictionary<int, Button>();

        public RoomSelectionForm()
        {
            InitializeComponent();

            //Loads 24 buttons to a dictionary that are inside panel_buttons
            loadButtonsToDictionary();

            //hides any buttons if room count is less than 24, 
            //used later if room count is less than number of rooms
            hideNecessaryButtons();

            //Loads the room numbers to the buttons
            loadRoomNumbers();

            lbl_currentPage.Text = pageNumber.ToString();

            //gets the max number of pages and sets it to a label
            maxPageNumber = (int)(numberOfRooms / 24);
            int mod = maxPageNumber % numberOfRooms;
            if(mod > 0 || numberOfRooms <= 24)
            {
                maxPageNumber++;
            }
            lbl_totalNumberOfPages.Text = maxPageNumber.ToString();
        }

        /// <summary>
        /// Loads the buttons to a dictionary for munipulation 
        /// </summary>
        private void loadButtonsToDictionary()
        {
            foreach (Button control in panel_buttons.Controls)
            {
                buttonDict.Add(Convert.ToInt32(control.Tag), control);
            }
        }

        /// <summary>
        /// Button click for previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_previous_Click(object sender, EventArgs e)
        {
            //decrements the current page number
            pageNumber = --pageNumber;
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            lbl_currentPage.Text = pageNumber.ToString();

            //loops throught each button and assigns a tag 
            //which will be a reference to the room number
            foreach (int key in buttonDict.Keys)
            {
                Button b = buttonDict[key];
                int newTag = Convert.ToInt32(b.Tag) - 24;
                if (newTag > 0)
                {
                    b.Tag = newTag;
                }
            }
            if (pageNumber == 1)
            {
                btn_previous.Enabled = false;
            }

            hideNecessaryButtons();
            loadRoomNumbers();
        }
        
        /// <summary>
        /// Button click for next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_next_Click(object sender, EventArgs e)
        {
            pageNumber = ++pageNumber;
            lbl_currentPage.Text = pageNumber.ToString();
            
            foreach (int key in buttonDict.Keys)
            {
                Button b = buttonDict[key];
                b.Tag = Convert.ToInt32(b.Tag) + 24;
            }
            if (pageNumber > 1)
            {
                btn_previous.Enabled = true;
            }
            hideNecessaryButtons();
            loadRoomNumbers();
        }

        /// <summary>
        /// Selects room '0' which represents payment by cash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cash_Click(object sender, EventArgs e)
        {
            RestaurantMenuSelection rsf = new RestaurantMenuSelection(0);
            rsf.Show();
        }

        /// <summary>
        /// The button click event for the 24 room selection buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dynamicBtn_Click(Object sender, EventArgs e)
        {
            //Gets the buttons tag and calls the RestuarantMenuSelection(tag)
            Button btn = sender as Button;
            String tag = ((Button)sender).Tag.ToString();
            int selectedRoom = Convert.ToInt32(tag);
            RestaurantMenuSelection rms = new RestaurantMenuSelection(selectedRoom);
            rms.Show();
        }

        /// <summary>
        /// Loads the room numbers from the dictionary to the buttons as tags
        /// </summary>
        private void loadRoomNumbers()
        {
            foreach (int key in buttonDict.Keys)
            {
                Button b = buttonDict[key];
                b.Text = b.Tag.ToString();
            }
        }

        /// <summary>
        /// Hides any buttons that exceed the number of rooms
        /// </summary>
        private void hideNecessaryButtons()
        {
            foreach (int key in buttonDict.Keys)
            {
                Button b = buttonDict[key];
                b.Visible = true;
            }

            if (pageNumber == 1)
            {
                btn_previous.Enabled = false;
            }
            btn_next.Enabled = true;
            if (numberOfRooms < (24 * pageNumber))
            {
                foreach (int key in buttonDict.Keys)
                {
                    Button b = buttonDict[key];
                    if (Convert.ToInt32(b.Tag) > numberOfRooms)
                    {
                        b.Visible = false;
                        btn_next.Enabled = false;
                    }
                }
            }
        }
    }
}
