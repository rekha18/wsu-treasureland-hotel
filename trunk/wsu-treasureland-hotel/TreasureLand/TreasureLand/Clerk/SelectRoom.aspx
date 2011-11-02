<%@ Page Title="Select a room" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="SelectRoom.aspx.cs" Inherits="TreasureLand.Clerk.SelectRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <asp:Calendar ID="calDatePicker" runat="server" 
                onselectionchanged="calDatePicker_SelectionChanged"></asp:Calendar>
        </td>
        <td>
            <div id="dateselectors" style="width:inherit; margin-left: auto; margin-right: auto; text-align:center;">
                <span style="padding: 0px 5px 5px 5px;">
                    <asp:LinkButton ID="lbtnDatePrevious" runat="server" onclick="lbtnPrevious_Click"></asp:LinkButton>
                </span>
                <span style="padding: 0px 5px 5px 5px;">
                    <asp:LinkButton ID="lbtnToday" runat="server" onclick="lbtnToday_Click">Today</asp:LinkButton>
                </span>
                <span style="padding: 0px 5px 5px 5px;">
                    <asp:LinkButton ID="lbtnDateFuture" runat="server" onclick="lbtnFuture_Click"></asp:LinkButton>
                </span>
            </div>
            <div id="table" style="width:inherit; margin-left: auto; margin-right: auto; text-align:center;">
                <asp:Label ID="lblTable" runat="server" onprerender="generateTable"></asp:Label>
            </div>
            <div id="pageselectors" style="width:inherit; margin-left: auto; margin-right: auto; text-align:center;">
                <span style="padding: 0px 5px 5px 5px;">
                    <asp:LinkButton ID="lbtnPagePrevious" runat="server" 
                    onclick="lbtnPagePrevious_Click"></asp:LinkButton>
                </span>
                <span style="padding: 0px 5px 5px 5px;">
                    <asp:LinkButton ID="lbtnPageNext" runat="server" 
                    onclick="lbtnPageNext_Click"></asp:LinkButton>
                </span>
            </div>
        </td>
    </tr>
    <tr><!-- New Page Code Starts Here -->
        <td colspan="2">
            <span style="padding: 2px 0px 5px 0px;">
                <asp:Literal ID="lblPageStatus" runat="server">Add Current Reservation:</asp:Literal>
            </span>
            <div id="highlightrooms">
                <span style="padding: 5px 5px 0px 0px">Highlight rooms of type:</span>
                <asp:DropDownList ID="ddlRoomTypes" runat="server" 
                    OnDataBound="ddlRoomTypes_OnDataBind" DataSourceID="sqlRoomTypes" 
                    DataTextField="RoomType" DataValueField="RoomType" AutoPostBack="True">
                </asp:DropDownList>
                <!-- I know this isn't the best choice for a data source, but I was having
                issues using the object data source -->
                <asp:SqlDataSource ID="sqlRoomTypes" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:TreasureLandDB %>" 
                    SelectCommand="SELECT RoomType, HotelRoomTypeID FROM HotelRoomType">
                </asp:SqlDataSource>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            
            <asp:MultiView ID="mvRooms" runat="server" ActiveViewIndex="1">
                <asp:View ID="viewUpdateRoom" runat="server">
                    <div id="UpdateReservation">
                        <span style="padding: 5px 5px 0px 0px">Reservation #:</span>
                        <asp:TextBox ID="txtReservationNumber" runat="server" ControlToValidate="txtReservationNumber"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revReservationNumber" runat="server" 
                            ForeColor="Red" ValidationExpression="^\d+$" 
                            ControlToValidate="txtReservationNumber">Reservation # must be a number!</asp:RegularExpressionValidator>
                        <br />
                        <span style="padding: 5px 5px 0px 0px">Room #:</span>
                        <asp:TextBox ID="txtRoomNumberUpdate" runat="server"></asp:TextBox>
                        <span style="padding: 5px 5px 0px 0px"></span>
                        <asp:Button ID="btnUpdateReservation" runat="server" Text="Update Reservation" 
                            onclick="btnUpdateReservation_Click" />
                        <br />
                        <asp:Label ID="lblUpdateError" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </asp:View>
                <asp:View ID="viewSelectRoom" runat="server">
                    <div id="AddReservation">
                        <span style="padding: 5px 5px 0px 0px">Room #:</span>
                        <asp:TextBox ID="txtRoomNumberSelect" runat="server"></asp:TextBox>
                        <span style="padding: 5px 15px 0px 0px"></span>
                        <asp:Button ID="btnSelectRoom" runat="server" Text="Select Room" 
                            onclick="btnSelectRoom_Click" />
                        <asp:Label ID="lblSelectError" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </asp:View>
            </asp:MultiView>
            <div id="BackButton" style="float:right;">
                <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/Clerk/CreateReservation.aspx" />
            </div>
        </td>
    </tr>
</table>
    <script type="text/javascript">
    <!--
        var oldColor;

        function select(room) {
            var colZero = document.getElementById("row" + room);
            oldColor = colZero.style.backgroundColor;
            colZero.style.backgroundColor = "#7CC4FF";

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("room" + room + "col" + i + "a");
                if (id != null)
                    id.style.color = "#008CFF";
                id = document.getElementById("room" + room + "col" + i + "b");
                if (id != null)
                    id.style.color = "#008CFF";
            }
        }

        function deselect(room) {
            document.getElementById("row" + room).style.backgroundColor = oldColor;

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("room" + room + "col" + i + "a");
                if (id != null)
                    id.style.color = "#696969";
                id = document.getElementById("room" + room + "col" + i + "b");
                if (id != null)
                    id.style.color = "#696969";
            }
        }
    -->
    </script>
</asp:Content>
