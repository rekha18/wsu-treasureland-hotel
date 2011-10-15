using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContactList : System.Web.UI.Page
{
    private SortedList cart;

    //Checks for postback on page load
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetCart();
        if (!IsPostBack)
            this.DisplayCart();
    }

    //Get/Create session for cart
    private void GetCart()
    {
        if (Session["Cart"] == null)
            Session.Add("Cart", new SortedList());
        cart = (SortedList)Session["Cart"];
    }


    //clears then populates cart with contacts
    private void DisplayCart()
    {
        lstCart.Items.Clear();
        Customer item;
        foreach (DictionaryEntry entry in cart)
        {
            item = (Customer)entry.Value;
            lstCart.Items.Add(item.Display());
        }
    }

    //button to remove one contact at a time from the cart
    protected void btnRemoveContact_Click(object sender, EventArgs e)
    {
        if (cart.Count > 0 && lstCart.SelectedIndex > -1)
        {
            cart.RemoveAt(lstCart.SelectedIndex);
            this.DisplayCart();
        }
    }

    //button to clear the cart of contacts
    protected void btnClearList_Click(object sender, EventArgs e)
    {
        cart.Clear();
        lstCart.Items.Clear();
    }

}