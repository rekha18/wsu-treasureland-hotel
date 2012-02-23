using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;


namespace Restaurant
{
    //System.Diagnostics.Debug.WriteLine("");
    public partial class RoomSelectionForm : Form
    {
        public static String LOGGED_IN_ID;

        private int numberOfRooms = 0;
        private int pageNumber = 1;
        private int maxPageNumber = 1;
        SortedDictionary<int, Button> buttonDict = new SortedDictionary<int, Button>();
        SortedDictionary<int, String> buttonInfoDict = new SortedDictionary<int, String>();

        public bool allowUserToClose = false;

        public RoomSelectionForm()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            wrapper_panel.Left = (this.ClientSize.Width - wrapper_panel.Width) / 2;
            wrapper_panel.Top = (this.ClientSize.Height - wrapper_panel.Height) / 2;

/////////
            refreshScreen();

            LoginForm login = new LoginForm(0);
            login.ShowDialog();
            LOGGED_IN_ID = login.password;

            System.Diagnostics.Debug.WriteLine("Password: " + LOGGED_IN_ID);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (allowUserToClose)
            {
                base.OnClosing(e);
                e.Cancel = false;
            }
            else
            {
                base.OnClosing(e);
                e.Cancel = true;
            }
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

            hideExtraButtons();
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
            hideExtraButtons();
            loadRoomNumbers();
        }

        /// <summary>
        /// Selects room '0' which represents payment by cash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cash_Click(object sender, EventArgs e)
        {
            new MenuSelection(0, "", LOGGED_IN_ID).Show();
        }

        /// <summary>
        /// The button click event for the 24 room selection buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dynamicBtn_Click(Object sender, EventArgs e)
        {
            //Gets the buttons tag and calls the MenuSelection(roomID)
            Button btn = sender as Button;
            String text = btn.Text;
            String[] arr = text.Split('\n');
            int selectedRoom = Convert.ToInt32(arr[0]);

            new MenuSelection(selectedRoom, arr[1], LOGGED_IN_ID).Show();
            refreshScreen();
        }

        private void refreshScreen()
        {
            System.Diagnostics.Debug.WriteLine("refreshScreen()");

            //Loads 24 buttons to a dictionary that are inside panel_buttons
            loadButtonsToDictionary();

            //gets/sets currently filled rooms from database
            numberOfRooms = getFilledRoomCount();

            //hides any buttons if room count is less than 24, 
            //used later if room count is less than number of rooms
            hideExtraButtons();

            //Loads the room numbers to the buttons
            loadRoomNumbers();

            lbl_currentPage.Text = pageNumber.ToString();

            //gets the max number of pages and sets it to a label
            if (numberOfRooms != 0)
            {
                maxPageNumber = (int)(numberOfRooms / 24);
                int mod = maxPageNumber % numberOfRooms;
                if (mod > 0 || numberOfRooms <= 24)
                {
                    maxPageNumber++;
                }
                lbl_totalNumberOfPages.Text = maxPageNumber.ToString();
            }
            else
            {
                lbl_totalNumberOfPages.Text = "1";
            }
        }

        /// <summary>
        /// Loads the room numbers from the dictionary to the buttons as tags
        /// </summary>
        private void loadRoomNumbers()
        {
            DataClassesDataContext db = new DataClassesDataContext();

            //room number, surname
            var query = from r in db.Rooms
                        join rd in db.ReservationDetails
                        on r.RoomID equals rd.RoomID
                        join rv in db.Reservations
                        on rd.ReservationID equals rv.ReservationID
                        join g in db.Guests
                        on rv.GuestID equals g.GuestID
                        where r.RoomStatus == 'C' & rv.ReservationStatus == 'A'
                        select new { r.RoomID, g.GuestSurName, r.RoomNumbers };

            if (query.Any())
            {
                foreach (var q in query)
                {
                    buttonInfoDict.Add(Convert.ToInt32(q.RoomNumbers), q.GuestSurName.ToString());
                }

                int num = 0;
                foreach (int key in buttonDict.Keys)
                {
                    Button b = buttonDict[key];
                    if (num < buttonInfoDict.Count)
                    {
                        String element = buttonInfoDict.ElementAt(num).ToString();
                        element = element.Replace("[", "");
                        element = element.Replace("]", "");
                        String[] arr = element.Split(',');
                        b.Text = arr[0] + "\n" + arr[1];
                    }
                    num++;
                }
            }
        }

        /// <summary>
        /// Hides any buttons that exceed the number of rooms
        /// </summary>
        private void hideExtraButtons()
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

            //HAS NOT BEEN TESTED YET
            if (numberOfRooms < 25)
            {
                nav_panel.Visible = false;
            }
        }

        private int getFilledRoomCount()
        {
            //connect to the database
            DataClassesDataContext db = new DataClassesDataContext();
            //get the number of rooms currently ('C') filled
            int count = 0;
            var query = from r in db.Rooms
                        where r.RoomStatus == 'C'
                        select r;

            foreach (var r in query)
            {
                count++;
            }

            return count;
        }

        //NEEDS TO BE WIRED UP TO LOGINFORM()
        private void btn_logOut_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm(1);
            login.ShowDialog();
            allowUserToClose = true;
        }
    }
}
