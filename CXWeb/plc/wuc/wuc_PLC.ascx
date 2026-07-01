<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuc_PLC.ascx.cs" Inherits="CXWeb.plc.wuc.wuc_PLC" %>

<div class="container-fluid">
    <div class="row">
        <table class="table table-bordered table-striped table-responsive">
            <tr>
                <th><%= DateTime.Now.ToString("yyyy-MM-dd") %></th>
                <th colspan="32">Tiêu đề</th>
            </tr>
            <tr>
                <td></td>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <td class="hheader"><%# Container.DataItem.ToString() %></td>
                    </ItemTemplate>
                    <FooterTemplate>
                        <td>PN</td>
                        <td>Pro</td>
                        <td>Time</td>
                        <td>Status</td>
                        <td>通知1</td>
                        <td>通知1</td>
                    </FooterTemplate>
                </asp:Repeater>
            </tr>
        </table>
    </div>
</div>