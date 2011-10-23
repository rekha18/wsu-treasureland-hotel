<%@ Page Title="Check Out" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="TreasureLand.Clerk.CheckOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style12
        {
            width: 187px;
        }
        .style13
        {
            width: 187px;
            height: 20px;
        }
        .style18
        {
            width: 151px;
        }
        .style19
        {
            width: 151px;
            height: 20px;
        }
        .style20
        {
            width: 140px;
        }
        .style21
        {
            width: 140px;
            height: 20px;
        }
        .style22
        {
            width: 160px;
        }
        .style23
        {
            width: 160px;
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    &nbsp;&nbsp;&nbsp; Reservation Number:
    <asp:TextBox ID="txtReservationNum" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; First Name:&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtFirstName" runat="server" style="margin-left: 2px"></asp:TextBox>
    <br />
&nbsp;&nbsp;&nbsp; Room Number: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox 
        ID="txtRoomNum" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Last Name:&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;Total: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox 
        ID="TextBox1" runat="server" style="margin-left: 2px" Width="128px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtEmail" runat="server" style="margin-left: 3px"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" 
        Text="Error Message"></asp:Label>
    <br />
    <br />
    <table style="width: 62%; height: 26px;">
        <tr>
            <td class="style22">
                Manger Username:</td>
            <td class="style20">
                <asp:TextBox ID="txtMangerUname" runat="server"></asp:TextBox>
            </td>
            <td class="style18">
                Discount Amount:</td>
            <td class="style12">
                <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style23">
                Manager Password:</td>
            <td class="style21">
                <asp:TextBox ID="txtManagerPword" runat="server"></asp:TextBox>
            </td>
            <td class="style19">
                &nbsp;</td>
            <td class="style13">
                <asp:Button ID="btnApply" runat="server" Text="Apply Discount" />
            </td>
        </tr>
    </table>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnBack" runat="server" Text="Back" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnAdjustPrice" runat="server" Text="Adjust Price" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Print Receipt" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" />
</asp:Content>
