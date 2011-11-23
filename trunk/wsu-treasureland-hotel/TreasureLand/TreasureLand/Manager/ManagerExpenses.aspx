<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ManagerExpenses.aspx.cs" Inherits="TreasureLand.Manager.ManagerExpenses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%">
    <tr>
        <td style="width: 112px; height: 30px">
            <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label>
        </td>
        <td style="width: 145px; height: 30px">
            <asp:TextBox ID="txtDate" runat="server" MaxLength="16"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfDate" runat="server" 
                ControlToValidate="txtDate" Display="Dynamic" 
                ErrorMessage="Date is a required field" ForeColor="Red" 
                ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvDate" runat="server" ControlToValidate="txtDate" 
                Display="Dynamic" ErrorMessage="Date must be a date" ForeColor="Red" 
                Operator="DataTypeCheck" Type="Date" ValidationGroup="vgAddExpense">*</asp:CompareValidator>
        </td>
        <td class="style1" style="width: 109px; height: 30px">
            &nbsp;</td>
        <td rowspan="3">
            <asp:TextBox ID="txtComments" runat="server" Height="71px" TextMode="MultiLine" 
                Width="230px" MaxLength="120"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 112px; height: 30px">
            <asp:Label ID="lblExpenseType" runat="server" Text="Type:"></asp:Label>
        </td>
        <td style="width: 145px; height: 30px">
            <asp:DropDownList ID="ddlExpenseType" runat="server" DataSourceID="sqlTypes" 
                DataTextField="AccountingType" DataValueField="AccountingTypeID">
            </asp:DropDownList>
        </td>
        <td class="style1" style="width: 109px; height: 30px">
            <asp:Label ID="lblExpenseDescription" runat="server" Text="Description:"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 112px; height: 30px">
            <asp:Label ID="lblExpenseCost" runat="server" Text="Cost:"></asp:Label>
        </td>
        <td style="width: 145px; height: 30px">
            <asp:TextBox ID="txtCost" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfCost" runat="server" 
                ControlToValidate="txtCost" Display="Dynamic" 
                ErrorMessage="Amount is a required field" ForeColor="Red" 
                ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvCost" runat="server" ControlToValidate="txtCost" 
                Display="Dynamic" ErrorMessage="Amount must be a number" ForeColor="Red" 
                Operator="DataTypeCheck" Type="Currency" ValidationGroup="vgAddExpense">*</asp:CompareValidator>
        </td>
        <td class="style1" style="width: 109px; height: 30px">
        </td>
    </tr>
</table>
<asp:Button ID="btnAddExpense" runat="server" Text="Add Expense" 
    Width="130px" onclick="btnAddExpense_Click" style="margin-bottom: 0px" 
        ValidationGroup="vgAddExpense" />
&nbsp;&nbsp;&nbsp;&nbsp;<br />
    &nbsp;&nbsp;
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        ValidationGroup="vgAddExpense" />
<br />
    <asp:SqlDataSource ID="sqlAccounting" runat="server"
      
    ConnectionString = "<%$ ConnectionStrings:TreasureLandConnectionString %>" 
    
        SelectCommand="SELECT        ACCOUNTING.AccountingID, ACCOUNTING.AccountingDate, ACCOUNTING.AccountingCost, ACCOUNTINGTYPE.AccountingType,  ACCOUNTING.AccountingDescription
FROM            ACCOUNTING INNER JOIN
                         ACCOUNTINGTYPE ON ACCOUNTING.AccountingTypeID = ACCOUNTINGTYPE.AccountingTypeID" 
        ConflictDetection="CompareAllValues" 
        DeleteCommand="DELETE FROM ACCOUNTING WHERE AccountingID = @original_AccountingID" 
        OldValuesParameterFormatString="original_{0}" 
        
        UpdateCommand="UPDATE    ACCOUNTING SET    [AccountingCost] = @AccountingCost, 
                     [AccountingDescription] = @AccountingDescription WHERE [AccountingID] = @original_AccountingID"  >
        <DeleteParameters>
            <asp:Parameter Name="original_AccountingID" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="AccountingCost" />
            <asp:Parameter Name="AccountingDescription" />
            <asp:Parameter Name="original_AccountingID" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTypes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TreasureLandConnectionString %>" 
        SelectCommand="SELECT [AccountingTypeID], [AccountingType] FROM [ACCOUNTINGTYPE]">
    </asp:SqlDataSource>
<br />
<asp:GridView ID="gvExpenses" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sqlAccounting" DataKeyNames="AccountingID" 
        AllowPaging="True" AllowSorting="True">
    <Columns>
        <asp:BoundField DataField="AccountingID" HeaderText="AccountingID" 
            InsertVisible="False" ReadOnly="True" SortExpression="AccountingID" />
        <asp:BoundField DataField="AccountingDate" DataFormatString="{0:d}" 
            HeaderText="AccountingDate" ReadOnly="True" />
        <asp:TemplateField HeaderText="AccountingCost" SortExpression="AccountingCost">
            <EditItemTemplate>
                <asp:TextBox ID="txtCost" runat="server" CausesValidation="True" 
                    Text='<%# Bind("AccountingCost", "{0:0.00}") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvGvCost" runat="server" 
                    ControlToValidate="txtCost" Display="Dynamic" 
                    ErrorMessage="Accounting Cost is required" ForeColor="Red" 
                    ValidationGroup="vgGV">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvAccountingCost" runat="server" 
                    ControlToValidate="txtCost" Display="Dynamic" 
                    ErrorMessage="Accounting Cost must be a number" ForeColor="Red" 
                    Operator="DataTypeCheck" Type="Currency" ValidationGroup="vgGV">*</asp:CompareValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" 
                    Text='<%# Bind("AccountingCost", "{0:0.00}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="AccountingType" 
            HeaderText="AccountingType" SortExpression="AccountingType" 
            ReadOnly="True" />
        <asp:BoundField DataField="AccountingDescription" 
            HeaderText="AccountingDescription" SortExpression="AccountingDescription" />
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
            ShowEditButton="True" ValidationGroup="vgGV" />
    </Columns>
</asp:GridView>
    <asp:ValidationSummary ID="vsAccounting" runat="server" ForeColor="Red" 
        ValidationGroup="vgGV" />
</asp:Content>
