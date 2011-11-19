<%@ Page Title="Select a room" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="SelectRoom.aspx.cs" Inherits="TreasureLand.Clerk.SelectRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <table>
                <tr><td>Name: </td><td id="idGuestName"></td></tr>
                <tr><td>ReservationID: </td><td id="idReservationID"></td></tr>
            </table>
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
                    DataTextField="RoomType" DataValueField="RoomType" AutoPostBack="True" 
                    onselectedindexchanged="ddlRoomTypes_onSelectedIndexChanged">
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
                        <asp:TextBox ID="txtReservationNumber" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revReservationNumber" runat="server" 
                            ForeColor="Red" ValidationExpression="^\d+$" 
                            ControlToValidate="txtReservationNumber">Reservation # must be a number!</asp:RegularExpressionValidator>
                        <br />
                        <span style="padding: 5px 5px 0px 0px">Room #:</span>
                        <asp:TextBox ID="txtRoomNumberUpdate" runat="server"></asp:TextBox>
                        <br />
                        <span style="padding: 5px 5px 0px 0px">Check in date:</span>
                        <asp:TextBox ID="txtReservationDate" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="cvDate" runat="server" ForeColor="Red" Type="Date" 
                            ValueToCompare="11/11/2011" SetFocusOnError="False" Operator="DataTypeCheck" 
                            ControlToValidate="txtReservationDate">Check in date must contain a valid date!</asp:CompareValidator>
                        <br />
                        <span style="padding: 5px 5px 0px 0px">Nights:</span>
                        <asp:DropDownList ID="ddlNightsStayed" runat="server" ViewStateMode="Enabled">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                        </asp:DropDownList>
                        <br />
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
                        <br />
                        <asp:Label ID="lblSelectError" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </asp:View>
            </asp:MultiView>
        </td>
    </tr>
</table>
    <script type="text/javascript">
    <!--
        var oldColor;
        var oldRowColor;
        var oldRowID;

        function select(room) {
            var colZero = document.getElementById("row" + room);
            oldColor = colZero.style.backgroundColor;
            colZero.style.backgroundColor = "#7CC4FF";

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("row" + room + "col" + i + "a");
                if (id != null)
                    id.style.color = "#008CFF";
                id = document.getElementById("row" + room + "col" + i + "b");
                if (id != null)
                    id.style.color = "#008CFF";
            }
        }

        function deselect(room) {
            document.getElementById("row" + room).style.backgroundColor = oldColor;

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("row" + room + "col" + i + "a");
                if (id != null)
                    id.style.color = "#696969";
                id = document.getElementById("row" + room + "col" + i + "b");
                if (id != null)
                    id.style.color = "#696969";
            }
        }

        function onRoomClick(room) {
            var txtBox = document.getElementById("MainContent_ContentPlaceHolder1_txtRoomNumberUpdate");

            if (txtBox != null) {
                txtBox.value = room;
            }
            else {
                txtBox = document.getElementById("MainContent_ContentPlaceHolder1_txtRoomNumberSelect");
                txtBox.value = room;
            }

            //Reset the colors of the old rooms
            if (oldRowColor != null) {
                var colZero = document.getElementById("row" + oldRowID);
                oldColor = colZero.style.backgroundColor = "#AAAAAA";
                for (var i = 0; i < 7; i++) {
                    var id = document.getElementById("row" + oldRowID + "col" + i + "a");
                    if (id != null) {
                        if (id.style.backgroundColor == "rgb(255, 204, 255)") {
                            id.style.backgroundColor = oldRowColor;
                        }
                    }
                    id = document.getElementById("row" + oldRowID + "col" + i + "b");
                    if (id != null) {
                        if (id.style.backgroundColor == "rgb(255, 204, 255)") {
                            id.style.backgroundColor = oldRowColor;
                        }
                    }
                }
            }

            //Highlight all non-room colored sections
            oldRowID = room;
            var colZero = document.getElementById("row" + room);
            oldColor = colZero.style.backgroundColor = "#FFCCFF";
            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("row" + room + "col" + i + "a");
                if (id != null) {
                    //alert(id.style.backgroundColor);
                    if (id.style.backgroundColor == "rgb(204, 204, 204)" || id.style.backgroundColor == "rgb(255, 255, 255)" || id.style.backgroundColor == "rgb(255, 241, 142)") {
                        oldRowColor = id.style.backgroundColor;
                        id.style.backgroundColor = "#FFCCFF";
                    }
                }
                id = document.getElementById("row" + room + "col" + i + "b");
                if (id != null) {
                    if (id.style.backgroundColor == "rgb(204, 204, 204)" || id.style.backgroundColor == "rgb(255, 255, 255)" || id.style.backgroundColor == "rgb(255, 241, 142)") {
                        oldRowColor = id.style.backgroundColor;
                        id.style.backgroundColor = "#FFCCFF";
                    }
                }
            }
        }

        function onReservationClick(resID, startDate, nights, info) {
            var txtBox = document.getElementById("MainContent_ContentPlaceHolder1_txtReservationNumber");
            if (txtBox != null && resID != 0)
                txtBox.value = resID;

            txtBox = document.getElementById("MainContent_ContentPlaceHolder1_ddlNightsStayed");
            if (txtBox != null && resID != 0)
                txtBox.value = nights;

            txtBox = document.getElementById("MainContent_ContentPlaceHolder1_txtReservationDate");
            if (txtBox != null && resID != 0)
                txtBox.value = startDate;

            txtBox = document.getElementById("idGuestName");
            if (txtBox != null && resID != 0)
                txtBox.innerHTML = info.substring(0, info.indexOf('?'));

            txtBox = document.getElementById("idReservationID");
            if (txtBox != null && resID != 0)
                txtBox.innerHTML = info.substring(info.indexOf('?')+1, info.length);
        }
    -->
    </script>
</asp:Content>
