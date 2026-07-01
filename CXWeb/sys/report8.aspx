<%@Page EnableViewState="true" EnableEventValidation= "false " Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="report8.aspx.cs" Inherits="CXWeb.sys.report8" %>
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
                        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID1="UpdatePanel1" AssociatedUpdatePanelID2="UpdatePanel2">
                            <ProgressTemplate>
                                <div style="width: 220px; height: 46px; left: 700px; position: fixed; font-size: 15px; color: #FFF; z-index: 3; float: right;">
                                    正在加载数据，请稍等...
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
                        <div style="position: fixed; width: 100%; height: 30px; background: #22324C;">
                            <font style="font-size: 15px; font-weight: bold; color: #C8DAF2; position: absolute;">
                           月报设备稼动率查询
                       </font>
                        </div>
                        <br />
                        <div style="width: 100%; margin: auto; height: 60px;  top: 30px; position: absolute; background: #22324C;">
                            <span hidden="true" style="margin-right: 15px;">From </span>
                            <asp:TextBox ID="date1" hidden='true' ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                            <span hidden='true' style="margin-right: 15px;">To </span>
                            <asp:TextBox ID="date2" hidden='true' ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox>

                            <span style="margin-right: 15px; color: #C8DAF2; font-size: 13px;">年份 </span>
                            <asp:TextBox ID="textyear" ClientIDMode="Static" Style="margin-right: 15px; width: 100px;" runat="server" CssClass="some_class"></asp:TextBox>

                            <span style="margin-right: 15px; color: #C8DAF2; font-size: 13px;">月份 </span>
                            <asp:TextBox ID="textmonth" ClientIDMode="Static" runat="server" CssClass="some_class" Style="width: 50px;"></asp:TextBox>

                            <br />

                          <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                                    <asp:Button ID="btnXem" runat="server" Text="查詢" OnClick="btnXem_Click" Style="position: absolute; left: 20px; top: 30px; width: 50px; height: 25px; font-size: 13px;"></asp:Button>
                              <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <asp:Button ID="btnExcel" runat="server" Text="導出Excel" OnClick="btnExcel_Click" Style="position: absolute; left: 100px; top: 30px; width: 90px; height: 25px; font-size: 13px;"></asp:Button>
                        </div>


                    </div>

                </div>
           <%--     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>

                        <table class="table-bordered table-striped" style="width: 1180px; margin-left: 0px" <%=code_hide_table%>>
                            <tr>

                                <td>
                                    <div style="width: 1180px; height: 420px; overflow: auto; margin: auto;">
                                        <table class="table-bordered table-striped" style="width: <%=table_width%>px">

                                            <thead>

                                                <tr>
                                                    <th <%=code_hiden[0]%> style="width: 100px"><%= col_name[0] %></th>
                                                    <th <%=code_hiden[1]%> style="width: 100px"><%= col_name[1] %></th>
                                                    <th <%=code_hiden[2]%> style="width: 200px"><%= col_name[2] %></th>
                                                    <th <%=code_hiden[3]%> style="width: 200px"><%= col_name[3] %></th>
                                                    <th <%=code_hiden[4]%> style="width: 200px"><%= col_name[4] %></th>
                                                    <th <%=code_hiden[5]%> style="width: 100px"><%= col_name[5] %></th>
                                                    <th <%=code_hiden[6]%> style="width: 100px"><%= col_name[6] %></th>
                                                    <th <%=code_hiden[7]%> style="width: 100px"><%= col_name[7] %></th>
                                                    <th <%=code_hiden[8]%> style="width: 100px"><%= col_name[8] %></th>
                                                    <th <%=code_hiden[9]%> style="width: 100px"><%= col_name[9] %></th>
                                                    <th <%=code_hiden[10]%> style="width: 100px"><%= col_name[10] %></th>
                                                    <th <%=code_hiden[11]%> style="width: 100px"><%= col_name[11] %></th>
                                                    <th <%=code_hiden[12]%> style="width: 100px"><%= col_name[12] %></th>
                                                    <th <%=code_hiden[13]%> style="width: 100px"><%= col_name[13] %></th>
                                                    <th <%=code_hiden[14]%> style="width: 100px"><%= col_name[14] %></th>
                                                    <th <%=code_hiden[15]%> style="width: 100px"><%= col_name[15] %></th>
                                                    <th <%=code_hiden[16]%> style="width: 100px"><%= col_name[16] %></th>
                                                    <th <%=code_hiden[17]%> style="width: 100px"><%= col_name[17] %></th>
                                                    <th <%=code_hiden[18]%> style="width: 100px"><%= col_name[18] %></th>
                                                    <th <%=code_hiden[19]%> style="width: 100px"><%= col_name[19] %></th>
                                                    <th <%=code_hiden[20]%> style="width: 100px"><%= col_name[20] %></th>
                                                    <th <%=code_hiden[21]%> style="width: 100px"><%= col_name[21] %></th>
                                                    <th <%=code_hiden[22]%> style="width: 100px"><%= col_name[22] %></th>
                                                    <th <%=code_hiden[23]%> style="width: 100px"><%= col_name[23] %></th>
                                                    <th <%=code_hiden[24]%> style="width: 100px"><%= col_name[24] %></th>
                                                    <th <%=code_hiden[25]%> style="width: 100px"><%= col_name[25] %></th>
                                                    <th <%=code_hiden[26]%> style="width: 100px"><%= col_name[26] %></th>
                                                    <th <%=code_hiden[27]%> style="width: 100px"><%= col_name[27] %></th>
                                                    <th <%=code_hiden[28]%> style="width: 100px"><%= col_name[28] %></th>
                                                    <th <%=code_hiden[29]%> style="width: 100px"><%= col_name[29] %></th>
                                                    <th <%=code_hiden[30]%> style="width: 100px"><%= col_name[30] %></th>
                                                    <th <%=code_hiden[31]%> style="width: 100px"><%= col_name[31] %></th>
                                                    <th <%=code_hiden[32]%> style="width: 100px"><%= col_name[32] %></th>
                                                    <th <%=code_hiden[33]%> style="width: 100px"><%= col_name[33] %></th>
                                                    <th <%=code_hiden[34]%> style="width: 100px"><%= col_name[34] %></th>
                                                    <th <%=code_hiden[35]%> style="width: 100px"><%= col_name[35] %></th>
                                                    <th <%=code_hiden[36]%> style="width: 100px"><%= col_name[36] %></th>
                                                </tr>
                                            </thead>

                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td <%=code_hiden[0]%>><%# Eval("0") %></td>
                                                            <td <%=code_hiden[1]%>><%# Eval("1") %></td>
                                                            <td <%=code_hiden[2]%>><%# Eval("2") %></td>
                                                            <td <%=code_hiden[3]%>><%# Eval("3") %></td>
                                                            <td <%=code_hiden[4]%>><%# Eval("4") %></td>
                                                            <td <%=code_hiden[5]%>><%# Eval("5") %></td>
                                                            <td <%=code_hiden[6]%>><%# Eval("6") %></td>
                                                            <td <%=code_hiden[7]%>><%# Eval("7") %></td>
                                                            <td <%=code_hiden[8]%>><%# Eval("8") %></td>
                                                            <td <%=code_hiden[9]%>><%# Eval("9") %></td>
                                                            <td <%=code_hiden[10]%>><%# Eval("10") %></td>
                                                            <td <%=code_hiden[11]%>><%# Eval("11") %></td>
                                                            <td <%=code_hiden[12]%>><%# Eval("12") %></td>
                                                            <td <%=code_hiden[13]%>><%# Eval("13") %></td>
                                                            <td <%=code_hiden[14]%>><%# Eval("14") %></td>
                                                            <td <%=code_hiden[15]%>><%# Eval("15") %></td>
                                                            <td <%=code_hiden[16]%>><%# Eval("16") %></td>
                                                            <td <%=code_hiden[17]%>><%# Eval("17") %></td>
                                                            <td <%=code_hiden[18]%>><%# Eval("18") %></td>
                                                            <td <%=code_hiden[19]%>><%# Eval("19") %></td>
                                                            <td <%=code_hiden[20]%>><%# Eval("20") %></td>
                                                            <td <%=code_hiden[21]%>><%# Eval("21") %></td>
                                                            <td <%=code_hiden[22]%>><%# Eval("22") %></td>
                                                            <td <%=code_hiden[23]%>><%# Eval("23") %></td>
                                                            <td <%=code_hiden[24]%>><%# Eval("24") %></td>
                                                            <td <%=code_hiden[25]%>><%# Eval("25") %></td>
                                                            <td <%=code_hiden[26]%>><%# Eval("26") %></td>
                                                            <td <%=code_hiden[27]%>><%# Eval("27") %></td>
                                                            <td <%=code_hiden[28]%>><%# Eval("28") %></td>
                                                            <td <%=code_hiden[29]%>><%# Eval("29") %></td>
                                                            <td <%=code_hiden[30]%>><%# Eval("30") %></td>
                                                            <td <%=code_hiden[31]%>><%# Eval("31") %></td>
                                                            <td <%=code_hiden[32]%>><%# Eval("32") %></td>
                                                            <td <%=code_hiden[33]%>><%# Eval("33") %></td>
                                                            <td <%=code_hiden[34]%>><%# Eval("34") %></td>
                                                            <td <%=code_hiden[35]%>><%# Eval("35") %></td>
                                                            <td <%=code_hiden[36]%>><%# Eval("36") %></td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>

                        </table>
              <%--      </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>

        </div>
    </div>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <!--Reference the SignalR library. -->
    <!--Reference the autogenerated SignalR hub script. -->
  <%--  <script src="/signalr/hubs"></script>--%>

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
