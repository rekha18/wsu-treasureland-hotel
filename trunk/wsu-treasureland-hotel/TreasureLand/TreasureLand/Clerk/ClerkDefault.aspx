<%@ Page Title="Home Screen" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="ClerkDefault.aspx.cs" Inherits="TreasureLand.Clerk.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <table>
                <tr><td>Name: </td><td id="idGuestName"></td></tr>
                <tr><td>Reservation ID: </td><td id="idReservationID"></td></tr>
                <tr><td>Detail ID: </td><td id="idResDetailID"></td></tr>
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
</table>
    <script type="text/javascript">
    <!--

        var oldColor;

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
            return;
        }

        function onReservationClick(resID, startDate, nights, info, room) {
            txtBox = document.getElementById("idGuestName");
            if (txtBox != null && resID != 0)
                txtBox.innerHTML = info.substring(0, info.indexOf('?'));

            txtBox = document.getElementById("idReservationID");
            if (txtBox != null && resID != 0)
                txtBox.innerHTML = info.substring(info.indexOf('?') + 1, info.length);

            txtBox = document.getElementById("idResDetailID");
            if (txtBox != null && resID != 0)
                txtBox.innerHTML = resID;
        }
    -->
    </script>
</asp:Content>
