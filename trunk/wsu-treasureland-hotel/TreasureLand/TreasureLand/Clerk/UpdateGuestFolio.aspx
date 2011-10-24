<%@ Page Title="UpdateGuestFolio" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="UpdateGuestFolio.aspx.cs" Inherits="TreasureLand.Clerk.WebForm4" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <asp:MultiView ID="mvUpdateGuestFolio" runat="server">
        <asp:View ID="vUpdateGuestFolio" runat="server">
            <table style="width: 99%">
                <tr>
                    <td style="width: 75px">
                        Salutation:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtSalutation" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Address:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px">
                        First Name:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        City:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px">
                        Surname:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        State/Region/Province:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px">
                        Phone #:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Country:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px">
                        CC #:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtCreditCard" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Postal Code:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75px">
                        Expiration:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtExpiration" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 115px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Email:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Back" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Update" />
        </asp:View>
        <asp:View ID="vLocateGuestFolio" runat="server">
            <table style="width: 70%">
                <tr>
                    <td style="width: 76px; height: 26px">
                        First Name:</td>
                    <td class="style1" style="width: 142px; height: 26px">
                        <asp:TextBox ID="txtFirstName2" runat="server" style="margin-left: 2px"></asp:TextBox>
                    </td>
                    <td style="width: 472px; height: 26px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 76px">
                        Surname:</td>
                    <td class="style1" style="width: 142px">
                        <asp:TextBox ID="txtSurname2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 472px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 76px">
                        Email:</td>
                    <td class="style1" style="width: 142px">
                        <asp:TextBox ID="txtEmail2" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 472px">
                        &nbsp;</td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnLocateGuest" runat="server" Text="Locate Guest" />
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
                            <asp:Button ID="CancelButton0" runat="server" CommandName="Cancel" 
                                Text="Clear" />
                        </td>
                        <td>
                            <asp:TextBox ID="GuestFirstNameTextBox0" runat="server" 
                                Text='<%# Bind("GuestFirstName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="GuestSurNameTextBox0" runat="server" 
                                Text='<%# Bind("GuestSurName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="GuestEmailTextBox0" runat="server" 
                                Text='<%# Bind("GuestEmail") %>' />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color:#DCDCDC;color: #000000;">
                        <td>
                            <asp:Label ID="GuestFirstNameLabel0" runat="server" 
                                Text='<%# Eval("GuestFirstName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuestSurNameLabel0" runat="server" 
                                Text='<%# Eval("GuestSurName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuestEmailLabel0" runat="server" 
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
                            <asp:Label ID="GuestFirstNameLabel1" runat="server" 
                                Text='<%# Eval("GuestFirstName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuestSurNameLabel1" runat="server" 
                                Text='<%# Eval("GuestSurName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuestEmailLabel1" runat="server" 
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
            <asp:Label ID="lblErrorMessage0" runat="server" ForeColor="Red" 
                Text="Error Message Goes Here"></asp:Label>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="Select" />
        </asp:View>
    </asp:MultiView>
</asp:Content>


