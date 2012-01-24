<%@ Page Title="" Language="C#" MasterPageFile="~/Restaurant.Master" AutoEventWireup="true" CodeBehind="RestaurantRoomSelection.aspx.cs" Inherits="TreasureLand.RestaurantRoomSelection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/RestaurantStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_RoleMenu" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lbl_pageNumber" runat="server" Text="0"></asp:Label>
        <br />

        <asp:Panel ID="panel_nav" runat="server" CssClass="panelNav">
            <asp:Button ID="btn_previous" runat="server" CssClass="buttonPrevious" 
                onclick="btn_previous_Click" />
            <asp:Button ID="btn_cash" runat="server" CssClass="buttonCash" />
            <asp:Button ID="btn_next" runat="server" CssClass="buttonNext" 
                onclick="btn_next_Click" />
        </asp:Panel>

       <asp:Panel ID="panel_buttons" runat="server" CssClass="panelButtons">

        </asp:Panel>
  
</asp:Content>
