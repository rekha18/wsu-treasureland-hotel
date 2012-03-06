<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserStatusAndApproval.ascx.cs"
    Inherits="TreasureLand.Admin.UserStatusAndApproval" %>
<style type="text/css">
    .style1
    {
        width: 276px;
    }
    .style2
    {
        width: 466px;
    }
</style>
<table style="width:100%;">
    <tr>
        <td class="style1">
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
        </td>
        <td>
            <table style="width: 47%;">
                <tr>
                    <td class="style2">
                        Resturant Pin:<asp:Label ID="lblPin" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:TextBox ID="txtPin" runat="server" MaxLength="4"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvPin" runat="server" 
                            ControlToValidate="txtPin" ErrorMessage="Field Required to change pin" 
                            ForeColor="Red" ValidationGroup="vgPin"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Button ID="btnUpdatePin" runat="server" Text="Change Pin" 
                            onclick="btnUpdatePin_Click" ValidationGroup="vgPin" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
