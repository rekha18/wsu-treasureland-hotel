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
    public partial class RestaurantMenuSelection : Form
    {
        public RestaurantMenuSelection()
        {
            InitializeComponent();
        }

        public RestaurantMenuSelection(int selectedRoom)
        {
            InitializeComponent();
            lbl_selectedRoom.Text = "Selected Room: " + selectedRoom.ToString();
        }
    }
}
