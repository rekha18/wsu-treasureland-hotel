<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="ViewGuests.aspx.cs" Inherits="TreasureLand.Clerk.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
       <asp:MultiView ID="mvViewGuest" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewLocate" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td class="style1" style="width: 100px">
                            <asp:Label ID="lblRoom" runat="server" Text="Room #:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:TextBox ID="txtRoom" runat="server" MaxLength="5"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 83px">
                            <asp:Label ID="lblFirst" runat="server" Text="First Name:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 100px">
                            &nbsp;</td>
                        <td style="width: 177px">
                            &nbsp;</td>
                        <td class="style1" style="width: 83px">
                            <asp:Label ID="lblSurName" runat="server" Text="Surname:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:TextBox ID="txtSurName" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnLocate" runat="server" onclick="btnLocate_Click" 
                                Text="Locate Guest" ValidationGroup="vgView" Width="121px" />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ValidationGroup="vgView" />
                <br />
                <br />
                <asp:GridView ID="gvGuest" runat="server" AutoGenerateColumns="False" 
                    onselectedindexchanged="gvGuest_SelectedIndexChanged" 
                    onselectedindexchanging="gvGuest_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ReservationID" HeaderText="ReservationID" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="GuestFirstName" HeaderText="FirstName" />
                        <asp:BoundField DataField="GuestSurName" HeaderText="SurName" />
                        <asp:BoundField DataField="RoomNumbers" HeaderText="Room Number" />
                        <asp:BoundField DataField="reservationDetailID" 
                            HeaderText="ReservationDetailID" SortExpression="reservationDetailID" />
                        <asp:BoundField DataField="ReservationStatus" HeaderText="Status" />
                        <asp:BoundField DataField="RoomID" HeaderText="Room ID" />
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
                        <td class="style1" style="width: 133px">
                            <asp:Label ID="lblReservation0" runat="server" Text="Reservation #:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:Label ID="txtShowReservation" runat="server"></asp:Label>
                        </td>
                        <td class="style1" style="width: 135px">
                            <asp:Label ID="lblFirst0" runat="server" Text="First Name:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:Label ID="txtShowFirstName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 133px">
                            <asp:Label ID="lblRoom0" runat="server" Text="Room #:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:Label ID="txtShowRoom" runat="server"></asp:Label>
                        </td>
                        <td class="style1" style="width: 135px">
                            <asp:Label ID="lblSurName0" runat="server" Text="Surname:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:Label ID="txtShowSurName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 133px">
                            <asp:Label ID="lblRoomTotal" runat="server" Text="Room Total:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:Label ID="txtRoomTotal" runat="server"></asp:Label>
                        </td>
                        <td class="style1" style="width: 135px">
                            <asp:Label ID="lblDiscount" runat="server" Text="Discount"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:Label ID="txtDiscount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="width: 133px">
                            <asp:Label ID="lblServicesTotal" runat="server" Text="Services Total:"></asp:Label>
                        </td>
                        <td style="width: 177px">
                            <asp:Label ID="txtServicesTotal" runat="server"></asp:Label>
                        </td>
                        <td class="style1" style="width: 135px">
                            <asp:Label ID="lblTotal" runat="server" Text="Total Amount Owed:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:Label ID="txtTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvRoomCost" runat="server" AutoGenerateColumns="False" 
                    Width="658px">
                    <Columns>
                        <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
                        <asp:BoundField DataField="Nights" HeaderText="Number of Nights" />
                        <asp:BoundField DataField="QuotedRate" DataFormatString="{0:0.00}" 
                            HeaderText="Total Room Cost" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="gvGuestServices" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" 
                    onrowcancelingedit="gvGuestServices_RowCancelingEdit" 
                    onrowdeleting="gvGuestServices_RowDeleting" 
                    onrowediting="gvGuestServices_RowEditing" 
                    onrowupdated="gvGuestServices_RowUpdated" 
                    onrowupdating="gvGuestServices_RowUpdating" PageSize="8" 
                    ShowHeaderWhenEmpty="True" Width="654px">
                    <Columns>
                        <asp:TemplateField HeaderText="Transaction ID">
                            <EditItemTemplate>
                                <asp:Label ID="lblTransactionID" runat="server" 
                                    Text='<%# Eval("ReservationBillingID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionID" runat="server" 
                                    Text='<%# Bind("ReservationBillingID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ApplyFormatInEditMode="True" DataField="BillingDescription" 
                            HeaderText="Service" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Qty">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" CausesValidation="True" 
                                    Text='<%# Bind("BillingItemQty") %>' Width="41px"></asp:TextBox>
                                <asp:CompareValidator ID="cvQty" runat="server" ControlToValidate="txtQty" 
                                    Display="Dynamic" ErrorMessage="Qty must be a number" ForeColor="Red" 
                                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="vgService">*</asp:CompareValidator>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtQty" Display="Dynamic" 
                                    ErrorMessage="You must enter a number" ForeColor="Red" 
                                    ValidationGroup="vgService">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("BillingItemQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPrice" runat="server" CausesValidation="True" 
                                    Text='<%# Bind("BillingAmount", "{0:0.00}") %>' Width="64px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvPrice" runat="server" 
                                    ControlToValidate="txtPrice" ErrorMessage="Price is a required field" 
                                    ForeColor="Red" ValidationGroup="vgService">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="txtPrice" ErrorMessage="Price must be an amount" 
                                    ForeColor="Red" Operator="DataTypeCheck" Type="Double" 
                                    ValidationGroup="vgService">*</asp:CompareValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" 
                                    Text='<%# Bind("BillingAmount", "{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BillingItemDate" HeaderText="Date" ReadOnly="True" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" />
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" Visible="False" />
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" Visible="False" />
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary ID="vsGuestServices" runat="server" ForeColor="Red" 
                    ValidationGroup="vgService" />
                <br />
                <table style="width: 69%; height: 26px;">
                    <tr>
                        <td class="style22">
                            <asp:Label ID="lblManagerUser" runat="server" Text="Manager UserName" 
                                Visible="False"></asp:Label>
                        </td>
                        <td class="style20">
                            <asp:TextBox ID="txtMangerUname0" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td class="style18">
                            <asp:Label ID="Label4" runat="server" Text="Discount Amount" Visible="False"></asp:Label>
                        </td>
                        <td class="style12">
                            <asp:DropDownList ID="ddlDiscount" runat="server" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style23">
                            <asp:Label ID="lblPassword" runat="server" Text="Password" Visible="False"></asp:Label>
                        </td>
                        <td class="style21">
                            <asp:TextBox ID="txtManagerPword0" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td class="style19">
                            &nbsp;</td>
                        <td class="style13">
                            <asp:Button ID="btnApply0" runat="server" onclick="btnApply0_Click" 
                                Text="Apply Discount" Visible="False" />
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 126px">
                            <asp:Label ID="lblServies" runat="server" Text="Services and Fees:"></asp:Label>
                        </td>
                        <td style="width: 115px">
                            <asp:DropDownList ID="ddlServices" runat="server" TabIndex="-1">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 60px">
                            <asp:Label ID="lblQty" runat="server" Text="Quantity"></asp:Label>
                        </td>
                        <td style="width: 135px; margin-left: 40px;">
                            <asp:DropDownList ID="ddlQuantity" runat="server">
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
                            </asp:DropDownList>
                        </td>
                        <td style="width: 45px; margin-left: 40px;">
                            <asp:Label ID="lblCost" runat="server" Text="Cost:"></asp:Label>
                        </td>
                        <td style="width: 94px">
                            <asp:TextBox ID="txtCostofService" runat="server" MaxLength="10">0</asp:TextBox>
                        </td>
                        <td style="width: 177px">
                            <asp:Button ID="btnAddService" runat="server" onclick="btnAddService_Click" 
                                Text="Add Service" ValidationGroup="vgGuest" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 126px; height: 60px;">
                            <asp:Label ID="lblComments" runat="server" Text="Comments:  "></asp:Label>
                        </td>
                        <td style="width: 115px; height: 60px;">
                            <asp:TextBox ID="txtComments" runat="server" Height="45px" TextMode="MultiLine" 
                                Width="245px"></asp:TextBox>
                        </td>
                        <td style="width: 60px; height: 60px;">
                        </td>
                        <td style="width: 135px; margin-left: 40px; height: 60px;">
                        </td>
                        <td style="width: 45px; margin-left: 40px; height: 60px;">
                        </td>
                        <td style="width: 94px; height: 60px;">
                        </td>
                        <td style="width: 177px; height: 60px;">
                        </td>
                    </tr>
                </table>
                <asp:CompareValidator ID="cvCost" runat="server" 
                    ControlToValidate="txtCostofService" Display="Dynamic" 
                    ErrorMessage="You must enter a monetary value." ForeColor="Red" 
                    Operator="DataTypeCheck" Type="Currency" ValidationGroup="vgGuest"></asp:CompareValidator>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCostofService" Display="Dynamic" 
                    ErrorMessage="You must enter  an amount" ForeColor="Red" 
                    ValidationGroup="vgGuest"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="btnPrevious" runat="server" onclick="btnPrevious_Click" 
                    Text="Previous" Width="100px" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAdjustDiscount" runat="server" 
                    onclick="btnAdjustDiscount_Click" Text="Adjust Discount" Width="110px" 
                    Enabled="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnPrint" runat="server" onclick="btnPrint_Click" 
                    style="height: 26px" Text="Print Reciept" Width="100px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnGoToCheckOut" runat="server" onclick="GoToCheckOut_Click" 
                    Text="CheckOut" Width="100px" />
                <br />
                <br />
                <asp:Label ID="lblErrorGuest" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:View>
            <asp:View ID="View1" runat="server">
                <asp:Label ID="lblGuestCheckedOut" runat="server" Font-Size="Large" 
                    Text="Guest has been checked out."></asp:Label>
            </asp:View>
        </asp:MultiView>
    </p>
</asp:Content>
