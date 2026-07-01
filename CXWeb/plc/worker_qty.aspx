<%@ Page Title="Thống kê số lượng nhân viên của các đơn vị" Language="C#" MasterPageFile="~/Site_plc.Master" AutoEventWireup="true" CodeBehind="worker_qty.aspx.cs" Inherits="CXWeb.plc.worker_qty" EnableEventValidation="false" %>

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
            padding-right: 5px;
            width: 50px;
        }

        .work-log-input-col-2 {
            text-align: left;
            padding: 2px 2px 2px 0px;
            width: 300px;
        }

        .work-log-input-col-2 input {
            margin: 0;
        }

        /* Chrome, Safari, Edge, Opera */
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        input[type=text] {
            height: 28px;
        }

        select {
            height: 28px;
            margin: 0;
        }

        .grvContainer {
            width: 100%;
            height: 550px;
            overflow: auto;
            position: relative;
            z-index: 2;
        }

        @media (max-height: 800px) {
            .grvContainer {
                height: 450px;
            }
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

    <asp:scriptmanager runat="server"></asp:scriptmanager>

    <div class="container">
        <table class="table table-bordered table-striped" style="width: 100%; padding: 0; border: 0; font-size: large;">
            <tr>
                <td style="padding: 0;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>
                                        <td class="work-log-input-col-1">Người tải lên:</td>
                                        <td style="width: 20px;">
                                            <asp:DropDownList ID="ddlCode" runat="server" Width="100%">
                                                <asp:ListItem Value="VN" Text="VN"></asp:ListItem>
                                                <asp:ListItem Value="TV" Text="TV"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtUser" runat="server" BackColor="#00ff99" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">File tải lên:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" CssClass="form-group" style="float: left;" Width="280px" />
                                            <asp:Button ID="btnLoad" runat="server" Text="Tải lên" OnClick="btnLoad_Click" style="float: left;" Width="80px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Chọn sheet dữ liệu:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:DropDownList ID="ddlSheet" runat="server" Width="360px" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center; padding: 5px;">
                                            <div class="btn-group">
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Font-Bold="true" CssClass="btn btn-success" OnClick="btnUpload_Click" />
                                                <asp:Button ID="btnLog" runat="server" Text="Lịch sử" Font-Bold="true" CssClass="btn btn-default"
                                                    OnClientClick="window.open('worker_qty_log.aspx','_newtab'); return false" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center;">
                                            <asp:Label ClientIDMode="Static" ID="lblMessage" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%;">
                                <div id="frmCaption" style="background: #006699;">
                                    <asp:Label ID="lblBody" runat="server" Font-Bold="true" ForeColor="White" Text="DỮ LIỆU ĐÃ TẢI LÊN"></asp:Label>
                                </div>
                                <div id="frmList" class="grvContainer">
                                    <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog" AutoGenerateColumns="false" BackColor="#CACBE1" 
                                        ShowFooter="false" Width="100%" OnRowDataBound="grvWLog_RowDataBound" ShowHeader="false" >
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />
                                        <Columns>
                                            <asp:BoundField DataField="NumOrd" />
                                            <asp:BoundField DataField="CNName" />
                                            <asp:BoundField DataField="VNName" />
                                            <asp:BoundField DataField="WorkQty" />
                                            <asp:BoundField DataField="WorkTime" DataFormatString="{0:#,##0.#}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                            <asp:PostBackTrigger ControlID="btnLoad" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
