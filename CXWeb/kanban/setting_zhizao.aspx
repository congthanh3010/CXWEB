<%@ Page Title="" Language="C#" MasterPageFile="~/Site_kanban.Master" AutoEventWireup="true" CodeBehind="setting_zhizao.aspx.cs" Inherits="CXWeb.kanban.setting_zhizao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="table-bordered table-striped" style="width: 400px; padding: 0px; border: 0px;">
        <tr>
            <th colspan="2" style="font-size:20px; background-color:#006699; color:white; ">製造前段時間區間設定<br />Cài đặt thời gian tiền chế tạo</th>
        </tr>
        <tr>
            <td> <asp:Label ID="label1" Text="第一段區間(天) Kỳ đầu tiên(ngày)：" Font-Bold="true" Font-Size="18px" runat="server" /></td>
            <td style="width:100px"><asp:TextBox ID="TextBox1"  Font-Bold="true" Font-Size="18px" Width="100%" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox></td>
        </tr>
        <tr>
            <td> <asp:Label ID="label2" Text="第二段區間(天) Kỳ thứ 2(ngày)：" Font-Bold="true" Font-Size="18px" runat="server" /></td>
            <td style="width:100px"><asp:TextBox ID="TextBox2"  Font-Bold="true" Font-Size="18px" Width="100%" ClientIDMode="Static"  runat="server" CssClass="some_class"></asp:TextBox></td>
        </tr>
        <tr>
            <td> <asp:Label ID="label3" Text="第三段區間(天) Kỳ thứ 3(ngày)：" Font-Bold="true" Font-Size="18px" runat="server" /></td>
            <td style="width:100px"><asp:TextBox ID="TextBox3"  Font-Bold="true" Font-Size="18px" Width="100%" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox></td>
        </tr>
        <tr>
            <td> <asp:Label ID="label4" Text="第四段區間(天) Kỳ thứ 4(ngày)：" Font-Bold="true" Font-Size="18px" runat="server" /></td>
            <td style="width:100px"><asp:TextBox ID="TextBox4"  Font-Bold="true" Font-Size="18px" Width="100%" ClientIDMode="Static"  runat="server" CssClass="some_class"></asp:TextBox></td>
        </tr>
        <tr>
            <td> <asp:Label ID="label5" Text="第五段區間(天) Kỳ thứ 5(ngày)：" Font-Bold="true" Font-Size="18px" runat="server" /></td>
            <td style="width:100px"><asp:TextBox ID="TextBox5"  Font-Bold="true" Font-Size="18px" Width="100%" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox></td>
        </tr>
          <tr>
            <td colspan="2"> <asp:Button ID="btnSave" runat="server" Style="font-size: large;" Text="Save" OnClick="btnSave_Click" Font-Bold="true"></asp:Button></td>            
        </tr>
    </table>
</asp:Content>
