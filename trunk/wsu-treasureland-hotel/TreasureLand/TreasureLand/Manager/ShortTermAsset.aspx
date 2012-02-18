<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ShortTermAsset.aspx.cs" Inherits="TreasureLand.Manager.ShortTermAsset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 421px">
    <tr>
        <td style="width: 81px; height: 30px">
            <asp:Label ID="lblInventoryName" runat="server" Text="Name"></asp:Label>
        </td>
        <td style="width: 128px; height: 30px">
            <asp:TextBox ID="txtInventoryName" runat="server" MaxLength="16"></asp:TextBox>
                <asp:CompareValidator ID="cvName" runat="server" 
                    ControlToValidate="txtInventoryName" Display="Dynamic" 
                    ErrorMessage="Name must be text" ForeColor="Red" Operator="DataTypeCheck" 
                    ValidationGroup="vgAddExpense">*</asp:CompareValidator> 
                <asp:RequiredFieldValidator ID="rfName" runat="server" 
                    ControlToValidate="txtInventoryName" Display="Dynamic" 
                    ErrorMessage="Name is a required field" ForeColor="Red" 
                    ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 81px; height: 30px">
            <asp:Label ID="lblTotalQuantity" runat="server" Text="Total Quantity"></asp:Label>
        </td>
        <td style="width: 128px; height: 30px">
            <asp:TextBox ID="txtTotalQuantity" runat="server" MaxLength="16"></asp:TextBox>
                <asp:CompareValidator ID="cvTotalQuantity" runat="server" 
                ControlToValidate="txtTotalQuantity" Display="Dynamic" 
                ErrorMessage="Total Quantity must be a number" ForeColor="Red" 
                Operator="DataTypeCheck" Type="Integer" ValidationGroup="vgAddExpense">*</asp:CompareValidator> 
                <asp:RequiredFieldValidator ID="rfTotalQuantity" runat="server" 
                ControlToValidate="txtTotalQuantity" Display="Dynamic" 
                ErrorMessage="Quantity is required" ForeColor="Red" 
                ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    </table>
&nbsp;&nbsp;&nbsp;&nbsp;<br />
    &nbsp;&nbsp;<asp:Button ID="btnAddExpense" runat="server" Text="Add Short Term Asset" 
    Width="150px" onclick="btnAddExpense_Click" style="margin-bottom: 0px" 
        ValidationGroup="vgAddExpense" Height="26px" />
&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        ValidationGroup="vgAddExpense" />
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
        TableName="ShortTermAssets">
    </asp:LinqDataSource>
    <br />
    <asp:GridView ID="gvInventory" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ShortTermAssetID" 
        DataSourceID="LinqDataSource2">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ShortTermItemName" HeaderText="Name" 
                SortExpression="ShortTermItemName" />
            <asp:BoundField DataField="ShortTermTotalQuantity" 
                HeaderText="Total Quantity" SortExpression="ShortTermTotalQuantity" />
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
