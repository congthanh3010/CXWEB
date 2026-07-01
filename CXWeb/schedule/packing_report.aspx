<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="packing_report.aspx.cs" Inherits="CXWeb.schedule.packing_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Packing Report</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid" id="frmContent">
            <div class="form-inline">
                <label for="txtDFrom">報表時間 (Từ ngày): </label>
                <asp:TextBox ClientIDMode="Static" ID="txtDFrom" runat="server" Width="80px"></asp:TextBox>
                <label for="txtDTo">đến ngày: </label>
                <asp:TextBox ClientIDMode="Static" ID="txtDTo" runat="server" Width="80px"></asp:TextBox>

                <asp:Button ID="btnView" runat="server" Text="Xem" OnClick="btnView_Click" />

                <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" Text=""></asp:Label>
            </div>
            <div style="width: 100%;">
                <div style="background: #006699; text-align: center;">
                    <asp:Label ID="lblTiltle" runat="server" Font-Bold="true" ForeColor="White" Font-Size="13" Text="電腦資料<br/>GHI CHÉP THEO DÕI SẢN XUẤT"></asp:Label>
                </div>
                <div class="grvContainer">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grvData" CssClass="grvData" runat="server" AutoGenerateColumns="false" 
                                BackColor="#CACBE1" ShowHeader="false" ShowFooter="false" 
                                OnDataBinding="grvData_DataBinding" OnRowDataBound="grvData_RowDataBound" >
                                <RowStyle Font-Names="Calibri" VerticalAlign="Middle" HorizontalAlign="Center"></RowStyle>
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
    </form>
</body>
</html>
