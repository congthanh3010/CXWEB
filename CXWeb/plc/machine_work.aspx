<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="machine_work.aspx.cs" Inherits="CXWeb.plc.machine_work" %>

<asp:repeater ID="rptDetail" runat="server">
    <ItemTemplate>
        <div class="modal" id="mcinfo" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <div style="display: inline-block; width: 100%;">
                            <div style='float:left; border-radius: 50%; width: 35px; height: 35px; background-color: <%= mcolor[index] %>;'></div>
                            <h3 class="modal-title" style="float: left; margin-left: 10px;"><%# Eval("MachineId") %></h3>
                            <button class="close" style="float: right;" data-dismiss="modal" aria-label="Close">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>作業員 Nhân viên thao tác</label>
                            <div class="form-control data"><%# Eval("dCreateUser") %></div>
                        </div>
                        <div class="form-group">
                            <label>狀態 Trạng thái máy</label>
                            <div class="form-control data"><%# Eval("dStatus") + " - " + Eval("cDesc") %></div>
                        </div>
                        <div class="form-group">
                            <label>累計次數/機台每班標準產能 Số lần dập/Công suất tiểu chuẩn của máy trong 10 tiếng</label>
                            <div class="form-control data"><%# Eval("iCount") + " / " + Eval("pro10h") %></div>
                        </div>
                        <div class="form-group">
                            <label>階段/類型 Công đoạn / Chủng loại</label>
                            <div class="form-control data"><%# Eval("ProcessName") + " / " + Eval("item") %></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:repeater>