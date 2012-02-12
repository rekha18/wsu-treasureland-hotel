<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageRestaurant.aspx.cs" Inherits="TreasureLand.Admin.ManageRestaurant" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:Button ID="btnManageCategories" runat="server" Text="Manage Categories" 
        onclick="btnManageCategories_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="btnManageMenuItems" runat="server" Text="Manage Items" 
        onclick="btnManageMenuItems_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; <asp:Button ID="btnEnterPurchase" 
        runat="server" Text="Create Purchase" 
        onclick="btnEnterPurchase_Click" Width="168px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br />
<br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<br />


<asp:MultiView ID="containerView" runat="server" ActiveViewIndex = "0">

<!--Begin View ActiveViewIndex[0]-->
    <asp:View ID="manageCatView" runat="server">
    <table class="style4">
    <tr>
    
        <td style="width: 248px">
          
            <asp:DropDownList ID="ddlCategory" runat="server" >
             
                
            </asp:DropDownList>
              <asp:TextBox ID="txtCategory" runat="server" style="margin-left: 60px" 
                Visible="true" ForeColor="Black"></asp:TextBox>
               <asp:FilteredTextBoxExtender ID="txtCategory_FilteredTextBoxExtender" 
                runat="server" FilterMode="InvalidChars" 
                InvalidChars="!@#$%^&amp;*()_+-=[]{}\|;:'&quot;/.,&lt;&gt;?`~" 
                TargetControlID="txtCategory">
            </asp:FilteredTextBoxExtender>
               <asp:Button ID="btnAddCategory" runat="server" Text="Add New Category" 
                onclick="btnManageCategories_Click" />
          
            <asp:Label ID="lblCatAddError" runat="server" ForeColor="Red"></asp:Label>
          
        </td>
    </tr>



</table>
<br />
        &nbsp;&nbsp;&nbsp;
    </asp:View>

    <!--Begin View ActiveViewIndex[1]-->
    <asp:View ID="manageMenuItemsView" runat="server">
        &nbsp;&nbsp;<asp:DropDownList ID="ddlChooseItem" runat="server" 
            AutoPostBack="True" onselectedindexchanged="ddlChooseItem_SelectedIndexChanged">
            <asp:ListItem>Beverages</asp:ListItem>
            <asp:ListItem>Menu Items</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblCategory" runat="server" Text="Choose a category:"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="ddlAddGetCategory" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlAddGetCategory_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <asp:GridView ID="gvMenuItems" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="ldsMenuItems" 
            PageSize="5">
            <Columns>
                <asp:BoundField DataField="MenuItemName" HeaderText="Name" ReadOnly="True" 
                    SortExpression="MenuItemName" />
                <asp:BoundField DataField="MenuItemPrice" HeaderText="Price" ReadOnly="True" 
                    SortExpression="MenuItemPrice" />
                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="gvDrink" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="ldsDrinks" 
            Enabled="False" PageSize="5">
            <Columns>
                <asp:BoundField DataField="DrinkName" HeaderText="DrinkName" ReadOnly="True" 
                    SortExpression="DrinkName" />
                <asp:BoundField DataField="DrinkRetailSalePrice" 
                    HeaderText="DrinkRetailSalePrice" ReadOnly="True" 
                    SortExpression="DrinkRetailSalePrice" />
                <asp:BoundField DataField="FoodDrinkCategoryID" 
                    HeaderText="FoodDrinkCategoryID" ReadOnly="True" 
                    SortExpression="FoodDrinkCategoryID" />
                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <br />
    <asp:Button ID="btnAddMenuItem" runat="server" Text="Add Item" 
            onclick="btnAddMenuItem_Click" />
    
        <asp:LinqDataSource ID="ldsMenuItems" runat="server" 
            ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
            EntityTypeName="" 
            Select="new (MenuItemName, MenuItemPrice)" 
            TableName="MenuItems" Where="FoodDrinkCategoryID == @FoodDrinkCategoryID1" 
            EnableUpdate="True">
            <WhereParameters>
                <asp:ControlParameter ControlID="ddlAddGetCategory" Name="FoodDrinkCategoryID1" 
                    PropertyName="SelectedValue" Type="Int16" />
            </WhereParameters>
        </asp:LinqDataSource>
    
        <asp:LinqDataSource ID="ldsDrinks" runat="server" 
            ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
            EntityTypeName="" Select="new (DrinkName, DrinkRetailSalePrice, FoodDrinkCategoryID)" 
            TableName="Drinks" Where="FoodDrinkCategoryID == @FoodDrinkCategoryID1" 
            EnableUpdate="True">
            <WhereParameters>
                <asp:ControlParameter ControlID="ddlAddGetCategory" Name="FoodDrinkCategoryID1" 
                    PropertyName="SelectedValue" Type="Int16" />
            </WhereParameters>
        </asp:LinqDataSource>
    
        <br />
        <br />
        <table class="style4" style="width: 45%">
            <tr>
                <td style="width: 114px">
                    <asp:Label ID="lblAddMenuItemName" runat="server" Text="Menu Item Name" 
                        Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddMenuItemName" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 114px">
                    <asp:Label ID="lblAddCategory" runat="server" Text="Category" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAddCategory" runat="server" Visible="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 114px">
                    <asp:Label ID="lblAddPrice" runat="server" Text="Price" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddPrice" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 114px">
                    <asp:Button ID="btnAddSubmit" runat="server" onclick="btnAddSubmit_Click" 
                        Text="Submit" Visible="False" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                        Text="Cancel" Visible="False" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    
    </asp:View>

      <!--Begin View ActiveViewIndex[2]-->
    <asp:View ID="createPurchaseView" runat="server">
     <table class="style4">
    <tr>
        <td style="width: 176px">
            <asp:DropDownList ID="ddlChooseItemForPurchase" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlChooseItemForPurchase_SelectedIndexChanged">
                <asp:ListItem>Beverages</asp:ListItem>
                <asp:ListItem>Menu Items</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 248px">
            &nbsp;</td>
    </tr>
         <tr>
             <td style="width: 176px">
                 <asp:Label ID="lblPurchaseItemName" runat="server" Text="Beverage Name:"></asp:Label>
             </td>
             <td style="width: 248px">
                 <asp:DropDownList ID="ddIngredient2" runat="server">
                 </asp:DropDownList>
                 <asp:Button ID="btnAddListItemIngredient" runat="server" 
                     onclick="btnAddListItemIngredient_Click1" Text="Add New Ingredient" 
                     Visible="False" />
                 <asp:TextBox ID="txtIngredient2" runat="server" Visible="False"></asp:TextBox>
             </td>
         </tr>
    <tr>
        <td style="width: 176px">
            Purchase Price:</td>
        <td style="width: 248px">
            <asp:TextBox ID="txtPrice" runat="server" Width="94px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 176px">
            Qty:</td>
        <td style="width: 248px">
            <asp:TextBox ID="txtQty" runat="server" Width="96px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 176px">
            <asp:Button ID="btnAddItemToPurchase" runat="server" Text="Add Item to Purchase" 
                Width="165px" onclick="btnAddItemToPurchase_Click" />
        </td>
        <td style="width: 248px">
            <asp:Button ID="btnSubmitPurchase" runat="server" Text="Submit Purchase" 
                Width="165px" onclick="btnSubmitPurchase_Click" />
        </td>
    </tr>
</table>
        <asp:GridView ID="gvshowIngredientPurchases" runat="server" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IngredientPurchaseHistoryID" 
                    HeaderText="Purchase ID" />
                <asp:BoundField DataField="IngredientID" HeaderText="Ingredient" />
                <asp:BoundField DataField="IngredientPurchaseHistoryPrice" HeaderText="Price" />
                <asp:BoundField DataField="IngredientPurchaseHistoryQty" HeaderText="Qty" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvShowDrinkPurchases" runat="server" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="DrinkPurchaseID" HeaderText="Purchase ID" />
                <asp:BoundField DataField="DrinkPurchaseHistoryWholesalePrice" 
                    HeaderText="Wholesale Price" />
                <asp:BoundField DataField="DrinkPurchaseHistoryQty" HeaderText="Qty" />
                <asp:BoundField DataField="DrinkID" HeaderText="Drink Name" />
            </Columns>
        </asp:GridView>
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </asp:View>

    <!--Begin View ActiveViewIndex[3]-->

    </asp:MultiView>
&nbsp;
    
<br />
<br />
</asp:Content>
