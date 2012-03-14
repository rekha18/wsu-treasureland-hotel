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
using System.Drawing;

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
                       where fdc.FoodDrinkCategoryTypeID == "F"
                       select fdc;
            }
            else if(isMenuItem == 'D')
            {
                return from fdc in db.FoodDrinkCategories
                       where fdc.FoodDrinkCategoryTypeID =="D"
                       select fdc;
            }
            else
            {
                return from fdc in db.FoodDrinkCategories
                       where fdc.FoodDrinkCategoryTypeID != "D"
                       & fdc.FoodDrinkCategoryTypeID != "F"
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
            return 
                   from i in db.Ingredients
                   join mii in db.MenuItemIngredients
                   on i.IngredientID equals mii.IngredientID
                   join mi in db.MenuItems
                   on mii.MenuItemID equals mi.MenuItemID
                   where mi.FoodDrinkCategoryID > 3
                   select new { i.IngredientName, i.IngredientID };
        }

        /// <summary>
        /// gets a list of all drinks and ids
        /// </summary>
        /// <returns></returns>
        private object getAllDrinks()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            return from i in db.Ingredients
                   join mii in db.MenuItemIngredients
                   on i.IngredientID equals mii.IngredientID
                   join mi in db.MenuItems
                   on mii.MenuItemID equals mi.MenuItemID
                   where mi.FoodDrinkCategoryID < 3
                   select new { i.IngredientName, i.IngredientID };
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

                //Makes sure the sessions are clear on Page Load
                Session["drinklist"] = null;
                Session["ingredientlist"] = null;
                getIngredientsDatabind();
                //getMenuItemIngredientsDatabind();
            }
        }

        private void getMenuItemIngredientsDatabind()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();

            var menuItemIngred = from mii in db.MenuItemIngredients
                                 join i in db.Ingredients
                                 on mii.IngredientID equals i.IngredientID
                                 where mii.MenuItemID == Convert.ToInt16(ddlMenuItemIngredients.SelectedValue)
                                 select  i ;

            /*foreach (Ingredient item in menuItemIngred)
            {
                lbMenuItems.Items.Add(new ListItem(item.IngredientName, item.IngredientID.ToString()));                
            }
            lbMenuItems.DataBind();*/
        }



        /// <summary>
        /// Gets a list of all the ingredients and binds it to the ingredient ddl
        /// </summary>
        private void getIngredientsDatabind()
        {            
           /* ddlIngredients.DataSource = getAllIngredients();
            ddlIngredients.DataValueField = "IngredientID";
            ddlIngredients.DataTextField = "IngredientName";
            ddlIngredients.DataBind();*/
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
                containerView.ActiveViewIndex = 1;
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
                gvEditCategories.DataBind();
                ddlAddGetCategory.DataBind();

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
            containerView.ActiveViewIndex = 2;
            lblIngredientInsert.Visible = false;
            btnManageCategories.BackColor = SystemColors.ButtonFace;
            btnManageMenuItems.BackColor = Color.Yellow;
            btnEnterPurchase.BackColor = SystemColors.ButtonFace;
            btnIngredients.BackColor = SystemColors.ButtonFace;
        }

        /// <summary>
        /// shows the Enter Purchase view
        /// </summary>
        protected void btnEnterPurchase_Click(object sender, EventArgs e)
        {
            //Change View Over to createPurchase View.
            //Set ManageCategoryView to visible
            containerView.ActiveViewIndex = 3;
            ddlIngredientPurchase.DataSource = getAllDrinks();
            ddlIngredientPurchase.DataValueField = "IngredientID";
            ddlIngredientPurchase.DataTextField = "IngredientName";
            ddlIngredientPurchase.Items.Insert(0,new ListItem("Select an item"));
            ddlIngredientPurchase.DataBind();
            lblIngredientInsert.Visible = false;
            btnManageCategories.BackColor = SystemColors.ButtonFace;
            btnManageMenuItems.BackColor = SystemColors.ButtonFace;
            btnEnterPurchase.BackColor = Color.Yellow;
            btnIngredients.BackColor = SystemColors.ButtonFace;
        }


        /// <summary>
        /// 
        /// </summary>
        protected void btnManageCategory_Click(object sender, EventArgs e)
        {
            containerView.ActiveViewIndex = 1;
            lblIngredientInsert.Visible = false;
            btnManageCategories.BackColor = Color.Yellow;
            btnManageMenuItems.BackColor = SystemColors.ButtonFace;
            btnEnterPurchase.BackColor = SystemColors.ButtonFace;
            btnIngredients.BackColor = SystemColors.ButtonFace;
      

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
                    gvshowIngredientPurchases.DataSource = null;
                    gvshowIngredientPurchases.DataBind();
                }

            };
            gvshowIngredientPurchases.DataSource = null;
            gvshowIngredientPurchases.DataBind();
            btnSubmitPurchase.Enabled = false;
            btnManageCategories.Enabled = true;
            btnManageMenuItems.Enabled = true;
            btnEnterPurchase.Enabled = true;
            btnIngredients.Enabled = true;

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
            gvshowIngredientPurchases.DataSource = null;
            gvshowIngredientPurchases.DataBind();
            btnSubmitPurchase.Visible = false;
            btnManageCategories.Enabled = true;
            btnManageMenuItems.Enabled = true;
            btnEnterPurchase.Enabled = true;
            btnIngredients.Enabled = true;
            btnClear.Visible = false;
        }

        /// <summary>
        /// Adds an individual item to to purchase
        /// </summary>
        protected void btnAddItemToPurchase_Click(object sender, EventArgs e)
        {
            if (ddlIngredientPurchase.SelectedIndex>-1)
            {


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

                    gvshowIngredientPurchases.Visible = true;
                    Session["ingredientlist"] = purchase;
                }
                btnSubmitPurchase.Visible = true;
                //reset fields for new item to be added
                txtQty.Text = "";
                txtPrice.Text = "";
                btnManageCategories.Enabled = false;
                btnManageMenuItems.Enabled = false;
                btnEnterPurchase.Enabled = false;
                btnIngredients.Enabled = false;
                btnClear.Visible = true;
                
            }
        }

        protected void btnAddListItemIngredient_Click1(object sender, EventArgs e)
        {

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();

            var ing = from fdc in db.Ingredients.Where(fdc => fdc.IngredientName == txtIngredient.Text)
                      select fdc;

            //checks to see if the item is in the database
            if (ing.Any())
            {
                //If something exists, then don't add
                //print error message
                lblIngredientInsert.Text = "Ingredient Is Already In the Hotel Restaurant";
                lblIngredientInsert.Visible = true;
                //reset txtIngredient to " "
                txtCategory.Text = "";
            }
            else
            {
                //add ingredient to database
                Ingredient addIngredient = new Ingredient();
                addIngredient.IngredientName = txtIngredient.Text;
                //addIngredient.IngredientDescription = txtIngredientComments.Text;
                db.Ingredients.InsertOnSubmit(addIngredient);
                db.SubmitChanges();
                lblIngredientInsert.Text = "Insert Successful";
                lblIngredientInsert.Visible = true;
                ddlIngredients.DataBind();
                txtIngredient.Text = "";
                txtIngredientComments.Text = "";
                //make text box invisible again.
                //make text box 
            }
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
                lblAddMenuItemName.Text = "Drink Name";
            }

            else
            {
                lblAddMenuItemName.Text = "Menu Item Name";
            }
        }

        /// <summary>
        /// The item new item is added to the menu item table or the drink table based on 
        /// the selected category.  The fields are then hidden.
        /// </summary>
        protected void btnAddSubmit_Click(object sender, EventArgs e)
        {
            //the item to be added is a drink
            //gets the nonalcohol or alcohol tage from the drop down list
            if (ddlChooseItem.SelectedIndex == 0)
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                TreasureLand.DBM.MenuItem d = new DBM.MenuItem();
                d.MenuItemName = txtAddMenuItemName.Text;
                d.MenuItemPrice = Convert.ToDecimal(txtAddPrice.Text);
                d.IsCurrentItem = true;
                d.FoodDrinkCategoryID = Convert.ToByte(ddlAddCategory.SelectedIndex + 1);

                db.MenuItems.InsertOnSubmit(d);
                db.SubmitChanges();
            }
            //the item to be added is a discount
            else if (ddlChooseItem.SelectedItem.Text=="Discounts")
            {
                try
                {
                    TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                    TreasureLand.DBM.MenuItem d = new DBM.MenuItem();
                    d.MenuItemName = txtAddMenuItemName.Text;
                    d.MenuItemPrice = Convert.ToDecimal(txtAddPrice.Text);
                    d.IsCurrentItem = true;
                    d.FoodDrinkCategoryID = Convert.ToByte(3);
                    db.MenuItems.InsertOnSubmit(d);
                    db.SubmitChanges();
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            else
            {
                //The item to be added is a menu item
                //gets the value from the category drop down list and adds 4 to that value
                //2 for drink categories, 1 for dicount category, and 1 since the first location is 0
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                TreasureLand.DBM.MenuItem mi = new TreasureLand.DBM.MenuItem();
                mi.FoodDrinkCategoryID = Convert.ToSByte(ddlAddCategory.SelectedIndex + 4);
                mi.MenuItemName = txtAddMenuItemName.Text;
                mi.MenuItemPrice = Convert.ToDecimal(txtAddPrice.Text);
                mi.IsCurrentItem = true;
                db.MenuItems.InsertOnSubmit(mi);
                db.SubmitChanges();
            }

            disableButtons(true);
            gvMenuItems.DataBind();
            ddlMenuItemIngredients.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ddlAddGetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMenuItems.DataBind();
           
            LinqDataSource1.DataBind();
        }

        void displayTypeCategories()
        {
            if (ddlChooseItem.SelectedItem.Text=="Beverages")
            {
                lblAddMenuItemName.Text = "Drink name";
                var foodDrinkCat = getCategories('A');
                ddlAddGetCategory.DataSource = foodDrinkCat;
                ddlAddGetCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddGetCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddGetCategory.DataBind();

                ddlAddCategory.DataSource = foodDrinkCat;
                ddlAddCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddCategory.DataBind();

                lblAddMenuItemName.Text = "Drink Name";
                
            }

            else if(ddlChooseItem.SelectedItem.Text == "Discounts")
            {
                lblAddMenuItemName.Text = "Discount name";
                var foodDrinkCat = getCategories('D');
                ddlAddGetCategory.DataSource = foodDrinkCat;
                ddlAddGetCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddGetCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddGetCategory.DataBind();
                
                ddlAddCategory.DataSource = foodDrinkCat;
                ddlAddCategory.DataValueField = "FoodDrinkCategoryID";
                ddlAddCategory.DataTextField = "FoodDrinkCategoryName";
                ddlAddCategory.DataBind();

                lblAddMenuItemName.Text = "Discount Name";
            }
            else
            {
                try
                {

                    TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                    var foodDrinkCat = getCategories('F');
                    ddlAddGetCategory.DataSource = foodDrinkCat;
                    ddlAddGetCategory.DataValueField = "FoodDrinkCategoryID";
                    ddlAddGetCategory.DataTextField = "FoodDrinkCategoryName";
                    ddlAddGetCategory.DataBind();

                    ddlAddCategory.DataSource = foodDrinkCat;
                    ddlAddCategory.DataValueField = "FoodDrinkCategoryID";
                    ddlAddCategory.DataTextField = "FoodDrinkCategoryName";
                    ddlAddCategory.DataBind();

                    gvMenuItems.DataBind();

                    lblAddMenuItemName.Text = "Menu Item Name";
                }
                catch (Exception)
                {
                    
                    throw;
                }
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

        /// <summary>
        /// 
        /// </summary>
        protected void ddlChooseItemForPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseItemForPurchase.SelectedIndex == 0)
            {
                lblPurchaseItemName.Text = "Select Beverage";
                btnAddListItemIngredient.Visible = false;
                ddlIngredientPurchase.DataSource = getAllDrinks();
                ddlIngredientPurchase.DataValueField = "IngredientID";
                ddlIngredientPurchase.DataTextField = "IngredientName";
                ddlIngredientPurchase.DataBind();


            }
            else
            {
                lblPurchaseItemName.Text = "Select Ingredient:";
                btnAddListItemIngredient.Visible = true;
                ddlIngredientPurchase.DataSource = getAllIngredients();
                ddlIngredientPurchase.DataValueField = "IngredientID";
                ddlIngredientPurchase.DataTextField = "IngredientName";
                ddlIngredientPurchase.DataBind();
            }
        }

        /// <summary>
        /// Cancels the add and enables the buttons when clicked
        /// </summary>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            disableButtons(true);
        }


        /// <summary>
        /// Disables the buttons when edit is selected
        /// </summary>
        protected void gvMenuItems_RowEditing(object sender, GridViewEditEventArgs e)
        {

            btnAddMenuItem.Enabled = false;
            ddlChooseItem.Enabled = false;
            ddlAddGetCategory.Enabled = false;
            btnEnterPurchase.Enabled = false;
            btnManageCategories.Enabled = false;
            btnManageMenuItems.Enabled = false;
            btnIngredients.Enabled = false;
        }


        /// <summary>
        /// Enables the buttons when the gridview edit is updated
        /// </summary>
        protected void gvMenuItems_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

            btnAddMenuItem.Enabled = true;
            ddlChooseItem.Enabled = true;
            ddlAddGetCategory.Enabled = true;
            btnEnterPurchase.Enabled = true;
            btnManageCategories.Enabled = true;
            btnManageMenuItems.Enabled = true;
            btnIngredients.Enabled = true;

        }

        /// <summary>
        /// Enables the buttons when the gridview edit is canceled
        /// </summary>
        protected void gvMenuItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            btnAddMenuItem.Enabled = true;
            ddlChooseItem.Enabled = true;
            ddlAddGetCategory.Enabled = true;
            btnEnterPurchase.Enabled = true;
            btnManageCategories.Enabled = true;
            btnManageMenuItems.Enabled = true;
            btnIngredients.Enabled = true;

        }

        protected void btnAddIngredientCancel_Click(object sender, EventArgs e)
        {

            txtIngredient.Visible = false;
            ddlIngredientPurchase.Visible = true;
            txtIngredientComments.Text = "";
            txtIngredient.Text = "";
            lblAddIngredientDescriptino.Visible = false;
            lblAddIngredient.Visible = false;
            txtIngredientComments.Visible = false;
            disableButtons(true);
            btnAddItemToPurchase.Visible = true;
            btnAddIngredientCancel.Visible = false;
            ddlChooseItemForPurchase.Enabled = true;
            txtPrice.Visible = true;
            txtQty.Visible = true;
            lblPrice.Visible = true;
            lblQty.Visible = true;
            btnIngredients.Enabled = true;
            lblIngredientInsert.Visible = false;
        }

        //Adds the selected Ingredient to the selected menu item
        protected void btnAddIngredientToMenuItem_Click(object sender, EventArgs e)
        {
            
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            MenuItemIngredient mii = new MenuItemIngredient();
            mii.IngredientID = Convert.ToInt16(ddlIngredients.SelectedValue);
            mii.MenuItemID = Convert.ToInt16(ddlMenuItemIngredients.SelectedValue);
            mii.MenuItemIngredientQty = Convert.ToDecimal(txtMenuItemIngredientQty.Text);
            db.MenuItemIngredients.InsertOnSubmit(mii);
            db.SubmitChanges();
            lbMenuItems.DataBind();
            lblIngredientInsert.Visible = false;
        }

        protected void btnRemoveIngredient_Click(object sender, EventArgs e)
        {
            if (lbMenuItems.SelectedIndex>-1)
            {

                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var deleteMenuItemIngredient =
                        from details in db.MenuItemIngredients
                        where details.MenuItemIngredientID == Convert.ToInt16(lbMenuItems.SelectedValue)
                        select details;

                foreach (var detail in deleteMenuItemIngredient)
                {
                    db.MenuItemIngredients.DeleteOnSubmit(detail);
                }
                db.SubmitChanges();
                lbMenuItems.DataBind();
                lblIngredientInsert.Visible = false;
            }
        }

        protected void btnIngredients_Click(object sender, EventArgs e)
        {
            containerView.ActiveViewIndex = 0;
            ddlIngredients.DataBind();
            btnManageCategories.BackColor = SystemColors.ButtonFace;
            btnManageMenuItems.BackColor = SystemColors.ButtonFace;
            btnEnterPurchase.BackColor = SystemColors.ButtonFace;
            btnIngredients.BackColor = Color.Yellow;
        }

        protected string GetItemName(string IngredientID)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var ingredientName = from items in db.Ingredients
                                 where items.IngredientID == Convert.ToInt16(IngredientID)
                                 select items;

            foreach (var i in ingredientName)
            {
                return i.IngredientName;
            }
            return "";
        }

  
    }

    
        
    
}