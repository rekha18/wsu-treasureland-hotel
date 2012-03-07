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
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnIngredients" 
        runat="server" onclick="btnIngredients_Click" Text="Manage Recipes" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br />
<br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<br />


<asp:MultiView ID="containerView" runat="server" ActiveViewIndex = "1">

<!--Begin View ActiveViewIndex[0]-->
    <asp:View ID="viewLinkIngredients" runat="server">
        <table class="style4">
            <tr>
                <td style="width: 286px" valign="top">
                    <br />
                    <asp:Label ID="lblChooseMenuItem" runat="server" Font-Bold="True" 
                        Text="Choose Menu Item:"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlMenuItemIngredients" runat="server" 
                        AutoPostBack="True" DataSourceID="ldsMenuItemddl" DataTextField="MenuItemName" 
                        DataValueField="MenuItemID">
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="ldsMenuItemddl" runat="server" 
                        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                        EntityTypeName="" TableName="MenuItems" 
                        Where="FoodDrinkCategoryID &gt; @FoodDrinkCategoryID">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="3" Name="FoodDrinkCategoryID" Type="Int16" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                </td>
                <td valign="top">
                    <br />
                    <asp:Label ID="lblChooseIngredient" runat="server" Font-Bold="True" 
                        Text="Choose Ingredient:"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlIngredients" runat="server" 
                        DataSourceID="ldsIngredients" DataTextField="IngredientName" 
                        DataValueField="IngredientID">
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="ldsIngredients" runat="server" 
                        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                        EntityTypeName="" TableName="Ingredients">
                    </asp:LinqDataSource>
                    <asp:Label ID="lblIngredientQty" runat="server" Text="Qty"></asp:Label>
                    <asp:TextBox ID="txtMenuItemIngredientQty" runat="server"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtMenuItemIngredientQty_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" TargetControlID="txtMenuItemIngredientQty" 
                        ValidChars="1234567890.">
                    </asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvLinkIngredients" runat="server" 
                        ControlToValidate="txtMenuItemIngredientQty" Display="Dynamic" 
                        ErrorMessage="Qty is required" ForeColor="Red" ValidationGroup="LinkIngredient">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvLinkIngredient" runat="server" 
                        ControlToValidate="txtMenuItemIngredientQty" Display="Dynamic" 
                        ErrorMessage="Qty must be a number" ForeColor="Red" Operator="DataTypeCheck" 
                        Type="Double" ValidationGroup="LinkIngredient">*</asp:CompareValidator>
                    <asp:CompareValidator ID="cvIngredientQtyAmount" runat="server" 
                        ControlToValidate="txtMenuItemIngredientQty" Display="Dynamic" 
                        ErrorMessage="Qty must be less than 100" ForeColor="Red" Operator="LessThan" 
                        Type="Double" ValidationGroup="LinkIngredient" ValueToCompare="100">*</asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ForeColor="Red" 
                        ValidationGroup="LinkIngredient" />
                </td>
            </tr>
            <tr>
                <td style="width: 286px">
                    <asp:ListBox ID="lbMenuItems" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="IngredientName" DataValueField="MenuItemIngredientID">
                    </asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:TreasurelandDB %>" 
                        SelectCommand="SELECT MenuItemIngredient.MenuItemIngredientID, (Ingredient.IngredientName + ' ' + Cast(MenuItemIngredient.MenuItemIngredientQty as varchar(10))) AS IngredientName 
FROM MenuItemIngredient INNER JOIN Ingredient ON MenuItemIngredient.IngredientID = Ingredient.IngredientID WHERE (MenuItemIngredient.MenuItemID = @MenuItemID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlMenuItemIngredients" Name="MenuItemID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 286px">
                    <asp:Button ID="btnRemoveIngredient" runat="server" 
                        onclick="btnRemoveIngredient_Click" Text="Remove Selected Ingredient" />
                </td>
                <td>
                    <asp:Button ID="btnAddIngredientToMenuItem" runat="server" 
                        onclick="btnAddIngredientToMenuItem_Click" Text="Add Selected Ingredient" 
                        Width="174px" ValidationGroup="LinkIngredient" />
                </td>
            </tr>
            <tr>
                <td style="width: 286px">
                    <br />
                    <strong>Add new Ingredient<br /> </strong>
                    <br />
                    <asp:Label ID="lblAddIngredient" runat="server" Text="Name:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtIngredient" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtIngredient_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterMode="InvalidChars" 
                        InvalidChars="!@#$%^&amp;*()_+-=[]{}\|;:'&quot;/.,&lt;&gt;?`~" 
                        TargetControlID="txtIngredient">
                    </asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvAddIngredient" runat="server" 
                        ControlToValidate="txtIngredient" Display="Dynamic" 
                        ErrorMessage="Name is required" ForeColor="Red" 
                        ValidationGroup="vgAddIngredient">*</asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="lblAddIngredientDescriptino" runat="server" Text="Description:"></asp:Label>
                    <asp:TextBox ID="txtIngredientComments" runat="server" Height="46px" 
                        onKeyUp="javascript:Count('MainContent_adminContentHolder_txtIngredientComments');"
                        onKeyDown="javascript:Count('MainContent_adminContentHolder_txtIngredientComments');" 
                        MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtIngredientComments_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterMode="InvalidChars" 
                        InvalidChars="!@#$%^&amp;*()_+-=[]{}\|;:'&quot;/.,&lt;&gt;?`~" 
                        TargetControlID="txtIngredientComments">
                    </asp:FilteredTextBoxExtender>
                    <div id="charsleft">
                        &nbsp;
                    </div>
                    <asp:Button ID="btnAddListItemIngredient" runat="server" 
                        onclick="btnAddListItemIngredient_Click1" Text="Add New Ingredient" 
                        ValidationGroup="vgAddIngredient" />
                    <asp:Button ID="btnAddIngredientCancel" runat="server" 
                        onclick="btnAddIngredientCancel_Click" Text="Cancel" Visible="False" />
                    <asp:ValidationSummary ID="vsAddNewIngredient" runat="server" ForeColor="Red" 
                        ValidationGroup="vgAddIngredient" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 286px">
                    <asp:Label ID="lblIngredientInsert" runat="server" ForeColor="Red" 
                        Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="manageCatView" runat="server">
    <table class="style4">
    <tr>
    
        <td style="width: 248px">
          
            <asp:Label ID="lblCurrentCategories" runat="server" Font-Bold="True" 
                Text="Current Categories"></asp:Label>
            <br />
          
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
                InvalidChars="!@#$%^&amp;*_+=-{}[]\|;:'&quot;" 
                TargetControlID="txtCategory">
            </asp:FilteredTextBoxExtender>
            <asp:Label ID="lblCatAddError" runat="server" ForeColor="Red"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
                ValidationGroup="vgAddCategory" />
          
            <br />
            <br />
            <asp:Label ID="lblEditCategories" runat="server" Font-Bold="True" 
                Text="Edit Categories"></asp:Label>
            <asp:GridView ID="gvEditCategories" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataKeyNames="FoodDrinkCategoryID" DataSourceID="ldsCategories" PageSize="5">
                <Columns>
                    <asp:BoundField DataField="FoodDrinkCategoryID" HeaderText="ID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="FoodDrinkCategoryID" />
                    <asp:TemplateField HeaderText="Name" SortExpression="FoodDrinkCategoryName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditCategories" runat="server" 
                                Text='<%# Bind("FoodDrinkCategoryName") %>'></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtEditCategories_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterMode="InvalidChars" 
                                InvalidChars="!~`@#$%^&amp;*-_=+[{]}\|';:&lt;&gt;?" 
                                TargetControlID="txtEditCategories">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvEditCategories" runat="server" 
                                ControlToValidate="txtEditCategories" Display="Dynamic" 
                                ErrorMessage="Name is required" ForeColor="Red" ValidationGroup="vgcategories">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# Bind("FoodDrinkCategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type" SortExpression="FoodDrinkCategoryTypeID">
                        <EditItemTemplate>
                            <asp:Label ID="Label3" runat="server" 
                                Text='<%# Eval("FoodDrinkCategoryTypeID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" 
                                Text='<%# Bind("FoodDrinkCategoryTypeID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" ShowEditButton="True" 
                        ValidationGroup="vgcategories" />
                </Columns>
            </asp:GridView>
            <asp:ValidationSummary ID="vsCategories" runat="server" ForeColor="Red" 
                ValidationGroup="vgcategories" />
            <asp:LinqDataSource ID="ldsCategories" runat="server" 
                ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                EnableUpdate="True" EntityTypeName="" TableName="FoodDrinkCategories">
            </asp:LinqDataSource>
          
        </td>
    </tr>



</table>
<br />
        &nbsp;&nbsp;&nbsp;
    </asp:View>

    <!--Begin View ActiveViewIndex[1]-->
    <asp:View ID="manageMenuItemsView" runat="server">
        <strong>Choose a category type:&nbsp;</strong>&nbsp;<asp:DropDownList ID="ddlChooseItem" runat="server" 
            AutoPostBack="True" onselectedindexchanged="ddlChooseItem_SelectedIndexChanged">
            <asp:ListItem>Beverages</asp:ListItem>
            <asp:ListItem>Menu Items</asp:ListItem>
            <asp:ListItem Value="Discounts">Discounts</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblCategory" runat="server" Text="Choose a category:" 
            style="font-weight: 700"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="ddlAddGetCategory" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlAddGetCategory_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <asp:GridView ID="gvMenuItems" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="ldsMenuItems" 
            PageSize="5" DataKeyNames="MenuItemID" 
            onrowcancelingedit="gvMenuItems_RowCancelingEdit" 
            onrowediting="gvMenuItems_RowEditing" onrowupdated="gvMenuItems_RowUpdated">
            <Columns>
                <asp:BoundField DataField="MenuItemID" HeaderText="Item ID" 
                    InsertVisible="False" ReadOnly="True" SortExpression="MenuItemID" />
                <asp:TemplateField HeaderText="Item Name" SortExpression="MenuItemName">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditMenuItem" runat="server" MaxLength="30" 
                            Text='<%# Bind("MenuItemName") %>'></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txtEditMenuItem_FilteredTextBoxExtender" 
                            runat="server" Enabled="True" FilterMode="InvalidChars" 
                            TargetControlID="txtEditMenuItem" 
                            InvalidChars="!@#$%^&amp;*()_+=-[]\{}|';&quot;:/.,&lt;&gt;?">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfvEditMenuItemName" runat="server" 
                            ControlToValidate="txtEditMenuItem" Display="Dynamic" 
                            ErrorMessage="Name is required" ForeColor="Red" ValidationGroup="EditMenuItem">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("MenuItemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Price" SortExpression="MenuItemPrice">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditMenuItemPrice" runat="server" 
                            Text='<%# Bind("MenuItemPrice", "{0:#.##}") %>' ValidationGroup="EditMenuItem"></asp:TextBox>
                        <asp:CompareValidator ID="cvEditMenuItemPrice" runat="server" 
                            ControlToValidate="txtEditMenuItemPrice" Display="Dynamic" 
                            ErrorMessage="Price must be a number" ForeColor="Red" Operator="DataTypeCheck" 
                            Type="Currency" ValidationGroup="EditMenuItem">*</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="rfvEditMenuItemPrice" runat="server" 
                            ControlToValidate="txtEditMenuItemPrice" Display="Dynamic" 
                            ErrorMessage="Price is required" ForeColor="Red" ValidationGroup="EditMenuItem">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("MenuItemPrice", "{0:#.##}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="Update" ValidationGroup="EditMenuItem" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" 
            ValidationGroup="EditMenuItem" />
        <br />
        <br />
    <asp:Button ID="btnAddMenuItem" runat="server" Text="Add Item" 
            onclick="btnAddMenuItem_Click" style="height: 26px" />
    
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
                    <asp:TextBox ID="txtAddMenuItemName" runat="server" Visible="False" 
                        MaxLength="30"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtAddMenuItemName_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterMode="InvalidChars" 
                        InvalidChars="!@#$%^&amp;*()_+=[]{}\|;:'&quot;/,&lt;&gt;?`~" 
                        TargetControlID="txtAddMenuItemName">
                    </asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvMenuItemName" runat="server" 
                        ControlToValidate="txtAddMenuItemName" ErrorMessage="Name is required" 
                        ForeColor="Red" ValidationGroup="MenuItem">*</asp:RequiredFieldValidator>
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
                    <asp:TextBox ID="txtAddPrice" runat="server" Visible="False" MaxLength="7"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtAddPrice_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" TargetControlID="txtAddPrice" 
                        ValidChars="1234567890.-">
                    </asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvMenuItemPrice" runat="server" 
                        ControlToValidate="txtAddPrice" ErrorMessage="Price is required" 
                        ForeColor="Red" ValidationGroup="MenuItem">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvMenuItemPrice" runat="server" 
                        ControlToValidate="txtAddPrice" ErrorMessage="Price must be a number" 
                        ForeColor="Red" Operator="DataTypeCheck" Type="Currency" 
                        ValidationGroup="MenuItem">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 114px">
                    <asp:Button ID="btnAddSubmit" runat="server" onclick="btnAddSubmit_Click" 
                        Text="Submit" Visible="False" ValidationGroup="MenuItem" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                        Text="Cancel" Visible="False" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
            ValidationGroup="MenuItem" ForeColor="Red" />
        <br />
        <br />
    
    </asp:View>

      <!--Begin View ActiveViewIndex[2]-->
    <asp:View ID="createPurchaseView" runat="server">
     <table class="style4">
    <tr>
        <td style="width: 144px; height: 54px;">
            <strong>Choose Category Type:<asp:DropDownList ID="ddlChooseItemForPurchase" 
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlChooseItemForPurchase_SelectedIndexChanged">
                <asp:ListItem>Beverages</asp:ListItem>
                <asp:ListItem>Menu Items</asp:ListItem>
            </asp:DropDownList>
            </strong>
        </td>
        <td style="width: 312px; height: 54px;">
            &nbsp;</td>
    </tr>
         <tr>
             <td style="width: 144px; height: 34px;">
                 <asp:Label ID="lblPurchaseItemName" runat="server" Text="Beverage Name:"></asp:Label>
             </td>
             <td style="width: 312px; height: 34px;">
                 <asp:DropDownList ID="ddlIngredientPurchase" runat="server">
                 </asp:DropDownList>
                 <br />
             </td>
         </tr>
    <tr>
        <td style="width: 144px">
            <asp:Label ID="lblPrice" runat="server" Text="Purchase Price:"></asp:Label>
        </td>
        <td style="width: 312px">
            <asp:TextBox ID="txtPrice" runat="server" Width="94px" MaxLength="7"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAddIngredientPurchasePrice" runat="server" 
                ControlToValidate="txtPrice" ErrorMessage="Purchase price is required" 
                ForeColor="Red" ValidationGroup="AddIngredient">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvAddIngredientPurchasePrice" runat="server" 
                ControlToValidate="txtPrice" ErrorMessage="Purchase Price must be a number" 
                ForeColor="Red" Operator="DataTypeCheck" Type="Currency" 
                ValidationGroup="AddIngredient">*</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 144px">
            <asp:Label ID="lblQty" runat="server" Text="Qty:"></asp:Label>
        </td>
        <td style="width: 312px">
            <asp:TextBox ID="txtQty" runat="server" Width="96px" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAddIngredientQty" runat="server" 
                ControlToValidate="txtQty" ErrorMessage="Qty is required" ForeColor="Red" 
                ValidationGroup="AddIngredient">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvAddIngredientQty" runat="server" 
                ErrorMessage="Qty must be a number" ForeColor="Red" Type="Double" 
                ValidationGroup="AddIngredient" ControlToValidate="txtQty" 
                Operator="DataTypeCheck">*</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 144px">
            <asp:Button ID="btnAddItemToPurchase" runat="server" Text="Add Item to Purchase" 
                Width="157px" onclick="btnAddItemToPurchase_Click" Height="21px" 
                ValidationGroup="AddIngredient" />
        </td>
        <td style="width: 312px">
            &nbsp;<asp:Button ID="btnSubmitPurchase" runat="server" Enabled="False" 
                onclick="btnSubmitPurchase_Click" Text="Submit Purchase" Width="118px" 
                Visible="False" />
        </td>
        <td style="width: 248px">
            &nbsp;</td>
    </tr>
         <tr>
             <td style="width: 144px">
                 <asp:Button ID="btnClear" runat="server" Enabled="False" 
                     onclick="btnClearPurchase" Text="Clear Purchase" Width="107px" 
                     Visible="False" />
             </td>
             <td style="width: 312px">
                 &nbsp;</td>
             <td style="width: 248px">
                 &nbsp;</td>
         </tr>
</table>
        <asp:ValidationSummary ID="vsAddIngredient" runat="server" 
            ValidationGroup="AddIngredient" ForeColor="Red" />
        <br />
        <asp:GridView ID="gvshowIngredientPurchases" runat="server" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Ingredient">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IngredientID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# GetItemName((Eval("IngredientID")).ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
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
<script type="text/javascript">
    function Count(text) {
        //asp.net textarea maxlength doesnt work; do it by hand
        var maxlength = 200; //set your value here (or add a parm and pass it in)
        var object = document.getElementById(text)  //get your object
        var string = object.value;
        if (string.length > maxlength) {
            object.value = string.substring(0, maxlength); //truncate the value
        }
        if ((maxlength - string.length) >= 0)
            document.getElementById("charsleft").innerHTML = (maxlength - string.length) + " chars left";
    }
</script>
</asp:Content>
