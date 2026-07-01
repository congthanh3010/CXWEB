<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="packing.aspx.cs" Inherits="CXWeb.schedule.packing" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Packing</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../js/jquery.js"></script>

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
                <asp:Label ID="Label1" runat="server" Text="GHI CHÉP ĐÓNG GÓI" Font-Bold="true" ForeColor="White" Font-Size="Large"></asp:Label>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-inline">
                        <asp:FileUpload ID="fileUpload" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="form-inline" style="margin-bottom: 10px">
                        <asp:Button ID="btnImport" runat="server" Text="Tải lên" CssClass="btn btn-success btn-block" 
                            Font-Bold="true" Width="80px" OnClick="btnImport_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Báo cáo" CssClass="btn btn-primary btn-block" 
                            Font-Bold="true" Width="80px" OnClientClick="window.open('packing_report.aspx','_newtab'); return false" 
                            style="margin-top: 0px; margin-left: 5px" />
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
                                ShowFooter="false" ShowHeader="false"
                                OnDataBinding="grvWLog_DataBinding" OnRowDataBound="grvWLog_RowDataBound" >
                                <RowStyle Font-Names="Calibri" HorizontalAlign="Center"></RowStyle>
                                <SelectedRowStyle BackColor="#A1DCF2" />
                                <Columns></Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnImport" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
