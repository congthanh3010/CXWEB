<%@Page EnableViewState="true" EnableEventValidation= "false " Title=""  Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="CXWeb.sys.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--  <form id="form1" runat="server">--%>
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <div class="container-fluid report">
        <div class="row">
            <div class="col-md-12 main">
                <div class="wrap">
                    <div class="page-header">
                        Department 
                        <div class="dropdown">
                            <button id="btnArea" class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                Select Department
                         <span class="caret"></span>
                            </button>

                            <ul class="dropdown-menu">

                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <li><a href="javascript: void(0)" onclick="<%# string.Format("loadDoc('{0}')",Eval("DepId")) %>"><%# Eval("DepId") %></a></li>
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
                        <asp:Button ID="btnExcel" runat="server" Text="Excel" OnClick="btnExcel_Click" />
                    </div>
                </div>
                <div class="wrap">
                    <div style="width: 800px; margin: 0;">
                        <asp:CheckBox Checked="true" ID="colPN" AutoPostBack="true" Text="P/N" OnCheckedChanged="colPN_CheckedChanged" runat="server" Style="margin: 10px;" />
                        <asp:CheckBox Checked="true" ID="colProcess" AutoPostBack="true" Text="Process" OnCheckedChanged="colProcess_CheckedChanged" runat="server" Style="margin: 10px;" />
                        <asp:CheckBox Checked="true" ID="colPLC" AutoPostBack="true" Text="Times/Min" OnCheckedChanged="colPLC_CheckedChanged" runat="server" Style="margin: 10px;" />
                        <asp:CheckBox Checked="true" ID="colStatus" AutoPostBack="true" Text="Status" OnCheckedChanged="colStatus_CheckedChanged" runat="server" Style="margin: 10px;" />
                    </div>
                </div>
                <div class="wrap">
                    <div style="width: <%=table_width %>px; height: 510px; overflow: auto; margin: 0;">
                        <table class=" table table-bordered table-striped table-responsive" style="width: <%=table_width %>px">
                            <thead>
                                <tr>
                                    <th colspan="5">Machine:
                               <asp:Label ID="mamay" ClientIDMode="Static" runat="server" Text="---"></asp:Label><%--  - AREA:<span id="marea">---</span>--%> </th>
                                    <asp:HiddenField ClientIDMode="Static" ID="hmamay" runat="server" />
                                </tr>
                                <tr>
                                    <th style="width: 135px">Time</th>
                                    <th <%=code_colPN %> style="width: 150px">P/N</th>
                                    <th <%=code_colProcess %> style="width: 95px">Process</th>
                                    <th <%=code_colPLC %> style="width: 95px">Times/Min</th>
                                    <th <%=code_colStatus %> style="width: 320px">Status</th>
                                </tr>
                            </thead>
                            <tbody id="data">
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 135px" class="<%# Eval("color") %>"><%# Convert.ToDateTime(Eval("tTime")).ToString("dd/MM/yyyy HH:mm") %></td>
                                            <td <%=code_colPN %> style="width: 150px" class="<%# Eval("color") %>"><%# Eval("PN") %></td>
                                            <td <%=code_colProcess %> style="width: 95px" class="<%# Eval("color") %>"><%# Eval("process") %></td>
                                            <td <%=code_colPLC %> style="width: 95px" class="<%# Eval("color") %>"><%# Eval("iCnt") %></td>
                                            <td <%=code_colStatus %> style="width: 320px" class="<%# Eval("color_report") %>"><%# Eval("cDesc") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>

                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--</form>--%>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <!--Reference the SignalR library. -->
    <!--Reference the autogenerated SignalR hub script. -->
    <script src="/signalr/hubs"></script>

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
