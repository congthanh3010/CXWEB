<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tracking.aspx.cs" Inherits="CXWeb.temperature.tracking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Theo dõi nhiệt độ lò Ủ</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/jquery.datetimepicker.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.datetimepicker.full.js"></script>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="form-inline">
                <label for="txtDFrom">報表時間 From: </label>
                <asp:TextBox ClientIDMode="Static" ID="txtDateFrom" runat="server" Width="120px"></asp:TextBox>
                <label for="txtDTo">To: </label>
                <asp:TextBox ClientIDMode="Static" ID="txtDateTo" runat="server" Width="120px"></asp:TextBox>

                <label for="ddlMachine">Machine: </label>
                <asp:DropDownList ID="ddlMachine" runat="server" ClientIDMode="Static">
                    <asp:ListItem Value="LU_GASA" Text="LU-GASA"></asp:ListItem>
                    <asp:ListItem Value="LU_GASB" Text="LU-GASB"></asp:ListItem>
                    <asp:ListItem Value="LU_GASC" Text="LU-GASC"></asp:ListItem>
                    <asp:ListItem Value="LU_GASD" Text="LU-GASD"></asp:ListItem>
                    <asp:ListItem Value="LU_GASE" Text="LU-GASE"></asp:ListItem>
                    <asp:ListItem Value="LU_GASPIT" Text="LU-GASPIT"></asp:ListItem>
                    <asp:ListItem Value="LU_DIEN#7" Text="LU-DIEN#7"></asp:ListItem>
                    <asp:ListItem Value="LU_DIEN#8" Text="LU-DIEN#8"></asp:ListItem>
                    <asp:ListItem Value="LU_DIEN#9" Text="LU-DIEN#9"></asp:ListItem>
                    <asp:ListItem Value="LU_DIEN#10" Text="LU-DIEN#10"></asp:ListItem>
                </asp:DropDownList>

                <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />

                <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" Text=""></asp:Label>
            </div>
            <div style="width: 100%">
                <div style="background: #006699; text-align: center;">
                    <asp:Label ID="lblTiltle" runat="server" Font-Bold="true" ForeColor="White" Font-Size="13" Text="電腦資料"></asp:Label>
                </div>
                <div>
                    <asp:Chart ID="Chart1" runat="server">
                        <Series>
                            <asp:Series Name="Series1" ChartType="Line" ChartArea="ChartArea1"></asp:Series>
                            <asp:Series Name="Series2" ChartType="Line" ChartArea="ChartArea1"></asp:Series>
                            <asp:Series Name="Series3" ChartType="Line" ChartArea="ChartArea2"></asp:Series>
                            <asp:Series Name="Series4" ChartType="Line" ChartArea="ChartArea2"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                            <asp:ChartArea Name="ChartArea2"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            $('#txtDateFrom').datetimepicker({ format: 'y-m-d H:i' });
            $('#txtDateTo').datetimepicker({ format: 'Y-m-d H:i' });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
                $('#txtDateFrom').datetimepicker({ format: 'Y-m-d H:i' });
                $('#txtDateTo').datetimepicker({ format: 'Y-m-d H:i' });
            });
        </script>
    </form>
</body>
</html>
