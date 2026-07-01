<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="Einvoice_Buyer.aspx.cs" Inherits="CXWeb.schedule.Einvoice_Buyer" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="position: absolute; width: 100%; height: 96%">
        <table class="table-bordered table-striped" style="width: 100%; padding: 0px; border: 0px; font-size: large;">
            <tr>
                <td style="padding: 0">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="background: #006699; text-align: center">
                                <asp:Label ID="lblBuyer" runat="server" Font-Bold="true" ForeColor="White" ></asp:Label>
                            </div>
                            <div style="background: #CACBE1; padding: 5px;">
                                <table style="width: 800px; padding: 0px; border: 0px; font-size: 16px">
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label5" runat="server" Text="Người mua hàng"></asp:Label>
                                        </td>
                                        <td colspan="3" class="buyer-info-col-2">
                                            <asp:TextBox ID="txtHoTen" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label6" runat="server" Text="Tên đơn vị"></asp:Label>
                                        </td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:TextBox ID="txtDonVi" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1" style="vertical-align: top;">
                                            <asp:Label ID="Label7" runat="server" Text="Địa chỉ"></asp:Label>
                                        </td>
                                        <td colspan="3" style="text-align: left; vertical-align: middle">
                                            <asp:TextBox ID="txtDiaChi" runat="server" TextMode="MultiLine" Width="100%" Height="60px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label8" runat="server" Text="Mã số thuế"></asp:Label>
                                        </td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:TextBox ID="txtMaSo" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                                        </td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:TextBox ID="txtEmail" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label10" runat="server" Text="Tên dịch vụ"></asp:Label>
                                        </td>
                                        <td colspan="3" style="text-align: left">
                                            <asp:TextBox ID="txtDichVu" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label11" runat="server" Text="Hình thức thanh toán"></asp:Label>
                                        </td>
                                        <td class="buyer-info-col-2">
                                            <asp:TextBox ID="txtThanhToan" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label12" runat="server" Text="Đơn vị tính HĐ"></asp:Label>
                                        </td>
                                        <td class="buyer-info-col-2">
                                            <asp:TextBox ID="txtTinhHD" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="buyer-info-col-3">
                                            <asp:Label ID="Label13" runat="server" Text="Đơn vị tính HT"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTinhHT" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label16" runat="server" Text="Đơn vị tiền tệ đơn giá"></asp:Label>
                                        </td>
                                        <td class="buyer-info-col-2">
                                            <asp:TextBox ID="txtTienTeDG" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="buyer-info-col-3">
                                            <asp:Label ID="Label17" runat="server" Text="Đơn vị tiền tệ thành tiền"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTienTeTT" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="buyer-info-col-1">
                                            <asp:Label ID="Label14" runat="server" Text="Đơn vị tiền tệ HT"></asp:Label>
                                        </td>
                                        <td class="buyer-info-col-2">
                                            <asp:TextBox ID="txtTienTeHT" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>    
                                        <td class="buyer-info-col-3">
                                            <asp:Label ID="Label15" runat="server" Text="Loại tỉ suất"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTiSuat" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                        </td>                                    
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="padding-top: 5px">
                                            <div class="btn-group btn-group-justified" style="width: 100%; padding-left: 220px; padding-right: 220px">
                                                <div class="btn-group">
                                                    <asp:Button ID="btnModify" CssClass="btn btn-default" runat="server" Text="Sửa" Font-Bold="true" OnClick="btnModify_Click" />
                                                </div>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnSave" CssClass="btn btn-default" runat="server" Text="Lưu" Font-Bold="true" Enabled="false" OnClick="btnSave_Click" />
                                                </div>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" Text="Hủy" Font-Bold="true" Enabled="false" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%">
                                <div style="background: #006699; color: white">
                                    <asp:Label ID="lblBody" runat="server" Font-Bold="true" ForeColor="White" Text="Danh sách người mua hàng"></asp:Label>
                                </div>
                                <div style="position: absolute; width: 100%; height: 300px; overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" BackColor="#CACBE1" ShowFooter="false" 
                                        OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataKeyNames="ma_don_vi">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#006699" Font-Bold="True" Font-Names="Cambira" ForeColor="White" Font-Size="18px"/>
                                        <RowStyle Font-Names="Calibri" Font-Size="16px"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Tên đơn vị" HeaderStyle-Width="400px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ten_don_vi") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mã số thuế" HeaderStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ma_so_thue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tên dịch vụ" HeaderStyle-Width="300px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ten_dich_vu") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
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

        .buyer-info-col-1 {
            width: 150px;
            text-align: left;
            vertical-align: middle;
        }

        .buyer-info-col-2 {
            text-align: left;
            width: 150px;
        }

        .buyer-info-col-3 {
            text-align: right;
            width: 180px;
            padding-right: 5px;
        }
    </style>

    <%--<script type="text/javascript">
        function MouseEvents(objRef, evt)
        {
            var select = objRef.getElementsByTagName("selected")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "orange";
            }
            else {
                if (select.selected) {
                    objRef.style.backgroundColor = "aqua";
                }
                else if (evt.type == "mouseout") {
                    objRef.style.backgroundColor = "#CACBE1";
                }
            }
        }
    </script>--%>
</asp:Content>
