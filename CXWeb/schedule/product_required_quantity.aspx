<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="product_required_quantity.aspx.cs" Inherits="CXWeb.schedule.product_required_quantity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div style="position: absolute; width: 100%; height: 100%;">
        <asp:HiddenField ClientIDMode="Static" ID="hreport_msg" runat="server" />
        <table class="table-bordered table-striped" style="width: 100%; font-size: large; padding: 0px; border: 0px;">
           
            <tr>
                <td style="text-align: left; background-color: #006699; color: white; font-weight: bold;">查詢條件
                </td>
            </tr>
            <tr>
                <td style="padding: 0px;">
                    料號-Mã
                    <asp:TextBox ID="product_no"  Style="width: 200px;" runat="server"></asp:TextBox>
                    訂單數量 -SL đơn hàng
                    <asp:TextBox ID="order_quantity"  Style="width: 100px;" runat="server"></asp:TextBox>
                     WIP數量-WIP
                    <asp:TextBox ID="wip_quantity"  Style="width: 100px;" runat="server"></asp:TextBox>
                    交貨日期-Ngày
                    <asp:TextBox ID="finish_date"  Style="width: 100px;" runat="server"></asp:TextBox>
                    排程階段-Công đoạn
                    <asp:TextBox ID="stage"  Style="width: 100px;" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding: 0px;">
                    <asp:Button ID="btnXem" runat="server" Style="font-size: large;" Text="查詢-Truy vấn" OnClick="btnXem_Click" Font-Bold="true"></asp:Button>
                    <asp:Button ID="btnSchedule" runat="server" Style="font-size: large;" Text="排DBR" OnClick="btnSchedule_Click" Font-Bold="true"></asp:Button>
                    <asp:Button ID="btnReturn" runat="server" Style="font-size: large;" Text="返回-Trở về" OnClick="btnReturn_Click" Font-Bold="true"></asp:Button>
                </td>
            </tr>
              <tr>
                <td  colspan="8"  style=" font-size: 20px; color: #f00;">
                                   <%=hreport_msg.Value%>                                   
                   
                     <table class='tabs' style="width: 100%; height: 30px; border: thin 0.5px grey;">
                        <tr>
                            <td class="grey">
                                <a href='#tab1'>訂單數量</a>
                            </td>
                            <td class="grey">
                                <a href='#tab2'>Wip 數量</a>
                            </td>
                        </tr>
                    </table>
                                </td>  
            </tr>
            <tr>
                <td style="padding: 0px;">
                   
                     <div id='tab1' style="position: absolute;width: 100%; height: 80%; overflow: auto; top: 215px; left: 1px;">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                            OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1" OnRowCommand="GridView1_RowCommand">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px" />
                            <RowStyle Font-Names="Calibri" Font-Size="18px" />
                            <Columns>
                                <asp:CheckBoxField DataField="Chon" HeaderText="Select" HeaderStyle-Width="100px" ReadOnly="False" >
                                 
<HeaderStyle Width="50px"></HeaderStyle>
                                </asp:CheckBoxField>
                                 
                                <asp:TemplateField HeaderText="料號-Mã" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("Item_No") %>'></asp:Label>
                                       <%--  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("link") %>'
                                        Text='<%# Eval("unit_name") %>'></asp:HyperLink>--%>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="訂單單號-Thứ tự" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("Order_No") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="客戶-Khách hàng" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("Cus_ID") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="客戶簡稱-Tên KH" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol031" runat="server" Text='<%# Eval("Cus_Name") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="交貨日期-Ngày DH" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("Delivery_Date") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="收訂量-Số lượng" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol05" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="未出貨量-SL chưa giao" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol06" runat="server" Text='<%# Eval("Unshipped_Quat") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="排定交貨日-Ngày GH" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol07" runat="server" Text='<%# Eval("Sche_Delivery") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                          </div>
                    <div id='tab2' style="position: absolute;width: 100%; height: 80%; overflow: auto;">

                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                            OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
                            ShowFooter="false" OnRowCommand="GridView1_RowCommand">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px" />
                            <RowStyle Font-Names="Calibri" Font-Size="18px" />
                            <Columns>
                               
                                <asp:TemplateField HeaderText="料件-Mã" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("料件") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序號" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("序號") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="作業編號" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("作業編號") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="作業名稱" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("作業名稱") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="未完成數量-SL chưa hoàn thành" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol05" runat="server" Text='<%# Eval("未完成數量") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="WIP累計" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol06" runat="server" Text='<%# Eval("WIP累計") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                        </asp:GridView>
                    </div>
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
