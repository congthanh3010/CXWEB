<%@ Page Title="" Language="C#" MasterPageFile="~/Site_kanban.Master" AutoEventWireup="true" CodeBehind="kanban_zhizao.aspx.cs" Inherits="CXWeb.kanban.kanban_zhizao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="10000" Enabled="false" OnTick="Timer1_Tick"></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="position: absolute; width: 100%; height: 96%;">
        <asp:HiddenField ClientIDMode="Static" ID="h_unit" runat="server" />
        <table class="table-bordered table-striped" style="width: 100%; padding: 0px; border: 0px;">
            <tr>
                <td style="padding: 0px">
                    <div style="background: #006699; text-align: left">
                        <asp:RadioButton ID="rdoButton1" GroupName="Group1" AutoPostBack="True" TextAlign="Left" Font-Size="18px" ForeColor="White" Text="全部 Tất cả" Value="1" Checked="true" runat="server" OnCheckedChanged="Group1_CheckedChanged" />
                        <asp:RadioButton ID="rdoButton2" GroupName="Group1" AutoPostBack="True" TextAlign="Left" Font-Size="18px" ForeColor="White" Text="全檢 Kiểm lựa" Value="2" runat="server" OnCheckedChanged="Group1_CheckedChanged" />
                        <asp:Label ID="head_label" Style="padding-left: 500px" Font-Bold="true" Font-Size="18px" ForeColor="White" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding: 0px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div style="position: absolute; width: 100%; height: 100%; overflow: auto;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BackColor="#CACBE1" ShowFooter="false">
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px" />
                                    <RowStyle Font-Names="Calibri" Font-Size="16px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="天數</br>Số ngày" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("0") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="5天以下" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="10天以下" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="15天以下" HeaderStyle-BackColor="Green" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("3") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="30天以下" HeaderStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol05" runat="server" Text='<%# Eval("4") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="90天以下" HeaderStyle-BackColor="Orange" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol06" runat="server" Text='<%# Eval("5") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="90天以上" HeaderStyle-BackColor="Red" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol07" runat="server" Text='<%# Eval("6") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>



                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BackColor="#CACBE1" ShowFooter="false" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="15">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px" />
                                    <RowStyle Font-Names="Calibri" Font-Size="16px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="有無排產</br>Có xếp lịch không" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("is_plan") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工單編號</br>MS công đơn" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("shm012") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="料件編號</br>Chủng loại" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <div style="text-align: left;">
                                                    <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("shm05") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序號</br>STT" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("sgm03") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="作業編號</br>MS công đoạn" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="text-align: left;">
                                                    <asp:Label ID="lblcol05" runat="server" Text='<%# Eval("sgm04") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="作業名稱</br>Tên công đoạn" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <div style="text-align: left;">
                                                    <asp:Label ID="lblcol06" runat="server" Text='<%# Eval("sgm45") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工作站</br>Mã trạm" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <div style="text-align: left;">
                                                    <asp:Label ID="lblcol07" runat="server" Text='<%# Eval("sgm06") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="最後一筆報工轉入日期</br>Ngày báo công sau cùng" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcol08" runat="server" Text='<%# Eval("shb03") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="本站剩餘數量</br>SL còn tại trạm" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblcol09" runat="server" Text='<%# Eval("wipqty") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="停留時間</br>Số ngày bị trễ" HeaderStyle-BackColor="Yellow" ItemStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblcol10" runat="server" Text='<%# Eval("sl_day") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="良品轉入</br>Hàng đạt chuyển vào" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblcol11" runat="server" Text='<%# Eval("sgm301") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="良品轉出</br>Hàng đạt chuyển ra" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblcol12" runat="server" Text='<%# Eval("sgm311") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="報廢數量</br>SL báo phế" HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblcol13" runat="server" Text='<%# Eval("sgm313") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
            text-align: center;
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
            height: 15px;
            vertical-align: middle;
        }

        .tabs a {
            padding: 0px 0px;
            display: inline-block;
            /*background: #666;*/
            /*background: #006699;*/
            color: #000;
            text-decoration: none;
            font-size: 16px;
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
