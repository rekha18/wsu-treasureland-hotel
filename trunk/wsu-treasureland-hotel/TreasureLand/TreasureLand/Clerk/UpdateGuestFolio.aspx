<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="UpdateGuestFolio.aspx.cs" Inherits="TreasureLand.Clerk.UpdateGuestFolio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvUpdateGuestFolio" runat="server" ActiveViewIndex="0">
        <asp:View ID="vLocateGuestFolio" runat="server">
            <table style="width: 70%">
                <tr>
                    <td style="width: 76px; height: 26px">
                        First Name:</td>
                    <td class="style1" style="width: 142px; height: 26px">
                        <asp:TextBox ID="txtFirstName" runat="server" style="margin-left: 0px" MaxLength="30"></asp:TextBox>
                    </td>
                    <td style="width: 472px; height: 26px">
                        <asp:Label ID="lblErrorMessageMissingData" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 76px">
                        Surname:</td>
                    <td class="style1" style="width: 142px">
                        <asp:TextBox ID="txtSurname" runat="server" Columns="30"></asp:TextBox>
                    </td>
                    <td style="width: 472px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 76px">
                        Phone Number:</td>
                    <td class="style1" style="width: 142px">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td style="width: 472px">
                        <asp:Label ID="lblErrorMessageNoCustomersFound" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnLocateGuest" runat="server" Text="Locate Guest" 
                onclick="btnLocateGuest_Click" />
            <br />
            <asp:GridView ID="gvGuestFolio" runat="server" AutoGenerateColumns="False" 
                onselectedindexchanged="gvGuestFolio_SelectedIndexChanged" 
                    onselectedindexchanging="gvGuestFolio_SelectedIndexChanging" >
                <Columns>
                    <asp:BoundField DataField="GuestID" HeaderText="Guest ID" />
                    <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" 
                        SortExpression="GuestFirstName" />
                    <asp:BoundField DataField="GuestSurName" HeaderText="Surname" 
                        SortExpression="GuestSurName" />
                    <asp:BoundField DataField="GuestEmail" HeaderText="Email" 
                        SortExpression="GuestEmail" />
                    <asp:BoundField DataField="GuestPhone" HeaderText="Phone Number" />
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                </Columns>
                <RowStyle Font-Size="Small" />
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
            </asp:GridView>
            <asp:Label ID="lblErrorMustSelectGuest" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnSelect" runat="server" Text="Select Guest" 
                onclick="btnSelect_Click" Width="121px" />
        </asp:View>
                <asp:View ID="vUpdateGuestFolio" runat="server">
            <table style="width: 99%">
                <tr>
                    <td style="width: 118px">
                        Salutation:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtSalutation" runat="server" MaxLength="5"></asp:TextBox>
                    </td>
                    <td style="width: 86px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Address:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        First Name:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtShowFirstName" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtShowFirstName" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 86px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        City:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtCity" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Surname:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtShowSurname" runat="server" MaxLength="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtShowSurname" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 86px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        State/Region/Province:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtState" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Phone #:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtPhone" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 86px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Country:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtCountry" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Guest ID #:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtGuestID" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td style="width: 86px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Postal Code:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Issue Country:</td>
                    <td style="width: 175px">
                        <asp:TextBox ID="txtIssueCountry" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td style="width: 86px">
                        &nbsp;</td>
                    <td style="width: 135px">
                        Email:</td>
                    <td class="style1" style="width: 175px">
                        <asp:TextBox ID="txtEmail" runat="server" 
                            MaxLength="30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Guest ID Comments:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtComments" runat="server" Width="542px" TextMode="MultiLine"
                         onKeyUp="javascript:Count('MainContent_ContentPlaceHolder1_txtComments');"
                         onKeyDown="javascript:Count('MainContent_ContentPlaceHolder1_txtComments');" ></asp:TextBox>
                        <div id="charsleft">
                            &nbsp;
                        </div>
                    </td>
                </tr>
            </table>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" 
                        CommandName="PrevView" onclick="btnBack_Click" Width="59px"  />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
        function Count(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 200; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text)  //get your object
            var string = object.value;
            if (string.length > maxlength) {
                object.value = string.substring(0, maxlength); //truncate the value
            }
            if((maxlength - string.length) >= 0)
                document.getElementById("charsleft").innerHTML = (maxlength - string.length) + " chars left";
        }
    </script>
</asp:Content>
