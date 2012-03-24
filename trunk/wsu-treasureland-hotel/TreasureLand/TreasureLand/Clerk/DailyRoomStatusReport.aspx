<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/Clerkreports.master" AutoEventWireup="true" CodeBehind="DailyRoomStatusReport.aspx.cs" Inherits="TreasureLand.Clerk.WebForm10" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:HotelDBM %>" SelectCommand="SELECT rm.RoomNumbers, rm.RoomStatus, rd.CheckinDate, rd.Nights, DATEADD(dd, rd.Nights, rd.CheckInDate) as CheckoutDate
FROM ReservationDetail rd
JOIN Reservation r
ON r.ReservationID = rd.ReservationID
JOIN Guest g
ON g.GuestID = r.GuestID
JOIN Room rm
ON rm.RoomID = rd.RoomID
WHERE rm.RoomStatus ='C'
ORDER BY CheckoutDate"></asp:SqlDataSource>
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajaxToolkit:ToolkitScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    SizeToReportContent="True" WaitMessageFont-Names="Verdana" 
    WaitMessageFont-Size="14pt">
    <LocalReport ReportPath="Clerk\Reports\DailyRoomBookingStatus.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<br />
</asp:Content>
