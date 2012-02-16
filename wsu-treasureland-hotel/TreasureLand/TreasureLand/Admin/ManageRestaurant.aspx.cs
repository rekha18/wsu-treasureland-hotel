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
        #region global variables
        private List<IngredientPurchaseHistory> purchase;//purchase history item for purchase history view = view2 at this point
        private List<IngredientPurchaseHistory> drinkPurchase;
        #endregion

        #region helper methods
        /// <summary>
        /// Gets all the drink categories or the menu item categories
        /// </summary>
        /// <param name="isMenuItem">the category type to return</param>
        /// <returns>food or drink categories</returns>
        private object getCategories(char isMenuItem)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            if (isMenuItem == 'F')
            { 
                return from fdc in db.FoodDrinkCategories
                       where fdc.FoodDrinkCategoryTypeID == isMenuItem.ToString()
                       select fdc;
            }
            else
            {
                return from fdc in db.FoodDrinkCategories
                       where fdc.FoodDrinkCategoryTypeID != "F"                        
                       select fdc;
            }

        }

        private object getAllCategories()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            return from fdc in db.FoodDrinkCategories
                   select fdc;
        }

        void disableButtons(bool b)
        {
            lblAddCategory.Visible = !b;
            lblAddMenuItemName.Visible = !b;
            lblAddPrice.Visible = !b;
            txtAddMenuItemName.Visible = !b;
            txtAddPrice.Visible = !b;
            ddlAddCategory.Visible = !b;
            btnAddSubmit.Visible = !b;
            btnCancel.Visible = !b;

            btnManageCategories.Enabled = b;
            btnManageMenuItems.Enabled = b;
            btnEnterPurchase.Enabled = b;
            ddlAddGetCategory.Enabled = b;
            ddlChooseItem.Enabled = b;
            gvDrink.Enabled = b;
            gvMenuItems.Enabled = b;
            btnAddMenuItem.Enabled = b;
            txtAddMenuItemName.Text = "";
            txtAddPrice.Text = "";
        }

        /// <summary>
        /// Gets a list of all the ingredient names and ids
        /// </summary>
        /// <returns></returns>
        private object getAllIngredients()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            return from i in db.Ingredients
                   select new { i.IngredientName, i.IngredientID };
        }

        /// <summary>
        /// gets a list of all drinks and ids
        /// </summary>
        /// <returns></returns>
        private object getAllDrinks()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            return from d in db.MenuItems
                   select new { d.MenuItemID, d.MenuItemName };
        }
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                displayTypeCategories();
                //populates the data for the create/view categories
                ddlCategory.DataSource = getAllCategories();
                ddlCategory.DataValueField = "FoodDrinkCategoryID";
                ddlCategory.DataTextField = "FoodDrinkCategoryName";
                ddlCategory.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// creates a new category
        /// checks to see if the category to be entered is already in the database
        /// if it doesn't exist it is created and added to the database
        /// </summary>
        protected void btnAddNewCategory(object sender, EventArgs e)
        {
            //Hides the error message
            lblCatAddError.Text = "";

            //If input is blank
            if (txtCategory.Text == "")
            {
                lblCatAddError.Text = "You have not entered a Category";
            }
            else
            {
                //Change View Over to ManageCategories View.
                containerView.ActiveViewIndex = 0;
                //populate grid

                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();

                var ing = from fdc in db.FoodDrinkCategories.Where(fdc => fdc.FoodDrinkCategoryName == txtCategory.Text)
                          select fdc;

                //checks to see if the item is in the database
                if (ing.Any())
                {
                    //If something exists, then don't add
                    //print error message
                    lblCatAddError.Text = "Category Is Already In the Hotel Restaurant";

                    //reset txtIngredient to " "
                    txtCategory.Text = "";
                }
                else
                {
                    //else if something exists
                    //add record to database
                    //USes an linq to sql to insert a guest into the guest table

                    FoodDrinkCategory addfdc = new FoodDrinkCategory();
                    addfdc.FoodDrinkCategoryName = txtCategory.Text;
                    addfdc.FoodDrinkCategoryTypeID = "F";
                    db.FoodDrinkCategories.InsertOnSubmit(addfdc);
                    db.SubmitChanges();
                }

                //repopulate dropdownlist
                ddlCategory.DataSource = getAllCategories();
                ddlCategory.DataBind();

                txtCategory.Text = "";
            }
        }

   
        /// <summary>
        /// Shows the manage menu items view
        /// </summary>
        protected void btnManageMenuItems_Click(object sender, EventArgs e)
        {
            //Change View Over to ManageCategories View.
            //Set ManageCategoryView to visible
            containerView.ActiveViewIndex = 1;   
        }

        /// <summary>
        /// shows the Enter Purchase view
        /// </summary>
        protected void btnEnterPurchase_Click(object sender, EventArgs e)
        {
            //Change View Over to createPurchase View.
            //Set ManageCategoryView to visible
            containerView.ActiveViewIndex = 2;
            ddlIngredientPurchase.DataSource = getAllDrinks();
            ddlIngredientPurchase.DataValueField = "DrinkID";
            ddlIngredientPurchase.DataTextField = "DrinkName";
            ddlIngredientPurchase.DataBind();
        }


        /// <summary>
        /// 
        /// </summary>
        protected void btnManageCategory_Click(object sender, EventArgs e)
        {
            containerView.ActiveViewIndex = 0; 
         /*   if (txtIngredient.Text != "")
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                FoodDrinkCategory addFoodDrinkCat = new FoodDrinkCategory();//New item to be added to database

                Ingredient ingredient = new Ingredient();
                addFoodDrinkCat.FoodDrinkCategoryName = txtIngredient.Text;//set name

                //always true/drinks are hardcoded
                addFoodDrinkCat.FoodDrinkCategoryIsMenuItem = true;//set to true

                db.FoodDrinkCategories.InsertOnSubmit(addFoodDrinkCat);
                db.SubmitChanges();

                //refresh contents of dropdown
                // TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var foodDrinkCat = from fdc in db.FoodDrinkCategories
                                   select fdc.FoodDrinkCategoryName;
                ddIngredient.DataSource = foodDrinkCat;
                ddIngredient.DataBind();
            }*/
        }

        /// <summary>
        /// Creates a purchase and saves it do the database
        /// </summary>
        protected void btnSubmitPurchase_Click(object sender, EventArgs e)
        {
            //insert drink purchase
            if (ddlChooseItemForPurchase.SelectedIndex==0)
            {
                if (Session["drinklist"] != null)
                {
                    drinkPurchase = (List<IngredientPurchaseHistory>)Session["drinklist"];
                    //create a database connection
                    TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                    IngredientPurchase addDrinkPurchase = new IngredientPurchase();
                    
                    addDrinkPurchase.PurchaseDate= DateTime.Now;
                    db.IngredientPurchases.InsertOnSubmit(addDrinkPurchase);
                    db.SubmitChanges();

                    var pch = (from i in db.IngredientPurchases
                               select i.PurchaseID).Max();

                    foreach (var dp in drinkPurchase)
                    {
                        dp.PurchaseID = System.Convert.ToInt16(pch.ToString());
                        db.IngredientPurchaseHistories.InsertOnSubmit(dp);
                        db.SubmitChanges();
                    }
                
                }
            }
            //enter food purchase
            else
            {
                if (Session["ingredientlist"] != null)
                {
                    purchase = (List<IngredientPurchaseHistory>)Session["ingredientlist"];
                    //create a database connection
                    TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();

                    //Create Ingredient Purchase
                    IngredientPurchase addIngredientPurchase = new IngredientPurchase();//create new purchase object

                    addIngredientPurchase.PurchaseDate = DateTime.Now;//get today
                    db.IngredientPurchases.InsertOnSubmit(addIngredientPurchase);
                    db.SubmitChanges();

                    //System.Diagnostics.Debug.WriteLine("");
                    //Query Recently added Purchase to get Purchase ID
                    var pch = (from i in db.IngredientPurchases
                               select i.PurchaseID).Max();//db.IngredientPurchases.Last(); //from p in db.IngredientPurchases select p.PurchaseDate//.Where(p => p. == purchaseTime )
                    //select p;

                    System.Diagnostics.Debug.WriteLine(pch.ToString());
                    //for each add to database
                    foreach (var ph in purchase)
                    {

                        ph.PurchaseID = System.Convert.ToInt16(pch.ToString());
                        db.IngredientPurchaseHistories.InsertOnSubmit(ph);
                        db.SubmitChanges();

                    }
                    Session["ingredientlist"] = null;
                    gvShowDrinkPurchases.DataSource = null;
                    gvshowIngredientPurchases.DataSource = null;
                    gvshowIngredientPurchases.DataBind();
                    gvShowDrinkPurchases.DataBind();
                }

            }
            gvShowDrinkPurchases.DataSource = null;
            gvShowDrinkPurchases.DataBind();
            gvshowIngredientPurchases.DataSource = null;
            gvshowIngredientPurchases.DataBind();
            ddlChooseItemForPurchase.Enabled = true;
            btnSubmitPurchase.Enabled = false;
            btnManageCategories.Enabled = true;
            btnManageMenuItems.Enabled = true;
            btnEnterPurchase.Enabled = true;
            btnClear.Enabled = false;
        }

        /// <summary>
        /// Clears all values associated with the current purchase
        /// and enables all buttons
        /// </summary>
        protected void btnClearPurchase(object sender, EventArgs e)
        {
            Session["drinklist"] = null;
            Session["ingredientlist"] = null;
            btnSubmitPurchase.Enabled = false;
            gvShowDrinkPurchases.DataSource = null;
            gvShowDrinkPurchases.DataBind();
            gvshowIngredientPurchases.DataSource = null;
            gvshowIngredientPurchases.DataBind();
            ddlChooseItemForPurchase.Enabled = true;
            btnSubmitPurchase.Enabled = false;
            btnManageCategories.Enabled = true;
            btnManageMenuItems.Enabled = true;
            btnEnterPurchase.Enabled = true;
        }

        /// <summary>
        /// Adds an individual item to to purchase
        /// </summary>
        protected void btnAddItemToPurchase_Click(object sender, EventArgs e)
        {
            ddlChooseItemForPurchase.Enabled = false;

            if (ddlChooseItemForPurchase.SelectedIndex == 0)
            {
                if (Session["drinklist"] == null)
                {
                    drinkPurchase = new List<IngredientPurchaseHistory>();
                }
                else
                {
                    drinkPurchase = (List<IngredientPurchaseHistory>)Session["drinklist"];
                }

                IngredientPurchaseHistory dph = new IngredientPurchaseHistory();//This is the purchase history object

                dph.IngredientPurchaseHistoryPrice = System.Convert.ToDecimal(txtPrice.Text);//add price to iph = convert string to decimal
                dph.IngredientPurchaseHistoryQty = short.Parse(txtQty.Text);//add qty = convert to short/int16

                dph.IngredientID = Convert.ToSByte(ddlIngredientPurchase.SelectedValue);

                drinkPurchase.Add(dph);
                gvShowDrinkPurchases.DataSource = drinkPurchase;
                gvShowDrinkPurchases.DataBind();
                gvShowDrinkPurchases.Visible = true;
                gvshowIngredientPurchases.Visible = false;
                Session["drinklist"] = drinkPurchase;

            }
            else
            {
                if (Session["ingredientlist"] == null)
                {
                    purchase = new List<IngredientPurchaseHistory>();
                }
                else
                {
                    purchase = (List<IngredientPurchaseHistory>)Session["ingredientlist"];
                }

                IngredientPurchaseHistory iph = new IngredientPurchaseHistory();//This is the purchase history object

                iph.IngredientPurchaseHistoryPrice = System.Convert.ToDecimal(txtPrice.Text);//add price to iph = convert string to decimal
                iph.IngredientPurchaseHistoryQty = short.Parse(txtQty.Text);//add qty = convert to short/int16
                iph.IngredientID = Convert.ToSByte(ddlIngredientPurchase.SelectedValue);
                                
                purchase.Add(iph);
                gvshowIngredientPurchases.DataSource = purchase;
                gvshowIngredientPurchases.DataBind();

                gvShowDrinkPurchases.Visible = false;
                gvshowIngredientPurchases.Visible = true;
                Session["ingredientlist"] = purchase;
            }
            btnSubmitPurchase.Enabled = true;
            //reset fields for new item to be added
            txtQty.Text = "";
            txtPrice.Text = "";
            btnManageCategories.Enabled = false;
            btnManageMenuItems.Enabled = false;
            btnEnterPurchase.Enabled = false;
            btnClear.Enabled = true;
        }


        protected void btnAddListItemIngredient_Click1(object sender, EventArgs e)
        {
            if (txtIngredient2.Visible == false)
            {
                txtIngredient2.Visible = true;
            }
            else
            {
                //add ingredient to database
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                Ingredient addIngredient = new Ingredient();
                addIngredient.IngredientName = txtIngredient2.Text;
                db.Ingredients.InsertOnSubmit(addIngredient);
                db.SubmitChanges();

                ddlIngredientPurchase.DataSource = getAllIngredients();
                ddlIngredientPurchase.DataValueField = "IngredientID";
                ddlIngredientPurchase.DataTextField = "IngredientName";
                ddlIngredientPurchase.DataBind();
                //make text box invisible again.
                txtIngredient2.Visible = false;

            }
            //make text box 
        }


        /// <summary>
        /// When the add menu item is clicked, the add menu items options are made visible
        /// The drop down list is populated with categories based on if it is a drink or menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddMenuItem_Click(object sender, EventArgs e)
        {
            disableButtons(false);
            if (ddlChooseItem.SelectedIndex == 0)
            {
                var foodDrinkCat = getCategories('A');
                ddlAddCategory.DataSource = foodDrinkCat;
                ddlAddCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddCategory.DataBind();
                lblAddMenuItemName.Text = "Drink Name";
            }
            else
            {
                var foodDrinkCat = getCategories('F');
                ddlAddCategory.DataSource = foodDrinkCat;
                ddlAddCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddCategory.DataBind();
                lblAddMenuItemName.Text = "Menu Item Name";
            }
        }

        /// <summary>
        /// The item new item is added to the menu item table or the drink table based on 
        /// the selected category.  The fields are then hidden.
        /// </summary>
        protected void btnAddSubmit_Click(object sender, EventArgs e)
        {
            //it is a drink item
            if (ddlChooseItem.SelectedIndex == 0)
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                TreasureLand.DBM.MenuItem d = new DBM.MenuItem();
                d.MenuItemName = txtAddMenuItemName.Text;
                d.MenuItemPrice = Convert.ToDecimal(txtAddPrice.Text);
                d.FoodDrinkCategoryID = Convert.ToByte(ddlAddCategory.SelectedIndex + 1);
                db.MenuItems.InsertOnSubmit(d);
                db.SubmitChanges();
            }
            //it is a food item
            else
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                TreasureLand.DBM.MenuItem mi = new TreasureLand.DBM.MenuItem();
                mi.FoodDrinkCategoryID = Convert.ToSByte(ddlAddCategory.SelectedIndex+3);
                mi.MenuItemName = txtAddMenuItemName.Text;
                mi.MenuItemPrice = Convert.ToDecimal(txtAddPrice.Text);
                db.MenuItems.InsertOnSubmit(mi);
                db.SubmitChanges();
            }

            disableButtons(true);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ddlAddGetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMenuItems.DataBind();
           
            gvDrink.DataBind();
            LinqDataSource1.DataBind();
        }

      


        void displayTypeCategories()
        {
            if (ddlChooseItem.SelectedIndex == 0)
            {
                lblAddMenuItemName.Text = "Drink name";
                var foodDrinkCat = getCategories('A');
                ddlAddGetCategory.DataSource = foodDrinkCat;
                ddlAddGetCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddGetCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddGetCategory.DataBind();
                
                gvDrink.Visible = true;
                gvMenuItems.Visible = false;                
            }
            else
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var foodDrinkCat = getCategories('F');
                ddlAddGetCategory.DataSource = foodDrinkCat;
                ddlAddGetCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddGetCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddGetCategory.DataBind();
                gvMenuItems.DataBind();
                gvMenuItems.Visible = true;
                gvDrink.Visible = false;
              
            }
        }

        /// <summary>
        /// Checks for Menu items or beverages.  If menu items, the second ddl is populated
        /// with all menu item categories, otherwise, the second list is populated with drink categories
        /// This is called with the choose item list is changed
        /// </summary>
        protected void ddlChooseItem_SelectedIndexChanged(object sender, EventArgs e)
        {
        displayTypeCategories();
        }

        protected void ddlChooseItemForPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseItemForPurchase.SelectedIndex == 0)
            {
                lblPurchaseItemName.Text = "Beverage Name";
                btnAddListItemIngredient.Visible = false;
                ddlIngredientPurchase.DataSource = getAllDrinks();
                ddlIngredientPurchase.DataValueField = "DrinkID";
                ddlIngredientPurchase.DataTextField = "DrinkName";
                ddlIngredientPurchase.DataBind();
            }
            else
            {
                lblPurchaseItemName.Text = "Ingredient Name";
                btnAddListItemIngredient.Visible = true;
                ddlIngredientPurchase.DataSource = getAllIngredients();
                ddlIngredientPurchase.DataValueField = "IngredientID";
                ddlIngredientPurchase.DataTextField = "IngredientName";
                ddlIngredientPurchase.DataBind();
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            disableButtons(false);
        }
    }

    
        
    
}