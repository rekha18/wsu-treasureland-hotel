<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="ContactList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contact List</title>
    <style type="text/css">
        .style1
        {
            width: 229px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" 
            Font-Size="XX-Large" ForeColor="Blue" Text="SportsPro"></asp:Label>
        <br />
        <br />
         <asp:Label ID="lblSubTitle" runat="server" ForeColor="Red" Font-Size="Medium"
        Text="Sports management software for the sports enthusiast"></asp:Label>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <table style="width:100%;">
            <tr>
                <td class="style1" rowspan="3">
        <asp:ListBox ID="lstCart" runat="server" Height="227px" Width="422px">
        </asp:ListBox>
                </td>
                <td>
        <asp:Button ID="btnRemoveContact" runat="server" 
            onclick="btnRemoveContact_Click" Text="Remove Contact" />
                </td>
            </tr>
            <tr>
                <td>
        <asp:Button ID="btnClearList" runat="server" onclick="btnClearList_Click" 
            Text="Clear List" />
    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnReturnToContact" runat="server"
                        PostBackUrl="~/CustomerDisplay.aspx" Text="Select Another Contact" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
