<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="UpdateReservation.aspx.cs" Inherits="TreasureLand.Clerk.UpdateReservation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pUpdateReservation" runat="server" BackColor="Silver">
        <asp:MultiView ID="mvUpdateReservation" runat="server" ActiveViewIndex="0">
            <asp:View ID="vLocateReservation" runat="server">
                <table style="width:100%;">
                    <tr>
                        <td class="style1" colspan="2">
                            Locate Reservation</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 134px">
                            Reservation Number:</td>
                        <td class="style1" style="width: 192px">
                            <asp:TextBox ID="txtResNumber" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 134px">
                            First Name:</td>
                        <td class="style1" style="width: 192px">
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 134px">
                            Sur Name:</td>
                        <td class="style1" style="width: 192px">
                            <asp:TextBox ID="txtSurName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 134px">
                            Phone:</td>
                        <td class="style1" style="width: 192px">
                            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:Button ID="btnLocateReservation" runat="server" 
                    onclick="btnLocateReservation_Click" Text="Locate Reservation" />
                <br />
                <br />
                <br />
                <asp:GridView ID="gvGuest" runat="server" 
                    onselectedindexchanged="gvGuest_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    </Columns>
                    <SelectedRowStyle BackColor="#FFFF66" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnSelectReservation" runat="server" CommandArgument="1" 
                    CommandName="SwitchViewByIndex" Enabled="False" 
                    onclick="btnSelectReservation_Click" Text="Select Reservation" />
                <br />
            </asp:View>
            <asp:View ID="vUpdateReservation" runat="server">
                <table style="width:100%;">
                    <tr>
                        <td rowspan="9" style="width: 25px">
                            <asp:DetailsView ID="dvReservation" runat="server" AutoGenerateRows="False" 
                                DataKeyNames="ReservationDetailID" DataSourceID="ldsReservations" Height="50px" 
                                Width="125px">
                                <Fields>
                                    <asp:BoundField DataField="ReservationDetailID" 
                                        HeaderText="ReservationDetailID" InsertVisible="False" ReadOnly="True" 
                                        SortExpression="ReservationDetailID" />
                                    <asp:BoundField DataField="RoomID" HeaderText="RoomID" 
                                        SortExpression="RoomID" />
                                    <asp:BoundField DataField="ReservationID" HeaderText="ReservationID" 
                                        SortExpression="ReservationID" />
                                    <asp:BoundField DataField="QuotedRate" HeaderText="QuotedRate" 
                                        SortExpression="QuotedRate" />
                                    <asp:BoundField DataField="CheckinDate" HeaderText="CheckinDate" 
                                        SortExpression="CheckinDate" />
                                    <asp:BoundField DataField="Nights" HeaderText="Nights" 
                                        SortExpression="Nights" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" 
                                        SortExpression="Status" />
                                    <asp:BoundField DataField="Comments" HeaderText="Comments" 
                                        SortExpression="Comments" />
                                    <asp:BoundField DataField="DiscountID" HeaderText="DiscountID" 
                                        SortExpression="DiscountID" />
                                    <asp:BoundField DataField="NumberOfAdults" HeaderText="NumberOfAdults" 
                                        SortExpression="NumberOfAdults" />
                                    <asp:BoundField DataField="NumberOfChildren" HeaderText="NumberOfChildren" 
                                        SortExpression="NumberOfChildren" />
                                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                </Fields>
                            </asp:DetailsView>
                        </td>
                        <td style="width: 94px">
                            Reservation ID:</td>
                        <td>
                            <asp:Label ID="lblReservationNumber" runat="server" Text="-1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:LinqDataSource ID="ldsReservations" runat="server" 
                    ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                    EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
                    TableName="ReservationDetails" Where="ReservationID == @ReservationID1">
                    <WhereParameters>
                        <asp:ControlParameter ControlID="lblReservationNumber" Name="ReservationID1" 
                            PropertyName="Text" Type="Int16" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </asp:View>
        </asp:MultiView>
        <br />
    </asp:Panel>
</asp:Content>
