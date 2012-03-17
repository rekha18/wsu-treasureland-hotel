<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="Collections.aspx.cs" Inherits="TreasureLand.Manager.Collections" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvCollections" runat="server" AllowPaging="True" 
        AllowSorting="True" DataSourceID="SqlDataSource1" 
        AutoGenerateColumns="False" onrowupdating="gvCollections_RowUpdating" 
        onselectedindexchanged="gvCollections_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="CollectionsID" HeaderText="CollectionsID" 
                InsertVisible="False" ReadOnly="True" SortExpression="CollectionsID" />
            <asp:BoundField DataField="GuestFirstName" HeaderText="FirstName" 
                ReadOnly="True" SortExpression="GuestFirstName" />
            <asp:BoundField DataField="GuestSurName" HeaderText="SurName" ReadOnly="True" 
                SortExpression="GuestSurName" />
            <asp:BoundField DataField="GuestPhone" HeaderText="GuestPhone" ReadOnly="True" 
                SortExpression="GuestPhone" />
            <asp:TemplateField HeaderText="CollectionsAmountOwed" 
                SortExpression="CollectionsAmountOwed">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("CollectionsAmountOwed") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("CollectionsAmountOwed") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:HotelDBM %>" 
        SelectCommand="SELECT Collections.CollectionsID, Collections.CollectionsAmountOwed, Guest.GuestFirstName, Guest.GuestSurName, Guest.GuestPhone, ReservationDetail.ReservationDetailID AS Expr1 FROM Collections INNER JOIN ReservationDetail ON Collections.ReservationDetailID = ReservationDetail.ReservationDetailID INNER JOIN Reservation ON ReservationDetail.ReservationID = Reservation.ReservationID INNER JOIN Guest ON Reservation.GuestID = Guest.GuestID" UpdateCommand="UPDATE Collections 
SET CollectionsAmountOwed = @CollectionsAmountOwed
WHERE CollectionsID = @CollectionsID">
        <UpdateParameters>
            <asp:Parameter Name="CollectionsAmountOwed" />
            <asp:Parameter Name="CollectionsID" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
