<%@ Page Title="Home Screen" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="ClerkDefault.aspx.cs" Inherits="TreasureLand.Clerk.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dateselectors">
        <span style="float:left;">
            <asp:LinkButton ID="lbtnDatePrevious" runat="server" onclick="lbtnPrevious_Click"></asp:LinkButton>
        </span>
        <span style="float:left;">
            <asp:LinkButton ID="lbtnToday" runat="server" onclick="lbtnToday_Click">Today</asp:LinkButton>
        </span>
        <span style="float:left; height: 17px;">
            <asp:LinkButton ID="lbtnDateFuture" runat="server" onclick="lbtnFuture_Click"></asp:LinkButton>
        </span>
    </div>
    <div id="table">
        <asp:Label ID="lblTable" runat="server" onprerender="generateTable"></asp:Label>
    </div>
    <div id="pageselectors">
        <span style="text-align:center;">
            <asp:LinkButton ID="lbtnPagePrevious" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnPageNext" runat="server"></asp:LinkButton>
        </span>
    </div>
    <script type="text/javascript">
    <!--

        function select(room) {
            document.getElementById("row" + room).style.backgroundColor = "#7CC4FF";
            var backColor;
            if (room % 2 == 0)
                backColor = "#AAAAAA";
            else
                backColor = "#FFFFFF";

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("room" + room + "col" + i);
                id.style.color = "#008CFF";
                //id.style.fontWeight = 'bold';
            }
        }

        function deselect(room) {
            document.getElementById("row" + room).style.backgroundColor = "#AAAAAA";
            var backColor;
            if (room % 2 == 0)
                backColor = "#AAAAAA";
            else
                backColor = "#FFFFFF";

            for (var i = 0; i < 7; i++) {
                var id = document.getElementById("room" + room + "col" + i);
                id.style.color = "#696969";
                //id.style.fontWeight = 'normal';
            }
        }
    -->
    </script>
</asp:Content>
