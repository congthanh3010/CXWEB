<%@Page EnableViewState="true" EnableEventValidation= "false " Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="report9.aspx.cs" Inherits="CXWeb.sys.report9" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid report">
        <div class="row">           
            <div class="col-md-12 main" style="padding: 0px;">
                <div class="wrap" style="height: 90px; padding: 0px;">
                    <div class="page-header">
                        <%-- <asp:Label  ID="label_test"  runat="server"  text="" style="width: 220px; height: 46px; left: 700px; position: fixed; font-size: 15px; color: #FFF; z-index: 3; float: right;">
                                  
                                </asp:Label>--%>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID1="UpdatePanel1" AssociatedUpdatePanelID2="UpdatePanel2">

                            <ProgressTemplate>
                                <div style="width: 220px; height: 46px; left: 700px; position: fixed; font-size: 15px; color: #FFF; z-index: 3; float: right;">
                                    正在加载数据，请稍等...
                                </div>

                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <div style="position: fixed; width: 100%; height: 30px; background: #22324C;">
                            <font style="font-size: 15px; font-weight: bold; color: #C8DAF2; position: absolute;">
                           國內公務外出表統計表查询
                       </font>
                        </div>
                        <br />
                        <div style="width: 100%; margin: auto; height: 60px;  top: 30px; position: absolute; background: #22324C;">

                            <span style="margin-right: 15px; color: #C8DAF2;">單據核准日期-起 </span>
                            <asp:TextBox ID="date1" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                            <span style="margin-right: 15px; color: #C8DAF2;">單據核准日期-迄 </span>
                            <asp:TextBox ID="date2" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox>


                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnXem" runat="server" Text="查詢" OnClick="btnXem_Click" Style="position: absolute; left: 20px; top: 30px; width: 50px; height: 25px; font-size: 13px;"></asp:Button>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btnExcel" runat="server" Text="導出Excel" OnClick="btnExcel_Click" Style="position: absolute; left: 100px; top: 30px; width: 90px; height: 25px; font-size: 13px;"></asp:Button>

                        </div>
                    </div>

                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <table class="table-bordered table-striped" style="width: 1180px; margin-left: 0px" <%=code_hide_table%>>

                            <tr hidden='true'>
                                <th>Time:
                <asp:Label ID="time" ClientIDMode="Static" runat="server" Text="---"></asp:Label>
                                    <asp:HiddenField ClientIDMode="Static" ID="hvender" runat="server" />

                                </th>
                            </tr>

                            <tr>

                                <td>
                                    <div style="width: 1180px; height: 420px; overflow: auto; margin: auto;">
                                        <table class="table-bordered table-striped" style="width: 2000px">

                                            <thead>

                                                <tr>
                                                    <th style="width: 200px"><%= col_name[0] %></th>
                                                    <th style="width: 100px"><%= col_name[1] %></th>
                                                    <th style="width: 100px"><%= col_name[2] %></th>
                                                    <th style="width: 200px"><%= col_name[3] %></th>
                                                    <th style="width: 200px"><%= col_name[4] %></th>
                                                    <th style="width: 200px"><%= col_name[5] %></th>
                                                    <th style="width: 300px"><%= col_name[6] %></th>
                                                    <th style="width: 100px"><%= col_name[7] %></th>
                                                    <th style="width: 100px"><%= col_name[8] %></th>
                                                    <th style="width: 100px"><%= col_name[9] %></th>
                                                    <th style="width: 100px"><%= col_name[10] %></th>
                                                    <th style="width: 100px"><%= col_name[11] %></th>
                                                    <th style="width: 200px"><%= col_name[12] %></th>

                                                </tr>
                                            </thead>

                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("0") %></td>
                                                            <td><%# Eval("1") %></td>
                                                            <td><%# Eval("2") %></td>
                                                            <td><%# Eval("3") %></td>
                                                            <td><%# Eval("4") %></td>
                                                            <td><%# Eval("5") %></td>
                                                            <td><%# Eval("6") %></td>
                                                            <td><%# Eval("7") %></td>
                                                            <td><%# Eval("8") %></td>
                                                            <td><%# Eval("9") %></td>
                                                            <td><%# Eval("10") %></td>
                                                            <td><%# Eval("11") %></td>
                                                            <td><%# Eval("12") %></td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>


        </div>
    </div>
    <div hidden="hidden">
        <asp:GridView ID="temptData" runat="server"></asp:GridView>
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
        //$('#loading2').text('正在加载数据，请稍等...').show();
        //function btnXem_Click2() {
        //    $('#loading').text('正在加载数据，请稍等...hhhhhhhhhhhhhhhh').show();
        //    PageMethods.loading();
        //    $.ajax({
        //        beforeSend: function () {

        //        },
        //        complete: function () {
        //            $('#loading').hide();
        //        },
        //        type: "POST",
        //        url: "/sys/report9.aspx",
        //        dataType: "json",
        //        data: $('#select').serialize(),
        //        success: function (data) {
        //            $('#loading').hide();
        //            // $("#report9").html(data);
        //        },
        //        error: function (XmlHttpRequest, textStatus, errorThrown) {
        //            $('#loading').hide();
        //            alert("error:" + XmlHttpRequest.status);
        //        }
        //    });
          
        //}

    </script>
</asp:Content>
