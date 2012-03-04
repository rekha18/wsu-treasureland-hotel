<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserStatusAndApproval.ascx.cs"
    Inherits="TreasureLand.Admin.UserStatusAndApproval" %>
<table>
    <tr>
        <td>
            <strong>Username:</strong>
        </td>
        <td>
            <asp:Label ID="Label_UserName" runat="server" Text="" />
        </td>
    </tr>
    <tr>
        <td>
            <strong>Enabled: </strong>
        </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsEnabled" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox_IsEnabled_CheckedChanged" />
        </td>
    </tr>
    <tr>
        <td>
            Locked Out:
        </td>
        <td>
            <asp:Label ID="Label_LastLockedOutDate" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="Button_UnlockUser" runat="server" Text="Unlock User" OnClick="Button_UnlockUser_Click" />
            <br />
        </td>
    </tr>
</table>
<asp:Repeater ID="Repeater_UsersRoleList" runat="server">
    <ItemTemplate>
        <asp:CheckBox runat="server" ID="RoleCheckBox" AutoPostBack="true" Text="<%#Container.DataItem %>"
            OnCheckedChanged="RoleCheckBox_CheckChanged" />
        <br />
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Button ID="btnSubmitRoleChanges" runat="server" Enabled="False" 
    onclick="btnSubmitRoleChanges_Click" Text="Submit Role Changes" />
<br />
<asp:Label ID="Label_StatusMsg" runat="server" ForeColor="Red" Font-Size="Large" />