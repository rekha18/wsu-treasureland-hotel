<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="UserInformation.aspx.cs" Inherits="TreasureLand.Admin.UserInformation" %>

<%@ Register TagPrefix="treasureland" TagName="UserStatusAndApproval" Src="~/Admin/Controls/UserStatusAndApproval.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
<h1>User Information</h1>
    <asp:HyperLink ID="HyperLink_ManageUsers" runat="server" NavigateUrl="~/Admin/ManageUsers.aspx">&lt;&lt;Return to User's List</asp:HyperLink>
    <br />
    <br />
    <treasureland:UserStatusAndApproval ID="UserStatusAndApproval" runat="server" />
</asp:Content>
