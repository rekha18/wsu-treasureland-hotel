<%@ Page Title="Locate Reservation" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="LocateReservation.aspx.cs" Inherits="TreasureLand.Clerk.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvLocateReservation" runat="server">
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
            CHECK OUT:<asp:ListView ID="lvLocateReservation" runat="server" 
                DataKeyNames="ReservationID" DataSourceID="sdsReservation">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;">
                        <td>
                            <asp:Label ID="ReservationIDLabel" runat="server" 
                                Text='<%# Eval("ReservationID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuestIDLabel" runat="server" Text='<%# Eval("GuestID") %>' />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="background-color: #008A8C; color: #FFFFFF;">
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                Text="Cancel" />
                        </td>
                        <td>
                            <asp:Label ID="ReservationIDLabel1" runat="server" 
                                Text='<%# Eval("ReservationID") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="GuestIDTextBox" runat="server" Text='<%# Bind("GuestID") %>' />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" 
                        style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
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
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="GuestIDTextBox0" runat="server" 
                                Text='<%# Bind("GuestID") %>' />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #DCDCDC; color: #000000;">
                        <td>
                            <asp:Label ID="ReservationIDLabel2" runat="server" 
                                Text='<%# Eval("ReservationID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuestIDLabel0" runat="server" Text='<%# Eval("GuestID") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                    style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th runat="server">
                                            ReservationID</th>
                                        <th runat="server">
                                            GuestID</th>
                                    </tr>
                                    <tr ID="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" 
                                style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                        <td>
                            <asp:Label ID="ReservationIDLabel3" runat="server" 
                                Text='<%# Eval("ReservationID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="GuestIDLabel1" runat="server" Text='<%# Eval("GuestID") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="sdsReservation" runat="server" 
                ConnectionString="<%$ ConnectionStrings:TreasureLandDB %>" 
                SelectCommand="SELECT [ReservationID] FROM [Reservation]">
            </asp:SqlDataSource>
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="Select" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" 
                Text="You must select a reservation"></asp:Label>
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
                Text="Error Message"></asp:Label>
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
