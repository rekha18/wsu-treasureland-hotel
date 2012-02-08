using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restaurant
{
    //System.Diagnostics.Debug.WriteLine("");
    public partial class MenuSelection : Form
    {
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
            lbl_selectedRoom.Text = "Room: " + selectedRoom.ToString();
            
            //Load Buttons to Dictionaries
            loadButtonsToDictionary();

            //Get how many food categories exist
            numberOfCategories = getCategoryCount();
            
            //Hides any extra category buttons
            hideExtraCategoryButtons();

            //Load Categories
            loadCategories();
            lbl_current_category_page.Text = categoryPageNumber.ToString();

            //gets the max number of pages and sets it to a label
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

            //sets up the initial display to non-alcoholic drinks
            setNonAlcoholicDrinksToItemsDisplay();
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

        #endregion

        #region Drinks Items
        //-----------------------------------------------------//
        //Drinks Category
        //-----------------------------------------------------//

        /// <summary>
        /// Non-Alcoholic Drinks Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_non_alcoholic_drinks_Click(object sender, EventArgs e)
        {
            setNonAlcoholicDrinksToItemsDisplay();
        }

        private void setNonAlcoholicDrinksToItemsDisplay()
        {
            clearItemsButtonText();
            
            int count = 0;
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
        /// Alcoholic Drinks Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_alcoholic_drinks_Click(object sender, EventArgs e)
        {

        }


        #endregion




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

            var drinkQuery = from d in db.Drinks
                        where d.FoodDrinkCategoryID == 2 //2 is non-alcoholic
                        select d;

            if (drinkQuery.Any())
            {
                foreach (var q in drinkQuery)
                {
                    itemInfoDict.Add(Convert.ToInt32(q.DrinkID), q.DrinkName);
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
    }
}
