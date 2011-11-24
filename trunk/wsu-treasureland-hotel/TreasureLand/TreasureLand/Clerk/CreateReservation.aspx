<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="CreateReservation.aspx.cs" Inherits="TreasureLand.Clerk.CreateReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <asp:MultiView ID="mvReservation" runat="server" ActiveViewIndex="0" >
            <asp:View ID="vLocateGuest" runat="server">
                <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderStyle="Solid">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 73px">
                                First Name:</td>
                            <td class="style1" style="width: 155px">

                                <asp:TextBox ID="txtFirstName" runat="server" OnClick="this.value=''" 
                                    MaxLength="30"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                                    ControlToValidate="txtFirstName" 
                                    ErrorMessage="First name is required to add a guest" ForeColor="Red" 
                                    ValidationGroup="vgNewGuest"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Surname:</td>
                            <td class="style1" style="width: 155px">
                                <asp:TextBox ID="txtSurName" runat="server" OnClick="this.value=''" 
                                    MaxLength="30"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvSurname" runat="server" 
                                    ControlToValidate="txtSurName" 
                                    ErrorMessage="Surname is required to add a guest" ForeColor="Red" 
                                    ValidationGroup="vgNewGuest"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Phone:</td>
                            <td class="style1" style="width: 155px">
                                <asp:TextBox ID="txtPhone" runat="server" OnClick="this.value=''" 
                                    MaxLength="20"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" 
                                    ControlToValidate="txtPhone" ErrorMessage="Phone is required to add a guest" 
                                    ForeColor="Red" ValidationGroup="vgNewGuest"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                <asp:Button ID="btnLocateGuest" runat="server" onclick="btnLocateGuest_Click" 
                                    Text="Locate Guest" ValidationGroup="lookup" />
                            </td>
                            <td class="style1" style="width: 155px">
                                <asp:Button ID="btnNewGuest" runat="server" 
                                    Text="New Guest" CommandArgument="2" 
                                    CommandName="SwitchViewByIndex" onclick="btnNewGuest_Click" 
                                    ValidationGroup="vgNewGuest" />
                            </td>
                            <td>
                                <asp:Label ID="lblErrorNoGuest" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="lblErrorInsertGuest" runat="server" Font-Bold="True" 
                        Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                    <br />
                    <asp:GridView ID="gvGuest" runat="server" AutoGenerateColumns="False" 
                        onselectedindexchanged="gvGuest_SelectedIndexChanged">
                        <Columns>
                             <asp:BoundField DataField="GuestID" HeaderText="ID" ReadOnly="True" 
                                SortExpression="GuestID" />
                            <asp:BoundField DataField="GuestSurName" HeaderText="Sur Name" ReadOnly="True" 
                                SortExpression="GuestSurName" />
                            <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" 
                                ReadOnly="True" SortExpression="GuestFirstName" />
                            <asp:BoundField DataField="GuestPhone" HeaderText="Phone Number" 
                                ReadOnly="True" SortExpression="GuestPhone" />
                            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFF66" ForeColor="Black" />
                    </asp:GridView>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnSelectGuest" runat="server" CommandArgument="2" 
                        CommandName="SwitchViewByIndex" Enabled="False" Height="26px" 
                        onclick="btnSelectGuest_Click" Text="Select Guest" />
                    <br />
                    <br />
                </asp:Panel>
            </asp:View>
            <asp:View ID="vNewGuest" runat="server">
                <asp:Panel ID="Panel2" runat="server" BackColor="Silver" BorderStyle="Solid">
                </asp:Panel>
            </asp:View>
            <asp:View ID="vCreateReservation" runat="server">
                <asp:Panel ID="Panel3" runat="server" BackColor="Silver" BorderStyle="Solid">
                    <br />
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 73px">
                                Date:</td>
                            <td style="width: 167px">
                                <asp:Label ID="lblDateToday" runat="server"></asp:Label>
                            </td>
                            <td style="width: 143px">
                                Number of Adults:</td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlAdults" runat="server" 
                                    >
                                    <asp:ListItem Selected="True">1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Sur Name:</td>
                            <td style="width: 167px">
                                <asp:Label ID="lblResSurName" runat="server"></asp:Label>
                            </td>
                            <td style="width: 143px">
                                Number of Children:</td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlChildren" runat="server"           
                                    >
                                    <asp:ListItem Selected="True">0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                First Name:</td>
                            <td style="width: 167px">
                                <asp:Label ID="lblResFirstName" runat="server"></asp:Label>
                            </td>
                            <td style="width: 143px">
                                &nbsp;</td>
                            <td rowspan="2">
                                &nbsp;</td>
                            <td rowspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Phone #:</td>
                            <td style="width: 167px">
                                <asp:Label ID="lblResPhone" runat="server"></asp:Label>
                            </td>
                            <td id="Discont" style="width: 143px">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td rowspan="4" style="width: 244px">
                                <asp:Calendar ID="calDateFrom" runat="server" 
                                    onselectionchanged="calDateFrom_SelectionChanged" />
                            </td>
                            <td style="width: 239px">
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" 
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 239px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 239px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 244px">
                                Checkin Date:</td>
                            <td style="width: 239px">
                                Number of Nights:</td>
                            <td>
                                Checkout Date:</td>
                        </tr>
                        <tr>
                            <td style="width: 244px">
                                <asp:Label ID="lblDateFrom" runat="server" Font-Size="Large"></asp:Label>
                            </td>
                            <td style="width: 239px">
                                <asp:DropDownList ID="ddlNumberOfDays" runat="server" AutoPostBack="True"          
                                    >
                                    <asp:ListItem Selected="True">1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblDateTo" runat="server" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 244px">
                                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                    Text="Cancel" />
                            </td>
                            <td style="width: 239px">
                                Select Room:</td>
                            <td>
                                <asp:Button ID="btnSelectRoom" runat="server" onclick="btnSelectRoom_Click" 
                                    Text="Select Room" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
            </asp:View>
            <asp:View ID="View1" runat="server">
                <asp:Panel ID="Panel4" runat="server" BackColor="Silver">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 475px">
                                &nbsp;Select any discounts:&nbsp;
                                <asp:DropDownList ID="ddlDiscounts" runat="server" AppendDataBoundItems="True" 
                                    AutoPostBack="True" DataTextField="DiscountDescription" 
                                    DataValueField="DiscountID">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 104px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="gvDiscount" runat="server" AutoGenerateColumns="False" 
                                    Width="228px">
                                    <Columns>
                                        <asp:BoundField DataField="DiscountRules" HeaderText="DiscountRules" 
                                            ReadOnly="True" SortExpression="DiscountRules" />
                                        <asp:BoundField DataField="DiscountAmount" HeaderText="DiscountAmount" 
                                            ReadOnly="True" SortExpression="DiscountAmount" />
                                        <asp:CheckBoxField DataField="IsPrecentage" HeaderText="IsPrecentage" 
                                            ReadOnly="True" SortExpression="IsPrecentage" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="gvRoomInfo" runat="server" Width="414px">
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 475px">
                                &nbsp;</td>
                            <td style="width: 104px">
                                Quoted Cost:</td>
                            <td>
                                <asp:Label ID="lblTotalCost" runat="server"></asp:Label>₵
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 475px">
                                &nbsp;</td>
                            <td style="width: 104px">
                                <asp:Button ID="btnBack2" runat="server" onclick="btnBack2_Click" Text="Back" />
                            </td>
                            <td>
                                <asp:Button ID="btnReserve" runat="server" CommandArgument="4" 
                                    CommandName="SwitchViewByIndex" onclick="btnReserve_Click" Text="Reserve" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:Panel ID="Panel5" runat="server" BackColor="Silver">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="center">
                                Thank you for placing your reservation.</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="center">
                                Your Reservation number is:
                                <asp:Label ID="lblFinalReservationNumber" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="center">
                                <asp:Button ID="btnDone" runat="server" onclick="btnDone_Click" Text="Done" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
  
   
</asp:Content>
