<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ManagerReports.aspx.cs" Inherits="TreasureLand.Manager.ManagerReports" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    Select report to run:&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlReportType_SelectedIndexChanged">
        <asp:ListItem Value="1">Financial</asp:ListItem>
        <asp:ListItem Value="2">Rooms</asp:ListItem>
        <asp:ListItem Value="3">Restaurant</asp:ListItem>
        <asp:ListItem Value="4">Housekeeping</asp:ListItem>
        <asp:ListItem Value="5">Repair</asp:ListItem>
    </asp:DropDownList>
</p>
<p>
    <asp:DropDownList ID="ddlFinancial" runat="server" AutoPostBack="True">
        <asp:ListItem>Deposits</asp:ListItem>
        <asp:ListItem>Payments</asp:ListItem>
        <asp:ListItem>Revenue</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlRooms" runat="server" Visible="False" 
        onselectedindexchanged="ddlRooms_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Value="RoomsOccupied">Currently Occupied</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlRestaurant" runat="server" Visible="False" 
        onselectedindexchanged="ddl_SelectedIndexChangedRestaurant" 
        AutoPostBack="True">
        <asp:ListItem>Dishes containing ingredient</asp:ListItem>
        <asp:ListItem Value="FoodSales">Food Sales</asp:ListItem>
        <asp:ListItem Value="DrinkSales">Drink Sales</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlHousekeeping" runat="server" Visible="False" 
        onselectedindexchanged="ddl_SelectedIndexChangedHousekeeping" 
        AutoPostBack="True">
        <asp:ListItem>Rooms that need to be cleaned</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlRepair" runat="server" Visible="False" 
        onselectedindexchanged="ddl_SelectedIndexChangedRepairs" 
        AutoPostBack="True">
        <asp:ListItem>Current Repairs</asp:ListItem>
    </asp:DropDownList>
</p>
<p>
    <asp:DropDownList ID="ddlFirstParameter" runat="server" 
        AppendDataBoundItems="True" Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSecondParameter" runat="server" 
        AppendDataBoundItems="True" Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlThirdParameter" runat="server" 
        AppendDataBoundItems="True" Visible="False">
    </asp:DropDownList>
</p>
    <p>
        <asp:Label ID="lblBeginDate" runat="server" Text="Begin Date"></asp:Label>
        <asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="txtBeginDate_CalendarExtender" runat="server" 
            TargetControlID="txtBeginDate">
        </asp:CalendarExtender>
        <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
        <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
            TargetControlID="txtEndDate">
        </asp:CalendarExtender>
</p>


<p>
    <asp:Button ID="btnCreateReport" runat="server" Text="Create Report" 
        onclick="btnCreateReport_Click" />
</p>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" EntityTypeName="" 
        Select="new (RoomID, RoomNumbers)" TableName="Rooms" 
    OrderBy="RoomNumbers">
    </asp:LinqDataSource>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" 
    SizeToReportContent="True" Font-Names="Verdana" Font-Size="8pt" 
        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Visible="False" Height="158px">
    <LocalReport ReportPath="App_Code\ReportFiles\Revenue.rdlc">
    </LocalReport>
</rsweb:ReportViewer>
<asp:ScriptManager id="SM1" runat="server" />
</asp:Content>
