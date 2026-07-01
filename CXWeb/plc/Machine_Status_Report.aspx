<%@ Page Title="Thống kê tình trạng Máy" Language="C#" MasterPageFile="~/Site_plc.Master" AutoEventWireup="true" CodeBehind="Machine_Status_Report.aspx.cs" Inherits="CXWeb.plc.Machine_Status_Report" EnableEventValidation="false"%>

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
            width: 50px;
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
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="THỐNG KÊ THEO DÕI TÌNH TRẠNG MÁY"></asp:Label>
                            </div>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                 <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>                                    
                                        <td class="work-log-input-col-1">Ngày BD:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="report_date" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>
                                        <td class="work-log-input-col-1">Ngày KT:</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="report_dateEnd" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>

                                         <td class="work-log-input-col-1">Trạm</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="txtTram" Style="font-size: 20px;" runat="server" Width="110px" AutoPostBack="true" OnSelectedIndexChanged="txtTram_SelectedIndexChanged"  ></asp:DropDownList>
                                        </td>

                                        <td class="work-log-input-col-1">Máy</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="txtMachine_no" Style="font-size: 20px;" runat="server" Width="110px"></asp:DropDownList>
                                        </td>
                                        
                                    
                                    </tr>
                                     <tr>
                                         <td colspan="3" style="text-align: center; padding: 5px;">
                                            <div class="btn-group">
                                                <asp:Button ID="btnLoad" runat="server" Text="Tra Cứu" Font-Bold="true" CssClass="btn btn-success"  />
                                                <asp:Button ID="btnExcel" runat="server" Text="Xuất Excel" Font-Bold="true" CssClass="btn btn-success" />
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
                                         >
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />

                                        <Columns>
                                            <asp:BoundField DataField="Stt_ID" DataFormatString="{0:0}"  ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="Station_No"  ItemStyle-Width="50px" />

                                            <asp:BoundField DataField="TD_Bien_The" DataFormatString="{0:0}" ItemStyle-Width="80px" />
                                            <asp:BoundField DataField="TD_Dao_Cat" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_CB"  DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_VT_DD"  ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_Cuong_Do"  ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TD_Nhiet_Do"  DataFormatString="{0:0}" ItemStyle-Width="85px" />

                                            <asp:BoundField DataField="TP_Tu_PP"  ItemStyle-Width="110px" />
                                            <asp:BoundField DataField="TP_Cuong_Do" DataFormatString="{0:0}" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TP_Cuong_Do_TT" DataFormatString="{0:0}"  ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="TP_Nhiet_Do" DataFormatString="{0:0}"  ItemStyle-Width="85px" />

                                            <asp:BoundField DataField="TB_MSM"  ItemStyle-Width="85px" />
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
        $('#report_date').datetimepicker({ format: 'd/m/Y H:i' });
        $('#report_dateEnd').datetimepicker({ format: 'd/m/Y H:i' });



    </script>




</asp:Content>