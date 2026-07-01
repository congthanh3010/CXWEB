<%@ Page Title="Danh mục " Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="QA_List.aspx.cs" Inherits="CXWeb.schedule.QA_List" EnableEventValidation="false" %>
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
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="DANH MỤC KHAI BÁO MÃ HÓA CHẤT QA"></asp:Label>
                            </div>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>
                                        <td class="work-log-input-col-1">Code ID</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtCode_Id" runat="server" Style="text-transform: uppercase" Width="130px" BackColor="#00ff99"></asp:TextBox>
                                        </td>
                                        <td></td>

                                        <td class="work-log-input-col-1">Machine ID</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="ddlMachine" runat="server" Width="130px">                                                
                                                <asp:ListItem Value="CR-000C" Text="CR-000C"></asp:ListItem>
                                                <asp:ListItem Value="CR-000D" Text="CR-000D"></asp:ListItem>
                                                <asp:ListItem Value="CR-000E" Text="CR-000E"></asp:ListItem>
                                                <asp:ListItem Value="CR-000F" Text="CR-000F"></asp:ListItem>
                                                <asp:ListItem Value="DC-000A" Text="DC-000A"></asp:ListItem>
                                                <asp:ListItem Value="XD-000A" Text="XD-000A"></asp:ListItem>                                                
                                                <asp:ListItem Value="XL-001E" Text="XL-001E"></asp:ListItem>
                                                <asp:ListItem Value="XL-001F" Text="XL-001F"></asp:ListItem>
                                                <asp:ListItem Value="XT-000C" Text="XT-000C"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>

                                         <td class="work-log-input-col-1">DVT</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtDon_Vi" runat="server"  Width="130px" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td class="work-log-input-col-1" style="vertical-align: top;">Code Name</td>
                                        <td colspan="8" class="work-log-input-col-2">
                                            <asp:TextBox Width="100%" ID="txtCode_Name" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="work-log-input-col-1" style="vertical-align: top;">Tiêu chuẩn</td>
                                        <td colspan="8" class="work-log-input-col-2">
                                            <asp:TextBox Width="100%" ID="txtTieu_Chuan" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>

                                     <tr>
                                        <td class="work-log-input-col-1">Value Min</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtValue_Min" runat="server" Width="130px" ></asp:TextBox>
                                        </td>
                                        <td></td>

                                        <td class="work-log-input-col-1">Value Max</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtValue_Max" runat="server" Width="130px" ></asp:TextBox>
                                        </td>
                                        <td></td>

                                         <td class="work-log-input-col-1">Số cột</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtSo_Cot" runat="server"  Width="130px" ></asp:TextBox>
                                        </td>
                                        <td></td>
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
                                                    CssClass="btn btn-danger" Font-Bold="true" ForeColor="Black" Width="60px" OnClick="btnCancel_Click"/>
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
                                        OnRowDataBound="grvWLog_RowDataBound" OnSelectedIndexChanged="grvWLog_SelectedIndexChanged" DataKeyNames="Code_Id">
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />
                                        <Columns>
                                            <asp:BoundField DataField="Machine_Id" HeaderText="Mã Máy" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="Code_Id" HeaderText="Mã Hóa chất" HeaderStyle-Width="40px" ItemStyle-Width="40px" />
                                            <asp:BoundField DataField="Code_Name" HeaderText="Tên Hóa Chất" HeaderStyle-Width="170px" ItemStyle-Width="170px" />
                                            <asp:BoundField DataField="Don_Vi" HeaderText="DVT" HeaderStyle-Width="40px" ItemStyle-Width="40px" />
                                            <asp:BoundField DataField="Tieu_Chuan" HeaderText="Tiêu Chuẩn" HeaderStyle-Width="190px" ItemStyle-Width="190px" />
                                            <asp:BoundField DataField="Value_Min" HeaderText="Giá trị Min" HeaderStyle-Width="110px" ItemStyle-Width="110px" DataFormatString="{0:###,###.##}"/>
                                            <asp:BoundField DataField="Value_Max" HeaderText="Giá trị Max" HeaderStyle-Width="110px" ItemStyle-Width="110px" DataFormatString="{0:###,###.##}"/>
                                            <asp:BoundField DataField="So_Cot" HeaderText="Số mốc giờ" HeaderStyle-Width="90px" ItemStyle-Width="90px" DataFormatString="{0:###,###.##}"/>
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

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#txtDate').datetimepicker({ format: 'Y-m-d' });
            $('#txtImpTime').on('focusout', calcTime);
            $('#txtExpTime').on('focusout', calcTime);
        });

        $(window).on('beforeunload', function (e) {
            var state = $('#state').val();
            if (state == "insert") {
                $('#btnCancel').click();
            }
        });
    </script>
</asp:Content>