<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="ViewGuests.aspx.cs" Inherits="TreasureLand.Clerk.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
       <asp:MultiView ID="mvViewGuest" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewLocate" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td class="style1" style="width: 100px">
                            <asp:Label ID="lblReservation" runat="server" Text="Reservation #:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:TextBox ID="txtReservation" runat="server"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 83px">
                            <asp:Label ID="lblFirst" runat="server" Text="First Name:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 100px">
                            <asp:Label ID="lblRoom" runat="server" Text="Room #:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:TextBox ID="txtRoomNumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 83px">
                            <asp:Label ID="lblSurName" runat="server" Text="Sur Name:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:TextBox ID="txtSurName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnLocate" runat="server" onclick="btnLocate_Click" 
                                Text="Locate Guest" Width="121px" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:GridView ID="gvGuest" runat="server" AutoGenerateColumns="False" 
                    onselectedindexchanged="gvGuest_SelectedIndexChanged" 
                    onselectedindexchanging="gvGuest_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ReservationID" HeaderText="ReservationID" />
                        <asp:BoundField DataField="GuestFirstName" HeaderText="FirstName" />
                        <asp:BoundField DataField="GuestSurName" HeaderText="SurName" />
                        <asp:BoundField DataField="RoomID" HeaderText="RoomID" />
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Button ID="btnNext" runat="server" onclick="btnNext_Click" Text="Next" 
                    Visible="False" Width="59px" />
                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </asp:View>
            <asp:View ID="viewGuest" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td class="style1" style="width: 100px">
                            <asp:Label ID="lblReservation0" runat="server" Text="Reservation #:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:TextBox ID="txtShowReservation" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 83px">
                            <asp:Label ID="lblFirst0" runat="server" Text="First Name:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:TextBox ID="txtShowFirstName" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 100px">
                            <asp:Label ID="lblRoom0" runat="server" Text="Room #:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:TextBox ID="txtShowRoom" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 83px">
                            <asp:Label ID="lblSurName0" runat="server" Text="Sur Name:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:TextBox ID="txtShowSurName" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 100px">
                            <asp:Label ID="lblTotal" runat="server" Text="Total:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:TextBox ID="txtTotal" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 83px">
                            &nbsp;</td>
                        <td class="style1" style="width: 236px">
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 135px">
                            <asp:DropDownList ID="ddlServices" runat="server" TabIndex="-1" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 94px">
                            <asp:TextBox ID="txtCostofService" runat="server">$0.00</asp:TextBox>
                        </td>
                        <td style="width: 177px">
                            <asp:Button ID="btnAddService" runat="server" Text="Add Service" 
                                onclick="btnAddService_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:Button ID="btnPrevious" runat="server" Text="Previous" />
                <br />
                <br />
                <asp:Label ID="lblErrorGuest" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:View>
        </asp:MultiView>
    </p>
</asp:Content>
