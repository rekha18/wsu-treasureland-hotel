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
        private List<IngredientPurchaseHistory> purchase = new List<IngredientPurchaseHistory>();//purchase history item for purchase history view = view2 at this point

        //List<IngredientOrderItem> ingOrdItms = new List<IngredientOrderItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
     
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var foodDrinkCat = from fdc in db.FoodDrinkCategories
                              select fdc.FoodDrinkCategoryName;
            ddIngredient.DataSource = foodDrinkCat;
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

            txtIngredient.Text = "";
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
                FoodDrinkCategory addFoodDrinkCat = new FoodDrinkCategory();//New item to be added to database

                Ingredient ingredient = new Ingredient();
                addFoodDrinkCat.FoodDrinkCategoryName = txtIngredient.Text;//set name
                addFoodDrinkCat.FoodDrinkCategoryTaxable = true;//set to true

                db.FoodDrinkCategories.InsertOnSubmit(addFoodDrinkCat);
                db.SubmitChanges();

                //refresh contents of dropdown
                // TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var foodDrinkCat = from fdc in db.FoodDrinkCategories
                                   select fdc.FoodDrinkCategoryName;
                ddIngredient.DataSource = foodDrinkCat;
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

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var ingredient = from i in db.Ingredients.Where(i => i.IngredientName == ddIngredient2.Text)
                      select i;

            foreach (var addIngID in ingredient)
            {
                iph.IngredientID = addIngID.IngredientID;
            }


            purchase.Add(iph);

            //reset fields for new item to be added
            txtQty.Text = "";
            txtPrice.Text = "";
        }

        protected void btnSubmitPurchase_Click(object sender, EventArgs e)
        {
            if (purchase.Count > 0)
            {
                //create a database connection
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                short pchID = 0;// purchase ID

                //Create Ingredient Purchase
                IngredientPurchase addIngredientPurchase = new IngredientPurchase();//create new purchase object
                DateTime purchaseTime = new DateTime();
                addIngredientPurchase.PurchaseDate = purchaseTime;//get today
                db.IngredientPurchases.InsertOnSubmit(addIngredientPurchase);
                db.SubmitChanges();

                //Query Recently added Purchase to get Purchase ID
                var pch = from p in db.IngredientPurchases.Where(p => p.PurchaseDate == purchaseTime )
                     select p;

                if(pch.Count() == 1){

                    foreach(var pid in pch){
                        pchID = pid.PurchaseID;
                    }
                }
                //for each add to database
                foreach(var ph in purchase)
                {

                    ph.PurchaseID = pchID;
                    db.IngredientPurchaseHistories.InsertOnSubmit(ph);
                    db.SubmitChanges();

                }
            }

        }

     


    }

    
}