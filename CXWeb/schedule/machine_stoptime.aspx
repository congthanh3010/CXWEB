<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="machine_stoptime.aspx.cs" Inherits="CXWeb.schedule.machine_stoptime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div style="position: absolute; width: 100%; height: 86%; overflow: auto;">
        <asp:HiddenField ClientIDMode="Static" ID="hreport_msg" runat="server" />
        <table class="table-bordered table-striped" style="width: 100%; font-size: large; padding: 0px; border: 0px;">
           
            <tr>
                <td style="text-align: left; background-color: #006699; color: white; font-weight: bold;">查詢條件
                </td>
            </tr>
            <tr>
                <td style="padding: 0px;">日期 
                    <asp:TextBox ID="textDate" ClientIDMode="Static" AutoCompleteType="Disabled" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>
                  <%--  班次 Ca--%>
                     <%--<asp:TextBox ID="textShift" ClientIDMode="Static" Style="margin-right: 15px; width:50px;" runat="server" CssClass="some_class"></asp:TextBox>--%>
                    <%--<asp:DropDownList ID="textShift" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="CA1">早班 CA1</asp:ListItem>
                        <asp:ListItem Value="CA2">晚班 CA2</asp:ListItem>
                    </asp:DropDownList>--%>
                    機台編號
                    <asp:TextBox ID="machine_no"  Style="width: 70px;" runat="server"></asp:TextBox>
                    停機時間（小時）
                    <asp:TextBox ID="stop_time"  Style="width: 70px;" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding: 0px;">
                    <asp:Button ID="btnXem" runat="server" Style="font-size: large;" Text="查詢/Truy cứu" OnClick="btnXem_Click" Font-Bold="true"></asp:Button>
                    <asp:Button ID="btnSave" runat="server" Style="font-size: large;" Text="保存/Lưu" OnClick="btnSave_Click" Font-Bold="true"></asp:Button>
                    <asp:Button ID="btnReturn" runat="server" Style="font-size: large;" Text="返回/Trở lại" OnClick="btnReturn_Click" Font-Bold="true"></asp:Button>
                </td>
            </tr>
              <tr>
                <td  colspan="8"  style=" font-size: 20px; color: #f00;">
                                   <%=hreport_msg.Value%>                                   
                                </td>  
            </tr>
            <tr>
                <td style="padding: 0px;">
                   
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                            OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
                            ShowFooter="false" OnRowCommand="GridView1_RowCommand">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px" />
                            <RowStyle Font-Names="Calibri" Font-Size="18px" />
                            <Columns>
                                <asp:TemplateField HeaderText="日期/Date" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                       <%--  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("link") %>'
                                        Text='<%# Eval("unit_name") %>'></asp:HyperLink>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="機台編號/Mã Máy" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("machine_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="停機時間（小時/Stop Time" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("stop_time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                </td>
            </tr>

        </table>

    </div>




    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>
    <script src="/js/foundation-datepicker.js"></script>
    <script src="/js/foundation-datepicker.zh-CN.js"></script>
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="/css/foundation-datepicker.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">

        $.datetimepicker.setLocale('en');

        $('#textDate').fdatepicker({
            format: 'yyyy/mm/dd',
        });

    </script>
    <script src="/js/jquery.js"></script>
    <script>
        // Wait until the DOM has loaded before querying the document
        $(document).ready(function () {
            $('table.tabs').each(function () {
                // For each set of tabs, we want to keep track of
                // which tab is active and it's associated content
                var $active, $content, $links = $(this).find('a');

                // If the location.hash matches one of the links, use that as the active tab.
                // If no match is found, use the first link as the initial active tab.
                $active = $($links.filter('[href="' + location.hash + '"]')[0] || $links[0]);
                $active.addClass('active');
                $content = $($active.attr('href'));

                // Hide the remaining content
                $links.not($active).each(function () {
                    $($(this).attr('href')).hide();
                });

                // Bind the click event handler
                $(this).on('click', 'a', function (e) {
                    // Make the old tab inactive.
                    $active.removeClass('active');
                    $content.hide();

                    // Update the variables with the new link and content
                    $active = $(this);
                    $content = $($(this).attr('href'));

                    // Make the tab active.
                    $active.addClass('active');
                    $content.show();

                    // Prevent the anchor's default click action
                    e.preventDefault();
                });
            });
        });
    </script>
    <script>
        // Wait until the DOM has loaded before querying the document
        $(document).ready(function () {
            $('ul.tabs').each(function () {
                // For each set of tabs, we want to keep track of
                // which tab is active and it's associated content
                var $active, $content, $links = $(this).find('a');

                // If the location.hash matches one of the links, use that as the active tab.
                // If no match is found, use the first link as the initial active tab.
                $active = $($links.filter('[href="' + location.hash + '"]')[0] || $links[0]);
                $active.addClass('active');
                $content = $($active.attr('href'));

                // Hide the remaining content
                $links.not($active).each(function () {
                    $($(this).attr('href')).hide();
                });

                // Bind the click event handler
                $(this).on('click', 'a', function (e) {
                    // Make the old tab inactive.
                    $active.removeClass('active');
                    $content.hide();

                    // Update the variables with the new link and content
                    $active = $(this);
                    $content = $($(this).attr('href'));

                    // Make the tab active.
                    $active.addClass('active');
                    $content.show();

                    // Prevent the anchor's default click action
                    e.preventDefault();
                });
            });
        });
    </script>
    
    <style type="text/css">
        th, td {
            text-align: left;
            padding: 2px;
        }

        * {
            padding: 0;
            margin: 0;
        }

        html {
            background: #d4d4d4;
            padding: 0px 0px 0;
            /*font-family: sans-serif;*/
            font-size: 14px;
        }

        p, h3 {
            margin-bottom: 15px;
        }

        /*去掉要不人日期工具不顯示*/
        /*div {
            padding: 0px;
            width: 100%;
            background: #fff;
            text-align: center;
        }*/

        .tabs li {
            list-style: none;
            display: inline;
        }

        .tabs td, th {
            list-style: none;
            /*display: inline-block;*/
            width: 33.3%;
            border: solid 0.5px grey;
            height: 35px;
            vertical-align: middle;
        }

        .tabs a {
            padding: 0px 0px;
            display: inline-block;
            /*background: #666;*/
            /*background: #006699;*/
            color: #000;
            text-decoration: none;
            font-size: 18px;
            font-weight: bold;
            width: 100%;
            height: 100%;
        }

            .tabs a.active {
                /*background: #fff;*/
                background: #ff6254; /* Old browsers */
                background: -moz-linear-gradient(top, #86D3FF 0%, #1CB3FF 100%); /* FF3.6-15 */
                background: -webkit-linear-gradient(top, #86D3FF 0%,#1CB3FF 100%); /* Chrome10-25,Safari5.1-6 */
                background: linear-gradient(to bottom, #86D3FF 0%,#1CB3FF 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
                filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#86D3FF', endColorstr='#1CB3FF',GradientType=0 ); /* IE6-9 */
                color: #fff;
            }

        .button {
            Width: 100%;
            height: 35px;
            Font-Size: 18px;
            font-weight: bold;
        }
    </style>
   
</asp:Content>
