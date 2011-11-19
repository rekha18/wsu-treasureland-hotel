<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="True" CodeBehind="LocateGuest.aspx.cs" Inherits="TreasureLand.Clerk.LocateGuest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvLocateGuest" runat="server" ActiveViewIndex="0">
        <asp:View ID="vLocateGuest" runat="server">
            <table class="style1" style="width: 676px">
                <tr>
                    <td class="style18">
                        Reservation #:</td>
                    <td class="style24">
                        <asp:TextBox ID="txtReservationNum" runat="server"></asp:TextBox>
                    </td>
                    <td class="style12" style="width: 80px">
                        First Name:</td>
                    <td class="style22" style="width: 136px">
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    </td>
                    <td class="style22" style="width: 136px">
                        &nbsp;</td>
                    <td class="style13">
                        <asp:Label ID="lblErrorMessageMissingData" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style19">
                        <asp:Button ID="btnLocateGuest" runat="server" OnClick="btnLocateGuest_Click" 
                            Text="Locate Guest" ValidationGroup="lookup" />
                    </td>
                    <td class="style25">
                        &nbsp;</td>
                    <td class="style4" style="width: 80px">
                        Surname:</td>
                    <td class="style23" style="width: 136px">
                        <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                    </td>
                    <td class="style23" style="width: 136px">
                        &nbsp;</td>
                    <td class="style5">
                        <asp:Label ID="lblErrorMessageNoCustomersFound" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
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
                        Reservation Number:</td>
                    <td class="style1" style="width: 156px">
                        <asp:Label ID="txtShowReservationNum" runat="server"></asp:Label>
                    </td>
                    <td style="width: 116px">
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
                    <td style="width: 116px">
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
                    <td style="width: 116px">
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
                    <td style="width: 116px">
                        Number of Guests:</td>
                    <td>
                        <asp:Label ID="txtShowNumGuests" runat="server"></asp:Label>
                    </td>
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
    </asp:MultiView>
    <br />
    <br />
</asp:Content>
