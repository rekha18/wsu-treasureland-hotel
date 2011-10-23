<%@ Page Title="UpdateGuestFolio" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="UpdateGuestFolio.aspx.cs" Inherits="TreasureLand.Clerk.WebForm4" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;
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
    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" 
        Text="Error Message Goes Here"></asp:Label>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnBack" runat="server" 
        Text="Back" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnUpdate" runat="server" Text="Update" />
</asp:Content>


