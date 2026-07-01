<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="layout.aspx.cs" Inherits="CXWeb.plc.layout" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>

    <link href="../css/plc.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>

    <title>Layout</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid" style="margin-left: 20px; margin-right: 20px;">
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="ajaxManager" runat="server" OnAjaxRequest="Ajax_Request"></telerik:RadAjaxManager>
            <asp:Timer ID="Timer1" runat="server" Interval="180000" Enabled="false" OnTick="Timer1_Tick"></asp:Timer>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <asp:Label ID="title" runat="server" Text="Layout" Width="100%" Font-Bold="true" Font-Size="30px" CssClass="title_layout"></asp:Label>
                    </div>
                    <div class="row">
                        <asp:DropDownList CssClass="btn btn-primary" ClientIDMode="Static" ID="Dep" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Dep_SelectedIndexChanged">
                            <asp:ListItem Value="">1-CXVN - toàn xưởng VN</asp:ListItem>
                            <asp:ListItem Value="CT1">2-製一 - CT1</asp:ListItem>
                            <asp:ListItem Value="CT2;CT21;CT2_LH">3-製二單打 - CT2</asp:ListItem>
                            <asp:ListItem Value="CT2_LH">4-製二連續 - CT2_LH</asp:ListItem>
                            <asp:ListItem Value="CT3;CT31">5-製三 - CT3</asp:ListItem>
                            <asp:ListItem Value="CT3_LH">6-製三組連續沖壓 - CT3_LH</asp:ListItem>
                            <asp:ListItem Value="CNC1;CNC11;CNC21">7-CNC一區 - CNC1</asp:ListItem>
                            <asp:ListItem Value="CNC2;CNC21">8-CNC二區 - CNC2</asp:ListItem>
                            <asp:ListItem Value="NC1;NC11">9-NC加工第一區 - NC1</asp:ListItem>
                            <asp:ListItem Value="NC2;NC21">10-NC加工第二區 - NC2</asp:ListItem>
                            <asp:ListItem Value="DE">11-鋁製區 - DE</asp:ListItem>
                            <asp:ListItem Value="JA">12-精密沖壓 - JA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="row" style="border: 1px solid; background-color: lightgray; margin-top: 10px;">
                        <div class="myDiagram" style="max-width: <%=max_width%>px; margin: auto; margin-top: 10px; margin-bottom: 10px;">
                            <telerik:RadDiagram ClientIDMode="Static" ID="brackets" runat="server" Selectable="false" EnableViewState="true" Editable="false" ZoomMin="0.254">
                                <ClientEvents OnClick="client_click" />
                            </telerik:RadDiagram>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
                    <asp:PostBackTrigger ControlID="Timer1" />
                </Triggers>
            </asp:UpdatePanel>
            
            <div id="mcclick">

            </div>
            <div class="modal" id="mcinfo" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button class="close" data-dismiss="modal" aria-label="Close">&times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">

                            <table class="table table-bordered table-responsive" style="text-align: center;">
                                <thead>
                                    <tr style="font-weight: bold;">
                                        <td>Time start</td>
                                        <td>Time end</td>
                                        <td>Total times</td>
                                        <td>Status</td>
                                    </tr>
                                </thead>
                                <tbody id="detail">
                                    
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        //<![CDATA[
        function getAjaxManager() { return $find("<%=ajaxManager.ClientID%>"); }
        //]]>

        function client_click(e) {
            var id = e.item.options.id;
            if (e.item instanceof kendo.dataviz.diagram.Shape) {
                alert(id);
                if (id.substr(0, 2) == "mc") {
                    var mid = e.item.options.content.text;
                    //$("#detail").empty();

                    //$.ajax({
                    //    method: "POST",
                    //    url: "/plc/machine_work/GetMachineWork",
                    //    data: JSON.stringify({ "id": mid }),
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "jsonp",
                    //    error: function (xhr, status, error) {
                    //        alert(xhr.responseText);
                    //        alert(xhr.statusText);
                    //        alert(error);
                    //    },
                    //    success: function (res) {
                    //        if (res != null && res.d != null) {
                    //            var data = res.d;
                    //            data = $.parseJSON(data);
                    //            for (var i in data) {
                    //                $("#detail").append(`<tr><td>${data[i]["sdate"]}</td><td>${data[i]["edate"]}</td><td>${data[i]["total"]}</td>${data[i]["stat"]}</tr>`);
                    //            }
                    //        }
                    //        $(".modal-title").text(mid);
                    //        $("#mcinfo").modal("show");
                    //    }
                    //});

                    //var xhttp;
                    //if (window.XMLHttpRequest) {
                    //    xhttp = new XMLHttpRequest();
                    //} else {
                    //    xhttp = new ActiveXObject("Microsoft.XMLHTTP");
                    //}

                    //xhttp.onreadystatechange = function () {
                    //    if (xhttp.readyState == 4 && xhttp.status == 200) {
                    //        $("#detail").empty();
                    //        document.getElementById("detail").innerHTML = xhttp.responseText;
                    //        $(".modal-title").text(mid);
                    //        $("#mcinfo").modal("show");
                    //    }
                    //}
                    //xhttp.open("post", "/plc/machine_work.aspx/GetMachineWork?id=" + mid, true);
                    //xhttp.send();
                }
            }
        }

        (function (global, undefined) {
            var diagram;

            function diagram_load(sender) {
                diagram = sender.get_kendoWidget();
                //getAjaxManager().ajaxRequest();
            }

            function loadFromServer(value) {
                diagram.load(value);
            }

            global.diagram_load = diagram_load;
            global.loadFromServer = loadFromServer;
        })(window);
    </script>
</body>
</html>
