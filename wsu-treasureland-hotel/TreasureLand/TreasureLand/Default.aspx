<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TreasureLand._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Ghana group. 
    </h2> <!-- Alex Harris added this comment -->
    <p>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
            ContextTypeName="TreasureLand.TreasureLandDataClassesDataContext" 
            EntityTypeName="" 
            Select="new (GuestFirstName, GuestSurName, GuestCompany, GuestAddress, GuestCity, GuestCountry, GuestComments)" 
            TableName="Guests">
        </asp:LinqDataSource>
    </p>
    <p>
        GUEST TABLE IS LISTED BELOW. JUST A TEST.</p>
    <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1">
        <AlternatingItemTemplate>
            <li style="">GuestFirstName:
                <asp:Label ID="GuestFirstNameLabel" runat="server" 
                    Text='<%# Eval("GuestFirstName") %>' />
                <br />
                GuestSurName:
                <asp:Label ID="GuestSurNameLabel" runat="server" 
                    Text='<%# Eval("GuestSurName") %>' />
                <br />
                GuestCompany:
                <asp:Label ID="GuestCompanyLabel" runat="server" 
                    Text='<%# Eval("GuestCompany") %>' />
                <br />
                GuestAddress:
                <asp:Label ID="GuestAddressLabel" runat="server" 
                    Text='<%# Eval("GuestAddress") %>' />
                <br />
                GuestCity:
                <asp:Label ID="GuestCityLabel" runat="server" Text='<%# Eval("GuestCity") %>' />
                <br />
                GuestCountry:
                <asp:Label ID="GuestCountryLabel" runat="server" 
                    Text='<%# Eval("GuestCountry") %>' />
                <br />
                GuestComments:
                <asp:Label ID="GuestCommentsLabel" runat="server" 
                    Text='<%# Eval("GuestComments") %>' />
                <br />
            </li>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <li style="">GuestFirstName:
                <asp:TextBox ID="GuestFirstNameTextBox" runat="server" 
                    Text='<%# Bind("GuestFirstName") %>' />
                <br />
                GuestSurName:
                <asp:TextBox ID="GuestSurNameTextBox" runat="server" 
                    Text='<%# Bind("GuestSurName") %>' />
                <br />
                GuestCompany:
                <asp:TextBox ID="GuestCompanyTextBox" runat="server" 
                    Text='<%# Bind("GuestCompany") %>' />
                <br />
                GuestAddress:
                <asp:TextBox ID="GuestAddressTextBox" runat="server" 
                    Text='<%# Bind("GuestAddress") %>' />
                <br />
                GuestCity:
                <asp:TextBox ID="GuestCityTextBox" runat="server" 
                    Text='<%# Bind("GuestCity") %>' />
                <br />
                GuestCountry:
                <asp:TextBox ID="GuestCountryTextBox" runat="server" 
                    Text='<%# Bind("GuestCountry") %>' />
                <br />
                GuestComments:
                <asp:TextBox ID="GuestCommentsTextBox" runat="server" 
                    Text='<%# Bind("GuestComments") %>' />
                <br />
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                    Text="Update" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                    Text="Cancel" />
            </li>
        </EditItemTemplate>
        <EmptyDataTemplate>
            No data was returned.
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <li style="">GuestFirstName:
                <asp:TextBox ID="GuestFirstNameTextBox" runat="server" 
                    Text='<%# Bind("GuestFirstName") %>' />
                <br />
                GuestSurName:
                <asp:TextBox ID="GuestSurNameTextBox" runat="server" 
                    Text='<%# Bind("GuestSurName") %>' />
                <br />
                GuestCompany:
                <asp:TextBox ID="GuestCompanyTextBox" runat="server" 
                    Text='<%# Bind("GuestCompany") %>' />
                <br />
                GuestAddress:
                <asp:TextBox ID="GuestAddressTextBox" runat="server" 
                    Text='<%# Bind("GuestAddress") %>' />
                <br />
                GuestCity:
                <asp:TextBox ID="GuestCityTextBox" runat="server" 
                    Text='<%# Bind("GuestCity") %>' />
                <br />
                GuestCountry:
                <asp:TextBox ID="GuestCountryTextBox" runat="server" 
                    Text='<%# Bind("GuestCountry") %>' />
                <br />
                GuestComments:
                <asp:TextBox ID="GuestCommentsTextBox" runat="server" 
                    Text='<%# Bind("GuestComments") %>' />
                <br />
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                    Text="Insert" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                    Text="Clear" />
            </li>
        </InsertItemTemplate>
        <ItemSeparatorTemplate>
            <br />
        </ItemSeparatorTemplate>
        <ItemTemplate>
            <li style="">GuestFirstName:
                <asp:Label ID="GuestFirstNameLabel" runat="server" 
                    Text='<%# Eval("GuestFirstName") %>' />
                <br />
                GuestSurName:
                <asp:Label ID="GuestSurNameLabel" runat="server" 
                    Text='<%# Eval("GuestSurName") %>' />
                <br />
                GuestCompany:
                <asp:Label ID="GuestCompanyLabel" runat="server" 
                    Text='<%# Eval("GuestCompany") %>' />
                <br />
                GuestAddress:
                <asp:Label ID="GuestAddressLabel" runat="server" 
                    Text='<%# Eval("GuestAddress") %>' />
                <br />
                GuestCity:
                <asp:Label ID="GuestCityLabel" runat="server" Text='<%# Eval("GuestCity") %>' />
                <br />
                GuestCountry:
                <asp:Label ID="GuestCountryLabel" runat="server" 
                    Text='<%# Eval("GuestCountry") %>' />
                <br />
                GuestComments:
                <asp:Label ID="GuestCommentsLabel" runat="server" 
                    Text='<%# Eval("GuestComments") %>' />
                <br />
            </li>
        </ItemTemplate>
        <LayoutTemplate>
            <ul ID="itemPlaceholderContainer" runat="server" style="">
                <li runat="server" id="itemPlaceholder" />
            </ul>
            <div style="">
            </div>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <li style="">GuestFirstName:
                <asp:Label ID="GuestFirstNameLabel" runat="server" 
                    Text='<%# Eval("GuestFirstName") %>' />
                <br />
                GuestSurName:
                <asp:Label ID="GuestSurNameLabel" runat="server" 
                    Text='<%# Eval("GuestSurName") %>' />
                <br />
                GuestCompany:
                <asp:Label ID="GuestCompanyLabel" runat="server" 
                    Text='<%# Eval("GuestCompany") %>' />
                <br />
                GuestAddress:
                <asp:Label ID="GuestAddressLabel" runat="server" 
                    Text='<%# Eval("GuestAddress") %>' />
                <br />
                GuestCity:
                <asp:Label ID="GuestCityLabel" runat="server" Text='<%# Eval("GuestCity") %>' />
                <br />
                GuestCountry:
                <asp:Label ID="GuestCountryLabel" runat="server" 
                    Text='<%# Eval("GuestCountry") %>' />
                <br />
                GuestComments:
                <asp:Label ID="GuestCommentsLabel" runat="server" 
                    Text='<%# Eval("GuestComments") %>' />
                <br />
            </li>
        </SelectedItemTemplate>
    </asp:ListView>
</asp:Content>
