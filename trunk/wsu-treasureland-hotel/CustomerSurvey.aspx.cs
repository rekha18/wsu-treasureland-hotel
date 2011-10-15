using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerSurvey : System.Web.UI.Page
{
    //Declare General Variables
    public Incident selectedIncident;
    public Survey selectedSurvey;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Set Focus to text box and check for and setting values to diabled if postback
        SetFocus(txtGetIncidents);
        if (IsPostBack)
        {
            lstIncidents.Enabled = false;
            txtAdditionalComments.Enabled = false;
            rblProblemResolution.Enabled = false;
            rblResponseTime.Enabled = false;
            rblTechnicianEfficiency.Enabled = false;
            rbContactByEmail.Enabled = false;
            rbContactByPhone.Enabled = false;
            btnSubmit.Enabled = false;
            cbDiscuss.Enabled = false;
        }
    }

    private void GetIncident()
    {
        //Creates Table for specific Customer Id
        DataView incidentTable = (DataView)AccessDataSource1.Select(DataSourceSelectArguments.Empty);
        incidentTable.RowFilter = "CustomerID = '" + txtGetIncidents.Text + "'";
        
        //Sorts table by closing date
        incidentTable.Sort = "DateClosed DESC";
        lstIncidents.Items.Clear();

        //Checks if the customer id exists in the table
        if (incidentTable.Count == 0)
        {
            lblErrorMessage.Text = "This is not a valid customer ID.";
            lblErrorMessage.ForeColor = Color.Blue;
        }
        else
        { 
            //For each Customer ID instance it runs and displays Items
            foreach (DataRowView row in incidentTable)
            {
                Incident i = new Incident();
                i.IncidentID = row["IncidentID"].ToString();
                i.CustomerID = row["CustomerID"].ToString();
                i.ProductCode = row["ProductCode"].ToString();
                i.TechID = row["TechID"].ToString();
                i.DateOpened = row["DateOpened"].ToString();
                i.DateClosed = row["DateClosed"].ToString();
                i.Title = row["Title"].ToString();

                if (i.DateClosed != "")
                {
                    lstIncidents.Items.Insert(0, new ListItem(i.CustomerIncidentDisplay(), i.IncidentID));
                }

                //Displays error if there are no closed incidents
                else
                {
                    lblErrorMessage.Text = "There are no completed incidents for this customer ID.";
                    lblErrorMessage.ForeColor = Color.Red;
                    SetFocus(txtGetIncidents);
                }
            }
        }

        //Enables controls and sets focus to list box
        if (lblErrorMessage.Text == "")
        {
            lstIncidents.Items.Insert(0, new ListItem("(Select an incident)", "None"));
            lstIncidents.Enabled = true;
            txtAdditionalComments.Enabled = true;
            rblProblemResolution.Enabled = true;
            rblResponseTime.Enabled = true;
            rblTechnicianEfficiency.Enabled = true;
            rbContactByEmail.Enabled = true;
            rbContactByPhone.Enabled = true;
            btnSubmit.Enabled = true;
            cbDiscuss.Enabled = true;
            SetFocus(lstIncidents);
        }
    }

    //On click event for GetIncident Button
    protected void btnGetIncidents_Click(object sender, EventArgs e)
    {
        btnGetIncidents.PostBackUrl = "~/CustomerSurvey.aspx";
        lblErrorMessage.Text = "";
        lblErrorMessage.ForeColor = Color.Empty;
        this.GetIncident();

    }

    //On click event for submit button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Text = lstIncidents.SelectedValue;
    }
}