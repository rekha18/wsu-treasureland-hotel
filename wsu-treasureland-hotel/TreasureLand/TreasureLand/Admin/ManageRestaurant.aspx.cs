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

        //View State


        protected void Page_Load(object sender, EventArgs e)
        {
           
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var foodDrinkCat = from fdc in db.FoodDrinkCategories
                              select fdc.FoodDrinkCategoryName;
            ddIngredient.DataSource = foodDrinkCat;
            ddIngredient.DataBind();


            //Postback check for MenuItems view[01]
            //if (!IsPostBack)
            //{
            //    IEnumerable<DataRow> query =
            //        from order in orders.AsEnumerable()
            //        where order.Field<DateTime>("OrderDate") > new DateTime(2001, 8, 1)
            //        select order;

            //    // Create a table from the query.
            //    DataTable boundTable = query.CopyToDataTable<DataRow>();

            //    //add to session.

            //}

        }

        protected void btnManageCategories_Click(object sender, EventArgs e)
        {
            //If input is blank
            if (txtIngredient.Text == "")
            {
                lblCatAddError.Text = "You have not entered a Category";
            }
            else
            {
                //Change View Over to ManageCategories View.
                containerView.ActiveViewIndex = 0;
                //populate grid

                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();

                var ing = from i in db.Ingredients.Where(i => i.IngredientName == txtIngredient.Text)
                          select i;

                if (ing.Any())
                {
                    //If something exists, then don't add
                    //print error message
                    lblCatAddError.Text = "Category Is Already In the Hotel Restaurant";
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

            }//end if

        }//end function

   
        protected void btnManageMenuItems_Click(object sender, EventArgs e)
        {
            //Change View Over to ManageCategories View.
            //if(!Page.IsPostBack)
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

            Session["gvMenuItems"] = gvMenuItems;
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


            }

        }




        protected void btnSubmitPurchase_Click(object sender, EventArgs e)
        {
            if (Session["ingredientlist"]!=null)
            {
                purchase = (List<IngredientPurchaseHistory>)Session["ingredientlist"];
                //create a database connection
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                short pchID = 0;// purchase ID

                //Create Ingredient Purchase
                IngredientPurchase addIngredientPurchase = new IngredientPurchase();//create new purchase object
                DateTime purchaseTime = new DateTime();
                purchaseTime = DateTime.Now;
                addIngredientPurchase.PurchaseDate = purchaseTime;//get today
                db.IngredientPurchases.InsertOnSubmit(addIngredientPurchase);
                db.SubmitChanges();

                //System.Diagnostics.Debug.WriteLine("");
                //Query Recently added Purchase to get Purchase ID
                var pch = (from i in db.IngredientPurchases
                  select i.PurchaseID).Max();//db.IngredientPurchases.Last(); //from p in db.IngredientPurchases select p.PurchaseDate//.Where(p => p. == purchaseTime )
                     //select p;

                //if(pch.Count() == 1){
                
                //    foreach(var pid in pch){
                //       pchID = pid.PurchaseID;
                //    }
                //}
                
                System.Diagnostics.Debug.WriteLine(pch.ToString());
                //for each add to database
                foreach(var ph in purchase)
                {
                    
                    ph.PurchaseID = System.Convert.ToInt16(pch.ToString());
                    db.IngredientPurchaseHistories.InsertOnSubmit(ph);
                    db.SubmitChanges();

                }
                Session["ingredientlist"] = null;
            }

        }




        //public DataTable DTSample
        //{
        //    get
        //    {
        //        if (ViewState["DTSample"] == null)
        //            return (DataTable)ViewState["DTSample"];

        //        return (DataTable)ViewState["DTSample"];

        //    }
        //    set
        //    {
        //        ViewState["DTSample"] = value;
        //    }
        //}
    


        protected void gvMenuItems_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        private void BindData()
        {
            gvMenuItems.DataSource = Session["gvMenuItems"];
            gvMenuItems.DataBind();
        }

        protected void gvMenuItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnAddItemToPurchase_Click(object sender, EventArgs e)
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
            //Ingredient ing = new Ingredient();

            //Assign Values in iph
            //iph. = ddIngredient2.SelectedItem.Value;//assign dropdown value to Ingredient
            //iph.Ingredient = ing;//add ingredient in iph
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
            Session["ingredientlist"] = purchase;

            //reset fields for new item to be added
            txtQty.Text = "";
            txtPrice.Text = "";
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

                //reset drop down
                var ingredients = from i in db.Ingredients
                                  select i.IngredientName;
                ddIngredient2.DataSource = ingredients;
                ddIngredient2.DataBind();
                //make text box invisible again.
                txtIngredient2.Visible = false;

            }
            //make text box 
        }

        protected void btnManageDrink_Click(object sender, EventArgs e)
        {
            containerView.ActiveViewIndex = 3;
        }

    


    }

    
}