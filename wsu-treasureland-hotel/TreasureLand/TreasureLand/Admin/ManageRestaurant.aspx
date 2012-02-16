<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageRestaurant.aspx.cs" Inherits="TreasureLand.Admin.ManageRestaurant" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:Button ID="btnManageCategories" runat="server" Text="Manage Categories" 
        onclick="btnManageCategory_Click" />
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
                Visible="true" ForeColor="Black" MaxLength="20"></asp:TextBox>
               <asp:Button ID="btnAddCategory" runat="server" Text="Add New Category" 
                onclick="btnAddNewCategory" ValidationGroup="vgAddCategory" />
          
            <asp:CompareValidator ID="cvAddCategory" runat="server" 
                ControlToValidate="txtCategory" Display="Dynamic" 
                ErrorMessage="Category Must be a String" ForeColor="Red" 
                Operator="DataTypeCheck" ValidationGroup="vgAddCategory">*</asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvAddCategory" runat="server" 
                ControlToValidate="txtCategory" ErrorMessage="You must enter a category" 
                ForeColor="Red" ValidationGroup="vgAddCategory">*</asp:RequiredFieldValidator>
            <asp:FilteredTextBoxExtender ID="txtCategory_FilteredTextBoxExtender" 
                runat="server" FilterMode="InvalidChars" 
                InvalidChars="!@#$%^&amp;*()_+-=1234567890[]{}\|;:'&quot;/.,&lt;&gt;?`~" 
                TargetControlID="txtCategory">
            </asp:FilteredTextBoxExtender>
            <asp:Label ID="lblCatAddError" runat="server" ForeColor="Red"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
                ValidationGroup="vgAddCategory" />
          
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
            PageSize="5" DataKeyNames="MenuItemID">
            <Columns>
                <asp:BoundField DataField="MenuItemID" HeaderText="MenuItemID" 
                    InsertVisible="False" ReadOnly="True" SortExpression="MenuItemID" />
                <asp:BoundField DataField="MenuItemName" HeaderText="MenuItemName" 
                    SortExpression="MenuItemName" />
                <asp:BoundField DataField="MenuItemPrice" HeaderText="MenuItemPrice" 
                    SortExpression="MenuItemPrice" />
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="gvDrink" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="LinqDataSource1" PageSize="5" DataKeyNames="MenuItemID" 
            Visible="False">
            <Columns>
                <asp:BoundField DataField="MenuItemID" HeaderText="MenuItemID" ReadOnly="True" 
                    SortExpression="MenuItemID" InsertVisible="False" />
                <asp:BoundField DataField="FoodDrinkCategoryID" 
                    HeaderText="FoodDrinkCategoryID" SortExpression="FoodDrinkCategoryID" />
                <asp:BoundField DataField="MenuItemName" HeaderText="MenuItemName" 
                    SortExpression="MenuItemName" />
                <asp:BoundField DataField="MenuItemPrice" HeaderText="MenuItemPrice" 
                    SortExpression="MenuItemPrice" />
            </Columns>
        </asp:GridView>
        <br />
    <asp:Button ID="btnAddMenuItem" runat="server" Text="Add Item" 
            onclick="btnAddMenuItem_Click" />
    
        <asp:LinqDataSource ID="ldsMenuItems" runat="server" 
            ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
            EntityTypeName=""  
            TableName="MenuItems" Where="FoodDrinkCategoryID == @FoodDrinkCategoryID1" 
            EnableUpdate="True" EnableDelete="True" EnableInsert="True">
            <WhereParameters>
                <asp:ControlParameter ControlID="ddlAddGetCategory" Name="FoodDrinkCategoryID1" 
                    PropertyName="SelectedValue" Type="Int16" />
            </WhereParameters>
        </asp:LinqDataSource>
    
        <asp:LinqDataSource ID="ldsDrinks" runat="server" 
            ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext"  
            TableName="MenuItems" Where="FoodDrinkCategoryID == @FoodDrinkCategoryID
Food" 
            EnableUpdate="True" EntityTypeName="">
            <WhereParameters>
                <asp:ControlParameter ControlID="ddlAddGetCategory" Name="newparameter" 
                    PropertyName="SelectedValue" />
            </WhereParameters>
        </asp:LinqDataSource>
    
        <br />
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
            ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
            EnableUpdate="True" EntityTypeName="" TableName="MenuItems" 
            Where="FoodDrinkCategoryID == @FoodDrinkCategoryID">
            <WhereParameters>
                <asp:ControlParameter ControlID="ddlAddGetCategory" Name="FoodDrinkCategoryID" 
                    PropertyName="SelectedValue" Type="Int16" />
            </WhereParameters>
        </asp:LinqDataSource>
        <br />
        <table class="style4" style="width: 45%">
            <tr>
                <td style="width: 114px">
                    <asp:Label ID="lblAddMenuItemName" runat="server" Text="Menu Item Name" 
                        Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddMenuItemName" runat="server" Visible="False"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtAddMenuItemName_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterMode="InvalidChars" 
                        InvalidChars="!@#$%^&amp;*()_+-=1234567890[]{}\|;:'&quot;/.,&lt;&gt;?`~" 
                        TargetControlID="txtAddMenuItemName">
                    </asp:FilteredTextBoxExtender>
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
        <td style="width: 144px">
            <asp:DropDownList ID="ddlChooseItemForPurchase" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlChooseItemForPurchase_SelectedIndexChanged">
                <asp:ListItem>Beverages</asp:ListItem>
                <asp:ListItem>Menu Items</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 312px">
            &nbsp;</td>
    </tr>
         <tr>
             <td style="width: 144px">
                 <asp:Label ID="lblPurchaseItemName" runat="server" Text="Beverage Name:"></asp:Label>
             </td>
             <td style="width: 312px">
                 <asp:DropDownList ID="ddlIngredientPurchase" runat="server">
                 </asp:DropDownList>
                 <asp:Button ID="btnAddListItemIngredient" runat="server" 
                     onclick="btnAddListItemIngredient_Click1" Text="Add New Ingredient" 
                     Visible="False" />
                 <asp:TextBox ID="txtIngredient2" runat="server" Visible="False"></asp:TextBox>
                 <asp:FilteredTextBoxExtender ID="txtIngredient2_FilteredTextBoxExtender" 
                     runat="server" Enabled="True" FilterMode="InvalidChars" 
                     InvalidChars="!@#$%^&amp;*()_+-=1234567890[]{}\|;:'&quot;/.,&lt;&gt;?`~" 
                     TargetControlID="txtIngredient2">
                 </asp:FilteredTextBoxExtender>
             </td>
         </tr>
    <tr>
        <td style="width: 144px">
            Purchase Price:</td>
        <td style="width: 312px">
            <asp:TextBox ID="txtPrice" runat="server" Width="94px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 144px">
            Qty:</td>
        <td style="width: 312px">
            <asp:TextBox ID="txtQty" runat="server" Width="96px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 144px">
            <asp:Button ID="btnAddItemToPurchase" runat="server" Text="Add Item to Purchase" 
                Width="157px" onclick="btnAddItemToPurchase_Click" Height="21px" />
        </td>
        <td style="width: 312px">
            &nbsp;<asp:Button ID="btnSubmitPurchase" runat="server" Enabled="False" 
                onclick="btnSubmitPurchase_Click" Text="Submit Purchase" Width="118px" />
        </td>
        <td style="width: 248px">
            &nbsp;</td>
    </tr>
         <tr>
             <td style="width: 144px">
                 <asp:Button ID="btnClear" runat="server" Enabled="False" 
                     onclick="btnClearPurchase" Text="Clear Purchase" Width="107px" />
             </td>
             <td style="width: 312px">
                 &nbsp;</td>
             <td style="width: 248px">
                 &nbsp;</td>
         </tr>
</table>
        <asp:GridView ID="gvshowIngredientPurchases" runat="server" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IngredientID" HeaderText="Ingredient" />
                <asp:BoundField DataField="IngredientPurchaseHistoryQty" HeaderText="Qty" />
                <asp:BoundField DataField="IngredientPurchaseHistoryPrice" HeaderText="Price" 
                    DataFormatString="{0:0.00}" />
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
