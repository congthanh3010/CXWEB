<%@ Page Title="Báo cáo Trạm Điện nhiệt" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="Station_Temperature_Report.aspx.cs" Inherits="CXWeb.schedule.Station_Temperature_Report" EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/dashboard.css" rel="stylesheet" />
    <link href="../css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="../css/foundation-datepicker.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../js/jquery.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery.datetimepicker.full.js"></script>
    <script src="../js/foundation-datepicker.min.js"></script>
    <script src="signalr/hubs"></script>
     <style type="text/css">
        .work-log-input-col-1 {
            text-align: right;
            padding: 2px;
            padding-right: 5px;
        }

        .work-log-input-col-2 {
            text-align: left;
            padding: 2px;
        }

        .work-log-input-col-2 input {
            width: 100%;
        }

        /* Chrome, Safari, Edge, Opera */
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        input[type=number] {
            -moz-appearance: textfield;
            height: 28px;
        }

        input[type=text] {
            height: 28px;
        }

        select {
            height: 28px;
        }

        .grvContainer {
            width: 100%;
            height: 650px;
            overflow: auto;
            position: relative;
            z-index: 2;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="container">
          <table class="table table-bordered table-striped" style="width: 100%; padding: 0; border: 0; font-size: large;">
              <tr>
                  <td style="padding: 0;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <Triggers>
                                    <asp:PostBackTrigger ControlID="btnLoad" />
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                         </Triggers>
                        <ContentTemplate>
                            <div id="frmTitle" style="background: #006699; text-align: center;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="BÁO CÁO THEO DÕI DỮ LIỆU TRẠM ĐIỆN"></asp:Label>
                            </div>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                 <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>                                    
                                        <td class="work-log-input-col-1">Ngày Bắt đầu:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="report_date" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>
                                        <td class="work-log-input-col-1">Ngày kết thúc:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="report_enddate" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>
                                         <td class="work-log-input-col-1">Trạm 變電站</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="ddlQua_Trinh" runat="server" >
                                                <asp:ListItem Value="T0" Text="Tất cả"></asp:ListItem>
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
                                         <td colspan="3" style="text-align: center; padding: 5px;">
                                            <div class="btn-group">
                                                <asp:Button ID="btnLoad" runat="server" Text="Tra Cứu" Font-Bold="true" CssClass="btn btn-success" OnClick="btnLoad_Click" />
                                                <asp:Button ID="btnExcel" runat="server" Text="Xuất Excel" Font-Bold="true" CssClass="btn btn-success" OnClick="btnExcel_Click" />
                                            </div>
                                        </td>
                                     </tr>
                                     </table>
                                <div style="width: 100%;">
                                <div id="frmCaption" style="background: #006699;">
                                    <asp:Label ID="lblBody" runat="server" Font-Bold="true" ForeColor="White" Text="DỮ LIỆU OUTPUT"></asp:Label>
                                </div>
                                <div id="frmList" class="grvContainer">
                                    <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog"
                                        AutoGenerateColumns="false" BackColor="#CACBE1" ShowFooter="false" Width="1300px" 
                                         OnRowDataBound="grvWLog_RowDataBound">
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />

                                        <Columns>
                                            <asp:BoundField DataField="Stt_ID" DataFormatString="{0:0}"  ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="Station_No"  ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="Ngay_Kiem_tra_2"  DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="100px" />

                                            <asp:BoundField DataField="TD_Bien_The" DataFormatString="{0:0}" ItemStyle-Width="80px" />
                                            <asp:BoundField DataField="TD_Dao_Cat" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_CB"  DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_VT_DD"  ItemStyle-Width="120px" />
                                            <asp:BoundField DataField="TD_Cuong_Do" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_QC"  ItemStyle-Width="100px" />
                                            <asp:BoundField DataField="TD_Nhiet_Do"  DataFormatString="{0:0}" ItemStyle-Width="85px" />

                                            <asp:BoundField DataField="TP_Tu_PP"  ItemStyle-Width="110px" />
                                            <asp:BoundField DataField="Tp_Lap_Dat"  ItemStyle-Width="150px" />
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

                            </div>


                        </ContentTemplate>
                        
                    </asp:UpdatePanel>
                  
                 </td>
                                
              </tr>

          </table>

     </div>


    <script type="text/javascript">
        $('#report_date').fdatepicker({ format: 'yyyy-mm-dd' });
        $('#report_enddate').fdatepicker({ format: 'yyyy-mm-dd' });
        
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#report_date').fdatepicker({ format: 'yyyy-mm-dd' });
            $
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#report_enddate').fdatepicker({ format: 'yyyy-mm-dd' });
            $
        });
    </script>

</asp:Content>