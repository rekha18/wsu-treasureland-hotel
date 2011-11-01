<%@ Page Title="UpdateGuestFolio" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="UpdateGuestFolio.aspx.cs" Inherits="TreasureLand.Clerk.WebForm4" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <asp:MultiView ID="mvUpdateGuestFolio" runat="server" ActiveViewIndex="0">
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
            <asp:GridView ID="gvGuestFolio" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsLocateGuestFolio">
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" 
                        SortExpression="GuestFirstName" />
                    <asp:BoundField DataField="GuestSurName" HeaderText="Surname" 
                        SortExpression="GuestSurName" />
                    <asp:BoundField DataField="GuestEmail" HeaderText="Email" 
                        SortExpression="GuestEmail" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:SqlDataSource ID="sdsLocateGuestFolio" runat="server" 
                ConnectionString="<%$ ConnectionStrings:TreasureLandDB %>" 
                
                SelectCommand="SELECT [GuestFirstName], [GuestSurName], [GuestEmail] FROM [Guest]">
            </asp:SqlDataSource>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblErrorMessage0" runat="server" ForeColor="Red" 
                Text="Error Message Goes Here" Visible="False"></asp:Label>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="Select" CommandArgument="1" 
                CommandName="SwitchViewByIndex" />
        </asp:View>
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
            <asp:Button ID="btnBack" runat="server" Text="Back" CommandArgument="-1" 
                        CommandName="SwitchViewByIndex" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Update" />
        </asp:View>
    </asp:MultiView>
</asp:Content>


