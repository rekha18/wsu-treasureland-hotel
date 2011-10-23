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
                                <asp:TextBox ID="txtFirstName" runat="server">-Enter Name-</asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                                    ControlToValidate="txtFirstName" ErrorMessage="First Name is required" 
                                    ForeColor="Red" ValidationGroup="lookup"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Sur Name:</td>
                            <td class="style1" style="width: 155px">
                                <asp:TextBox ID="txtSurName" runat="server">-Enter Name-</asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvSurName" runat="server" 
                                    ErrorMessage="Sur Name is required" ForeColor="Red" 
                                    ValidationGroup="lookup" ControlToValidate="txtSurName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Phone:</td>
                            <td class="style1" style="width: 155px">
                                <asp:TextBox ID="txtPhone" runat="server">-Enter Phone-</asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" 
                                    ErrorMessage="Phone is required" ForeColor="Red" ValidationGroup="lookup" 
                                    ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                <asp:Button ID="btnLocateGuest" runat="server" onclick="btnLocateGuest_Click" 
                                    Text="Locate Guest" ValidationGroup="lookup" />
                            </td>
                            <td class="style1" style="width: 155px">
                                <asp:Button ID="btnNewGuest" runat="server" 
                                    Text="New Guest" CausesValidation="False" CommandArgument="1" 
                                    CommandName="SwitchViewByIndex" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView ID="gvGuest" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="odsLocateGuest" 
                        onselectedindexchanged="gvGuest_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="GuestSurName" HeaderText="Sur Name" ReadOnly="True" 
                                SortExpression="GuestSurName" />
                            <asp:BoundField DataField="GuestFirstName" HeaderText="First Name" 
                                ReadOnly="True" SortExpression="GuestFirstName" />
                            <asp:BoundField DataField="GuestPhone" HeaderText="Phone Number" 
                                ReadOnly="True" SortExpression="GuestPhone" />
                            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnSelectGuest" runat="server" CommandArgument="2" 
                        CommandName="SwitchViewByIndex" Enabled="False" Height="26px" 
                        onclick="btnSelectGuest_Click" Text="Select Guest" />
                    <br />
                    <br />
                    <asp:ObjectDataSource ID="odsLocateGuest" runat="server" 
                        ConflictDetection="CompareAllValues" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="LocateGuest" 
                        TypeName="LocateGuestDB" InsertMethod="AddGuest">
                        <InsertParameters>
                            <asp:Parameter Name="FirstName" Type="String" />
                            <asp:Parameter Name="SurName" Type="String" />
                            <asp:Parameter Name="Phone" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtFirstName" DefaultValue="" Name="FirstName" 
                                PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtSurName" Name="SurName" PropertyName="Text" 
                                Type="String" />
                            <asp:ControlParameter ControlID="txtPhone" Name="Phone" PropertyName="Text" 
                                Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
            </asp:View>
            <asp:View ID="vNewGuest" runat="server">
                <asp:Panel ID="Panel2" runat="server" BackColor="Silver" BorderStyle="Solid">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 75px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 75px">
                                First Name:</td>
                            <td>
                                <asp:TextBox ID="txtFirstNameInsert" runat="server" Width="160px"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:RequiredFieldValidator ID="rfvFirstNameInsert" runat="server" 
                                    ControlToValidate="txtFirstNameInsert" ErrorMessage="First Name is required" 
                                    ForeColor="Red" ValidationGroup="lookup"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 75px">
                                Sur Name:</td>
                            <td>
                                <asp:TextBox ID="txtSurNameInsert" runat="server" Width="160px"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:RequiredFieldValidator ID="rfvSurNameInsert" runat="server" 
                                    ControlToValidate="txtSurNameInsert" ErrorMessage="Sur Name is required" 
                                    ForeColor="Red" ValidationGroup="lookup"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 75px">
                                Phone:</td>
                            <td>
                                <asp:TextBox ID="txtPhoneInsert" runat="server" Width="160px"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:RequiredFieldValidator ID="rfvPhoneInsert" runat="server" 
                                    ControlToValidate="txtPhoneInsert" ErrorMessage="Phone is required" 
                                    ForeColor="Red" ValidationGroup="lookup"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button ID="btnBack" runat="server" CommandArgument="0" 
                        CommandName="SwitchViewByIndex" Text="Back" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAddGuest" runat="server" Text="Add Guest" 
                        onclick="btnAddGuest_Click" CommandArgument="2" 
                        CommandName="SwitchViewByIndex" />
                    <asp:LinqDataSource ID="ldsGuest" runat="server" 
                        ContextTypeName="TreasureLand.TreasureLandDataClassesDataContext" 
                        EnableInsert="True" EntityTypeName="" TableName="Guests">
                    </asp:LinqDataSource>
                </asp:Panel>
            </asp:View>
            <asp:View ID="vCreateReservation" runat="server">
                <asp:Panel ID="Panel3" runat="server" BackColor="Silver" BorderStyle="Solid">
                    <br />
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 73px">
                                Date:</td>
                            <td style="width: 176px">
                                <asp:Label ID="lblDateToday" runat="server"></asp:Label>
                            </td>
                            <td style="width: 129px">
                                Number of Adults:</td>
                            <td>
                                <asp:DropDownList ID="ddlAdults" runat="server" AutoPostBack="True">
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
                            <td style="width: 176px">
                                <asp:Label ID="lblResSurName" runat="server"></asp:Label>
                            </td>
                            <td style="width: 129px">
                                Number of Children:</td>
                            <td>
                                <asp:DropDownList ID="ddlChildren" runat="server" AutoPostBack="True">
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
                            <td style="width: 176px">
                                <asp:Label ID="lblResFirstName" runat="server"></asp:Label>
                            </td>
                            <td style="width: 129px">
                                Discount:</td>
                            <td>
                                <asp:DropDownList ID="ddlDiscounts" runat="server" DataSourceID="ldsDiscout" 
                                    DataTextField="DiscountDescription" DataValueField="DiscountID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                Phone #:</td>
                            <td style="width: 176px">
                                <asp:Label ID="lblResPhone" runat="server"></asp:Label>
                            </td>
                            <td ID="Discont" style="width: 129px">
                                Dicount Amount:</td>
                            <td>
                                <asp:Label ID="lblDiscountAmount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                &nbsp;</td>
                            <td style="width: 176px">
                                &nbsp;</td>
                            <td rowspan="2" style="width: 129px">
                                Dicsount Rules:</td>
                            <td rowspan="2">
                                <asp:Label ID="lblDiscountRules" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 73px">
                                &nbsp;</td>
                            <td style="width: 176px">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <asp:LinqDataSource ID="ldsDiscout" runat="server" 
                        ContextTypeName="TreasureLand.TreasureLandDataClassesDataContext" 
                        EntityTypeName="" TableName="Discounts" 
                        Where="DiscountExpiration &lt; @DiscountExpiration">
                        <WhereParameters>
                            <asp:ControlParameter ControlID="lblDateToday" Name="DiscountExpiration" 
                                PropertyName="Text" Type="DateTime" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    <asp:LinqDataSource ID="ldsRoomType" runat="server" 
                        ContextTypeName="TreasureLand.TreasureLandDataClassesDataContext" 
                        EntityTypeName="" TableName="HotelRoomTypes">
                    </asp:LinqDataSource>
                    <br />
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 244px" rowspan="4">
                                <asp:Calendar ID="calDateFrom" runat="server" 
                                    onselectionchanged="calDateFrom_SelectionChanged" />
                            </td>
                            <td style="width: 239px">
                                RoomType:</td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                    DataSourceID="ldsRoomType" DataTextField="RoomType" 
                                    DataValueField="HotelRoomTypeID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 239px">
                                Select Room:</td>
                            <td>
                                <asp:Button ID="btnSelectRoom" runat="server" Text="Select Room" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 239px">
                                Total Quoted Cost:</td>
                            <td>
                                <asp:Label ID="lblTotalCost" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 239px">
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnReserve" runat="server" Enabled="False" Text="Reserve" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 244px">
                                Date From:</td>
                            <td style="width: 239px">
                                Number of Days:</td>
                            <td>
                                Date To:</td>
                        </tr>
                        <tr>
                            <td style="width: 244px">
                                <asp:Label ID="lblDateFrom" runat="server" Font-Size="Large"></asp:Label>
                            </td>
                            <td style="width: 239px">
                                <asp:DropDownList ID="ddlNumberOfDays" runat="server" AutoPostBack="True">
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
                    </table>
                    <br />
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
  
   
</asp:Content>
