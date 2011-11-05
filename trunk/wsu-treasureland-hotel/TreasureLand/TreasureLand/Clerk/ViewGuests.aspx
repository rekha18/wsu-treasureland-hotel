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
                            &nbsp;</td>
                        <td style="width: 177px">
                            &nbsp;</td>
                        <td class="style1" style="width: 83px">
                            <asp:Label ID="lblSurName" runat="server" Text="Sur Name:"></asp:Label>
                        </td>
                        <td class="style1" style="width: 236px">
                            <asp:TextBox ID="txtSurName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnLocate" runat="server" onclick="btnLocate_Click" 
                                Text="Locate Guest" Width="121px" ValidationGroup="vgView" />
                        </td>
                    </tr>
                </table>
                <asp:CompareValidator ID="cvReservationID" runat="server" 
                    ControlToValidate="txtReservation" Display="None" 
                    ErrorMessage="Reservation must be a number" ForeColor="Red" 
                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="vgView"></asp:CompareValidator>
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
                        <asp:BoundField DataField="RoomID" HeaderText="RoomID" />
                        <asp:BoundField DataField="reservationDetailID" 
                            HeaderText="ReservationDetailID" SortExpression="reservationDetailID" />
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
                            <asp:Label ID="lblSurName0" runat="server" Text="Sur Name:"></asp:Label>
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
                        <asp:BoundField DataField="RoomDescription" HeaderText="Room Type" />
                        <asp:BoundField DataField="Nights" HeaderText="Number of Nights" />
                        <asp:BoundField DataField="QuotedRate" HeaderText="Cost Per Night" 
                            DataFormatString="{0:0.00}" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="gvGuestServices" runat="server" AutoGenerateColumns="False" 
                    onrowediting="gvGuestServices_RowEditing" 
                    onrowupdating="gvGuestServices_RowUpdating" PageSize="8" 
                    ShowHeaderWhenEmpty="True" Width="654px" AllowSorting="True" 
                    onrowcancelingedit="gvGuestServices_RowCancelingEdit" 
                    onrowdeleting="gvGuestServices_RowDeleting" 
                    onrowupdated="gvGuestServices_RowUpdated">
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
                        <asp:BoundField DataField="BillingDescription" HeaderText="Service" 
                            ApplyFormatInEditMode="True" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Qty">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("BillingItemQty") %>' 
                                    Width="41px"></asp:TextBox>
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
                                <asp:TextBox ID="txtPrice" runat="server" 
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
                        <asp:TemplateField HeaderText="Additional Comments">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtComments" runat="server" Height="50px" Rows="2" 
                                    TextMode="MultiLine" Width="219px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" Visible="False" />
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" Visible="False" />
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" 
                    ValidationGroup="vgService" />
                <br />
                <br />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 126px">
                            Services and Fees:</td>
                        <td style="width: 115px">
                            <asp:DropDownList ID="ddlServices" runat="server" 
                                TabIndex="-1">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 60px">
                            Quantity:</td>
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
                            <asp:TextBox ID="txtCostofService" runat="server">0</asp:TextBox>
                        </td>
                        <td style="width: 177px">
                            <asp:Button ID="btnAddService" runat="server" onclick="btnAddService_Click" 
                                Text="Add Service" ValidationGroup="vgGuest" />
                        </td>
                    </tr>
                </table>
                <asp:CompareValidator ID="cvCost" runat="server" Display="Dynamic" 
                    ErrorMessage="You must enter a monetary value." ForeColor="Red" 
                    Operator="DataTypeCheck" Type="Currency" ValidationGroup="vgGuest" 
                    ControlToValidate="txtCostofService"></asp:CompareValidator>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCostofService" Display="Dynamic" 
                    ErrorMessage="You must enter  an amount" ForeColor="Red" 
                    ValidationGroup="vgGuest"></asp:RequiredFieldValidator>
                <br />
                <asp:Button ID="btnPrevious" runat="server" Text="Previous" 
                    onclick="btnPrevious_Click" />
                <br />
                <br />
                <asp:Label ID="lblErrorGuest" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:View>
        </asp:MultiView>
    </p>
</asp:Content>
