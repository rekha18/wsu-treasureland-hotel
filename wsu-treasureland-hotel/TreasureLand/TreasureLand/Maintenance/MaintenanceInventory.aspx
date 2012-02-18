<%@ Page Title="" Language="C#" MasterPageFile="~/Maintenance/MaintenanceMasterPage.master" AutoEventWireup="true" CodeBehind="MaintenanceInventory.aspx.cs" Inherits="TreasureLand.Maintenance.MaintenanceInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 247px; height: 136px;">
        <tr>
            <td style="width: 86px; height: 30px">
                <asp:Label ID="lblLongTermName" runat="server" Text="Name:"></asp:Label>
            </td>
            <td style="width: 138px; height: 30px">
                <asp:TextBox ID="txtLongTermName" runat="server" MaxLength="16"></asp:TextBox>
                <asp:CompareValidator ID="cvName" runat="server" 
                    ControlToValidate="txtLongTermName" Display="Dynamic" 
                    ErrorMessage="Name must be text" ForeColor="Red" Operator="DataTypeCheck" 
                    ValidationGroup="vgAddExpense">*</asp:CompareValidator> 
                <asp:RequiredFieldValidator ID="rfName" runat="server" 
                    ControlToValidate="txtLongTermLocation" Display="Dynamic" 
                    ErrorMessage="Name is a required field" ForeColor="Red" 
                    ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 86px; height: 30px">
                <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
            </td>
            <td style="width: 138px; height: 30px">
                <asp:TextBox ID="txtLongTermLocation" runat="server" MaxLength="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfLocation" runat="server" 
                    ControlToValidate="txtLongTermLocation" Display="Dynamic" 
                    ErrorMessage="Location is a required field" ForeColor="Red" 
                    ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvLocation" runat="server" 
                    ControlToValidate="txtLongTermLocation" Display="Dynamic" 
                    ErrorMessage="Location must be text" ForeColor="Red" Operator="DataTypeCheck" 
                    ValidationGroup="vgAddExpense">*</asp:CompareValidator> 
            </td>
        </tr>
        <tr>
            <td style="width: 86px; height: 30px">
                <asp:Label ID="lblLongTermCost" runat="server" Text="Cost:"></asp:Label>
            </td>
            <td style="width: 138px; height: 30px">
                <asp:TextBox ID="txtCost" runat="server" MaxLength="10" Width="128px"></asp:TextBox>
                <asp:CompareValidator ID="cvCost" runat="server" ControlToValidate="txtCost" Display="Dynamic" ErrorMessage="Amount must be a number" ForeColor="Red" Operator="DataTypeCheck" Type="Currency" ValidationGroup="vgAddExpense">*</asp:CompareValidator> 
                <asp:RequiredFieldValidator ID="rfCost" runat="server" ControlToValidate="txtCost" Display="Dynamic" ErrorMessage="Amount is a required field" ForeColor="Red" ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 86px; height: 30px">
                <asp:Label ID="lblDate0" runat="server" Text="In Use:"></asp:Label>
            </td>
            <td style="width: 138px; height: 30px">
                <asp:CheckBox ID="cbLongTermInUse" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 86px; height: 30px">
                <asp:Label ID="lblDate" runat="server" Text="Purchase Date:"></asp:Label>
            </td>
            <td style="width: 138px; height: 30px">
                <asp:TextBox ID="txtPurchaseDate" runat="server" MaxLength="16" Width="128px"></asp:TextBox>
                <asp:CompareValidator ID="cvDate" runat="server" ControlToValidate="txtPurchaseDate" 
                Display="Dynamic" ErrorMessage="Date must be a date" ForeColor="Red" 
                Operator="DataTypeCheck" Type="Date" ValidationGroup="vgAddExpense">*</asp:CompareValidator>
                <asp:RequiredFieldValidator ID="rfDate" runat="server" 
                ControlToValidate="txtPurchaseDate" Display="Dynamic" 
                ErrorMessage="Date is a required field" ForeColor="Red" 
                ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
&nbsp;&nbsp;&nbsp;&nbsp;<br />&nbsp;&nbsp;<asp:Button ID="btnAddLongTermAsset" 
        runat="server" Text="Add Long Term Asset" 
    Width="175px" onclick="btnAddLongTermAsset_Click" style="margin-bottom: 0px" 
        ValidationGroup="vgAddExpense" />
&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        ValidationGroup="vgAddExpense" />
    <asp:LinqDataSource ID="ldsLongTermAsset" runat="server" 
        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
        TableName="LongTermAssets">
    </asp:LinqDataSource>
    <asp:GridView ID="gvLongTermAsset" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="LongTermAssetID" 
        DataSourceID="ldsLongTermAsset">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="LongTermAssetName" HeaderText="Name" 
                SortExpression="LongTermAssetName" />
            <asp:BoundField DataField="LongTermAssetLocation" HeaderText="Location" 
                SortExpression="LongTermAssetLocation" />
            <asp:BoundField DataField="LongTermAssetCost" HeaderText="Cost" 
                SortExpression="LongTermAssetCost" />
            <asp:CheckBoxField DataField="LongTermAssetInUse" HeaderText="In Use" 
                SortExpression="LongTermAssetInUse" />
            <asp:BoundField DataField="LongTermAssetPurchaseDate" 
                HeaderText="Purchase Date" SortExpression="LongTermAssetPurchaseDate" />
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
