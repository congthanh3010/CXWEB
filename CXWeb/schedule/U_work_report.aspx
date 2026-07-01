<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="U_work_report.aspx.cs" Inherits="CXWeb.schedule.U_work_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/dashboard.css" rel="stylesheet" />
    <link href="../css/foundation-datepicker.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
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
            max-height: 800px;
            overflow: auto;
            position: relative;
            z-index: 2;
        }

        @media (max-height: 800px) {
            .grvContainer {
                max-height: 650px;
            }
        }

        .grvData {
            position: relative;
            background-color: #CACBE1;
            width: 1380px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container" id="frmContent">
        <div class="form-inline">
            <label for="txtDFrom">報表時間 (Từ ngày): </label>
            <asp:TextBox ClientIDMode="Static" ID="txtDFrom" runat="server" Width="80px"></asp:TextBox>
            <label for="txtDTo">đến ngày: </label>
            <asp:TextBox ClientIDMode="Static" ID="txtDTo" runat="server" Width="80px"></asp:TextBox>

            <label for="ddlMachine">機台 (Mã lò): </label>
            <asp:DropDownList ClientIDMode="Static" ID="ddlMachine" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
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

            <label for="ddlShift">班別 (Ca): </label>
            <asp:DropDownList ClientIDMode="Static" ID="ddlShift" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="CA1" Text="CA1"></asp:ListItem>
                <asp:ListItem Value="CA2" Text="CA2"></asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
            <asp:Button ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />

            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" Text=""></asp:Label>
        </div>
        <div style="width: 100%;">
            <div style="background: #006699; text-align: center;">
                <asp:Label ID="lblTiltle" runat="server" Font-Bold="true" ForeColor="White" Font-Size="13" Text="退火线上料记录表<br/>GHI CHÉP THEO DÕI SẢN XUẤT CỦA LÒ Ủ"></asp:Label>
            </div>
            <div class="grvContainer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvData" CssClass="grvData" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                            RowStyle-Font-Names="Calibri" OnDataBound="grvData_DataBound">
                            <HeaderStyle BackColor="#006699" ForeColor="White" Font-Bold="true" Font-Names="Cambria" Wrap="true" />
                            <Columns>
                                <asp:BoundField DataField="WorkDate" HeaderText="日期<br/>Ngày" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="WorkShift" HeaderText="班別<br/>Ca" HtmlEncode="false" HeaderStyle-Width="35px" ItemStyle-Width="35px" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="MachineId" HeaderText="機台<br/>Mã lò" HtmlEncode="false" HeaderStyle-Width="75px" ItemStyle-Width="75px" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="Employee" HeaderText="工號<br/>Thao tác viên" HtmlEncode="false" HeaderStyle-Width="75px" ItemStyle-Width="75px" />
                                <asp:BoundField DataField="RecordNo" HeaderText="工單<br/>Mã số công đơn" HtmlEncode="false" HeaderStyle-Width="140px" ItemStyle-Width="140px" />
                                <asp:BoundField DataField="RuncardNo" HeaderText="Runcard<br/>Mã số Runcard" HtmlEncode="false" HeaderStyle-Width="160px" ItemStyle-Width="160px" />
                                <asp:BoundField DataField="ItemNo" HeaderText="機種代碼<br/>Mã số chủng loại" HtmlEncode="false" HeaderStyle-Width="140px" ItemStyle-Width="140px" />
                                <asp:BoundField DataField="StepNo" HeaderText="階段<br/>Công đoạn" HtmlEncode="false" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="ProWeight" HeaderText="克/單<br/>gram/kg" HtmlEncode="false" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="RealWeight" HeaderText="進爐重量<br/>Trọng lượng vô lò" HtmlEncode="false" HeaderStyle-Width="95px" ItemStyle-Width="95px" />
                                <asp:BoundField DataField="Qty" HeaderText="PCS<br/>Số lượng thực tế" HtmlEncode="false" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="ImportTime" HeaderText="進爐時間<br/>Thời gian vô lò" HtmlEncode="false" DataFormatString="{0:HH:mm}" HeaderStyle-Width="75px" ItemStyle-Width="75px" />
                                <asp:BoundField DataField="ExportTime" HeaderText="出爐時間<br/>Thời gian ra lò" HtmlEncode="false" DataFormatString="{0:HH:mm}" HeaderStyle-Width="75px" ItemStyle-Width="75px" />
                                <asp:BoundField DataField="Tempe1" HeaderText="溫度1<br/>Giai đoạn 1" HtmlEncode="false" DataFormatString="{0:n0}" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Tempe2" HeaderText="溫度2<br/>Giai đoạn 2" HtmlEncode="false" DataFormatString="{0:n0}" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Tempe3" HeaderText="溫度3<br/>Giai đoạn 3" HtmlEncode="false" DataFormatString="{0:n0}" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#txtDFrom').fdatepicker({ format: 'yyyy-mm-dd' });
        $('#txtDTo').fdatepicker({ format: 'yyyy-mm-dd' });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#txtDFrom').fdatepicker({ format: 'yyyy-mm-dd' });
            $('#txtDTo').fdatepicker({ format: 'yyyy-mm-dd' });
        });
    </script>
</asp:Content>
