<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginRedirect.aspx.cs" Inherits="TreasureLand.LoginRedirect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_RoleMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
    <tr>
        <td>
            <asp:Button ID="btnAdmin" runat="server" Text="Admin" Visible="False" 
                onclick="btnAdmin_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnManager" runat="server" Text="Manager" Visible="False" 
                onclick="btnManager_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnClerk" runat="server" Text="Clerk" Visible="False" 
                onclick="btnClerk_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnHousekeeping" runat="server" Text="Housekeeping" 
                Visible="False" onclick="btnHousekeeping_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnMaintenance" runat="server" Text="Maintenance" 
                Visible="False" onclick="btnMaintenance_Click" />
        </td>
    </tr>
</table>
</asp:Content>
