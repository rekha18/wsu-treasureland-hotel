<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="ManageUsers.aspx.cs" Inherits="TreasureLand.Admin.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentHolder" runat="server">
    <h1>User Management</h1>
    <asp:Repeater ID="Repeater_FilteringUI" runat="server" OnItemCommand="Repeater_FilteringUI_ItemCommand">
        <ItemTemplate>
            <asp:LinkButton runat="server" ID="lnkFilter" Text="<%#Container.DataItem %>" CommandName="<%#Container.DataItem %>" />
        </ItemTemplate>
        <SeparatorTemplate>
            |</SeparatorTemplate>
    </asp:Repeater>
    <asp:GridView ID="GridView_UserAccounts" runat="server" AutoGenerateColumns="False"
        Width="95%">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="UserName" 
                DataNavigateUrlFormatString="UserInformation.aspx?user={0}" Text="Manage" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:CheckBoxField DataField="IsApproved" HeaderText="Enabled?" />
            <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Locked Out?" />
            <asp:CheckBoxField DataField="IsOnline" HeaderText="Online?" />
            <asp:BoundField DataField="Comment" HeaderText="Comment" />
        </Columns>
    </asp:GridView>
    <p>
        <asp:LinkButton ID="lnkFirst" runat="server"  onclick="lnkFirst_Click">First</asp:LinkButton>|
        <asp:LinkButton ID="lnkPrev" runat="server" onclick="lnkPrev_Click"> Prev</asp:LinkButton>|
        <asp:LinkButton ID="lnkNext" runat="server" onclick="lnkNext_Click">Next </asp:LinkButton>|
        <asp:LinkButton ID="lnkLast" runat="server" onclick="lnkLast_Click">Last </asp:LinkButton>
    </p>
</asp:Content>
