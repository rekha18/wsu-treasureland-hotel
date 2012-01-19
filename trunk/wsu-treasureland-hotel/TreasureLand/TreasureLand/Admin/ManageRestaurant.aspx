<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageRestaurant.aspx.cs" Inherits="TreasureLand.Admin.ManageRestaurant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:Button ID="btnManageCategories" runat="server" Text="Manage Categories" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnManageMenuItems" runat="server" Text="Manage Menu Items" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnEnterPurchase" runat="server" Text="Create Purchase" />
<br />
<br />
<br />
<table class="style4">
    <tr>
        <td style="width: 176px">
            Ingredient Purchased Name:</td>
        <td style="width: 248px">
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>Rice</asp:ListItem>
                <asp:ListItem>Malta</asp:ListItem>
                <asp:ListItem>Coffee</asp:ListItem>
                <asp:ListItem>Chicken</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button3" runat="server" Text="Add New Ingredient" />
            <asp:TextBox ID="TextBox4" runat="server" style="margin-left: 60px" 
                Visible="False"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 176px">
            Purchase Price:</td>
        <td style="width: 248px">
            <asp:TextBox ID="TextBox2" runat="server" Width="94px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 176px">
            Qty:</td>
        <td style="width: 248px">
            <asp:TextBox ID="TextBox3" runat="server" Width="96px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 176px">
            <asp:Button ID="Button1" runat="server" Text="Add Item to Purchase" 
                Width="165px" />
        </td>
        <td style="width: 248px">
            <asp:Button ID="Button2" runat="server" Text="Submit Purchase" Width="165px" />
        </td>
    </tr>
</table>
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<br />
<br />
</asp:Content>
