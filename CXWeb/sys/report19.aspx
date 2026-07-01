<%@Page EnableViewState="true" EnableEventValidation= "false " Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="report19.aspx.cs" Inherits="CXWeb.sys.report19" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div class="container-fluid report">
        <div class="row">
            <div class="col-md-12 main" style="padding: 0px;">
                <div class="wrap" style="height: 90px; padding: 0px;">
                    <div class="page-header">



                        <div style="position: fixed; width: 100%; height: 30px; background: #22324C;">
                            <font style="font-size: 15px; font-weight: bold; color: #C8DAF2; position: absolute;">
                           維修技術員之設備維修耗時
                       </font>
                        </div>
                        <br />
                        <div style="width: 100%; margin: auto; height: 60px;  top: 30px; position: absolute; background: #22324C;">

                            <span style="margin-right: 15px; color: #C8DAF2;">起始日期 </span>
                            <asp:TextBox ID="date1" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                            <span style="margin-right: 15px; color: #C8DAF2;">终止日期 </span>
                            <asp:TextBox ID="date2" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>

                          
                            <br />

                            <asp:Button ID="btnXem" runat="server" Text="查詢" OnClick="btnXem_Click" Style="position: absolute; left: 20px; top: 30px; width: 50px; height: 25px; font-size: 13px;"></asp:Button>


                            <%--<asp:Button ID="btnExcel" runat="server" Text="導出Excel" OnClick="btnExcel_Click" Style="position: absolute; left: 100px; top: 30px; width: 90px; height: 25px; font-size: 13px;"></asp:Button>--%>
                        </div>
                    </div>

                </div>




                <table class="table-bordered table-striped" style="width: 1180px; margin-left: 0px" <%=code_hide_table%>>

                    <tr>
                        <td>
                            <asp:Chart ID="Chart1" runat="server" Width="820" Height="450" Palette="SeaGreen">
                                <Series>
                                    <asp:Series Name="Category" ChartArea="ChartArea1" Font="Microsoft Sans Serif, 8.25pt, style=Bold">
                                    </asp:Series>
                                    <asp:Series Name="Category2" ChartArea="ChartArea1" Font="Microsoft Sans Serif, 8.25pt, style=Bold">
                                    </asp:Series>
                                 
                                    
                                </Series>
                              <%--  <Legends>
                                    <asp:Legend Name="Legend1"></asp:Legend>
                                </Legends>--%>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                    <%--   <asp:ChartArea Name="ChartArea2">
                                            </asp:ChartArea>--%>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                         <td>
                                    <div style="width: 360px; height: 410px; overflow: auto; margin: auto;top:0px;">
                                        <table class="table-bordered table-striped" style="width: 360px;top:0px">

                                            <thead>

                                                <tr>
                                                    <th style="width: 80px"><%= col_name[0] %></th>
                                                    <th style="width: 200px"><%= col_name[1] %></th>
                                                    <th style="width: 80px"><%= col_name[2] %></th>
                                                    
                                                </tr>
                                            </thead>

                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("0") %></td>
                                                            <td><%# Eval("1") %></td>
                                                              <td><%# Eval("2") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                    </tr>

                </table>

            </div>
        </div>
    </div>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <!--Reference the SignalR library. -->
    <!--Reference the autogenerated SignalR hub script. -->
    <%--<script src="/signalr/hubs"></script>--%>

    <script>

        var count = 0;
        var colors = ["none", "green", "yellow", "red"];
        function getcolor() {

            return 'green';
        }

        function formatSeconds(seconds) {
            var date = new Date(1970, 0, 1);
            date.setSeconds(seconds);
            return date.toTimeString().replace(/.*(\d{2}:\d{2}:\d{2}).*/, "$1");
        }
        function getmachinedata(value, value2) {
            $('#data').empty();
            $('#mamay').text(value2);
            $('#hmamay').attr("value", value);
            document.getElementById("btnMachine").innerText = value2;
        }
        function loadDoc(area) {
            var xhttp;
            if (window.XMLHttpRequest) {
                // code for modern browsers
                xhttp = new XMLHttpRequest();
            } else {
                // code for IE6, IE5
                xhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhttp.onreadystatechange = function () {
                if (xhttp.readyState == 4 && xhttp.status == 200) {
                    document.getElementById("lstmachine").innerHTML = xhttp.responseText;
                }
            };
            document.getElementById("btnArea").innerText = area;

            xhttp.open("GET", "/sys/getmachines.aspx?m=" + area, true);
            xhttp.send();
        }

    </script>

    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>

    <script type="text/javascript">
        //var currentdate = new Date();
        //var datetime1 = currentdate.getDate() + "/"
        //                + (currentdate.getMonth() + 1) + "/"
        //                + currentdate.getFullYear() + " 0:0";
        //var datetime2 =  currentdate.getDate() + "/"
        //                + (currentdate.getMonth() + 1) + "/"
        //                + currentdate.getFullYear() + " 23:59";

        $.datetimepicker.setLocale('en');
        $('#date1').datetimepicker({ format: 'd/m/Y' });
        $('#date2').datetimepicker({ format: 'd/m/Y' });



    </script>
</asp:Content>
