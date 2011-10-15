using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public Customer selectCustomer;

    //Retrives Customers information and posts to proper labels
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ddlCustomer.DataBind();
        lblErrorMessage.Text = "";
        selectCustomer = this.GetSelectCustomer();
        lblAddress2.Text = selectCustomer.Address;
        lblCity.Text = selectCustomer.City;
        lblState.Text = selectCustomer.State;
        lblZip.Text = selectCustomer.ZipCode;
        lblPhone2.Text = selectCustomer.Phone;
        lblEmail2.Text = selectCustomer.Email;
    }

    //Creates and filters table for specific customer id
    private Customer GetSelectCustomer()
    {
        DataView customerTable = (DataView) AccessDataSource1.Select(DataSourceSelectArguments.Empty);
        customerTable.RowFilter = "CustomerID = '" + ddlCustomer.SelectedValue + "'";
        DataRowView row = (DataRowView) customerTable[0];
        Customer c = new Customer();
        c.CustomerID = row["CustomerID"].ToString();
        c.Name = row["Name"].ToString();
        c.Address = row["Address"].ToString();
        c.City = row["City"].ToString();
        c.State = row["State"].ToString();
        c.ZipCode = row["ZipCode"].ToString();
        c.Phone = row["Phone"].ToString();
        c.Email = row["Email"].ToString();
        return c;
    }

    //On click event for Add button
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Customer person = new Customer();
            person = selectCustomer;
            this.AddToContactList(person);
            
        }
    }

    //Adds Customer to Contact list display box
    private void AddToContactList(Customer person)
    {
        SortedList cart = this.GetCart();
        string CustomerID = selectCustomer.CustomerID;
        if (cart.ContainsKey(CustomerID))
        {
            lblErrorMessage.Text = "Contact Already In List";
        }
        else
        {
            lblErrorMessage.Text = "";
            cart.Add(CustomerID, person);
            Response.Redirect("~/ContactList.aspx");
        }
    }

    //Sets session for contact list
    private SortedList GetCart()
    {
        if (Session["Cart"] == null)
            Session.Add("Cart", new SortedList());
        return (SortedList)Session["Cart"];
    }
}