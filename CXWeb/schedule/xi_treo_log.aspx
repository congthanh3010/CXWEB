<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xi_treo_log.aspx.cs" Inherits="CXWeb.schedule.xi_treo_log" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Ghi chép sản xuất Xi treo - 吊電電腦資料</title>

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
        input[type=file] {
	        display: block !important;
	        right: 1px;
	        top: 1px;
	        height: 34px;
	        opacity: 0;
            width: 100%;
	        background: none;
	        position: absolute;
            overflow: hidden;
            z-index: 2;
        }

        .control-fileupload {
	        display: block;
	        border: 1px solid #d6d7d6;
	        background: #FFF;
	        border-radius: 4px;
	        width: 100%;
	        height: 36px;
	        line-height: 36px;
	        padding: 0px 10px 2px 10px;
            overflow: hidden;
            position: relative;
  
            &:before, input, label {
                cursor: pointer !important;
            }

            /* File upload button */
            &:before {
                /* inherit from boostrap btn styles */
                padding: 4px 12px;
                margin-bottom: 0;
                font-size: 14px;
                line-height: 20px;
                color: #333333;
                text-align: center;
                text-shadow: 0 1px 1px rgba(255, 255, 255, 0.75);
                vertical-align: middle;
                cursor: pointer;
                background-color: #f5f5f5;
                background-image: linear-gradient(to bottom, #ffffff, #e6e6e6);
                background-repeat: repeat-x;
                border: 1px solid #cccccc;
                border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.25);
                border-bottom-color: #b3b3b3;
                border-radius: 4px;
                box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);
                transition: color 0.2s ease;

                /* add more custom styles*/
                content: 'Browse';
                display: block;
                position: absolute;
                z-index: 1;
                top: 2px;
                right: 2px;
                line-height: 20px;
                text-align: center;
            }

            &:hover, &:focus {
                &:before {
                    color: #333333;
                    background-color: #e6e6e6;
                    color: #333333;
                    text-decoration: none;
                    background-position: 0 -15px;
                    transition: background-position 0.2s ease-out;
                }
            }
  
            label {
                line-height: 24px;
                color: #999999;
                font-size: 14px;
                font-weight: normal;
                overflow: hidden;
                white-space: nowrap;
                text-overflow: ellipsis;
                position: relative;
                z-index: 1;
                margin-right: 90px;
                margin-bottom: 0px;
                cursor: text;
            }
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
                <div class="container-fluid">
            <div style="width: 100%; padding: 0; border: 0; background: #006699; text-align: center;">
                <asp:Label ID="Label1" runat="server" Text="吊電電腦資料<br/>GHI CHÉP THEO DÕI SẢN XUẤT XI TREO" Font-Bold="true" ForeColor="White" Font-Size="Large"></asp:Label>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="padding: 5px; background: #CACBE1;">
                        <div class="row">
                            <div class="col-sm-4 col-md-4">
                                <span class="control-fileupload">
                                    <label for="fileUpload" class="text-left">Nhấn vào đây để chọn file</label>
                                    <asp:FileUpload ID="fileUpload" runat="server" ClientIDMode="Static" />
                                </span>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <asp:Button ID="btnUpload" runat="server" Text="Tải lên" CssClass="btn btn-success btn-block" Font-Bold="true" OnClick="btnUpload_Click" />
                            </div>
                            <div class="col-sm-12" style="margin-top: 5px;">
                                <asp:Label ID="lblMessage" runat="server" ClientIDMode="Static" Font-Size="14px" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%; padding: 0; border: 0; background: #006699; text-align: center;">
                        <asp:Label ID="Label2" runat="server" Text="DỮ LIỆU ĐÃ TẢI LÊN TRONG NGÀY" Font-Bold="true" ForeColor="White" Font-Size="Large"></asp:Label>
                    </div>
                    <div style="width:100%;">
                        <div class="grvContainer">
                            <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog" AutoGenerateColumns="false" BackColor="#CACBE1"
                                ShowFooter="false" ShowHeader="false" Width="4500px"
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

    <script type="text/javascript">
        $(function () {
            $('input[type=file]').change(function () {
                var t = $(this).val();
                var labelText = 'File : ' + t.substr(12, t.length);
                $(this).prev('label').text(labelText);
            })
        });
    </script>
</body>
</html>
