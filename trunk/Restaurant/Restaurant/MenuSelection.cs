using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Reflection;


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
 * 
 * An after thought of how this is done is that datasets would have been a better approach.
 */


namespace Restaurant
{
    //System.Diagnostics.Debug.WriteLine("");
    public partial class MenuSelection : Form
    {
        public static String LOGGED_IN_ID;
        public RefreshDelegateRoomSelection RefreshDelegateMenuSelection;

        //private bool isSelectedCategoryDrink = true;
        int ROOMNUMBER = 0;

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
        private int totalItemsKey = 0;
        private int totalItemsPosition = 0;
        SortedDictionary<int, String> totalDict = new SortedDictionary<int, String>();


        public MenuSelection()
        {
            InitializeComponent();
        }

        public MenuSelection(int selectedRoom, String guestSurname, String LoginID)
        {
            InitializeComponent();
            MinimizeBox = false;
            MaximizeBox = false;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            LOGGED_IN_ID = LoginID;

            if (selectedRoom != 0) // 0 means paying with cash
            {
                lbl_selectedRoom.Text = "Room: " + selectedRoom.ToString();
                lbl_guestSurname.Text = "Guest: " + guestSurname;
                ROOMNUMBER = Convert.ToInt32(selectedRoom);
            }
            else
            {
                lbl_selectedRoom.Text = "";
                lbl_guestSurname.Text = "";
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
            //HAS NOT BEEN TESTED
            if (numberOfCategories < 8)
            {
                nav_panel_categories.Visible = false;
            }

            #endregion

            #region Initial Items Setup - Non-Alcoholic
            //Loads the item dictionary with non-alcoholic drinks
            loadItemsDictWithDrinks("N");
            //gets the number of non-alcoholic drinks
            numberOfItems = getDrinksCount("N");
            //Hides any extra item buttons
            hideExtraItemButtons();
            //sets up the initial display to non-alcoholic drinks
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            //gets the max number of category pages and sets it to a label
            setMaxNumberOfPagesForItems();

            //HAS NOT BEEN TESTED
            if (numberOfItems < 17)
            {
                nav_panel_items.Visible = false;
            }
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
                        where c.FoodDrinkCategoryTypeID == "F"
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

        private int getDrinksCount(String beverage)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            int count = 0;

            var drinkQuery = from d in db.FoodDrinkCategories
                                join m in db.MenuItems
                                on d.FoodDrinkCategoryID equals m.FoodDrinkCategoryID
                                where d.FoodDrinkCategoryTypeID == beverage
                                select new { m.MenuItemID};

            if (drinkQuery.Any())
            {
                foreach (var q in drinkQuery)
                {
                    count++;
                }
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

            //HAS NOT BEEN TESTED
            if (numberOfItems > 16)
            {
                nav_panel_items.Visible = true;
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

        private void loadItemsDictWithDrinks(String beverage)
        {
            itemInfoDict.Clear();
            DataClassesDataContext db = new DataClassesDataContext();

            if (beverage.Equals("N"))
            {
                var drinkQuery = from d in db.FoodDrinkCategories
                                 join m in db.MenuItems
                                 on d.FoodDrinkCategoryID equals m.FoodDrinkCategoryID
                                 where d.FoodDrinkCategoryTypeID == beverage
                                 select new { m.MenuItemID, m.MenuItemName };

                if (drinkQuery.Any())
                {
                    foreach (var q in drinkQuery)
                    {
                        itemInfoDict.Add(Convert.ToInt32(q.MenuItemID), q.MenuItemName);
                    }
                }
            }
            else if (beverage.Equals("A"))
            {
                var drinkQuery = from d in db.FoodDrinkCategories
                                 join m in db.MenuItems
                                 on d.FoodDrinkCategoryID equals m.FoodDrinkCategoryID
                                 where d.FoodDrinkCategoryTypeID == beverage
                                 select new { m.MenuItemID, m.MenuItemName };

                if (drinkQuery.Any())
                {
                    foreach (var q in drinkQuery)
                    {
                        itemInfoDict.Add(Convert.ToInt32(q.MenuItemID), q.MenuItemName);
                    }
                }
            }
        }

        private void loadItemsDictWithDiscounts()
        {
            itemInfoDict.Clear();
            DataClassesDataContext db = new DataClassesDataContext();

            var drinkQuery = from d in db.FoodDrinkCategories
                                join m in db.MenuItems
                                on d.FoodDrinkCategoryID equals m.FoodDrinkCategoryID
                                where d.FoodDrinkCategoryTypeID == "D"
                                select new { m.MenuItemID, m.MenuItemName };

            if (drinkQuery.Any())
            {
                foreach (var q in drinkQuery)
                {
                    itemInfoDict.Add(Convert.ToInt32(q.MenuItemID), q.MenuItemName);
                }
            }
        }

        private int getDiscountsCount()
        {
            int count = 0;

            DataClassesDataContext db = new DataClassesDataContext();
            var drinkQuery = from d in db.FoodDrinkCategories
                             join m in db.MenuItems
                             on d.FoodDrinkCategoryID equals m.FoodDrinkCategoryID
                             where d.FoodDrinkCategoryTypeID == "D"
                             select new { m.MenuItemID, m.MenuItemName };

            foreach (var d in drinkQuery)
            {
                count++;
            }
            return count;
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
            loadItemsDictWithDrinks("N");
            numberOfItems = getDrinksCount("N");
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
        }

        private void btn_alcoholic_drinks_Click(object sender, EventArgs e)
        {
            resetItemPageNumber();
            loadItemsDictWithDrinks("A");
            numberOfItems = getDrinksCount("A");
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
        }

        private void menuItemSelected(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            String itemName = btn.Text.Trim();
            String itemPrice = getMoneyValueForItem(itemName);

            Decimal dec = Convert.ToDecimal(itemPrice);

            String totalLabel = lbl_grand_total.Text.ToString();
            Decimal total = Convert.ToDecimal(totalLabel);
            total = total + dec;
            if (total >= 0)
            {
                createTotalItemButton(itemPrice, itemName);
                lbl_grand_total.Text = total.ToString();
            }           
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
                        where c.FoodDrinkCategoryTypeID == "F"
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

        #region Total List of Items

        private void createTotalItemButton(String menuItemPrice, String menuItemName)
        {
            String buttonLabel = menuItemPrice + " - " + menuItemName;

            Button btn = new Button();
            btn.Click += new System.EventHandler(btn_remove_item_Click);

            Bitmap bitmap = Restaurant.Properties.Resources.button_item_total;
            btn.BackgroundImage = bitmap;
            

            btn.Height = 36;
            btn.Width = 166;
            btn.Location = new Point(20, 4 + totalItemsPosition * 36);
            btn.BackColor = Color.WhiteSmoke;
            btn.Tag = totalItemsKey;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Text = buttonLabel;

            totalDict.Add(totalItemsKey, buttonLabel);
            total_panel.Controls.Add(btn);
            totalItemsKey++;
            totalItemsPosition++;
        }

        private void btn_remove_item_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Remove item from order?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                Button btn = sender as Button;
                String buttonText = btn.Text;
                Boolean negativeNumber = false;
                if (buttonText.StartsWith("-"))
                {
                    buttonText = buttonText.Substring(1, buttonText.Length - 1);
                    negativeNumber = true;
                }

                int tag = Convert.ToInt32(btn.Tag);

                //subtract from totals
                String amount = getItemInfoFromButton(buttonText, 0);
                if (negativeNumber)
                {
                    amount = "-" + amount;
                    Decimal dec = Convert.ToDecimal(amount);
                    String totalLabel = lbl_grand_total.Text.ToString();
                    Decimal total = Convert.ToDecimal(totalLabel);
                    total = total - dec;
                    lbl_grand_total.Text = total.ToString();
                }
                else
                {
                    Decimal dec = Convert.ToDecimal(amount);
                    String totalLabel = lbl_grand_total.Text.ToString();
                    Decimal total = Convert.ToDecimal(totalLabel);
                    total = total - dec;
                    lbl_grand_total.Text = total.ToString();
                }
                

                totalDict.Remove(tag);
                recreateAllTotalButtons();
            }
        }

        private void recreateAllTotalButtons()
        {
            total_panel.Controls.Clear();

            totalItemsPosition = 0;

            foreach (var item in totalDict)
            {
                Button btn = new Button();
                btn.Click += new System.EventHandler(btn_remove_item_Click);
                btn.Height = 36;
                btn.Width = 166;
                btn.Location = new Point(20, 4 + totalItemsPosition * 36);
                btn.BackColor = Color.WhiteSmoke;
                btn.Tag = item.Key;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Text = item.Value;
                total_panel.Controls.Add(btn);
                totalItemsPosition++;
            }
        }

        private String getMoneyValueForItem(String menuItemName)
        {
            Decimal menuItemPrice = 0;

            DataClassesDataContext db = new DataClassesDataContext();
            var query = from d in db.MenuItems
                        where d.MenuItemName == menuItemName
                        select new { d.MenuItemPrice };

            if(query.Any())
            {
                foreach (var q in query)
                {
                    menuItemPrice = Convert.ToDecimal(q.MenuItemPrice);
                }
            }

            String value = String.Format("{0:N}", menuItemPrice);
            
            return value;
        }

        private String getItemInfoFromButton(String buttonName, int operation)
        {
            String itemInfo = "";
            String[] arr;

            arr = buttonName.Split('-');
            if (arr.Length > 0 && (operation == 1 || operation == 0))
            {
                // 0 = item amount
                // 1 = item name
                itemInfo = arr[operation].Trim();
            }
            return itemInfo;
        }

        #endregion

        private void btn_submit_order_Click(object sender, EventArgs e)
        {
            if (totalDict.Count() > 0)
            {
                Int16 reservationDetailID = 0;
                DataClassesDataContext db = new DataClassesDataContext();

                if (ROOMNUMBER != 0)
                {
                    var query = from r in db.Rooms
                                join rd in db.ReservationDetails
                                on r.RoomID equals rd.RoomID
                                where r.RoomNumbers == ROOMNUMBER.ToString() & rd.ReservationStatus == 'A'
                                select new { rd.ReservationDetailID };

                    if (query.Any())
                    {
                        foreach (var item in query)
                        {
                            reservationDetailID = Convert.ToInt16(item.ReservationDetailID);
                        }
                    }
                }

                //RESERVATIONDETAILBILLING
                ReservationDetailBilling reservationOBJ = new ReservationDetailBilling();

                if (ROOMNUMBER == 0)
                {
                    reservationOBJ.ReservationDetailID = null;
                }
                else
                {
                    reservationOBJ.ReservationDetailID = Convert.ToInt16(reservationDetailID);
                }

                reservationOBJ.BillingCategoryID = 1;
                reservationOBJ.BillingDescription = "food & drink purchase";
                reservationOBJ.BillingAmount = Convert.ToDecimal(lbl_grand_total.Text);
                reservationOBJ.BillingItemQty = 1;
                reservationOBJ.BillingItemDate = System.DateTime.Now;
                reservationOBJ.TransEmployee = LOGGED_IN_ID;

                db.ReservationDetailBillings.InsertOnSubmit(reservationOBJ);
                db.SubmitChanges();


                var rdbID = (from r in db.ReservationDetailBillings
                             select r.ReservationDetailBillingID).Max();

                foreach (var item in totalDict)//all purchase items in the list
                {
                    String itemInfo = item.Value;
                    if (itemInfo.StartsWith("-"))
                    {
                        itemInfo = itemInfo.Substring(1, itemInfo.Length - 1);
                    }
                    String itemName = getItemInfoFromButton(itemInfo, 1);

                    var query = from i in db.MenuItems
                                where i.MenuItemName == itemName
                                select new { i.MenuItemID, i.MenuItemPrice };

                    short menuItemID = 0;
                    decimal menuItemPrice = 0;

                    foreach (var menuItem in query)
                    {
                        menuItemID = menuItem.MenuItemID;
                        menuItemPrice = menuItem.MenuItemPrice;
                    }

                    //LINEITEM
                    LineItem lineOBJ = new LineItem();
                    lineOBJ.ReservationDetailBillingID = rdbID;
                    lineOBJ.MenuItemID = menuItemID;
                    lineOBJ.LineItemAmount = menuItemPrice;
                    db.LineItems.InsertOnSubmit(lineOBJ);
                    db.SubmitChanges();
                }

                Close();
            }
        }

        private void btn_cancel_order_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_discount_Click(object sender, EventArgs e)
        {
            resetItemPageNumber();
            loadItemsDictWithDiscounts();
            numberOfItems = getDiscountsCount();
            hideExtraItemButtons();
            loadItems();
            lbl_current_item_page.Text = itemPageNumber.ToString();
            setMaxNumberOfPagesForItems();
        }

        private void onClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDelegateMenuSelection.Invoke();
        }
    }
}
