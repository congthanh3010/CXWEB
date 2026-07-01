<%@ Page Title="BẢNG KIỂM TRA ĐỘ DẪN ĐIỆN CỦA NƯỚC SAU XỬ LÝ " Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="QA_Water_Chart.aspx.cs" Inherits="CXWeb.schedule.QA_Water_Chart" EnableEventValidation="false"  %>

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
    <script src="http://cdn.syncfusion.com/js/assets/external/jquery-1.10.2.min.js"></script>
     <script src="http://cdn.syncfusion.com/19.3.0.43/js/web/ej.web.all.min.js"></script>    

       <style type="text/css">
        .work-log-input-col-1 {
            text-align: right;
            padding-right: 5px;
            width: 100px;
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="120000" ></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="frmTitle" style="background: #006699; text-align: center;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="ĐỒ THỊ BẢNG KIỂM TRA ĐỘ DẪN ĐIỆN CỦA NƯỚC SAU XỬ LÝ"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="White" Text="導電度檢測表 CHART"></asp:Label>
                    </div>
                    <div id="frmChart" style="background: #CACBE1; padding: 5px;">
                                <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>
                                        <td>
                                        <asp:Chart ID="Chart1" runat="server" Width="700" BackGradientStyle="Center"  Palette="SeaGreen">
                                            <Titles>
                                                <asp:Title Text="XiDen Chart"></asp:Title>
                                            </Titles>
                                            <Series>
                                                <asp:Series Name="Series_XiDen" ChartType="Line"  >
            
                                                </asp:Series>
                                                <asp:Series Name="s2" ChartType="Line"  >
            
                                                </asp:Series>

                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea_XiDen">
                                                    <AxisX Title="Date"></AxisX>
                                                    <AxisY Title="XiDen Value"></AxisY>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                       </td>
                                    <%--</tr>

                                    <tr>--%>
                                        <td>
                                        <asp:Chart ID="Chart_XiLan" runat="server" Width="700" BackGradientStyle="LeftRight"  Palette="SeaGreen">
                                            <Titles>
                                                <asp:Title Text="XiLan Chart"></asp:Title>
                                            </Titles>
                                            <Series>
                                                <asp:Series Name="Series_XiLan" ChartType="Line"  >
            
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea_XiLan">
                                                    <AxisX Title="Date"></AxisX>
                                                    <AxisX Title ="Date"></AxisX>
                                                    <AxisY Title="XiLan Value"></AxisY>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                        <asp:Chart ID="Chart_XiTreo" runat="server" Width="700" BackGradientStyle="LeftRight"  Palette="SeaGreen">
                                            <Titles>
                                                <asp:Title Text="XiTreo Chart"></asp:Title>
                                            </Titles>
                                            <Series>
                                                <asp:Series Name="Series_XiTreo" ChartType="Line"  >
                                
                                                </asp:Series>
                                            </Series>
                                           
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea_XiTreo">
                                                    <AxisX Title="Date"></AxisX>
                                                    <AxisY Title="XiTreo Value"></AxisY>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                        </td>
                                    </tr>
                                </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel> 
        </div>
   </div>
    


</asp:Content>