<%@ Page Title="Ghi Chép Số Liệu Trạm Điện"Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="Station_Temperature.aspx.cs" Inherits="CXWeb.schedule.Station_Temperature" EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/dashboard.css" rel="stylesheet" />
    <link href="../css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="../css/foundation-datepicker.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.datetimepicker.full.js"></script>
    <script src="../js/foundation-datepicker.min.js"></script>

    <style type="text/css">
        .work-log-input-col-1 {
            text-align: right;
            padding-right: 5px;
            width: 100px;
        }

        .work-log-input-col-2 {
            text-align: left;
            padding: 2px 2px 2px 0px;
            width: 300px;
        }

        .work-log-input-col-2 input {
            margin: 0;
        }

        /* Chrome, Safari, Edge, Opera */
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        input[type=text] {
            height: 28px;
        }

        select {
            height: 28px;
            margin: 0;
        }

        .grvContainer {
            width: 100%;
            height: 550px;
            overflow: auto;
            position: relative;
            z-index: 2;
        }

        @media (max-height: 800px) {
            .grvContainer {
                height: 450px;
            }
        }

        .grvWLog {
            position: absolute;
        }

        .grvHeader {
            background-color: #006699;
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: Cambria;
        }

    </style>

    <asp:scriptmanager runat="server"></asp:scriptmanager>
    <div class="container">
        <table class="table table-bordered table-striped" style="width: 100%; padding: 0; border: 0; font-size: large;">
            <tr>
                <td style="padding: 0;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>
                                        <td class="work-log-input-col-1">Người tải lên:</td>
                                        <td style="width: 50px;">
                                            <asp:DropDownList ID="ddlCode" runat="server" Width="100%">
                                                <asp:ListItem Value="VN" Text="VN"></asp:ListItem>
                                                <asp:ListItem Value="TV" Text="TV"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtUser" runat="server" BackColor="#00ff99" Width="100px"></asp:TextBox>
                                        </td>
                                        <%--<td></td>--%>
                                        <td class="work-log-input-col-1">Trạm 變電站</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="ddlQua_Trinh" runat="server" >
                                                <asp:ListItem Value="T1" Text="TRẠM 1 第一變電站"></asp:ListItem>
                                                <asp:ListItem Value="T2" Text="TRẠM 2 第2變電站 1500KVA "></asp:ListItem>
                                                <asp:ListItem Value="T3" Text="TRẠM 3 第3變電站 1250KVA "></asp:ListItem>
                                                <asp:ListItem Value="T4" Text="TRẠM 4 第四變電站 1250KVA"></asp:ListItem>
                                                <asp:ListItem Value="T5" Text="TRẠM 5 第五變電站"></asp:ListItem>
                                                <asp:ListItem Value="T6" Text="TRẠM 6 第六變電站 "></asp:ListItem>
                                                <asp:ListItem Value="T7" Text="TRẠM 7 第七變電站"></asp:ListItem>                                                                                             
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">File tải lên:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" CssClass="form-group" style="float: left;" Width="280px" />
                                            <asp:Button ID="btnLoad" runat="server" Text="Tải lên" style="float: left;" Width="80px" OnClick="btnLoad_Click"/>
                                        </td>
                                        <td class="work-log-input-col-1">Ngày Kiểm tra:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="report_date" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Chọn sheet dữ liệu:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:DropDownList ID="ddlSheet" runat="server" Width="360px" >
                                            </asp:DropDownList>
                                        </td>
                                        <td class="work-log-input-col-1">Cos Fi:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtCos_Int" Style="width: 120px;font-size: 20px;text-align:right; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" text ="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center; padding: 5px;">
                                            <div class="btn-group">
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Font-Bold="true" CssClass="btn btn-success" OnClick="btnUpload_Click"  />
                                                <asp:Button ID="btnReoort" runat="server" Text="Báo cáo" Font-Bold="true" CssClass="btn btn-success"  OnClientClick="window.open('Station_Temperature_Report.aspx','_newtab'); return false" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center;">
                                            <asp:Label ClientIDMode="Static" ID="lblMessage" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>


                            </div>
                            <div style="width: 100%;">
                                <div id="frmCaption" style="background: #006699;">
                                    <asp:Label ID="lblBody" runat="server" Font-Bold="true" ForeColor="White" Text="DỮ LIỆU ĐÃ TẢI LÊN"></asp:Label>
                                </div>
                                <div id="frmList" class="grvContainer">
                                    <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog" AutoGenerateColumns="false" BackColor="#CACBE1" 
                                        ShowFooter="false" Width="100%" OnRowDataBound="grvWLog_RowDataBound" ShowHeader="false" >
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />
                                        <Columns>
                                            <asp:BoundField DataField="Stt_ID" DataFormatString="{0:0}"  ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="Station_No"  ItemStyle-Width="50px" />

                                            <asp:BoundField DataField="TD_Bien_The" DataFormatString="{0:0}" ItemStyle-Width="80px" />
                                            <asp:BoundField DataField="TD_Dao_Cat" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_CB"  DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_VT_DD"  ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_Cuong_Do"  DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_QC"  ItemStyle-Width="100px" />
                                            <asp:BoundField DataField="TD_Nhiet_Do"  DataFormatString="{0:0}" ItemStyle-Width="85px" />

                                            <asp:BoundField DataField="TP_Tu_PP"  ItemStyle-Width="110px" />
                                            <asp:BoundField DataField="Tp_Lap_Dat"  ItemStyle-Width="110px" />
                                            <asp:BoundField DataField="TP_Cuong_Do" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TP_Cuong_Do_TT" DataFormatString="{0:0}"  ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TP_Nhiet_Do" DataFormatString="{0:0}"  ItemStyle-Width="85px" />

                                            <asp:BoundField DataField="TB_MSM"  ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TB_Lap_Dat"  ItemStyle-Width="150px" />
                                            <asp:BoundField DataField="TB_Nhiet_Do" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TB_CB" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TB_AP_MAX" DataFormatString="{0:0}"  ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TB_Cuong_Do_TT" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TB_Ghi_Chu"  ItemStyle-Width="85px" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                            <asp:PostBackTrigger ControlID="btnLoad" />
                        </Triggers>
                        
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
      <script type="text/javascript">

        $.datetimepicker.setLocale('en');

        $('#report_date').fdatepicker({
            format: 'yyyy-mm-dd',
            width: 100
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
   </asp:Content>
