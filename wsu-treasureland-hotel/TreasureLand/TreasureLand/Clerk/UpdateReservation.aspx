<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="UpdateReservation.aspx.cs" Inherits="TreasureLand.Clerk.WebForm6" %>
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
            <p>Add Current Reservation</p>
            <div id="Div1">
                <span style="padding: 5px 5px 0px 0px">Highlight rooms of type:</span>
                <asp:DropDownList ID="ddlRoomTypes" runat="server">
                </asp:DropDownList>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div id="AddReservation">
                <span style="padding: 5px 5px 0px 0px">Room #:</span>
                <asp:TextBox ID="txtRoomNumber" runat="server"></asp:TextBox>
                <span style="padding: 5px 25px 0px 0px"></span>
                <asp:Button ID="btnSelectRoom" runat="server" Text="Button" />
            </div>
        </td>
    </tr>
</table>
    <script type="text/javascript">
    <!--

        function select(room) {
            document.getElementById("row" + room).style.backgroundColor = "#7CC4FF";

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("room" + room + "col" + i);
                id.style.color = "#008CFF";
                //id.style.fontWeight = 'bold';
            }
        }

        function deselect(room) {
            document.getElementById("row" + room).style.backgroundColor = "#AAAAAA";

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("room" + room + "col" + i);
                id.style.color = "#696969";
                //id.style.fontWeight = 'normal';
            }
        }
    -->
    </script>
</asp:Content>
