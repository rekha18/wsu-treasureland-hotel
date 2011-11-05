<%@ Page Title="Room Management" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeBehind="ManageRooms.aspx.cs" Inherits="TreasureLand.Admin.ManageRooms" EnableEventValidation="true" %>

<%@ Register TagPrefix="treasureland" TagName="RoomManagement" Src="~/Admin/Controls/RoomManagement.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentHolder" runat="server">
    <treasureland:RoomManagement ID="Control_RoomManagement" runat="server" />
</asp:Content>
