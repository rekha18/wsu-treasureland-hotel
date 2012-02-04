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

            //Ingredient ingredients = new Ingredient();//to insert create ingredient object
//            if (txtIngredient.Text == "")
//            {
//                lblV1error.Text = "please enter a value";
//            }
//            else
//            {
//                //check object type to be completed soon
//              //  if(){
//                //}

//                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
//                Ingredient ing = new Ingredient
//{
//    IngredientID = 12000,
//    IngredientName = "Seattle",

//};



//// Add the new object to the Orders collection.
//                // Add the new object to the Orders collection.
//                db.Ingredients.InsertOnSubmit(ing);

//                // Submit the change to the database.
//                //try
//                //{
//                    db.SubmitChanges();
//                //}
//               // catch (Exception i)
//               // {
//                   // Console.WriteLine(i);
//                    // Make some adjustments.
//                    // ...
//                    // Try again.
//                 //   db.SubmitChanges();
//               //}//.InsertOnSubmit(ing);

//            }//end if

//            //make a connection
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
            Table tb = new Table();
            TableRow r1 = new TableRow();//row
            TableCell c1 = new TableCell();//table cell
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();//table cell
            TableCell c4 = new TableCell();//table cell
            TableCell c5 = new TableCell();//table cell
            TableCell c6 = new TableCell();//table cell
            TableCell c7 = new TableCell();//table cell
            TableCell c8 = new TableCell();//table cell

            //increment count of items
            IngredientOrderItem i1 = new IngredientOrderItem(ingOrdItms.Count);

            tb.Rows.Add(r1);
            r1.Cells.Add(c1);//add name




            

            ingOrdItms.Add(i1);//add item to the orderlist
            c1.Text = ingOrdItms[ingOrdItms.Count-1].getIdName();
            //create table

            tb.ID = ingOrdItms[ingOrdItms.Count-1].getIdName();//Get ID

           
            this.View2.Controls.Add(tb);
            ddIngredient.DataBind();

        }



    }

     //LINQ function calls
    
}