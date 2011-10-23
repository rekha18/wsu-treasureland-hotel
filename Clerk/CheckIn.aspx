<%@ Page Title="Check In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckIn.aspx.cs" Inherits="TreasureLand.Clerk.CheckIn" %>
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
&nbsp;&nbsp;&nbsp; Reservation Number:&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtReservationNum" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    First Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;&nbsp; Room Type:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtRoomType" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    Surname:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;&nbsp; Room Number:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtRoomNumber" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    Phone Number:&nbsp;
    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;&nbsp; Check out:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtCheckOut" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;&nbsp; Number of guests:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtNumGuests" runat="server"></asp:TextBox>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" 
        Text="Error message"></asp:Label>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnBack" runat="server" Text="Back" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCheckIn" runat="server" Text="Check in" />
</asp:Content>
