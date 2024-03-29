﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="TreasureLand.Clerk.Print" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    
    <style type="text/css">
        .style1
        {
            width: 74%;
        }
        .style2
        {
            height: 23px;
        }
        .style6
        {
            width: 74px;
        }
        .style7
        {
            width: 47px;
        }
        .style8
        {
            width: 37px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
        <table class="style1">
            <tr>
                <td class="style2" colspan="2">
                    <asp:Image ID="Image1" runat="server" Height="66px" 
                        ImageUrl="~/Images/CompanyLogo.png" Width="504px" />
                </td>
                <td class="style8" rowspan="11">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <strong>SUMMARY OF CHARGES&nbsp;</strong></td>
            </tr>
            <tr>
                <td class="style6">
                    Guest Name:</td>
                <td class="style7">
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Reservation Number:</td>
                <td class="style7">
                    <asp:Label ID="lblReservationNumber" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Check-in Date:</td>
                <td class="style7">
                    <asp:Label ID="lblCheckinDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Check-out Date:</td>
                <td class="style7">
                    <asp:Label ID="lblCheckoutDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Room Number:</td>
                <td class="style7">
                    <asp:Label ID="lblRoomNumber" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Room Total:</td>
                <td class="style7">
                    <asp:Label ID="lblRoomTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Services Total:</td>
                <td class="style7">
                    <asp:Label ID="lblServicesTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Discount:</td>
                <td class="style7">
                    <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <strong>Total:</strong></td>
                <td class="style7">
                    <asp:Label ID="lblTotal" runat="server" style="font-weight: 700"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
                <asp:GridView ID="gvRoomCost" runat="server" AutoGenerateColumns="False" 
                    Width="658px">
                    <Columns>
                        <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
                        <asp:BoundField DataField="Nights" HeaderText="Number of Nights" />
                        <asp:BoundField DataField="QuotedRate" DataFormatString="{0:0.00}" 
                            HeaderText="Price per night" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="gvGuestServices" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" PageSize="8" 
                    ShowHeaderWhenEmpty="True" Width="654px">
                    <Columns>
                        <asp:TemplateField HeaderText="Transaction ID">
                            <EditItemTemplate>
                                <asp:Label ID="lblTransactionID" runat="server" 
                                    Text='<%# Eval("ReservationDetailBillingID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionID0" runat="server" 
                                    Text='<%# Bind("ReservationDetailBillingID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BillingItemDate" HeaderText="Date" ReadOnly="True" />
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
                        <asp:TemplateField HeaderText="Total Cost">
                            <EditItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" 
                                    
                                    Text='<%# ((Convert.ToDecimal(Eval("BillingAmount"))) * (Convert.ToDecimal(Eval("BillingItemQty")))).ToString("0.00") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" 
                                    
                                    Text='<%# ((Convert.ToDecimal(Eval("BillingAmount"))) * (Convert.ToDecimal(Eval("BillingItemQty")))).ToString("0.00") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Comments" HeaderText="Comments" />
                    </Columns>
                </asp:GridView>
                <input type="button"  value="Print"  onclick="window.print();" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnGoToCheckOut" runat="server" onclick="GoToCheckOut_Click" 
                    Text="CheckOut" Width="100px" />
                <asp:Label ID="lblReservationDetailID" runat="server" 
        Visible="False"></asp:Label>
    <br />
    <asp:Panel ID="pnCheckout" runat="server" Visible="False">
        <asp:Label ID="lblCheckout" runat="server" Text="Guest has been Checked out." 
            Font-Size="X-Large" ForeColor="Red"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Continue" />
    </asp:Panel>
    <asp:Panel ID="PnGuestOwedMoney" runat="server" Visible="False">
        <asp:Label ID="lblRefund" runat="server" 
            Text="Cannot check out.  Guest is owed a refund." Font-Size="X-Large" 
            ForeColor="Red"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="PnCollections" runat="server" Visible="False">
        <asp:Label ID="lblCheckoutMessage" runat="server" 
            Text="This guest still has a balance due.  Send to collections?" 
            Font-Size="X-Large" ForeColor="Red"></asp:Label>
        <br />
        <asp:Button ID="btnSendToCollections" runat="server" 
            onclick="btnSendToCollections_Click" Text="Send to Collections" />
    </asp:Panel>
    </form>
</body>
</html>
