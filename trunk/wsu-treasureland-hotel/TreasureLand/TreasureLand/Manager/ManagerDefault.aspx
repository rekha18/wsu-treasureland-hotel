<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ManagerDefault.aspx.cs" Inherits="TreasureLand.Manager.ManagerDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div> 
        <table style="width:62px;">
            <tr>
                <td align="center" style="width: 56px; text-align: left;">
                    <asp:Button ID="btnClerkHP" runat="server" onclick="Button1_Click" 
                        PostBackUrl="~/Clerk/ClerkDefault.aspx" style="text-align: center" 
                        Text="Clerk Homepage" Width="222px" />
                </td>
            </tr>
            <tr>
                <td style="width: 56px">
                    <asp:Button ID="btnHousekeepingHP" runat="server" onclick="Button2_Click" 
                        PostBackUrl="~/HouseKeeping/HouseKeepingDefault.aspx" 
                        style="text-align: center" Text="Housekeeping Homepage" />
                </td>
            </tr>
            <tr>
                <td style="width: 56px">
                    <asp:Button ID="btnMaintenanceHP" runat="server" 
                        onclick="btnMaintenanceHP_Click" 
                        PostBackUrl="~/Maintenance/MaintenanceDefault.aspx" style="text-align: center" 
                        Text="Maintenance Homepage" Width="225px" />
                </td>
            </tr>
            <tr>
                <td style="width: 56px">
                    <asp:Button ID="btnRestaurantBarHP" runat="server" 
                        onclick="btnRestaurantBarHP_Click" style="text-align: center" 
                        Text="Restaurant/Bar Homepage" Width="224px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
