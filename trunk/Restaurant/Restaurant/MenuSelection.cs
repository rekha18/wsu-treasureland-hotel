using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * Items are loaded based off of a category selection
 * All items for a selected category are stored in the "itemInfoDict"
 * Whenever a new category is selected the "itemInfoDict" must be cleared
 * After they are cleared the number of items stored inside "itemInfoDict" needs to be set
 * Unneccessary buttons then need to be hid
 * Load the Items to the buttons
 * set the labels
 * 
 * Below is the typical steps taken when a new category is selected
 
            loadItemsDictWithDrinks(4); or loadItemsDictWithFood("someFoodCategoryHere");
            numberOfItems = getDrinksCount(4);
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
            CURRENT_SELECTED_CATEGORY = btn_alcoholic_drinks.Text;
 */


namespace Restaurant
{
    //System.Diagnostics.Debug.WriteLine("");
    public partial class MenuSelection : Form
    {
        //GLOBAL USE
        private String CURRENT_SELECTED_CATEGORY = "";

        //FOOD CATEGORIES
        private int numberOfCategories = 0;
        private int categoryPageNumber = 1;
        private int maxCategoryPageNumber = 1;
        SortedDictionary<int, Button> categoryBtnDict = new SortedDictionary<int, Button>();
        SortedDictionary<int, String> categoryInfoDict = new SortedDictionary<int, String>();

        //ITEMS
        private int numberOfItems = 0;
        private int itemPageNumber = 1;
        private int maxItemPageNumber = 1;
        SortedDictionary<int, Button> itemBtnDict = new SortedDictionary<int, Button>();
        SortedDictionary<int, String> itemInfoDict = new SortedDictionary<int, String>();

        public MenuSelection()
        {
            InitializeComponent();
        }

        public MenuSelection(int selectedRoom)
        {
            InitializeComponent();
            if (selectedRoom != 0) // 0 means paying with cash
            {
                lbl_selectedRoom.Text = "Room: " + selectedRoom.ToString();
            }

            #region Categorie Setup
            //Load Buttons to Dictionaries
            loadButtonsToDictionary();

            //Get how many food categories exist
            numberOfCategories = getCategoryCount();
            
            //Hides any extra category buttons
            hideExtraCategoryButtons();

            //Load Categories
            loadCategories();
            lbl_current_category_page.Text = categoryPageNumber.ToString();

            //gets the max number of category pages and sets it to a label
            if (numberOfCategories != 0)
            {
                maxCategoryPageNumber = (int)(numberOfCategories / 7);
                int mod = maxCategoryPageNumber % numberOfCategories;
                if (mod > 0 || numberOfCategories <= 7)
                {
                    maxCategoryPageNumber++;
                }
                lbl_total_category_page.Text = maxCategoryPageNumber.ToString();
            }
            #endregion
            #region Initial Items Setup - Non-Alcoholic
            //Loads the item dictionary with non-alcoholic drinks
            loadItemsDictWithDrinks(2);
            //gets the number of non-alcoholic drinks
            numberOfItems = getDrinksCount(2);
            //Hides any extra item buttons
            hideExtraItemButtons();
            //sets up the initial display to non-alcoholic drinks
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            //gets the max number of category pages and sets it to a label
            setMaxNumberOfPagesForItems();
            CURRENT_SELECTED_CATEGORY = btn_non_alcoholic_drinks.Text;
            #endregion
        }

        #region Food Categories

        private void loadCategories()
        {
            //load categories based on page number
            int count = 0;
            if (categoryPageNumber == 1)
            {
                count = 0;
            }
            else
            {
                count = (categoryPageNumber - 1) * 7;
            }
            foreach (int key in categoryBtnDict.Keys)
            {
                Button b = categoryBtnDict[key];
                if (count < categoryInfoDict.Count())
                {
                    String element = categoryInfoDict.ElementAt(count).ToString();
                    element = element.Replace("[", "");
                    element = element.Replace("]", "");
                    String[] arr = element.Split(',');
                    b.Text = arr[1];
                }
                count++;
            }
        }

        /// <summary>
        /// Hides any buttons that exceed the number of cagetories
        /// </summary>
        private void hideExtraCategoryButtons()
        {
            foreach (int key in categoryBtnDict.Keys)
            {
                Button b = categoryBtnDict[key];
                b.Visible = true;
            }

            if (categoryPageNumber == 1)
            {
                btn_previous_categories.Enabled = false;
            }
            btn_next_categories.Enabled = true;
            if (numberOfCategories < (7 * categoryPageNumber))
            {
                foreach (int key in categoryBtnDict.Keys)
                {
                    Button b = categoryBtnDict[key];
                    if (Convert.ToInt32(b.Tag) > numberOfCategories)
                    {
                        b.Visible = false;
                        btn_next_categories.Enabled = false;
                    }
                }
            }
        }

        private void clearCategoryButtonText()
        {
            foreach (Button control in categories_panel.Controls)
            {
                control.Text = "";
            }
        }

        private int getCategoryCount()
        {
            //connect to the database
            DataClassesDataContext db = new DataClassesDataContext();
            int count = 0;
            var query = from c in db.FoodDrinkCategories
                        where c.FoodDrinkCategoryIsMenuItem == true
                        select c;

            foreach (var r in query)
            {
                count++;
            }
            return count;
        }
        private void btn_previous_categories_Click(object sender, EventArgs e)
        {
            //decrements the current page number
            categoryPageNumber = --categoryPageNumber;
            if (categoryPageNumber < 1)
            {
                categoryPageNumber = 1;
            }
            lbl_current_category_page.Text = categoryPageNumber.ToString();

            foreach (int key in categoryBtnDict.Keys)
            {
                Button b = categoryBtnDict[key];
                int newTag = Convert.ToInt32(b.Tag) - 7;
                if (newTag > 0)
                {
                    b.Tag = newTag;
                }
            }
            if (categoryPageNumber == 1)
            {
                btn_previous_categories.Enabled = false;
            }

            hideExtraCategoryButtons();
            loadCategories();
        }

        private void btn_next_categories_Click(object sender, EventArgs e)
        {
            categoryPageNumber = ++categoryPageNumber;
            lbl_current_category_page.Text = categoryPageNumber.ToString();

            foreach (int key in categoryBtnDict.Keys)
            {
                Button b = categoryBtnDict[key];
                b.Tag = Convert.ToInt32(b.Tag) + 7;
            }
            if (categoryPageNumber > 1)
            {
                btn_previous_categories.Enabled = true;
            }
            hideExtraCategoryButtons();
            loadCategories();
        }


        private void btn_category_selected(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            System.Diagnostics.Debug.WriteLine("btn text: " + btn.Text);

            loadItemsDictWithFood(btn.Text);
            numberOfItems = getFoodItemsCount(btn.Text);
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
            
            CURRENT_SELECTED_CATEGORY = btn.Text;
        }
        #endregion




        private void btn_previous_items_Click(object sender, EventArgs e)
        {
            //decrements the current page number
            itemPageNumber = --itemPageNumber;
            if (itemPageNumber < 1)
            {
                itemPageNumber = 1;
            }
            lbl_current_item_page.Text = itemPageNumber.ToString();

            foreach (int key in itemBtnDict.Keys)
            {
                Button b = itemBtnDict[key];
                int newTag = Convert.ToInt32(b.Tag) - 16;
                if (newTag > 0)
                {
                    b.Tag = newTag;
                }
            }
            if (itemPageNumber == 1)
            {
                btn_previous_items.Enabled = false;
            }

            if (CURRENT_SELECTED_CATEGORY == btn_non_alcoholic_drinks.Text)
            {
                hideExtraItemButtons();
                loadItems();
            }
            else if (CURRENT_SELECTED_CATEGORY == btn_alcoholic_drinks.Text)
            {
                hideExtraItemButtons();
                loadItems();
            }
        }

        private int getDrinksCount(int nonORnot)
        {
            //connect to the database
            DataClassesDataContext db = new DataClassesDataContext();
            int count = 0;
            var query = from d in db.Drinks
                        where d.FoodDrinkCategoryID == nonORnot
                        select d;

            foreach (var d in query)
            {
                count++;
            }
            return count;
        }

        private int getFoodItemsCount(String foodItemCategory)
        {
            //connect to the database
            DataClassesDataContext db = new DataClassesDataContext();
            int count = 0;
            var query = from d in db.FoodDrinkCategories
                        where d.FoodDrinkCategoryName == foodItemCategory
                        select d;

            foreach (var d in query)
            {
                count++;
            }
            return count;
        }

        private void btn_next_items_Click(object sender, EventArgs e)
        {
            itemPageNumber = ++itemPageNumber;
            lbl_current_item_page.Text = itemPageNumber.ToString();

            foreach (int key in itemBtnDict.Keys)
            {
                Button b = itemBtnDict[key];
                b.Tag = Convert.ToInt32(b.Tag) + 16;
            }
            if (itemPageNumber > 1)
            {
                btn_previous_items.Enabled = true;
            }

            if (CURRENT_SELECTED_CATEGORY == btn_non_alcoholic_drinks.Text)
            {
                hideExtraItemButtons();
                loadItems();
            }
            else if (CURRENT_SELECTED_CATEGORY == btn_alcoholic_drinks.Text)
            {
                hideExtraItemButtons();
                loadItems();
            }
        }

        /// <summary>
        /// Hides any buttons that exceed the number of items
        /// </summary>
        private void hideExtraItemButtons()
        {
            foreach (int key in itemBtnDict.Keys)
            {
                Button b = itemBtnDict[key];
                b.Visible = true;
            }

            if (itemPageNumber == 1)
            {
                btn_previous_items.Enabled = false;
            }
            btn_next_items.Enabled = true;
            if (numberOfItems < (16 * itemPageNumber))
            {
                foreach (int key in itemBtnDict.Keys)
                {
                    Button b = itemBtnDict[key];
                    if (Convert.ToInt32(b.Tag) > numberOfItems)
                    {
                        b.Visible = false;
                        btn_next_items.Enabled = false;
                    }
                }
            }
        }

        private void setMaxNumberOfPagesForItems()
        {
            if (numberOfItems != 0)
            {
                maxItemPageNumber = (int)(numberOfItems / 16);
                int mod = maxItemPageNumber % numberOfItems;
                if (mod > 0 || numberOfItems <= 16)
                {
                    maxItemPageNumber++;
                }
                lbl_total_item_page.Text = maxItemPageNumber.ToString();
            }
        }

        private void loadItems()
        {
            clearItemsButtonText();

            int count = 0;
            if (itemPageNumber == 1)
            {
                count = 0;
            }
            else
            {
                count = (itemPageNumber - 1) * 16;
            }
            foreach (int key in itemBtnDict.Keys)
            {
                Button b = itemBtnDict[key];
                if (count < itemInfoDict.Count())
                {
                    String element = itemInfoDict.ElementAt(count).ToString();
                    element = element.Replace("[", "");
                    element = element.Replace("]", "");
                    String[] arr = element.Split(',');
                    b.Text = arr[1];
                }
                count++;
            }


            foreach (int key in itemBtnDict.Keys)
            {
                Button b = itemBtnDict[key];
                if (b.Text.Equals(""))
                {
                    b.Visible = false;
                }
            }
        }

        /// <summary>
        /// Loads all category and item buttons to dictionarys
        /// </summary>
        private void loadButtonsToDictionary()
        {
            DataClassesDataContext db = new DataClassesDataContext();

            foreach (Button control in categories_panel.Controls)
            {
                categoryBtnDict.Add(Convert.ToInt32(control.Tag), control);
            }

            
            var query = from c in db.FoodDrinkCategories
                        where c.FoodDrinkCategoryIsMenuItem == true
                        select c;

            if (query.Any())
            {
                foreach (var q in query)
                {
                    categoryInfoDict.Add(Convert.ToInt32(q.FoodDrinkCategoryID), q.FoodDrinkCategoryName);
                }
            }

            foreach (Button control in items_panel.Controls)
            {
                itemBtnDict.Add(Convert.ToInt32(control.Tag), control);
            }
        }

        private void loadItemsDictWithDrinks(int nonORnot)
        {
            itemInfoDict.Clear();
            DataClassesDataContext db = new DataClassesDataContext();
            var drinkQuery = from d in db.Drinks
                             where d.FoodDrinkCategoryID == nonORnot //4 is alcoholic, 2 is non-alcoholic
                             select d;

            if (drinkQuery.Any())
            {
                foreach (var q in drinkQuery)
                {
                    itemInfoDict.Add(Convert.ToInt32(q.DrinkID), q.DrinkName);
                }
            }
        }

        //var query = from r in db.Rooms
        //                join rd in db.ReservationDetails
        //                on r.RoomID equals rd.RoomID
        //                join rv in db.Reservations
        //                on rd.ReservationID equals rv.ReservationID
        //                join g in db.Guests
        //                on rv.GuestID equals g.GuestID
        //                where r.RoomStatus == 'C' & rv.ReservationStatus == 'A'
        //                select new { r.RoomID, g.GuestSurName };

        private void loadItemsDictWithFood(String foodCategory)
        {

            System.Diagnostics.Debug.WriteLine("foodCategory: " + foodCategory);
            itemInfoDict.Clear();
            DataClassesDataContext db = new DataClassesDataContext();
            var query = from m in db.MenuItems
                             join f in db.FoodDrinkCategories
                             on m.FoodDrinkCategoryID equals f.FoodDrinkCategoryID
                             where f.FoodDrinkCategoryName == foodCategory
                             select new { m.MenuItemID, f.FoodDrinkCategoryName};

            if (query.Any())
            {
                System.Diagnostics.Debug.WriteLine("any found? - yes!");
                foreach (var q in query)
                {
                    System.Diagnostics.Debug.WriteLine("insert to dict: " + q.FoodDrinkCategoryName);
                    itemInfoDict.Add(Convert.ToInt32(q.MenuItemID), q.FoodDrinkCategoryName);
                }
            }
        }

        private void clearItemsButtonText()
        {
            foreach (Button control in items_panel.Controls)
            {
                control.Text = "";
            }
        }

        private void btn_submit_order_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_cancel_order_Click(object sender, EventArgs e)
        {
            Close();
        }






        //FINISHED

        private void btn_non_alcoholic_drinks_Click(object sender, EventArgs e)
        {
            loadItemsDictWithDrinks(2);
            numberOfItems = getDrinksCount(2);
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
            CURRENT_SELECTED_CATEGORY = btn_non_alcoholic_drinks.Text;
        }

        private void btn_alcoholic_drinks_Click(object sender, EventArgs e)
        {
            loadItemsDictWithDrinks(4);
            numberOfItems = getDrinksCount(4);
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
            CURRENT_SELECTED_CATEGORY = btn_alcoholic_drinks.Text;
        }



    }
}
