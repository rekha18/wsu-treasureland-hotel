<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/Clerkreports.master" AutoEventWireup="true" CodeBehind="CheckinReports.aspx.cs" Inherits="TreasureLand.Clerk.WebForm2" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <p>
        <asp:Label ID="lblDailyCheckin" runat="server" Font-Size="X-Large" 
            Text="Daily Check-in Report"></asp:Label>
    </p>
    <br />
    <asp:Label ID="lblChooseDate" runat="server" Text="ChooseDates:" 
        style="font-weight: 700" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblStartDate" runat="server" Text="Begin Date:" 
        style="font-weight: 700" Visible="False"></asp:Label>
    <asp:TextBox ID="txtBeginDate" runat="server" Visible="False"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="txtBeginDate_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtBeginDate">
    </ajaxToolkit:CalendarExtender>
&nbsp;<asp:RequiredFieldValidator ID="rffBeginDate" runat="server" 
        ControlToValidate="txtBeginDate" Display="Dynamic" 
        ErrorMessage="Begin Date is Required" ForeColor="Red" 
        ValidationGroup="vgDate" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cvBeginDate" runat="server" 
        ControlToValidate="txtBeginDate" Display="Dynamic" 
        ErrorMessage="Begin Date must be a date" ForeColor="Red" 
        Operator="DataTypeCheck" Type="Date" ValidationGroup="vgDate" 
        Enabled="False">*</asp:CompareValidator>
    &nbsp;
    <asp:Label ID="lblEndDate" runat="server" Text="End Date:" 
        style="font-weight: 700" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="txtEndDate" runat="server" Visible="False"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtEndDate">
    </ajaxToolkit:CalendarExtender>
    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" 
        ControlToValidate="txtEndDate" Display="Dynamic" 
        ErrorMessage="End date is required" ForeColor="Red" 
        ValidationGroup="vgDate" Enabled="False">*</asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cvEndDate" runat="server" 
        ControlToValidate="txtEndDate" Display="Dynamic" 
        ErrorMessage="End Date must be a date" ForeColor="Red" Operator="DataTypeCheck" 
        Type="Date" ValidationGroup="vgDate" Enabled="False">*</asp:CompareValidator>
    <br />
    <asp:Button ID="btnGetcheckins" runat="server" 
        onclick="btnGetTransactions_Click" Text="Get check-ins" 
        ValidationGroup="vgDate" Visible="False" />
    <br />
    <asp:ValidationSummary ID="vsDates" runat="server" ForeColor="Red" 
        ValidationGroup="vgDate" />
    <asp:SqlDataSource ID="sdsCheckIn" runat="server" 
        ConnectionString="<%$ ConnectionStrings:HotelDBM %>" 
        SelectCommand="SELECT rm.RoomNumbers, g.GuestFirstName, g.GuestSurName, rd.QuotedRate, rd.CheckinDate, rd.Nights
FROM ReservationDetail rd
JOIN Reservation r
ON r.ReservationID = rd.ReservationID
JOIN Guest g
ON g.GuestID = r.GuestID
JOIN Room rm
ON rm.RoomID = rd.RoomID
WHERE (rd.CheckinDate &gt; DATEDIFF(dd, 1, GetDate()))
AND  (rd.CheckinDate &lt; DATEADD(dd, 1, GetDate()))
ORDER BY rm.RoomNumbers">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsCheckInBetween" runat="server" 
        ConnectionString="<%$ ConnectionStrings:HotelDBM %>" 
        SelectCommand="SELECT rm.RoomNumbers, g.GuestFirstName, g.GuestSurName, rd.QuotedRate, rd.CheckinDate
FROM ReservationDetail rd
JOIN Reservation r
ON r.ReservationID = rd.ReservationID
JOIN Guest g
ON g.GuestID = r.GuestID
JOIN Room rm
ON rm.RoomID = rd.RoomID
WHERE (rd.CheckinDate between @CheckinDate1 and @CheckinDate2)">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtBeginDate" Name="CheckinDate1" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" Name="CheckinDate2" 
                PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
        SizeToReportContent="True">
        <LocalReport ReportPath="Clerk\Reports\DailyCheckin.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="sdsCheckIn" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
