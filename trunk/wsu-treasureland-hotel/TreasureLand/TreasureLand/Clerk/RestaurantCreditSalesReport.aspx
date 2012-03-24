<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/Clerkreports.master" AutoEventWireup="true" CodeBehind="RestaurantCreditSalesReport.aspx.cs" Inherits="TreasureLand.Clerk.WebForm5" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:SqlDataSource ID="sdsCreditSalesDay" runat="server" 
    ConnectionString="<%$ ConnectionStrings:HotelDBM %>" SelectCommand="
SELECT rdb.ReservationDetailBillingID, rdb.BillingDescription, rdb.BillingAmount,
              rdb.BillingItemQty, rdb.Comments, rdb.TransEmployee, u.UserName,
              li.lineItemTransactionID, li.LineItemAmount, me.MenuItemName
FROM ReservationDetailBilling rdb
JOIN LineItem li
ON rdb.ReservationDetailBillingID = li.ReservationDetailBillingID
JOIN MenuItem me 
ON me.MenuItemID = li.MenuItemID
JOIN ReservationDetailBilling rd
ON rd.ReservationDetailBillingID = li.ReservationDetailBillingID
LEFT JOIN dbo.aspnet_Membership m
ON m.Pin = rdb.TransEmployee
JOIN dbo.aspnet_Users u
ON u.UserID = m.UserID
WHERE rdb.ReservationDetailID is not NULL
AND  (rdb.BillingItemDate &gt; DATEDIFF(dd, 1, GetDate()))
AND  (rdb.BillingItemDate &lt; DATEADD(dd, 1, GetDate()))
ORDER BY me.MenuItemName"></asp:SqlDataSource>
<asp:SqlDataSource ID="sdsCreditSalesDateRange" runat="server" 
    ConnectionString="<%$ ConnectionStrings:HotelDBM %>" SelectCommand="
SELECT rdb.ReservationDetailBillingID, rdb.BillingDescription, rdb.BillingAmount,
              rdb.BillingItemQty, rdb.Comments, rdb.TransEmployee, u.UserName,
              li.lineItemTransactionID, li.LineItemAmount, me.MenuItemName
FROM ReservationDetailBilling rdb
JOIN LineItem li
ON rdb.ReservationDetailBillingID = li.ReservationDetailBillingID
JOIN MenuItem me 
ON me.MenuItemID = li.MenuItemID
JOIN ReservationDetailBilling rd
ON rd.ReservationDetailBillingID = li.ReservationDetailBillingID
LEFT JOIN dbo.aspnet_Membership m
ON m.Pin = rdb.TransEmployee
JOIN dbo.aspnet_Users u
ON u.UserID = m.UserID
WHERE rdb.ReservationDetailID is not NULL
AND  (rdb.BillingItemDate &gt;= @date1)
AND  (rdb.BillingItemDate &lt;= @date2)
ORDER BY me.MenuItemName" onselected="sdsCreditSalesDateRange_Selected" 
        onselecting="sdsCreditSalesDateRange_Selecting">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtBeginDate" Name="date1" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txtEndDate" Name="date2" PropertyName="Text" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:Label ID="lblCashSalesReport" runat="server" Font-Size="X-Large" 
    Text="Credit Sales Report"></asp:Label>
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajaxToolkit:ToolkitScriptManager>
<asp:Label ID="lblChooseDate" runat="server" Text="ChooseDates:" 
        style="font-weight: 700"></asp:Label>
<br />
<br />
<asp:Label ID="lblStartDate" runat="server" Text="Begin Date:" 
        style="font-weight: 700"></asp:Label>
<asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="txtBeginDate_CalendarExtender" runat="server" 
    Enabled="True" TargetControlID="txtBeginDate" Format="d/MM/yyyy" 
        TodaysDateFormat="d MMMM , yyyy">
</ajaxToolkit:CalendarExtender>
&nbsp;<asp:RequiredFieldValidator ID="rffBeginDate" runat="server" 
        ControlToValidate="txtBeginDate" Display="Dynamic" 
        ErrorMessage="Begin Date is Required" ForeColor="Red" ValidationGroup="vgDate">*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="cvBeginDate" runat="server" 
        ControlToValidate="txtBeginDate" Display="Dynamic" 
        ErrorMessage="Begin Date must be a date" ForeColor="Red" 
        Operator="DataTypeCheck" Type="Date" ValidationGroup="vgDate">*</asp:CompareValidator>
    &nbsp;
    <asp:Label ID="lblEndDate" runat="server" Text="End Date:" 
        style="font-weight: 700"></asp:Label>
&nbsp;<asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
    Enabled="True" TargetControlID="txtEndDate" Format="d/MM/yyyy" 
        TodaysDateFormat="d MMMM , yyyy">
</ajaxToolkit:CalendarExtender>
<asp:RequiredFieldValidator ID="rfvEndDate" runat="server" 
        ControlToValidate="txtEndDate" Display="Dynamic" 
        ErrorMessage="End date is required" ForeColor="Red" ValidationGroup="vgDate">*</asp:RequiredFieldValidator>
<asp:CompareValidator ID="cvEndDate" runat="server" 
        ControlToValidate="txtEndDate" Display="Dynamic" 
        ErrorMessage="End Date must be a date" ForeColor="Red" Operator="DataTypeCheck" 
        Type="Date" ValidationGroup="vgDate">*</asp:CompareValidator>
<br />
<asp:Button ID="btnGetSales" runat="server" 
        onclick="btnGetTransactions_Click" Text="Get Credit Sales" 
        ValidationGroup="vgDate" />
<br />
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    SizeToReportContent="True" WaitMessageFont-Names="Verdana" 
    WaitMessageFont-Size="14pt" Visible="False">
    <LocalReport ReportPath="Clerk\Reports\TotalRestaurantSales.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="sdsCreditSalesDay" 
                Name="reportDatasource" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
</asp:Content>
