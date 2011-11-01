<%@ Page Title="Locate Reservation" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="True" CodeBehind="LocateReservation.aspx.cs" Inherits="TreasureLand.Clerk.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvLocateReservation" runat="server" ActiveViewIndex="0">
        <asp:View ID="vLocateReservation" runat="server">
            <table class="style1" style="width: 543px">
                <tr>
                    <td class="style5">
                        Reservation Number:</td>
                    <td class="style3">
                        <asp:TextBox ID="txtReservationNum" runat="server"></asp:TextBox>
                    </td>
                    <td class="style4">
                        First Name:</td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        &nbsp;</td>
                    <td class="style3">
                        &nbsp;</td>
                    <td class="style4">
                        Last Name:</td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        <asp:Button ID="btnSearchReservation" runat="server" 
                            Text="Search Reservation" />
                    </td>
                    <td class="style3">
                        &nbsp;</td>
                    <td class="style4">
                        Email:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:GridView ID="gvGuest" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ReservationID" DataSourceID="sdsReservation">
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    <asp:BoundField DataField="ReservationID" HeaderText="Reservation #" 
                        InsertVisible="False" ReadOnly="True" SortExpression="ReservationID" />
                    <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" 
                        SortExpression="GuestFirstName" />
                    <asp:BoundField DataField="GuestSurName" HeaderText="Surname" 
                        SortExpression="GuestSurName" />
                    <asp:BoundField DataField="RoomNumbers" HeaderText="Room #" 
                        SortExpression="RoomNumbers" />

                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsReservation" runat="server" 
                ConnectionString="<%$ ConnectionStrings:TreasureLandDB %>" 
                SelectCommand="SELECT Reservation.ReservationID, Guest.GuestFirstName, Guest.GuestSurName, Room.RoomNumbers 
                               FROM Guest
                               INNER JOIN Reservation ON Guest.GuestID = Reservation.GuestID
                               INNER JOIN ReservationDetail ON ReservationDetail.ReservationID = Reservation.ReservationID 
                               INNER JOIN Room ON Room.RoomID = ReservationDetail.RoomID">
            </asp:SqlDataSource>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="Select" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" 
                Text="You must select a reservation" Visible="False"></asp:Label>
        </asp:View>
        <asp:View ID="vCheckOut" runat="server">
            <table class="style24">
                <tr>
                    <td class="style25">
                        Reservation number:</td>
                    <td class="style26">
                        <asp:TextBox ID="txtReservationNumber" runat="server"></asp:TextBox>
                    </td>
                    <td class="style27">
                        First Name:&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtFirstName2" runat="server" style="margin-left: 2px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style25">
                        Room Number:</td>
                    <td class="style26">
                        <asp:TextBox ID="txtRoomNumber" runat="server"></asp:TextBox>
                    </td>
                    <td class="style27">
                        Surname:</td>
                    <td>
                        <asp:TextBox ID="txtSurname2" runat="server" style="margin-left: 2px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style25">
                        Total:</td>
                    <td class="style26">
                        <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                    </td>
                    <td class="style27">
                        Email:</td>
                    <td>
                        <asp:TextBox ID="txtEmail2" runat="server" style="margin-left: 3px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" 
                Text="Error Message" Visible="False"></asp:Label>
            <br />
            <br />
            <table style="width: 62%; height: 26px;">
                <tr>
                    <td class="style22">
                        Manger Username:</td>
                    <td class="style20">
                        <asp:TextBox ID="txtMangerUname" runat="server"></asp:TextBox>
                    </td>
                    <td class="style18">
                        Discount Amount:</td>
                    <td class="style12">
                        <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style23">
                        Manager Password:</td>
                    <td class="style21">
                        <asp:TextBox ID="txtManagerPword" runat="server"></asp:TextBox>
                    </td>
                    <td class="style19">
                        &nbsp;</td>
                    <td class="style13">
                        <asp:Button ID="btnApply" runat="server" Text="Apply Discount" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Back" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAdjustPrice" runat="server" Text="Adjust Price" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Print Receipt" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" />
        </asp:View>
    </asp:MultiView>
    <br />
</asp:Content>
