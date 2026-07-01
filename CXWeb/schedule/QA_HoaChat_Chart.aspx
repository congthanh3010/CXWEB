<%@ Page Title="BIỂU ĐỒ THEO DÕI CÁC HÓA CHẤT QA " Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="QA_HoaChat_Chart.aspx.cs" Inherits="CXWeb.schedule.QA_HoaChat_Chart" EnableEventValidation="false"  %>


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

           .auto-style1 {
               width: 483px;
           }

    </style>
    <asp:scriptmanager runat="server"></asp:scriptmanager>

    <asp:UpdatePanel ID="UpdatePanel_Time" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Enabled="true" Interval="60000" OnTick="Timer1_Tick"></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container-fluid">
        <div class="row">
                    <asp:UpdatePanel ID="UpdatePanel0" runat="server" >
                    <ContentTemplate>
                    <div id="frmTitle" style="background: #006699; text-align: center;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="ĐỒ THỊ BẢNG HÓA CHẤT QA"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="White" Text=" 導電度檢測表 CHART"></asp:Label>
                    </div>
                    <div id="frmChart" style="background: #CACBE1; padding: 5px;">
                                <table class="work-log-input" style="width: 100%; padding: 0; border: 2; font-size: 16px;">
                                    <thead>
                                        <tr>
                                            <td >
                                                
                                                <table class="work-log-input" style="width: 50%; padding: 0; border: 0; font-size: 16px;">
                                                    <tr>
                                                        <td class="auto-style1" >                                                           
                                                            Trạm
                                                            <asp:DropDownList ID="ddMachine_Id" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="dd_SelectedIndexChanged">
                                                                <asp:ListItem Value="CR-000E">CR-000E</asp:ListItem>
                                                                <asp:ListItem Value="CR-000F">CR-000F</asp:ListItem>
                                                                <asp:ListItem Value="CR-000G">CR-000G</asp:ListItem>
                                                            </asp:DropDownList>
                                                           Loại Hóa chất
                                                                <asp:DropDownList ClientIDMode="Static" ID="ddCode_Id" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddCode_Id_SelectedIndexChanged"></asp:DropDownList>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1">
                                                            <asp:UpdatePanel ID="UpdatePane_Chart1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            <div>
                                                            <asp:Chart ID="Chart_CR" runat="server" Width="700" BackGradientStyle="Center"  Palette="SeaGreen">
                                                            <Titles>
                                                                <asp:Title Text="CR Chart"></asp:Title>
                                                            </Titles>
                                                            <Series>
                                                                <asp:Series Name="Series_CR" ChartType="Line"  >
            
                                                                </asp:Series>
                                                                <asp:Series Name="s2" ChartType="Line"  >            
                                                                </asp:Series>
                                                                </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea_CR">
                                                                    <AxisX Title="Date"></AxisX>
                                                                    <AxisY Title="CR Value"></AxisY>
                                                                </asp:ChartArea>
                                                            </ChartAreas>
                                                        </asp:Chart>
                                                        </div>
                                                        </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddMachine_Id" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddCode_Id" EventName="SelectedIndexChanged" />
                                                        
                                                    </Triggers>
                                                </asp:UpdatePanel>  
                                                        </td>
                                                    </tr>                                                 
                                                </table> 
                                                                                                                                            
                                            </td>
                                            <td  >                                                
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <div>
                                                            Trạm
                                                            <asp:DropDownList ID="ddMachine2" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddMachine2_SelectedIndexChanged">
                                                                <asp:ListItem Value="DC-000A">DC-000A</asp:ListItem>
                                                            </asp:DropDownList>
                                                                Loại Hóa chất
                                                                <asp:DropDownList ClientIDMode="Static" ID="ddCode_Id2" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddCode_Id2_SelectedIndexChanged"></asp:DropDownList>
                                                           </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel_Chart2" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            <div>
                                                            <asp:Chart ID="Chart2" runat="server" Width="700" BackGradientStyle="Center"  Palette="SeaGreen">
                                                            <Titles>
                                                                <asp:Title Text="DC Chart"></asp:Title>
                                                            </Titles>
                                                            <Series>
                                                                <asp:Series Name="Series2" ChartType="Line"  >
            
                                                                </asp:Series>
                                                                
                                                                </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea2">
                                                                    <AxisX Title="Date"></AxisX>
                                                                    <AxisY Title="DC Value"></AxisY>
                                                                </asp:ChartArea>
                                                            </ChartAreas>
                                                            </asp:Chart>
                                                            </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                 <asp:AsyncPostBackTrigger ControlID="ddMachine2" EventName="SelectedIndexChanged" />
                                                                <asp:AsyncPostBackTrigger ControlID="ddCode_Id2" EventName="SelectedIndexChanged" />
                                                                <asp:PostBackTrigger ControlID ="Chart2" />                                                              
                                                                
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>                                           
                                            </td>
                                        </tr>
                                        <tr>
                                             <td >                                                
                                                <table class="work-log-input" style="width: 50%; padding: 0; border: 0; font-size: 16px;">
                                                    <tr>
                                                        <td class="auto-style1" >
                                                           
                                                            Trạm
                                                            <asp:DropDownList ID="ddMachine_Id3" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddMachine_Id3_SelectedIndexChanged">
                                                                <asp:ListItem Value="XD-000A">XD-000A</asp:ListItem>
                                                            </asp:DropDownList>
                                                           Loại Hóa chất
                                                                <asp:DropDownList ClientIDMode="Static" ID="ddCode_Id3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddCode_Id3_SelectedIndexChanged"></asp:DropDownList>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            <div>
                                                            <asp:Chart ID="Chart3" runat="server" Width="700" BackGradientStyle="Center"  Palette="SeaGreen">
                                                            <Titles>
                                                                <asp:Title Text="XD Chart"></asp:Title>
                                                            </Titles>
                                                            <Series>
                                                                <asp:Series Name="Series3" ChartType="Line"  >
                                                                </asp:Series>
                                                               
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea3">
                                                                    <AxisX Title="Date"></AxisX>
                                                                    <AxisY Title="CR Value"></AxisY>
                                                                </asp:ChartArea>
                                                            </ChartAreas>
                                                        </asp:Chart>
                                                        </div>
                                                        </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddMachine_Id3" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddCode_Id3" EventName="SelectedIndexChanged" />
                                                        <asp:PostBackTrigger ControlID ="Chart3" />
                                                    </Triggers>
                                                    </asp:UpdatePanel>  
                                                        </td>
                                                    </tr>                                                 
                                                </table>                                                                                                                                             
                                            </td>
                                            <td >                                                
                                                <table class="work-log-input" style="width: 50%; padding: 0; border: 0; font-size: 16px;">
                                                    <tr>
                                                        <td class="auto-style1" >
                                                           
                                                            Trạm
                                                            <asp:DropDownList ID="ddMachine_Id4" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddMachine_Id4_SelectedIndexChanged">
                                                                <asp:ListItem Value="XL-001E">XL-001E</asp:ListItem>
                                                                <asp:ListItem Value="XL-001F">XL-001F</asp:ListItem>
                                                                <asp:ListItem Value="XT-000C">XT-000C</asp:ListItem>
                                                            </asp:DropDownList>
                                                           Loại Hóa chất
                                                                <asp:DropDownList ClientIDMode="Static" ID="ddCode_Id4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddCode_Id4_SelectedIndexChanged"></asp:DropDownList>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            <div>
                                                            <asp:Chart ID="Chart4" runat="server" Width="700" BackGradientStyle="Center"  Palette="SeaGreen">
                                                            <Titles>
                                                                <asp:Title Text="XiLan and XiTreo Chart"></asp:Title>
                                                            </Titles>
                                                            <Series>
                                                                <asp:Series Name="Series4" ChartType="Line"  >
                                                                </asp:Series>
                                                               
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="ChartArea4">
                                                                    <AxisX Title="Date"></AxisX>
                                                                    <AxisY Title="CR Value"></AxisY>
                                                                </asp:ChartArea>
                                                            </ChartAreas>
                                                        </asp:Chart>
                                                        </div>
                                                        </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddMachine_Id4" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddCode_Id4" EventName="SelectedIndexChanged" />
                                                        <%--<asp:PostBackTrigger ControlID ="Chart4" />--%>
                                                    </Triggers>
                                                    </asp:UpdatePanel>  
                                                        </td>
                                                    </tr>                                                 
                                                </table>                                                                                                                                             
                                            </td>
                                        </tr>
                                    </thead>
                                </table>
                    </div>
                </ContentTemplate>
                        <Triggers>
                            <%--<asp:PostBackTrigger ControlID ="Chart_CR" />--%>
                        </Triggers>
              </asp:UpdatePanel>
        </div>
   </div>
    


</asp:Content>