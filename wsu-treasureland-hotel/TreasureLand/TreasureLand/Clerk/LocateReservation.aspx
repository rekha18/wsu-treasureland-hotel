<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="LocateReservation.aspx.cs" Inherits="TreasureLand.Clerk.LocateReservation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:MultiView ID="mvLocateReservation" runat="server" ActiveViewIndex="0">
        <asp:View ID="vLocateReservation" runat="server">
            <table class="style1" style="width: 713px">
                <tr>
                    <td class="style5" style="width: 186px">
                        Reservation #:</td>
                    <td class="style3" style="width: 134px">
                        <asp:TextBox ID="txtReservationNum" runat="server"></asp:TextBox>
                    </td>
                    <td class="style4" style="width: 80px">
                        First Name:</td>
                    <td style="width: 94px">
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblErrorMessageMissingData" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style5" style="width: 186px">
                        Room #:</td>
                    <td class="style3" style="width: 134px">
                        <asp:TextBox ID="txtRoomID" runat="server"></asp:TextBox>
                    </td>
                    <td class="style4" style="width: 80px">
                        Surname:</td>
                    <td style="width: 94px">
                        <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style5" style="width: 186px">
                        <asp:Button ID="btnLocateReservation" runat="server" 
                            Text="Locate Reservation" onclick="btnLocateReservation_Click" />
                    </td>
                    <td class="style3" style="width: 134px">
                        &nbsp;</td>
                    <td class="style4" style="width: 80px">
                        Email:</td>
                    <td style="width: 94px">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblErrorMessageNoCustomersFound" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:GridView ID="gvGuest" runat="server" AutoGenerateColumns="False"
                onselectedindexchanged="gvGuest_SelectedIndexChanged" 
                    onselectedindexchanging="gvGuest_SelectedIndexChanging" >
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    <asp:BoundField DataField="ReservationID" HeaderText="Reservation #" 
                        InsertVisible="False" ReadOnly="True" SortExpression="ReservationID" />
                    <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" 
                        SortExpression="GuestFirstName" />
                    <asp:BoundField DataField="GuestSurName" HeaderText="Surname" 
                        SortExpression="GuestSurName" />
                    <asp:BoundField DataField="RoomID" HeaderText="Room #" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" Font-Size="Small" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="CornflowerBlue" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="Blue" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="Blue" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                    ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="Select" 
                onclick="btnSelect_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblErrorMessageReservation" runat="server" ForeColor="Red"></asp:Label>
        </asp:View>
        <asp:View ID="vCheckOut" runat="server">
            <table class="style24">
                <tr>
                    <td class="style25">
                        Reservation number:</td>
                    <td class="style26">
                        <asp:TextBox ID="txtShowReservationNum" runat="server"></asp:TextBox>
                    </td>
                    <td class="style27">
                        First Name:&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtShowFirstName" runat="server" style="margin-left: 2px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style25">
                        Room Number:</td>
                    <td class="style26">
                        <asp:TextBox ID="txtShowRoomID" runat="server"></asp:TextBox>
                    </td>
                    <td class="style27">
                        Surname:</td>
                    <td>
                        <asp:TextBox ID="txtShowSurname" runat="server" style="margin-left: 2px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style25">
                        Total:</td>
                    <td class="style26">
                        <asp:TextBox ID="txtShowTotal" runat="server"></asp:TextBox>
                    </td>
                    <td class="style27">
                        Email:</td>
                    <td>
                        <asp:TextBox ID="txtShowEmail" runat="server" style="margin-left: 3px"></asp:TextBox>
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
            <asp:Button ID="btnBack" runat="server" Text="Back" 
                CommandName="PrevView" />
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
