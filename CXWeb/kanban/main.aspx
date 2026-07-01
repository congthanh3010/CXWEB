<%@ Page Title="" Language="C#" MasterPageFile="~/Site_kanban.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="CXWeb.kanban.main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- <ul class='tabs'>
        <li><a href='#tab1' style="width:33%">製一 CT1</a></li>
        <li><a href='#tab2' style="width:33%">製二 CT2</a></li>
        <li><a href='#tab3' style="width:33%">製三 CT3</a></li><br />
        <li><a href='#tab4' style="width:24.5%">&nbsp &nbsp &nbsp &nbsp &nbsp NC</a></li>
        <li><a href='#tab5' style="width:24.5%">&nbsp &nbsp &nbsp &nbsp &nbsp CNC</a></li>
        <li><a href='#tab6' style="width:24.5%">加工 GC</a></li>
        <li><a href='#tab7' style="width:24.5%">電鍍 XLBM</a></li>
      
    </ul>--%>
    <%--<table class='tabs' style="width: 100%;  height:120px; border:thin 0.5px grey;  ">
        <tr>
            <td class="grey">
                 <a href='#tab1'>製一 CT1</a>               
            </td>
            <td class="grey">
                <a href='#tab2'>製二 CT2</a>

            </td>
            <td class="grey">
                <a href='#tab3'>製三 CT3</a>
            </td>
        </tr>
        <tr>
            <td class="grey">
                <a href='#tab4'>NC</a>
            </td>
            <td class="grey">
                <a href='#tab5'>CNC</a>

            </td>
            <td class="grey">
                <a href='#tab6'>加工 GC</a>
            </td>

        </tr>
        <tr style="width: 100%">
           
            <td class="grey">
                <a href='#tab7'>電鍍 XLBM</a>

            </td>
          
        </tr>
    </table>--%>


    <table style="width: 100%; height: 100%;">

        <tr>
            <td>
                <table class='tabs' style="width: 100%; height: 120px;">
                    <thead style="font-size: 18px; font-weight: bold; background: #006699; color: white;">
                        <tr>
                            <th>部門 (Bộ phận)</th>
                            <th>單位 (Đơn vị)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="grey" style="width: 40%;">
                                <a href='#tab1'>製造 (Chế tạo)</a>
                            </td>
                            <td rowspan="3" style="width: 60%; vertical-align: top; border: none;">
                                <div id='tab1' style="white-space: normal">
                                    <asp:Button ID="btnCT5" CssClass="button" runat="server" Text="製造前段看板設定 (Cài đặt Kanban chế tạo)" OnClick="btnCT5_Click"></asp:Button>
                                    <asp:Button ID="btnCT1" CssClass="button" runat="server" Text="製一 (Chế tạo 1)" OnClick="btnCT1_Click"></asp:Button>
                                    <asp:Button ID="btnCT2" CssClass="button" runat="server" Text="製二 (Chế tạo 2)" OnClick="btnCT2_Click"></asp:Button>
                                    <asp:Button ID="btnCT3" CssClass="button" runat="server" Text="製三 (Chế tạo 3)" OnClick="btnCT3_Click"></asp:Button>
                                    <asp:Button ID="btnCNC" CssClass="button" runat="server" Text="CNC" OnClick="btnCNC_Click" />
                                    <asp:Button ID="btnGC" CssClass="button" runat="server" Text="加工組 (Gia công)" OnClick="btnGC_Click" />
                                    <asp:Button ID="btnNC" CssClass="button" runat="server" Text="NC" OnClick="btnNC_Click" />
                                    <asp:Button ID="btnDCX" CssClass="button" runat="server" Text="精密沖壓組 (Đột dập chính xác)" OnClick="btnDCX_Click" />
                                    <asp:Button ID="btnCT4" CssClass="button" runat="server" Text="製造後端 (Chế tạo bước cuối)" OnClick="btnCT4_Click"></asp:Button>
                                    <asp:Button ID="btnQA" CssClass="button" runat="server" Text="QA" OnClick="btnQA_Click"></asp:Button>
                                    <br />
                                </div>
                              <%--  <div id='tab2'>
                                    <asp:Button ID="btnCT2_DD" CssClass="button" runat="server" Text="單打 Dập đơn"></asp:Button><br />
                                    <asp:Button ID="btnCT2_LH" CssClass="button" runat="server" Text="連續鍛打Liên hợp"></asp:Button><br />
                                    <asp:Button ID="btnCT2_URPC" CssClass="button" runat="server" Text="退噴洗 Ủ rửa phun cát"></asp:Button>
                                </div>--%>
                                <%--  <div id='tab3'>
                                    <asp:Button ID="btnCT3" CssClass="button" runat="server" Text="製三 Chế tạo 3" OnClick="btnCT3_Click"></asp:Button><br />
                                    <asp:Button ID="btnCT3_ML" CssClass="button" runat="server" Text="鋁製 Màng loa" OnClick="btnCT3_ML_Click"></asp:Button><br />
                                    <asp:Button ID="btnCT3_DG" CssClass="button" runat="server" Text="包裝 Đóng gói" OnClick="btnCT3_DG_Click"></asp:Button><br />
                                </div>
                                 <div id='tab31'>
                                    <asp:Button ID="btnDDCX" CssClass="button" runat="server" Text="精密沖壓 DDCX" OnClick="btnDDCX_Click"></asp:Button><br />
                                 </div>

                                <div id='tab4'>
                                    <asp:Button ID="btnNC" CssClass="button" runat="server" Text="NC" OnClick="btnNC_Click"></asp:Button><br />
                                </div>

                                <div id='tab5'>
                                    <asp:Button ID="btnCNC" CssClass="button" runat="server" Text="CNC" OnClick="btnCNC_Click"></asp:Button><br />
                                </div>

                                <div id='tab6'>
                                    <asp:Button ID="btnGC" CssClass="button" runat="server" Text="加工 GC" OnClick="btnGC_Click"></asp:Button><br />
                                </div>
                                <div id='tab7'>
                                    <asp:Button ID="btnXL" CssClass="button" runat="server" Text="滾電 Xi lăn" OnClick="btnXL_Click"></asp:Button><br />
                                    <asp:Button ID="btnXT" CssClass="button" runat="server" Text="吊電 Xi treo" OnClick="btnXT_Click"></asp:Button><br />
                                    <asp:Button ID="btnXD" CssClass="button" runat="server" Text="電著 Xi đen" OnClick="btnXD_Click"></asp:Button><br />
                                </div>--%>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td class="grey">
                                <a href='#tab2'>加工</a>

                            </td>
                        </tr>--%>
                        <%--  <tr>
                            <td class="grey">
                                <a href='#tab3'>製三 CT3</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="grey">
                                <a href='#tab31'>精密沖壓 DDCX</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="grey">
                                <a href='#tab4'>NC</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="grey">
                                <a href='#tab5'>CNC</a>

                            </td>
                        </tr>
                        <tr>
                            <td class="grey">
                                <a href='#tab6'>加工 GC</a>
                            </td>

                        </tr>
                        <tr style="width: 100%">

                            <td class="grey">
                                <a href='#tab7'>電鍍 XLBM</a>

                            </td>

                        </tr>--%>
                    </tbody>
                </table>


            </td>
        </tr>

    </table>


    <%-- <div id='tab1'>
        <asp:Button ID="btnCT1" CssClass="button" runat="server" Text="製一 Chế tạo 1" OnClick="btnCT1_Click"></asp:Button><br />
    </div>
    <div id='tab2'>
        <asp:Button ID="btnCT2_DD" CssClass="button"  runat="server" Text="單打 Dập đơn" OnClick="btnCT2_DD_Click"></asp:Button><br />
         <asp:Button ID="btnCT2_LH"  CssClass="button" runat="server" Text="連續鍛打Liên hợp" OnClick="btnCT2_LH_Click"></asp:Button><br />
         <asp:Button ID="btnCT2_URPC"  CssClass="button" runat="server" Text="退噴洗 Ủ rửa phun cát" OnClick="btnCT2_URPC_Click"></asp:Button>
    </div>
    <div id='tab3'>
        <asp:Button ID="btnCT3"  CssClass="button" runat="server" Text="製三 Chế tạo 3" OnClick="btnCT3_Click"></asp:Button><br />
        <asp:Button ID="btnCT3_ML"  CssClass="button" runat="server" Text="鋁製 Màng loa" OnClick="btnCT3_ML_Click"></asp:Button><br />
        <asp:Button ID="btnCT3_DG" CssClass="button"  runat="server" Text="包裝 Đóng gói" OnClick="btnCT3_DG_Click"></asp:Button><br />
    </div>

    <div id='tab4'>
        <asp:Button ID="btnNC"  CssClass="button" runat="server" Text="NC" OnClick="btnNC_Click"></asp:Button><br />
    </div>

    <div id='tab5'>
        <asp:Button ID="btnCNC"  CssClass="button" runat="server" Text="CNC" OnClick="btnCNC_Click"></asp:Button><br />
    </div>

    <div id='tab6'>
        <asp:Button ID="btnGC"  CssClass="button" runat="server" Text="加工 GC" OnClick="btnGC_Click"></asp:Button><br />
    </div>
    <div id='tab7'>
        <asp:Button ID="btnXL"  CssClass="button" runat="server" Text="滾電 Xi lăn" OnClick="btnXL_Click"></asp:Button><br />
         <asp:Button ID="btnXT"  CssClass="button" runat="server" Text="吊電 Xi treo" OnClick="btnXT_Click"></asp:Button><br />
         <asp:Button ID="btnXD" CssClass="button"  runat="server" Text="電著 Xi đen" OnClick="btnXD_Click"></asp:Button><br />
    </div>--%>



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

        div {
            padding: 0px;
            width: 100%;
            background: #fff;
            text-align: center;
        }

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
