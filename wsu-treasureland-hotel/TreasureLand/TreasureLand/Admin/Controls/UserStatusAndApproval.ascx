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
     Locked Out: </strong>
        </td>
        <td>
            <asp:Label ID="Label_LastLockedOutDate" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="Button_UnlockUser" runat="server" Text="Unlock User" OnClick="Button_UnlockUser_Click" />
            <br />
        </td>
    </tr>
</table>
<asp:CheckBoxList ID="CheckBoxList_Roles" runat="server">
</asp:CheckBoxList>
<asp:Button ID="Button_UpdateRoles" runat="server" Text="Update Roles" 
    onclick="Button_UpdateRoles_Click" />
<br />
<asp:Label ID="Label_StatusMsg" runat="server" ForeColor="Red" Font-Size="Large" />