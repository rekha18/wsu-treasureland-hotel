<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true"
    CodeBehind="RoomItemPay.aspx.cs" Inherits="TreasureLand.Clerk.RoomItemPay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Table ID="Table1" runat="server" Height="440px" Width="707px">

        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="3">
                <asp:Button ID="RoomBack" runat="server" Text="Previous 14" Height="50px" Width="200px" />
            </asp:TableCell>
            <asp:TableCell><span style="text-decoration: underline; font-size: large; color:  #000066;"><strong>ROOM</strong></span></asp:TableCell>
            <asp:TableCell ColumnSpan="3">
                <asp:Button ID="RoomForward" runat="server" Text="Next 14" Height="50px" Width="200px" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow HorizontalAlign="Center" VerticalAlign="Top">
            <asp:TableCell>
                <asp:Button ID="btnRoom1" runat="server" Text="RM 101" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom2" runat="server" Text="RM 102" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom3" runat="server" Text="RM 103" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom4" runat="server" Text="RM 104" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom5" runat="server" Text="RM 105" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom6" runat="server" Text="RM 106" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom7" runat="server" Text="RM 107" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow HorizontalAlign="Center" VerticalAlign="Top">
                  <asp:TableCell>
                <asp:Button ID="btnRoom8" runat="server" Text="RM 108" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom9" runat="server" Text="RM 109" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom10" runat="server" Text="RM 110" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom11" runat="server" Text="RM 111" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom12" runat="server" Text="RM 112" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom13" runat="server" Text="RM 113" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnRoom14" runat="server" Text="RM 114" Height="73px" Width="72px" BackColor="Blue" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow HorizontalAlign="Center" VerticalAlign="Top">
         <asp:TableCell ColumnSpan="7">
                <span style="text-decoration: underline;
                    font-size: large; color: #000066;"><strong>PAY CASH</strong></span><br />
                <br />
                <asp:Button ID="btnCashPay" runat="server" Text="Pay Cash" Width="288px" Height="72px" /></asp:TableCell>

        </asp:TableRow>
    </asp:Table>
</asp:Content>

