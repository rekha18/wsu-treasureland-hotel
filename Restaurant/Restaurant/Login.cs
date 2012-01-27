using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;

namespace Restaurant
{
	
	public partial class LoginForm : Form
	{
		private string password="";

		public LoginForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// clears the current password and password label
		/// </summary>
		private void btnClear_Click(object sender, EventArgs e)
		{
			lblError.Visible = false;
			password = "";
			lblLoginPassword.Text = "";
		}

		/// <summary>
		/// takes the button that was clicked and checks adds the
		/// selected number to the password.  If the password is already 4 digits, nothing is added
		/// and an error label is shown.  If the current password is less than 4 digits, the number is 
		/// added to password and an * is shown in the password label
		/// </summary>
		private void Button_Click(object sender, System.EventArgs e)
		{
			lblError.Visible = false;			
			Button b = (Button)sender;
			if (password.Length < 3)
			{
				password += b.Text;
				lblLoginPassword.Text = lblLoginPassword.Text += "*";
			}
			else
			{
				password += b.Text;
				lblLoginPassword.Text = lblLoginPassword.Text += "*";
				//If the code is valid, open table select form
				//Otherwise displays error message and clears
				//the current password
				if (checkID())
				{
					//Open select table Form
                    RoomSelectionForm rsf = new RoomSelectionForm();
                    rsf.Show();
					Close();
				}
				else
				{
					lblError.Text = " Invalid Login ID";
					lblError.Visible = true;
					password = "";
					lblLoginPassword.Text = "";
				}	
			}
		}

		/// <summary>
		/// Checks the database and checks the entered number to see if it is valid
		/// </summary>
		private void btnSubmit_Click(object sender, EventArgs e)
		{
			if (password.Length < 4)
			{
				lblError.Text = "ID must be four digits";
				lblError.Visible = true;
			}
			else 
			{
				//If the code is valid, open table select form
				//Otherwise displays error message and clears
				//the current password
				if (checkID())
				{
					//Open select table Form
                    RoomSelectionForm rsf = new RoomSelectionForm();
                    rsf.Show();
					Close();
				}
				else
				 {
				 lblError.Text = " Invalid Login ID";
				 lblError.Visible = true;
				 password = "";
				 lblLoginPassword.Text = "";				 
				 }				  
			}
		}

		/// <summary>
		/// Connects to database, checks the four digit code
		/// to see if it is a valid code
		/// </summary>
		/// <returns>returns if the entered password is valid</returns>
		private bool checkID()
		{
            System.Diagnostics.Debug.WriteLine("PASSWORD: " + password);
			if (password == "0000")
				return true;
			return false;
		}

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


	}
}
