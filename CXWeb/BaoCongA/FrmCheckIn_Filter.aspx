<%@ Page Title="Báo công CheckIn" Language="C#"  MasterPageFile="~/Site_schedule.Master"  AutoEventWireup="true" CodeBehind="FrmCheckIn_Filter.aspx.cs" Inherits="CXWeb.BaoCong.FrmCheckIn_Filter" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
     <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/dashboard.css" rel="stylesheet" />
    <link href="../css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="../css/foundation-datepicker.min.css" rel="stylesheet" />

    <script src="../js/html5-qrcode.min.js"></script>
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.datetimepicker.full.js"></script>
    <script src="../js/foundation-datepicker.min.js"></script>
    
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 600px;
            height: 640px;
        }
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
            height: 350px;
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
    <div class="row">
    <div class="col-sm" style="align-content:flex-start">
     <div class="container">         
        <table class="table table-bordered table-striped" style="width: 100%; padding: 0; border: 0; font-size: large;">
            <tr>
                <td style="padding: 0;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="frmTitle" style="background: #006699; text-align: center;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="Báo Công CheckIn"></asp:Label>
                            </div>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 22px;">
                                   <tr>
                                   <td class="work-log-input-col-1">1.Run Card</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtRunCard" runat="server"  style="text-transform:uppercase" ></asp:TextBox>
                                        </td>                         
                                                                
                                   </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">2.Process/Công đoạn</td>
                                        <td class="work-log-input-col-2" colspan="2">
                                            <asp:TextBox ID="txtProcess" runat="server"   style="text-transform:uppercase" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <tr>
                                            <td class="work-log-input-col-1">3.Emp ID</td>
                                            <td style="width: 80px;">
                                                <asp:DropDownList ID="ddlCode" runat="server" Width="100%">
                                                    <asp:ListItem Text="VN" Value="VN"></asp:ListItem>
                                                    <asp:ListItem Text="TV" Value="TV"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="work-log-input-col-2">
                                                <asp:TextBox ID="txtEmployee" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="work-log-input-col-1">4.Machine ID/Mã máy</td>
                                            <td class="work-log-input-col-2" colspan="2">
                                                <asp:TextBox ID="txtMachineId" runat="server" ClientIDMode="Static" style="text-transform:uppercase"></asp:TextBox>
                                                 
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="work-log-input-col-1">5.BeginDate</td>
                                            <td style="width: 80px;">
                                                <asp:TextBox ID="txtBeginDate" runat="server" ClientIDMode="Static" placeholder="Date/Ngày"></asp:TextBox>
                                            </td>
                                            <td class="work-log-input-col-2">
                                                <asp:TextBox ID="txtBeginGio" runat="server" BackColor="#00ff99" ClientIDMode="Static" placeholder="giờ:phút"></asp:TextBox>
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                             <td class="work-log-input-col-1">6.EndDate</td>
                                            <td style="width: 80px;">
                                                <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" placeholder="Date/Ngày"></asp:TextBox>
                                            </td>
                                            <td class="work-log-input-col-2">
                                                <asp:TextBox ID="txtEndGio" runat="server" BackColor="#00ff99" ClientIDMode="Static" placeholder="giờ:phút"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" style="text-align: center; padding: 5px;" font-size: 22px;>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnInsert" runat="server" ClientIDMode="Static" CssClass="btn btn-default" Font-Bold="true" OnClick="btnInsert_Click" Text="Thêm" Width="60px" />
                                                </div>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnModify" runat="server" ClientIDMode="Static" CssClass="btn btn-default" Font-Bold="true" OnClick="btnModify_Click" Text="Sửa" Width="60px" />
                                                </div>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnDelete" runat="server" ClientIDMode="Static" CssClass="btn btn-default" Font-Bold="true" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có muốn xóa ghi chép này?');" Text="Xóa" Width="60px" />
                                                </div>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" CssClass="btn btn-success" Font-Bold="true" ForeColor="Black" OnClick="btnSave_Click" Text="Lưu" Width="60px" />
                                                </div>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnCancel" runat="server" ClientIDMode="Static" CssClass="btn btn-danger" Font-Bold="true" ForeColor="Black" OnClick="btnCancel_Click" Text="Hủy" Width="60px" />
                                                </div>
                                                <div class="btn-group">
                                                    <asp:Button ID="btnReport" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Font-Bold="true" ForeColor="Black" OnClick="btnReport_Click" OnClientClick="window.open('FrmCheckIn_report.aspx','_newtab'); return false" Text="Báo cáo" Width="70px" />
                                                </div>
                                                <asp:HiddenField ID="state" runat="server" ClientIDMode="Static" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" style="text-align: center;">
                                                <asp:Label ID="lblMessage" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  style="width: 500px;">
                                            
                                           

                                        </div>
                                           
                                        </td>   
                                        </tr>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 100%;">
                                <div id="frmCaption" style="background: #006699;">
                                    <asp:Label ID="lblBody" runat="server" Font-Bold="true" ForeColor="White" Text="GHI CHÉP ĐÃ NHẬP TRONG NGÀY"></asp:Label>
                                </div>
                                <div id="frmList" class="grvContainer" >
                                    <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog" HorizontalAlign ="Center"
                                        AutoGenerateColumns="false" BackColor="#CACBE1" ShowFooter="false" Width="1300px"
                                        OnRowDataBound="grvWLog_RowDataBound" OnSelectedIndexChanged="grvWLog_SelectedIndexChanged" DataKeyNames="Ident00">
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />
                                        <Columns>
                                            <asp:BoundField DataField="BeginDate" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="Begin Date" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                            <asp:BoundField DataField="EndDate" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="End Date" HeaderStyle-Width="70px" ItemStyle-Width="70px" /> 
                                            <asp:BoundField DataField="Machine_ID" HeaderText="Mã Máy" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="Rundcard_ID"  HeaderText="Run Card" HeaderStyle-Width="70px" ItemStyle-Width="70px" />                                            
                                            <asp:BoundField DataField="Emp_ID" HeaderText="Thao tác viên" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                            <asp:BoundField DataField="Process" HeaderText="Công Đoạn" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                                                                                                                             
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:PostBackTrigger ControlID="btnShow" />--%>
                            <%--<asp:PostBackTrigger ControlID="Panel1" />--%>

                        </Triggers>
                        
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
       </div>
    </div>
   </div>
    
    <script type="text/javascript">
        $.datetimepicker.setLocale('en');
             
        $('#txtBeginDate').fdatepicker({ format: 'yyyy-mm-dd' });
        $('#txtEndDate').fdatepicker({ format: 'yyyy-mm-dd' });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#txtBeginDate').fdatepicker({ format: 'yyyy-mm-dd' });
            $('#txtEndDate').fdatepicker({ format: 'yyyy-mm-dd' });
        });
       
     
        
    </script>
  
   
</asp:Content>