<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="order_hangmau.aspx.cs" Inherits="CXWeb.schedule.order_hangmau" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div style="position: absolute; width: 100%; height: 86%; overflow: auto;">
        <asp:HiddenField ClientIDMode="Static" ID="hreport_msg" runat="server" />
        <table class="table-bordered table-striped" style="width: 100%; font-size: large; padding: 0px; border: 0px;">
           
            <tr>
                <td style="text-align: left; background-color: #006699; color: white; font-weight: bold;" class="auto-style1">Sample Order
                    </td>
            </tr>
                <asp:HiddenField ID="hfContactID" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbtOrder_Id" runat="server" Text="Order ID" Style="font-size: large;"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="txtOrder_Id" runat="server" Style="font-size: large;"></asp:TextBox>
                        </td>
                     </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbtItem_No" runat="server" Text="Product ID" Style="font-size: large;"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="txtItem_No" runat="server" Style="font-size: large;"></asp:TextBox>
                        </td>
                     </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LbtMachine_No" runat="server" Text="Machine ID" Style="font-size: large;"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="txtMachine_No" runat="server" Style="font-size: large;"></asp:TextBox>
                        </td>
                     </tr>
                  <%--  <tr>
                        <td>
                            <asp:Label ID="lbtCus_name" runat="server" Text="Cus Name" Style="font-size: large;"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="txtCus_Name" runat="server" Style="font-size: large;"></asp:TextBox>
                        </td>
                     </tr>--%>
                     <tr>
                        <td>
                            <asp:Label ID="lbtBegin_Date" runat="server" Text="Begin Date"  Style="font-size: large;"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="txtBegin_Date" ClientIDMode="Static"   CssClass="some_class" runat="server"  Style="font-size: large;" OnTextChanged="txtBegin_Date_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbtEnd_Date" runat="server" Text="End Date" Style="font-size: large;"></asp:Label>
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="txtEnd_Date"  ClientIDMode="Static"   CssClass="some_class" runat="server"  Style="font-size: large;" OnTextChanged="txtEnd_Date_TextChanged"></asp:TextBox>
                        </td>
                        </tr> 
                    <tr>
                        <td>
                            <asp:Label ID="lbtTime_Date" runat="server" Text="Days" Style="font-size: large;"></asp:Label>
                             
                        </td>
                        <td colspan="2" class="auto-style2">
                            <asp:TextBox ID="txtTime_Date" runat="server"  Style="font-size: large;" Width="72px"></asp:TextBox>
                            <asp:Button ID="btnCalc_Date" runat="server" Style="font-size: large;" Text="Tính ngày" OnClick="btnCalc_Date_Click" Font-Bold="true" Height="38px"></asp:Button>
                        </td>
                    </tr> 
                    
                     <tr>
                        <td>
                    
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                        </td>
                    <tr>
                        <td>
                    
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    </tr>                        

                </table>
        
            <tr>
                <td style="padding: 0px;" class="auto-style1">
                    <asp:Button ID="btnSave" runat="server" Style="font-size: large;" Text="New/Thêm mới" OnClick="btnSave_Click" Font-Bold="true"></asp:Button>
                   <%-- <asp:Button ID="btnUpdate" runat="server" Style="font-size: large;" Text="Update/Cập nhật" OnClick="btnUpdate_Click" Font-Bold="true"></asp:Button>--%>
                    <asp:Button ID="btnClear" runat="server" Style="font-size: large;" Text="Clear/Làm mới" OnClick="btnClear_Click" Font-Bold="true"></asp:Button>
                    <asp:Button ID="btnReturn" runat="server" Style="font-size: large;" Text="返回/Trở lại" OnClick="btnReturn_Click" Font-Bold="true"></asp:Button>
                </td>
            </tr>
              <tr>
                <td  colspan="8"  style=" font-size: 20px; color: #f00;">
                                   <%=hreport_msg.Value%>                                   
                                </td>  
            </tr>
            <tr>
                <td style="padding: 0px;" class="auto-style1">
                   
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                            OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
                            ShowFooter="false" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px" />
                            <RowStyle Font-Names="Calibri" Font-Size="18px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Begin Date" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBegin_Date" runat="server" Text='<%# Eval("Begin_Date_Convert") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEnd_Date" runat="server" Text='<%# Eval("End_Date_Convert") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order ID" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbOrder_ID" runat="server" Text='<%# Eval("Order_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Cus Name" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCus_Name" runat="server" Text='<%# Eval("Cus_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="Product ID" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem_No" runat="server" Text='<%# Eval("Item_No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Machine ID" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMachine_No" runat="server" Text='<%# Eval("Machine_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDays" runat="server" Text='<%# Eval("Time_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作-Hoạt động" HeaderStyle-Width="10%">

                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id_Num") %>' OnClick="lnk_OnClick">更改-E  </asp:LinkButton>
                                     <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id_Num") %>' OnClick="lnk_Delete">刪除-D  </asp:LinkButton>
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

        $('#txtBegin_Date').fdatepicker({
            format: 'mm/dd/yyyy',
        });

    </script>
    <script type="text/javascript">

        $.datetimepicker.setLocale('en');

        $('#txtEnd_Date').fdatepicker({
            format: 'mm/dd/yyyy',
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
        .auto-style1 {
            width: 1311px;
        }
        .auto-style2 {
            width: 361px;
        }
    </style>
   
</asp:Content>
