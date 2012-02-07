using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;
using System.Security;
using System.Web.Security;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using TreasureLand.DBM;

namespace TreasureLand.Admin
{
    public partial class ManageRestaurant : System.Web.UI.Page
    {
        private List<IngredientPurchaseHistory> purchase;//purchase history item for purchase history view = view2 at this point

        public class IngredientOrderItem
        {
            private string idNumber;//logical id for memmory and page.
            private string pchName;//purchase name
            private double pchPrice;//Purchase Price
            private string pchQuantity;//purchase price//Need to check this type
            //Table t1;

            IngredientOrderItem()
            {
                idNumber = "";

            }

            //The integer is the number it is assigned.
            public IngredientOrderItem(int i)
            {
                idNumber = "OrderItem" + i.ToString();
                pchName = "";
                pchPrice = 0.0;
                pchQuantity = "";

            }
            public IngredientOrderItem(int i, string name, double price, string quantity)
            {
                idNumber = "OrderItem" + i.ToString();//set name
                pchName = "";
                pchPrice = 0.0;
                pchQuantity = "";
            }
            public string getIdName()
            {

                return idNumber;
            }


        }

        List<IngredientOrderItem> ingOrdItms = new List<IngredientOrderItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //     btnManageCategories.Focus();
            //   {
            //       if (!Page.IsPostBack)
            //        { }

            //    }

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var ingredients = from i in db.Ingredients
                              select i.IngredientName;
            ddIngredient.DataSource = ingredients;
            ddIngredient.DataBind();

        }

        protected void btnManageCategories_Click(object sender, EventArgs e)
        {
            //Change View Over to ManageCategories View.
            containerView.ActiveViewIndex = 0;
            //populate grid

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
      
            var ing = from i in db.Ingredients.Where(i => i.IngredientName== txtIngredient.Text )
                     select i;

            if (ing.Any())
            {
                //If something exists, then don't add
                //print error message


                //reset txtIngredient to " "
                txtIngredient.Text = "";
            }
            else
            {
               //else if something exists
               //add record to database
                    //USes an linq to sql to insert a guest into the guest table
                    Ingredient addIngredient = new Ingredient();
                    addIngredient.IngredientName = txtIngredient.Text;
                    db.Ingredients.InsertOnSubmit(addIngredient);
                    db.SubmitChanges();
            }


            //repopulate dropdownlist
            var ingredients = from i in db.Ingredients
                              select i.IngredientName;
            ddIngredient.DataSource = ingredients;
            ddIngredient.DataBind();

            //if (gvGuest.Rows.Count == 0)
            //{
            //    //USes an linq to sql to insert a guest into the guest table
            //    Guest addGuest = new Guest();
            //    addGuest.GuestFirstName = txtFirstName.Text;
            //    addGuest.GuestSurName = txtSurName.Text;
            //    addGuest.GuestPhone = txtPhone.Text;
            //    db.Guests.InsertOnSubmit(addGuest);
            //    db.SubmitChanges();

            //    lblResFirstName.Text = txtFirstName.Text;
            //    lblResSurName.Text = txtSurName.Text;
            //    lblResPhone.Text = txtPhone.Text;
            //    reserving.GuestID = addGuest.GuestID;

            //    reserving.view = 2;
            //    btnNewGuest.CommandArgument = "2";
            //}
            //else
            //{
            //    lblErrorInsertGuest.Text = "Guest already exists please select below or enter a new guest";
            //    btnNewGuest.CommandArgument = "0";
            //    reserving.view = 0;

            //}
        }

        protected void btnManageMenuItems_Click(object sender, EventArgs e)
        {
            //Change View Over to ManageCategories View.

            //Set ManageCategoryView to visible
            containerView.ActiveViewIndex = 1;

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();

            //Linq SQL
            var menuItem = from i in db.MenuItems
                           join f in db.FoodDrinkCategories on
                               i.FoodDrinkCategoryID equals f.FoodDrinkCategoryID
                           select new { i.MenuItemName, f.FoodDrinkCategoryName, i.MenuItemPrice };

            if (menuItem.Count() > 0)
            {
                gvMenuItems.DataSource = menuItem;
                gvMenuItems.DataBind();
            }
        }

        protected void btnEnterPurchase_Click(object sender, EventArgs e)
        {
            //Change View Over to createPurchase View.
            //Set ManageCategoryView to visible
            containerView.ActiveViewIndex = 2;

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var ingredients = from i in db.Ingredients
                              select i.IngredientName;
            ddIngredient2.DataSource = ingredients;
            ddIngredient2.DataBind();
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtIngredient.Text == "")
            {
                //output alert no text
            }
            else
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                Ingredient ingredient = new Ingredient();
                ingredient.IngredientName = txtIngredient.Text;

                db.Ingredients.InsertOnSubmit(ingredient);
                db.SubmitChanges();

                //refresh contents of dropdown
                // TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var ingredients = from i in db.Ingredients
                                  select i.IngredientName;
                ddIngredient.DataSource = ingredients;
                ddIngredient.DataBind();


            }

        }


        protected void btnAddListItemIngredient_Click(object sender, EventArgs e)
        {

            IngredientPurchaseHistory iph = new IngredientPurchaseHistory();//This is the purchase history object
            Ingredient ing = new Ingredient();

            //Assign Values in iph
            ing.IngredientName = ddIngredient2.SelectedItem.Value;//assign dropdown value to Ingredient
            iph.Ingredient = ing;//add ingredient in iph
            iph.IngredientPurchaseHistoryPrice = System.Convert.ToDecimal(txtPrice.Text);//add price to iph = convert string to decimal
            iph.IngredientPurchaseHistoryQty = short.Parse(txtQty.Text);//add qty = convert to short/int16

            purchase.Add(iph);
            //Table tb = new Table();
            //TableRow r1 = new TableRow();//row
            //TableCell c1 = new TableCell();//table cell
            //TableCell c2 = new TableCell();//table cell
            //TableCell c3 = new TableCell();//table cell
            //TableCell c4 = new TableCell();//table cell
            //TableCell c5 = new TableCell();//table cell
            //TableCell c6 = new TableCell();//table cell
            //TableCell c7 = new TableCell();//table cell
            //TableCell c8 = new TableCell();//table cell

            //increment count of items
            //IngredientOrderItem i1 = new IngredientOrderItem(ingOrdItms.Count);

            //tb.Rows.Add(r1);
            //r1.Cells.Add(c1);//add name




            

            //ingOrdItms.Add(i1);//add item to the orderlist
            //c1.Text = ingOrdItms[ingOrdItms.Count-1].getIdName();
            //create table

            //tb.ID = ingOrdItms[ingOrdItms.Count-1].getIdName();//Get ID

           
            //this.View2.Controls.Add(tb);
            //ddIngredient.DataBind();

        }

        protected void btnSubmitPurchase_Click(object sender, EventArgs e)
        {
            if (purchase.Count > 0)
            {

                //for each add to database
                foreach(var ph in purchase)
                {
                  //insert ph into IngredientPurchase
              //  ph.purchaseID = ;//get ID of IngredientPurchase
                }
            }

        }

     


    }

     //LINQ function calls
    
}