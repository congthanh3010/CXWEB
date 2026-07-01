<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mcReport.aspx.cs" Inherits="CXWeb.sys.mcReport" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <div class="container-fluid report">
        <div class="row">
            <div class="col-md-12 main">
                <div class="wrap">
                    <div class="page-header">
                        Area 
                        <div class="dropdown">
                            <button id="btnArea" class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                Select Area
                         <span class="caret"></span>
                            </button>

                            <ul class="dropdown-menu">

                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <li><a href="javascript: void(0)" onclick="<%# string.Format("loadDoc('{0}')",Eval("clocate")) %>"><%# Eval("clocate") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </ul>

                        </div>
                        Machine
	                 <div class="dropdown">
                         <button id="btnMachine" class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                             ---
                        <span class="caret"></span>
                         </button>
                         <ul id="lstmachine" class="dropdown-menu">
                             <li><a href="#">----</a></li>
                         </ul>
                     </div>

                        <span style="margin-right: 15px;">From </span>
                        <asp:TextBox ID="date1" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                        <span style="margin-right: 15px;">To </span>
                        <asp:TextBox ID="date2" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox>

                        <asp:Button ID="btnXem" runat="server" Text="View" OnClick="btnXem_Click" />
                    </div>
                </div>

            </div>
       

    <table class="table-bordered table-striped"">
        <tr>
            <th colspan="3">Machine:
                <asp:Label ID="mamay" ClientIDMode="Static" runat="server" Text="---"></asp:Label><%--  - AREA:<span id="marea">---</span>--%>
                <asp:HiddenField ClientIDMode="Static" ID="hmamay" runat="server" />
            </th>
        </tr>
        <tr>
            <th colspan="3">Time:
                <asp:Label ID="time" ClientIDMode="Static" runat="server" Text="---"></asp:Label>
               
            </th>
        </tr>
        <tr>
            <th>Machine Report Status
            </th>
            <th>Report Code
            </th>
            <th>Machine PLC Status
            </th>
        </tr>
        <tr>
            <td>
                <table style="width: 99%">

                    <%--   <tr>
            <td style="width: 150px; text-align: right">
                Chart Type:
            </td>
            <td>
                <asp:DropDownList ID="drTypeChart" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drTypeChart_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>--%>
                    <tr>
                        <td colspan="2">
                            <asp:Chart ID="Chart1" runat="server" Width="400" Height="360" Palette="SeaGreen">
                                <Series>
                                    <asp:Series Name="Category" ChartArea="ChartArea1">
                                                  
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
            </td>
       
            <td>
                <table class="table-bordered table-striped" style="width: 400px">

                    <thead>
                        <tr>
                            <th style="width: 10%">Code</th>
                            <th style="width: 80%">Desc</th>
                            <th style="width: 10%">Percent</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td <%# Eval("code") %>><%# Eval("cCode") %></td>
                                    <td><%# Eval("cDesc") %></td>
                                    <td><%# Eval("percent") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </td>
        
            <td>
                <table style="width: 99%">
                    <%--   <tr>
          <td style="width: 150px; text-align: right">
                Chart Type:
            </td>
            <td>
                <asp:DropDownList ID="drTypeChart2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drTypeChart2_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>--%>
                    <tr>
                        <td colspan="2">
                            <asp:Chart ID="Chart2" runat="server" Width="400" Height="360" Palette="SeaGreen">
                                <Series>
                                    <asp:Series Name="Category" ChartArea="ChartArea1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
        $('#date1').datetimepicker({ format: 'd/m/Y H:i' });
        $('#date2').datetimepicker({ format: 'd/m/Y H:i' });



    </script>
</asp:Content>
