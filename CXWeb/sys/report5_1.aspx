<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report5_1.aspx.cs" Inherits="CXWeb.sys.report5_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <style type="text/css">
        body {
            background-color:#d3e6e8;
        }

        .gvDataCss {
            text-align:center;
        }

        .tbTitle {
            background-color:#88cec7;
            text-align:right;
            color:white;
            /*width:10%;*/
        }
        .tbContent {
            background-color:white;
            /*width:15%;*/
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      
        <div>
            <table style="width: 800px; margin: 0 auto">
                <tr>
                    <td colspan="6">
                        <asp:GridView ID="gvData" runat="server" CellPadding="4" ForeColor="#333333"
                            DataKeyNames="code" GridLines="None" Width="800px" AutoGenerateColumns="False"
                            CssClass="gvDataCss" AllowSorting="True" OnRowDeleting="gvData_RowDeleting">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="code" HeaderText="材料編號" />
                                <asp:BoundField DataField="description" HeaderText="分類" />
                                <asp:BoundField DataField="description2" HeaderText="原物料波動說明原因" />

                                <asp:CommandField HeaderText="操作" ShowDeleteButton="true" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </td>
                </tr>
                <tr>

                    <td class="tbTitle">材料編號：</td>
                    <td class="tbContent">
                        <asp:TextBox ID="mat_no" runat="server"></asp:TextBox></td>
                    <td class="tbTitle">備註：</td>
                    <td class="tbContent">
                        <asp:TextBox ID="remark" runat="server"></asp:TextBox>
                    </td>
                     <td class="tbTitle">備註2：</td>
                    <td class="tbContent">
                        <asp:TextBox ID="remark2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center;">
                        <asp:Button ID="btnSave" Text="新增" runat="server" OnClick="btnSave_Click"  />
                    </td>

                </tr>
            </table>
            <%--<input type="hidden" id="hiddenID" runat="server" />--%>
        </div>
    </form>
</body>
</html>
