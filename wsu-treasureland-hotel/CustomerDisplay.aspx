<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerDisplay.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sports Pro Home</title>
    <style type="text/css">
        .style1
        {
            width: 117px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 352px">
    
        <asp:Label ID="lblTitle" runat="server" EnableTheming="True" 
             ForeColor="Blue" Font-Bold="true" Font-Size="XX-Large" 
            Text="Sports Pro"></asp:Label>
    
        <br />
    <asp:Label ID="lblSubTitle" runat="server" ForeColor="Red" Font-Size="Medium"
        Text="Sports management software for the sports enthusiast"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblCustomer" runat="server" Text="Select a Customer:"></asp:Label>
&nbsp;&nbsp;
    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" 
        DataSourceID="AccessDataSource1" DataTextField="Name" 
        DataValueField="CustomerID">
    </asp:DropDownList>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="~/App_Data/TechSupport.mdb" SelectCommand="SELECT * FROM [Customers]">
    </asp:AccessDataSource>
    <br />
    <br />
    <table style="width: 37%;">
        <tr>
            <td class="style1">
                <asp:Label ID="lblAddress1" runat="server" Text="Address:"></asp:Label>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblCity" runat="server"></asp:Label>
                ,
                <asp:Label ID="lblState" runat="server"></asp:Label>
&nbsp;<asp:Label ID="lblZip" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblPhone1" runat="server" Text="Phone:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPhone2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblEmail1" runat="server" Text="Email:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmail2" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
        <br />
        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        <br />
    <br />
    <asp:Button ID="butAddToContact" runat="server" onclick="btnAdd_Click"
       Text="Add to Contact List"   />
&nbsp;&nbsp;
    <asp:Button ID="butDisplayContactList" runat="server" PostBackUrl="~/ContactList.aspx"
        Text="Display Contact List" />
     </div>  
    </form>
</body>
</html>
