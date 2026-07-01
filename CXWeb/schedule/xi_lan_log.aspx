<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xi_lan_log.aspx.cs" Inherits="CXWeb.schedule.xi_lan_log" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Ghi chép sản xuất Xi lăn - 滾電電腦資料</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="../css/foundation-datepicker.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.datetimepicker.full.js"></script>
    <script src="../js/foundation-datepicker.min.js"></script>

    <style type="text/css">
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
            table-layout: fixed;
        }

        .grvHeader {
            background-color: #006699;
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: Cambria;
        }

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
        .form-inline select {
            margin: 10px 10px 10px 0;
            height: 26px;
        }

        .form-inline input[type=file] {
            margin: 10px 10px 10px 0;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div style="width: 100%; padding: 0; border: 0; background: #006699; text-align: center;">
                <asp:Label ID="Label1" runat="server" Text="滾電電腦資料<br/>GHI CHÉP THEO DÕI SẢN XUẤT XI LĂN" Font-Bold="true" ForeColor="White" Font-Size="Large"></asp:Label>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-inline">
                        <label for="ddlMachine" style="width: 90px;">Chọn chuyền : </label>
                        <asp:DropDownList ID="ddlMachine" ClientIDMode="Static" runat="server" Width="80px">
                            <asp:ListItem Value="XL-001E" Text="Chuyền E"></asp:ListItem>
                            <asp:ListItem Value="XL-001F" Text="Chuyền F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-inline">
                        <label for="fileUpload" style="width: 90px;">Chọn file : </label>
                        <asp:FileUpload ID="fileUpload" runat="server" ClientIDMode="Static" />
                        <asp:Button ID="btnUpload" runat="server" Text="Tải lên" CssClass="btn btn-success btn-block"
                            Font-Bold="true" Width="100px" OnClick="btnUpload_Click" />
                    </div>
                    <div class="form-inline">
                        <asp:Label ID="lblMessage" runat="server" ClientIDMode="Static" Font-Size="14px" Font-Bold="true"></asp:Label>
                    </div>
                    <div style="width: 100%; padding: 0; border: 0; background: #006699; text-align: center;">
                        <label style="font-size: large; font-weight: bold; color: white;">DỮ LIỆU ĐÃ TẢI LÊN TRONG NGÀY</label>
                    </div>
                    <div style="width: 100%;">
                        <div class="grvContainer">
                            <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog" AutoGenerateColumns="false" BackColor="#CACBE1"
                                ShowFooter="false" ShowHeader="false" Width="4100px"
                                OnDataBinding="grvWLog_DataBinding" OnRowDataBound="grvWLog_RowDataBound" OnRowCreated="grvWLog_RowCreated">
                                <RowStyle Font-Names="Calibri"></RowStyle>
                                <SelectedRowStyle BackColor="#A1DCF2" />
                                <Columns></Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
