<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/Clerkreports.master" AutoEventWireup="true" CodeBehind="OwedByRoomReport.aspx.cs" Inherits="TreasureLand.Clerk.WebForm6" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:HotelDBM %>" SelectCommand="SELECT DISTINCT rm.RoomNumbers, SUM(rdb.BillingAmount * rdb.BillingItemQty)+ ((rd.QuotedRate) * DateDiff(dd, rd.CheckinDate, GETDATE())) as Total, rd.Nights
FROM Room rm
JOIN ReservationDetail rd 
ON rm.RoomID = rd.RoomID
RIGHT JOIN ReservationDetailBilling rdb
ON rdb.ReservationDetailID = rd.ReservationDetailID
WHERE rm.RoomStatus ='C'
GROUP BY rm.RoomNumbers, rd.QuotedRate, Nights, rd.CheckinDate, rd.Nights">
</asp:SqlDataSource>
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajaxToolkit:ToolkitScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
    SizeToReportContent="True">
    <LocalReport ReportPath="Clerk\Reports\AmountOwedByRoom.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<br />
</asp:Content>
