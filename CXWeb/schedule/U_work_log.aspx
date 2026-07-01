<%@ Page Title="Theo dõi sản xuất lò ủ" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="U_work_log.aspx.cs" Inherits="CXWeb.schedule.U_work_log" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/dashboard.css" rel="stylesheet" />
    <link href="../css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="../css/foundation-datepicker.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.datetimepicker.full.js"></script>
    <script src="../js/foundation-datepicker.min.js"></script>
    
    <style type="text/css">
        .work-log-input-col-1 {
            text-align: right;
            padding: 2px;
            padding-right: 5px;
        }

        .work-log-input-col-2 {
            text-align: left;
            padding: 2px;
        }

        .work-log-input-col-2 input {
            width: 100%;
        }

        /* Chrome, Safari, Edge, Opera */
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        input[type=number] {
            -moz-appearance: textfield;
            height: 28px;
        }

        input[type=text] {
            height: 28px;
        }

        select {
            height: 28px;
        }

        .grvContainer {
            width: 100%;
            height: 350px;
            overflow: auto;
            position: relative;
            z-index: 2;
        }

        .grvWLog {
            position: absolute;
        }

        .grvHeader {
            background-color: #006699;
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: Cambria;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">
        <table class="table table-bordered table-striped" style="width: 100%; padding: 0; border: 0; font-size: large;">
            <tr>
                <td style="padding: 0;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="frmTitle" style="background: #006699; text-align: center;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="GHI CHÉP THEO DÕI SẢN XUẤT CỦA LÒ Ủ"></asp:Label>
                            </div>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>
                                        <td class="work-log-input-col-1">Ngày</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" Width="130px" BackColor="#00ff99"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Ca</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="ddlShift" runat="server" Width="70px">
                                                <asp:ListItem Value="CA1" Text="CA1"></asp:ListItem>
                                                <asp:ListItem Value="CA2" Text="CA2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Thao tác viên</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCode" runat="server" Width="100%" Style="margin-left: 2px;">
                                                <asp:ListItem Value="VN" Text="VN"></asp:ListItem>
                                                <asp:ListItem Value="TV" Text="TV"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtEmployee" runat="server" Width="100%" BackColor="#00ff99"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Mã máy</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="ddlMachine" runat="server" Width="130px">
                                                <asp:ListItem Value="LU-GASA" Text="LU-GASA"></asp:ListItem>
                                                <asp:ListItem Value="LU-GASB" Text="LU-GASB"></asp:ListItem>
                                                <asp:ListItem Value="LU-GASC" Text="LU-GASC"></asp:ListItem>
                                                <asp:ListItem Value="LU-GASD" Text="LU-GASD"></asp:ListItem>
                                                <asp:ListItem Value="LU-GASE" Text="LU-GASE"></asp:ListItem>
                                                <asp:ListItem Value="LU-GASPIT" Text="LU-GASPIT"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#2" Text="LU-DIEN#2"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#3" Text="LU-DIEN#3"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#4" Text="LU-DIEN#4"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#5" Text="LU-DIEN#5"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#7" Text="LU-DIEN#7"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#8" Text="LU-DIEN#8"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#9" Text="LU-DIEN#9"></asp:ListItem>
                                                <asp:ListItem Value="LU-DIEN#10" Text="LU-DIEN#10"></asp:ListItem>
                                                <asp:ListItem Value="LU-Z270" Text="LU-Z270"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Run Card</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtRunCard" runat="server" AutoPostBack="true" OnTextChanged="txtRunCard_TextChanged" BackColor="#00ff99"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Mã công đơn</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtRecordNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Chủng loại</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Công đoạn</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtStep" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">gram/kg</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtProWeight" ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Trọng lượng vô lò</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtRealWeight" ClientIDMode="Static" runat="server" BackColor="#00ff99"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Số lượng thực tế</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtQty" ClientIDMode="Static" runat="server" BackColor="#00ff99"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Trọng lượng thùng rỗng</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtBarrelWeight" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Thời gian vô lò</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtImpTime" runat="server" ClientIDMode="Static" BackColor="#00ff99" placeholder="giờ:phút"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Thời gian ra lò</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtExpTime" runat="server" ClientIDMode="Static" BackColor="#00ff99" placeholder="giờ:phút"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Tổng thời gian</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtSpan" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="hdfSpan" runat="server" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Giai đoạn 1</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtTempe1" runat="server" ClientIDMode="Static" BackColor="#00ff99"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Giai đoạn 2</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtTempe2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Giai đoạn 3</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtTempe3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1" style="vertical-align: top;">Ghi chú</td>
                                        <td colspan="8" class="work-log-input-col-2">
                                            <asp:TextBox Width="100%" ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="text-align: center; padding: 5px;">
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnInsert" runat="server" Text="Thêm"
                                                    CssClass="btn btn-default" Font-Bold="true" Width="60px" OnClick="btnInsert_Click" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnModify" runat="server" Text="Sửa"
                                                    CssClass="btn btn-default" Font-Bold="true" Width="60px" OnClick="btnModify_Click" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnDelete" runat="server" Text="Xóa"
                                                    CssClass="btn btn-default" Font-Bold="true" Width="60px" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có muốn xóa ghi chép này?');" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnSave" runat="server" Text="Lưu"
                                                    CssClass="btn btn-success" Font-Bold="true" ForeColor="Black" Width="60px" OnClick="btnSave_Click" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnCancel" runat="server" Text="Hủy"
                                                    CssClass="btn btn-danger" Font-Bold="true" ForeColor="Black" Width="60px" OnClick="btnCancel_Click" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnReport" runat="server" Text="Báo cáo"
                                                    CssClass="btn btn-primary" Font-Bold="true" ForeColor="Black" Width="70px" OnClientClick="window.open('U_work_report.aspx','_newtab'); return false" />
                                            </div>
                                            <asp:HiddenField ID="state" runat="server" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="text-align: center;">
                                            <asp:Label ClientIDMode="Static" ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%;">
                                <div id="frmCaption" style="background: #006699;">
                                    <asp:Label ID="lblBody" runat="server" Font-Bold="true" ForeColor="White" Text="GHI CHÉP ĐÃ NHẬP TRONG NGÀY"></asp:Label>
                                </div>
                                <div id="frmList" class="grvContainer">
                                    <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog"
                                        AutoGenerateColumns="false" BackColor="#CACBE1" ShowFooter="false" Width="1300px"
                                        OnRowDataBound="grvWLog_RowDataBound" OnSelectedIndexChanged="grvWLog_SelectedIndexChanged" DataKeyNames="Id">
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />
                                        <Columns>
                                            <asp:BoundField DataField="WorkDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Ngày" HeaderStyle-Width="110px" ItemStyle-Width="110px" />
                                            <asp:BoundField DataField="MachineId" HeaderText="Mã lò" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="WorkShift" HeaderText="Ca" HeaderStyle-Width="40px" ItemStyle-Width="40px" />
                                            <asp:BoundField DataField="Employee" HeaderText="Thao tác viên" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="RuncardNo" HeaderText="Run Card" HeaderStyle-Width="190px" ItemStyle-Width="190px" />
                                            <asp:BoundField DataField="RecordNo" HeaderText="Mã công đơn" HeaderStyle-Width="170px" ItemStyle-Width="170px" />
                                            <asp:BoundField DataField="ItemNo" HeaderText="Chủng loại" HeaderStyle-Width="170px" ItemStyle-Width="170px" />
                                            <asp:BoundField DataField="StepNo" HeaderText="Công đoạn" HeaderStyle-Width="60px" ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="RealWeight" HeaderText="Trọng lượng vô lò" HeaderStyle-Width="110px" ItemStyle-Width="110px" />
                                            <asp:BoundField DataField="Qty" HeaderText="Số lượng thực tế" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="ImportTime" DataFormatString="{0:HH:mm}" HeaderText="Thời gian vô lò" HeaderStyle-Width="95px" ItemStyle-Width="95px" />
                                            <asp:BoundField DataField="ExportTime" DataFormatString="{0:HH:mm}" HeaderText="Thời gian ra lò" HeaderStyle-Width="95px" ItemStyle-Width="95px" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    
    <script type="text/javascript">
        var calcTime = function () {
            var tin = $('#txtImpTime').val();
            var tout = $('#txtExpTime').val();

            if (tin.trim() != "" && tout.trim() != "") {
                if (tin.length < 5) { tin = '0' + tin; }
                if (tout.length < 5) { tout = '0' + tout; }
                var imp = Date.parse('2021-01-01T' + tin +':00');
                var exp = Date.parse('2021-01-01T' + tout + ':00');

                var span = Math.round((exp - imp) / 1000 / 60);
                if (span < 0)
                    span += 1440;
                $('#txtSpan').val(span);
                $('#hdfSpan').val(span);
                $('#txtTempe1').focus();
            }
            else {
                $('#txtSpan').val(0);
                $('#hdfSpan').val(0);
            }
        }

        var calcQty = function () {
            var pro = $('#txtProWeight').val();
            var real = $('#txtRealWeight').val();

            if (pro.trim() != "" && real.trim() != "") {
                var qty = Math.floor(real / (pro / 1000));
                $('#txtQty').val(qty);
            }
            else {
                $('#txtQty').val(0);
            }
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#txtDate').datetimepicker({ format: 'Y-m-d' });
            $('#txtImpTime').on('focusout', calcTime);
            $('#txtExpTime').on('focusout', calcTime);
            $('#txtRealWeight').on('focusout', calcQty);
        });

        $(window).on('beforeunload', function (e) {
            var state = $('#state').val();
            if (state == "insert") {
                $('#btnCancel').click();
            }
        });
    </script>
</asp:Content>
