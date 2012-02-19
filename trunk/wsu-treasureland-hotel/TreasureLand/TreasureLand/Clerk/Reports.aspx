<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="TreasureLand.Clerk.WebForm8" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        </p> 
    <asp:Label ID="lblChooseDate" runat="server" Text="ChooseDates:"></asp:Label>
    <br />
    <asp:Label ID="lblStartDate" runat="server" Text="Begin Date:"></asp:Label>
    <asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="txtBeginDate_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtBeginDate">
    </asp:CalendarExtender>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
&nbsp;<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtEndDate">
    </asp:CalendarExtender>
    <asp:Button ID="btnCreateReport" runat="server" onclick="btnCreateReport_Click" 
        Text="Submit" />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Clerk\Reports\RestaurantSales.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
