<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginRedirect.aspx.cs" Inherits="TreasureLand.LoginRedirect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_RoleMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Please select a role:
    </h2>
    <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderStyle="Solid" Width="127px">
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Button ID="btnAdmin" runat="server" Text="Admin" Visible="False" 
                        onclick="btnAdmin_Click" Width="120px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnManager" runat="server" Text="Manager" Visible="False" 
                        onclick="btnManager_Click" Width="120px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnClerk" runat="server" Text="Clerk" Visible="False" 
                        onclick="btnClerk_Click" Width="120px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnHousekeeping" runat="server" Text="Housekeeping" 
                        Visible="False" onclick="btnHousekeeping_Click" Width="120px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnMaintenance" runat="server" Text="Maintenance" 
                        Visible="False" onclick="btnMaintenance_Click" Width="120px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
