<%@ Page Title="LocateGuestFolio" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="LocateGuestFolio.aspx.cs" Inherits="TreasureLand.Clerk.WebForm4" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <br />
    <br />
    &nbsp;&nbsp;&nbsp; First Name:&nbsp;&nbsp;
    <asp:TextBox ID="txtFirstName" runat="server" style="margin-left: 2px"></asp:TextBox>
    <br />
&nbsp;&nbsp;&nbsp; Surname:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    <br />
&nbsp;&nbsp;&nbsp; Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnLocateGuest" runat="server" 
        Text="Locate Guest" />
    <br />
    <br />
    <br />
    <asp:ListView ID="ListView1" runat="server" DataSourceID="sdsLocateGuestFolio">
        <AlternatingItemTemplate>
            <tr style="background-color:#FFF8DC;">
                <td>
                    <asp:Label ID="GuestFirstNameLabel" runat="server" 
                        Text='<%# Eval("GuestFirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="GuestSurNameLabel" runat="server" 
                        Text='<%# Eval("GuestSurName") %>' />
                </td>
                <td>
                    <asp:Label ID="GuestEmailLabel" runat="server" 
                        Text='<%# Eval("GuestEmail") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color:#008A8C;color: #FFFFFF;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="GuestFirstNameTextBox" runat="server" 
                        Text='<%# Bind("GuestFirstName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="GuestSurNameTextBox" runat="server" 
                        Text='<%# Bind("GuestSurName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="GuestEmailTextBox" runat="server" 
                        Text='<%# Bind("GuestEmail") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" 
                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>
                        No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                        Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="GuestFirstNameTextBox" runat="server" 
                        Text='<%# Bind("GuestFirstName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="GuestSurNameTextBox" runat="server" 
                        Text='<%# Bind("GuestSurName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="GuestEmailTextBox" runat="server" 
                        Text='<%# Bind("GuestEmail") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color:#DCDCDC;color: #000000;">
                <td>
                    <asp:Label ID="GuestFirstNameLabel" runat="server" 
                        Text='<%# Eval("GuestFirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="GuestSurNameLabel" runat="server" 
                        Text='<%# Eval("GuestSurName") %>' />
                </td>
                <td>
                    <asp:Label ID="GuestEmailLabel" runat="server" 
                        Text='<%# Eval("GuestEmail") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="1" 
                            style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                <th runat="server">
                                    GuestFirstName</th>
                                <th runat="server">
                                    GuestSurName</th>
                                <th runat="server">
                                    GuestEmail</th>
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" 
                        style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                <td>
                    <asp:Label ID="GuestFirstNameLabel" runat="server" 
                        Text='<%# Eval("GuestFirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="GuestSurNameLabel" runat="server" 
                        Text='<%# Eval("GuestSurName") %>' />
                </td>
                <td>
                    <asp:Label ID="GuestEmailLabel" runat="server" 
                        Text='<%# Eval("GuestEmail") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="sdsLocateGuestFolio" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TreasureLandDB %>" 
        SelectCommand="SELECT [GuestFirstName], [GuestSurName], [GuestEmail] FROM [Guest] ORDER BY [GuestSurName]">
    </asp:SqlDataSource>
    <br />
&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" 
        Text="Error Message Goes Here"></asp:Label>
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnSelect" runat="server" Text="Select" />
</asp:Content>


