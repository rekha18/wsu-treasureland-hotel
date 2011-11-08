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
                        <td class="style1" style="width: 329px">
                            <asp:TextBox ID="txtResNumber" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 134px">
                            First Name:</td>
                        <td class="style1" style="width: 329px">
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 134px">
                            Sur Name:</td>
                        <td class="style1" style="width: 329px">
                            <asp:TextBox ID="txtSurName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 134px">
                            Phone:</td>
                        <td class="style1" style="width: 329px">
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
                    onselectedindexchanged="gvGuest_SelectedIndexChanged" 
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ReservationID" HeaderText="Reservation Number" />
                        <asp:BoundField DataField="GuestSurName" HeaderText="Sur Name" />
                        <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="GuestPhone" HeaderText="Phone" />
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
                        <td style="width: 94px">
                            Reservation ID:</td>
                        <td>
                            <asp:Label ID="lblReservationNumber" runat="server" Text="-1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            Sur Name:</td>
                        <td>
                            <asp:Label ID="lblSurName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            First Name:</td>
                        <td>
                            <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 94px">
                            Phone:</td>
                        <td>
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvReservationDetails" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="ReservationDetailID" 
                    DataSourceID="ldsReservations" 
                    onselectedindexchanged="gvReservationDetails_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="RoomID" HeaderText="ID" />
                        <asp:BoundField DataField="QuotedRate" DataFormatString="{0:c}" 
                            HeaderText="QuotedRate" SortExpression="QuotedRate">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CheckinDate" DataFormatString="{0:d}" 
                            HeaderText="CheckinDate" SortExpression="CheckinDate">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nights" HeaderText="Nights" SortExpression="Nights">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Comments" HeaderText="Comments" 
                            SortExpression="Comments">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NumberOfAdults" HeaderText="Adults" 
                            SortExpression="NumberOfAdults">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NumberOfChildren" HeaderText="Children" 
                            SortExpression="NumberOfChildren">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                    </Columns>
                    <SelectedRowStyle BackColor="#FFFF66" />
                </asp:GridView>
                <asp:LinqDataSource ID="ldsReservations" runat="server" 
                    ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                    EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
                    TableName="ReservationDetails" Where="ReservationID == @ReservationID1">
                    <WhereParameters>
                        <asp:ControlParameter ControlID="lblReservationNumber" Name="ReservationID1" 
                            PropertyName="Text" Type="Int16" />
                    </WhereParameters>
                </asp:LinqDataSource>
                <br />
                <asp:GridView ID="gvRoom" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="RoomNumbers" HeaderText="Room Number" />
                        <asp:BoundField DataField="RoomDescription" HeaderText="Description" />
                        <asp:BoundField DataField="RoomBedConfiguration" HeaderText="Layout" />
                        <asp:BoundField DataField="RoomStatus" HeaderText="Status" />
                        <asp:BoundField DataField="RoomTypeRackRate" HeaderText="Rack Rate" />
                    </Columns>
                </asp:GridView>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View2" runat="server">
                    </asp:View>
                    <asp:View ID="View1" runat="server">
                    </asp:View>
                </asp:MultiView>
                <br />
                <br />
            </asp:View>
        </asp:MultiView>
        <br />
    </asp:Panel>
</asp:Content>
