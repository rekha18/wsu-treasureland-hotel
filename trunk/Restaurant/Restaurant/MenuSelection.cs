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

        }
    }
}
