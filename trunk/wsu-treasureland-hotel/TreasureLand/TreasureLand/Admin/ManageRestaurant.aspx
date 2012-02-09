<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageRestaurant.aspx.cs" Inherits="TreasureLand.Admin.ManageRestaurant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:Button ID="btnManageCategories" runat="server" Text="Manage Categories" 
        onclick="btnManageCategories_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnManageMenuItems" runat="server" Text="Manage Menu Items" 
        onclick="btnManageMenuItems_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;<asp:Button ID="btnEnterPurchase" runat="server" Text="Create Purchase" 
        onclick="btnEnterPurchase_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="btnManageDrink" runat="server" Text="Manage Drinks" 
        onclick="btnManageDrink_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br />
<br />
<br />


<asp:MultiView ID="containerView" runat="server" ActiveViewIndex = "0">

<!--Begin View ActiveViewIndex[0]-->
    <asp:View ID="manageCatView" runat="server">
    <table class="style4">
    <tr>
    
        <td style="width: 248px">
          
            <asp:DropDownList ID="ddIngredient" runat="server" >
             
                
            </asp:DropDownList>
              <asp:TextBox ID="txtIngredient" runat="server" style="margin-left: 60px" 
                Visible="true" ForeColor="Black"></asp:TextBox>
               <asp:Button ID="btnAddCategory" runat="server" Text="Add New Category" 
                onclick="btnManageCategories_Click" />
          
            <asp:Label ID="lblCatAddError" runat="server" ForeColor="Red"></asp:Label>
          
        </td>
    </tr>



</table>
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </asp:View>

    <!--Begin View ActiveViewIndex[1]-->
    <asp:View ID="manageMenuItemsView" runat="server">
        <asp:GridView ID="gvMenuItems" runat="server" AutoGenerateColumns="False" 
            onrowcommand="gvMenuItems_RowCommand" 
            onrowediting="gvMenuItems_RowEditing" Width="491px">
            <Columns>
                <asp:BoundField DataField="MenuItemName" HeaderText="Menu Name" />
                <asp:BoundField DataField="FoodDrinkCategoryName" HeaderText="Category" />
                <asp:BoundField DataField="MenuItemPrice" HeaderText="Price" />
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="btnAddMenuItem" runat="server" Text="Add Item" />
    
        <br />
        <br />
        <br />
        <br />
    
    </asp:View>

      <!--Begin View ActiveViewIndex[2]-->
    <asp:View ID="createPurchaseView" runat="server">
     <table class="style4">
    <tr>
        <td style="width: 176px">
            Ingredient Purchased Name:</td>
        <td style="width: 248px">
            <asp:DropDownList ID="ddIngredient2" runat="server">
     
            </asp:DropDownList>
            <asp:Button ID="btnAddListItemIngredient" runat="server" 
                Text="Add New Ingredient" onclick="btnAddListItemIngredient_Click1" />
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
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </asp:View>

    <!--Begin View ActiveViewIndex[3]-->
    <asp:View ID="manageDrinksView" runat="server">

    </asp:View>

    </asp:MultiView>
&nbsp;
    
<br />
<br />
</asp:Content>
