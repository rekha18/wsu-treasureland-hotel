<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerSurvey.aspx.cs" Inherits="CustomerSurvey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 152px;
            height: 30px;
        }
        .style3
        {
            width: 134px;
            height: 30px;
        }
        .style4
        {
            height: 30px;
        }
        .style5
        {
            width: 157px;
            height: 30px;
        }
        .style7
        {
            width: 143px;
            height: 7px;
        }
        .style8
        {
            height: 23px;
        }
        .style9
        {
            width: 143px;
        }
        .style10
        {
            height: 23px;
            width: 148px;
        }
        .style11
        {
            width: 26px;
        }
        .style12
        {
            height: 7px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Italic="True" 
            Font-Size="XX-Large" ForeColor="Blue" Text="SportsPro"></asp:Label>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            DataFile="~/App_Data/TechSupport.mdb" SelectCommand="SELECT * FROM [Incidents]">
        </asp:AccessDataSource>
        <br />
        <br />
         <asp:Label ID="lblSubTitle" runat="server" ForeColor="Red" Font-Size="Medium"
        Text="Sports management software for the sports enthusiast"></asp:Label>
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Enter your customer ID:
                </td>
                <td class="style5">
                    <asp:TextBox ID="txtGetIncidents" runat="server" ontextchanged="Page_Load"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Button ID="btnGetIncidents" runat="server" Text="Get Incidents" 
                        Width="106px" onclick="btnGetIncidents_Click" />
                </td>
                <td class="style4">
                    <asp:RangeValidator ID="rgvCustomerID" runat="server" 
                        ControlToValidate="txtGetIncidents" Display="Dynamic" 
                        ErrorMessage="Field must be an interger" Font-Bold="True" ForeColor="Red" 
                        MaximumValue="1000000000" MinimumValue="0" SetFocusOnError="True" 
                        Type="Integer"></asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="rfvCustomerID" runat="server" 
                        ControlToValidate="txtGetIncidents" 
                        ErrorMessage="You must enter a customer ID." Font-Bold="True" ForeColor="Red" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
        <br />
        <br />
        <asp:ListBox ID="lstIncidents" runat="server" Height="97px" Width="548px" 
            Enabled="False">
        </asp:ListBox>
        <br />
        <br />
        <asp:Label ID="lblheader1" runat="server" Font-Bold="True" 
            Text="Please rate this incidnet by the following catagories:"></asp:Label>
        <table style="width:100%;">
            <tr>
                <td class="style7">
                    <asp:Label ID="lblResponseTime" runat="server" Text="Response time:"></asp:Label>
                </td>
                <td class="style12">
                    <asp:RadioButtonList ID="rblResponseTime" runat="server" CellPadding="1" 
                        CellSpacing="25" RepeatDirection="Horizontal" Enabled="False">
                        <asp:ListItem Value="1">Not Satisfied</asp:ListItem>
                        <asp:ListItem Value="2">Somewhat Satisfied</asp:ListItem>
                        <asp:ListItem Value="3">Satisfied</asp:ListItem>
                        <asp:ListItem Value="4">Completely satisfied</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="lblTechnicianEfficiency" runat="server" 
                        Text="Technician efficiency:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblTechnicianEfficiency" runat="server" 
                        CellPadding="1" CellSpacing="25" Height="43px" RepeatDirection="Horizontal" 
                        Width="583px" Enabled="False">
                        <asp:ListItem Value="1">Not Satisfied</asp:ListItem>
                        <asp:ListItem Value="2">Somewhat Satisfied</asp:ListItem>
                        <asp:ListItem Value="3">Satisfied</asp:ListItem>
                        <asp:ListItem Value="4">Completely satisfied</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="lblProblemResolution" runat="server" Text="Problem Resolution:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblProblemResolution" runat="server" CellPadding="1" 
                        CellSpacing="25" RepeatDirection="Horizontal" Enabled="False">
                        <asp:ListItem Value="1">Not Satisfied</asp:ListItem>
                        <asp:ListItem Value="2">Somewhat Satisfied</asp:ListItem>
                        <asp:ListItem Value="3">Satisfied</asp:ListItem>
                        <asp:ListItem Value="4">Completely satisfied</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td class="style10">
                    Additional Commnets:</td>
                <td class="style8">
                    <asp:TextBox ID="txtAdditionalComments" runat="server" Height="96px" 
                        Width="518px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:CheckBox ID="cbDiscuss" runat="server" 
            Text="Please contact me to discuss this incident" Enabled="False" />
        <table style="width:100%;">
            <tr>
                <td class="style11">
                    &nbsp;</td>
                <td>
                    <asp:RadioButton ID="rbContactByEmail" runat="server" Enabled="False" 
                        GroupName="Contact" Text="Contact by email" />
                    <br />
                    <asp:RadioButton ID="rbContactByPhone" runat="server" Enabled="False" 
                        GroupName="Contact" Text="Contact by phone" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Enabled="False" 
            onclick="btnSubmit_Click" />
    
    </div>
    </form>
</body>
</html>
