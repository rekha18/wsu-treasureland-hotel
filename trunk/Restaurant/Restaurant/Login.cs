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
    //System.Diagnostics.Debug.WriteLine("");
	public partial class LoginForm : Form
	{
        private int logInLogOut = 0; // 0 for login, 1 for logout

		public string password="";

		public LoginForm(int inORout)
		{
			InitializeComponent();
            logInLogOut = inORout;

            if (logInLogOut == 0)
            {
                lblHeader.Text = "ENTER LOGIN ID";
            }
            else
            {
                lblHeader.Text = "ENTER LOGOUT ID";
                btnExit.Text = "Cancel";
            }
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
                validateLogin();
			}
		}

        private void validateLogin()
        {
            
            //If the code is valid, open table select form
            //Otherwise displays error message and clears
            //the current password

            if (logInLogOut == 0)
            {
                if (checkID())
                {
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
            else
            {
                if (logOutCheckID())
                {
                    Environment.Exit(0);
                }
                else
                {
                    lblError.Text = " Invalid Logout ID";
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
            submitButtonClick();
		}

        private void submitButtonClick()
        {
            if (password.Length < 4)
            {
                lblError.Text = "ID must be four digits";
                lblError.Visible = true;
            }
            else
            {
                validateLogin();
            }
        }

		/// <summary>
		/// Connects to database, checks the four digit code
		/// to see if it is a valid code
		/// </summary>
		/// <returns>returns if the entered password is valid</returns>
		private bool checkID()
		{
            DataClassesDataContext db = new DataClassesDataContext();
            var loginQuery = from l in db.aspnet_Memberships
                             where l.Pin == password & l.IsApproved == true
                             select new { l.UserId, l.LastLoginDate };

            if(loginQuery.Any())
            {
                //need to add in lastLogin, more secure?? users can guess passwords and succeed, max trys?
                return true;
            }

			return false;
		}

        private bool logOutCheckID()
        {
            if (password == "1111")
                return true;
            return false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (logInLogOut == 0)
            {
                Environment.Exit(0);
            }
            else
            {                
                Close();
            }
        }

        private void numberKeyEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyValue >= 48 && e.KeyValue <= 57) // horizontal keyboard numbers
            {
                int value = e.KeyValue - 48;
                password += value.ToString();
                lblLoginPassword.Text = lblLoginPassword.Text += "*";
                if (lblLoginPassword.Text.Length >= 4)
                {
                    validateLogin();
                }
            }
            else if (e.KeyValue >= 96 && e.KeyValue <= 105) // 10 keyboard numberpad
            {
                int value = e.KeyValue - 96;
                password += value.ToString();
                lblLoginPassword.Text = lblLoginPassword.Text += "*";
                if (lblLoginPassword.Text.Length >= 4)
                {
                    validateLogin();
                }
            }
            else if (e.KeyValue == 13) // return
            {
                submitButtonClick();
            }
        }
	}
}
