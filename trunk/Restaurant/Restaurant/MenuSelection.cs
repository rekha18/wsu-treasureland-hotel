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
        //CATEGORIES
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

        //TOTALS
        private int totalItemsCount = 0;
        SortedDictionary<int, String> totalDict = new SortedDictionary<int, String>();


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

            loadButtonsToDictionary();
            numberOfCategories = getCategoryCount();
            hideExtraCategoryButtons();
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
                        select new { c.FoodDrinkCategoryID };

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
            String selectedCategory = btn.Text.Trim();

            resetItemPageNumber();
            loadItemsDictWithFood(selectedCategory);
            numberOfItems = getFoodItemsCount(selectedCategory);
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
        }

        #endregion

        #region Food/Drink Items

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

            hideExtraItemButtons();
            loadItems();
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


            hideExtraItemButtons();
            loadItems();
        }

        private int getDrinksCount(int nonORnot)
        {
            //connect to the database
            DataClassesDataContext db = new DataClassesDataContext();
            int count = 0;
            var query = from d in db.Drinks
                        where d.FoodDrinkCategoryID == nonORnot
                        select new { d.FoodDrinkCategoryID };

            foreach (var d in query)
            {
                count++;
            }
            return count;
        }

        private int getFoodItemsCount(String foodCategory)
        {
            //connect to the database
            DataClassesDataContext db = new DataClassesDataContext();
            int count = 0;
            var query = from m in db.MenuItems
                        join f in db.FoodDrinkCategories
                        on m.FoodDrinkCategoryID equals f.FoodDrinkCategoryID
                        where f.FoodDrinkCategoryName == foodCategory
                        select new { m.MenuItemID };

            foreach (var d in query)
            {
                count++;
            }
            return count;
        }

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

        private void resetItemPageNumber()
        {
            if (itemPageNumber != 1)
            {
                itemPageNumber = 1;
                lbl_current_item_page.Text = itemPageNumber.ToString();
            }

            int count = 1;
            foreach (int key in itemBtnDict.Keys)
            {
                Button b = itemBtnDict[key];
                b.Tag = count;
                count++;
            }
            btn_previous_items.Enabled = false;
        }

        private void loadItemsDictWithDrinks(int nonORnot)
        {
            itemInfoDict.Clear();
            DataClassesDataContext db = new DataClassesDataContext();
            var drinkQuery = from d in db.Drinks
                             where d.FoodDrinkCategoryID == nonORnot //4 is alcoholic, 2 is non-alcoholic
                             select new { d.DrinkID, d.DrinkName };

            if (drinkQuery.Any())
            {
                foreach (var q in drinkQuery)
                {
                    itemInfoDict.Add(Convert.ToInt32(q.DrinkID), q.DrinkName);
                }
            }
        }

        private void loadItemsDictWithFood(String foodCategory)
        {
            itemInfoDict.Clear();
            DataClassesDataContext db = new DataClassesDataContext();
            var query = from m in db.MenuItems
                        join f in db.FoodDrinkCategories
                        on m.FoodDrinkCategoryID equals f.FoodDrinkCategoryID
                        where f.FoodDrinkCategoryName == foodCategory
                        select new { m.MenuItemID, m.MenuItemName };

            if (query.Any())
            {
                foreach (var q in query)
                {
                    itemInfoDict.Add(Convert.ToInt32(q.MenuItemID), q.MenuItemName);
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

        private void btn_non_alcoholic_drinks_Click(object sender, EventArgs e)
        {
            resetItemPageNumber();
            loadItemsDictWithDrinks(2);
            numberOfItems = getDrinksCount(2);
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
        }

        private void btn_alcoholic_drinks_Click(object sender, EventArgs e)
        {
            resetItemPageNumber();
            loadItemsDictWithDrinks(4);
            numberOfItems = getDrinksCount(4);
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
        }

        private void menuItemSelected(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            String menuItemName = btn.Text.Trim();
            createTotalItemButton(menuItemName);
        }

        #endregion

        #region Helper Methods for Categroies and Items

        private void loadButtonsToDictionary()
        {
            DataClassesDataContext db = new DataClassesDataContext();

            foreach (Button control in categories_panel.Controls)
            {
                categoryBtnDict.Add(Convert.ToInt32(control.Tag), control);
            }


            var query = from c in db.FoodDrinkCategories
                        where c.FoodDrinkCategoryIsMenuItem == true
                        select new { c.FoodDrinkCategoryID, c.FoodDrinkCategoryName };

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

        #endregion


        private void createTotalItemButton(String menuItemName)
        {
            Button btn = new Button();
            btn.Click += new System.EventHandler(btn_remove_item_Click);
            btn.Height = 36;
            btn.Width = 166;
            btn.Location = new Point(20, 4 + totalItemsCount * 36);
            btn.BackColor = Color.WhiteSmoke;
            btn.Tag = totalDict.Count();
            btn.Text = menuItemName;
            total_panel.Controls.Add(btn);
            System.Diagnostics.Debug.WriteLine("button added.");
            totalItemsCount++;
        }


        private void btn_remove_item_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("button removed.");
            Button btn = sender as Button;
            total_panel.Controls.Remove(btn);
        }

        private void btn_submit_order_Click(object sender, EventArgs e)
        {
            foreach (var item in totalDict)
            {
                //loop through and do an insert for each item
            }

            Close();
        }

        private void btn_cancel_order_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
