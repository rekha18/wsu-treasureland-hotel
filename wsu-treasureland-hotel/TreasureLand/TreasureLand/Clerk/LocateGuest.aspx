﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="True" CodeBehind="LocateGuest.aspx.cs" Inherits="TreasureLand.Clerk.LocateGuest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvLocateGuest" runat="server" ActiveViewIndex="0">
        <asp:View ID="vLocateGuest" runat="server">
            <table class="style1" style="width: 610px">
                <tr>
                    <td class="style18" style="width: 85px">
                        Reservation #:</td>
                    <td class="style24" style="width: 102px">
                        <asp:TextBox ID="txtReservationNum" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td class="style12" style="width: 80px">
                        First Name:</td>
                    <td class="style22" style="width: 136px">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style19" style="width: 85px">
                        <asp:Button ID="btnLocateGuest" runat="server" OnClick="btnLocateGuest_Click" 
                            Text="Locate Guest" ValidationGroup="vgView" />
                    </td>
                    <td class="style25" style="width: 102px">
                        &nbsp;</td>
                    <td class="style4" style="width: 80px">
                        Surname:</td>
                    <td class="style23" style="width: 136px">
                        <asp:TextBox ID="txtSurname" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:CompareValidator ID="cvReservationID" runat="server" 
                    ControlToValidate="txtReservationNum" Display="None" 
                    ErrorMessage="Reservation must be a number" ForeColor="Red" 
                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="vgView"></asp:CompareValidator>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
                ValidationGroup="vgView" />
            <br />
            <asp:GridView ID="gvGuest" runat="server" AutoGenerateColumns="False" 
                                    onselectedindexchanged="gvGuest_SelectedIndexChanged" 
                    onselectedindexchanging="gvGuest_SelectedIndexChanging" 
                >
                <Columns>
                    <asp:BoundField DataField="ReservationDetailID" 
                        HeaderText="Reservation Detail ID" />
                    <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" 
                        SortExpression="GuestFirstName" />
                    <asp:BoundField DataField="GuestSurName" HeaderText="Surname" 
                        SortExpression="GuestSurName" />
                    <asp:BoundField DataField="ReservationID" HeaderText="Reservation ID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="ReservationID" />
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                </Columns>
                <RowStyle Font-Size="Small" />
                <HeaderStyle Font-Names="Arial" Font-Size="Small" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSelectGuest" runat="server" 
                Text="Select Guest" onclick="btnSelectGuest_Click" Visible="False" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblErrorMessage2" runat="server" ForeColor="Red"></asp:Label>
        </asp:View>
        <asp:View ID="vCheckInGuest" runat="server">
            <table style="width: 84%">
                <tr>
                    <td style="width: 133px">
                        Customer ID Number:</td>
                    <td class="style1" style="width: 156px">
                        <asp:Label ID="lblCustomerId" runat="server"></asp:Label>
                    </td>
                    <td style="width: 148px">
                        &nbsp;</td>
                    <td style="width: 134px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Reservation Number:</td>
                    <td class="style1" style="width: 156px">
                        <asp:Label ID="txtShowReservationNum" runat="server"></asp:Label>
                    </td>
                    <td style="width: 148px">
                        First Name:</td>
                    <td style="width: 134px">
                        <asp:Label ID="txtShowFirstName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Room Type:</td>
                    <td class="style1" style="width: 156px">
                        <asp:Label ID="txtShowRoomType" runat="server"></asp:Label>
                    </td>
                    <td style="width: 148px">
                        Surname:</td>
                    <td style="width: 134px">
                        <asp:Label ID="txtShowSurname" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Room Number:</td>
                    <td class="style1" style="width: 156px">
                        <asp:Label ID="txtShowRoomNum" runat="server"></asp:Label>
                    </td>
                    <td style="width: 148px">
                        Phone Number:</td>
                    <td style="width: 134px">
                        <asp:Label ID="txtShowPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Check out:</td>
                    <td class="style1" style="width: 156px">
                        <asp:Label ID="txtShowCheckOut" runat="server"></asp:Label>
                    </td>
                    <td style="width: 148px">
                        Number of Guests:</td>
                    <td>
                        <asp:Label ID="txtShowNumGuests" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Email:</td>
                    <td class="style1" style="width: 156px">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 148px">
                        Fax Number:</td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" Width="181px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Company:</td>
                    <td class="style1" style="width: 156px">
                        <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 148px">
                        ID Number:</td>
                    <td>
                        <asp:TextBox ID="txtIdNumber" runat="server" Width="181px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Address:</td>
                    <td class="style1" style="width: 156px">
                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 148px">
                        ID Issue Country:</td>
                    <td>
                        <asp:TextBox ID="txtIdCountry" runat="server" Width="181px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        City:</td>
                    <td class="style1" style="width: 156px">
                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 148px">
                        ID Comments:</td>
                    <td>
                        <asp:TextBox ID="txtIdComments" runat="server" Width="181px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Region:</td>
                    <td class="style1" style="width: 156px">
                        <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 148px">
                        &nbsp;</td>
                    <td rowspan="3">
                        <asp:TextBox ID="txtComments" runat="server" Height="46px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Postal Code:</td>
                    <td class="style1" style="width: 156px">
                        <asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 148px">
                        Comments:</td>
                </tr>
                <tr>
                    <td style="width: 133px">
                        Country:</td>
                    <td class="style1" style="width: 156px">
                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 148px">
                        &nbsp;</td>
                </tr>
            </table>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Back" 
                CommandName="PrevView" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCheckIn" runat="server" Text="Check in" 
                onclick="btnCheckIn_Click" />
        </asp:View>
        <asp:View ID="vCheckedIn" runat="server">
            <asp:Label ID="lblGuestCheckedIn" runat="server" Font-Size="Large" 
                Text="Guest has been checked in."></asp:Label>
        </asp:View>
    </asp:MultiView>
    <br />
    <br />
</asp:Content>
