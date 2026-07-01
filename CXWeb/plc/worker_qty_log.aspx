<%@ Page Title="" Language="C#" MasterPageFile="~/Site_plc.Master" AutoEventWireup="true" CodeBehind="worker_qty_log.aspx.cs" 
    Inherits="CXWeb.plc.worker_qty_log" EnableEventValidation="false" %>
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
        /* Style the form - display items horizontally */
        .form-inline {
            display: flex;
            flex-flow: row wrap;
            align-items: center;
        }

        /* Add some margins for each label */
        .form-inline label {
            margin: 10px 10px 10px 0;
        }

        /* Style the input fields */
        .form-inline input,select {
            margin: 10px 10px 10px 0;
            height: 26px;
        }

        /* Add responsiveness - display the form controls vertically instead of horizontally on screens that are less than 800px wide */
        @media (max-width: 800px) {
            .form-inline input {
                margin: 10px 0;
            }

            .form-inline {
                flex-direction: column;
                align-items: stretch;
            }
        }

        .grvContainer {
            width: 100%;
            height: 700px;
            overflow: auto;
            position: relative;
            z-index: 2;
        }

        @media (max-height: 800px) {
            .grvContainer {
                height: 550px;
            }
        }

        .grvData {
            position: absolute;
            table-layout: fixed;
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
        <div class="form-inline">
            <label for="txtDate">Ngày nhập: </label>
            <asp:TextBox ClientIDMode="Static" ID="txtDate" runat="server" Width="80px"></asp:TextBox>
            <asp:Button ID="btnView" runat="server" Text="Xem" OnClick="btnView_Click" />
            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" Text=""></asp:Label>
        </div>
        <div style="width: 100%;">
            <div style="background: #006699; text-align: center;">
                <asp:Label ID="lblTiltle" runat="server" Font-Bold="true" ForeColor="White" Font-Size="13" Text="DỮ LIỆU ĐÃ NHẬP"></asp:Label>
            </div>
            <div class="grvContainer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#txtDate').fdatepicker({ format: 'yyyy-mm-dd' });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#txtDate').fdatepicker({ format: 'yyyy-mm-dd' });
        });
    </script>
</asp:Content>
