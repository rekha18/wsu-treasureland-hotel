<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/Clerkreports.master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="TreasureLand.Clerk.WebForm4" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <asp:Label ID="lblDailyCheckin" runat="server" Font-Size="X-Large" 
            Text="Daily Check-out Report"></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    SizeToReportContent="True" WaitMessageFont-Names="Verdana" 
    WaitMessageFont-Size="14pt">
    <LocalReport ReportPath="Clerk\Reports\DailyCheckout.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:HotelDBM %>" SelectCommand="SELECT rm.RoomNumbers, g.GuestFirstName + ' ' + g.GuestSurName as GuestSurName, rd.QuotedRate, rd.CheckinDate, rd.Nights 
FROM ReservationDetail rd
JOIN Reservation r
ON r.ReservationID = rd.ReservationID
JOIN Guest g
ON g.GuestID = r.GuestID
JOIN Room rm
ON rm.RoomID = rd.RoomID
WHERE (DATEADD(dd, rd.Nights, rd.CheckInDate) &gt; DATEDIFF(dd, 1, GetDate()))
AND  (DATEADD(dd, rd.Nights, rd.CheckInDate) &lt; DATEADD(dd, 0, GetDate()))

ORDER BY rm.RoomNumbers"></asp:SqlDataSource>
<br />
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    </asp:Content>
