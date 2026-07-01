<%@ Page Title="Ghi Chép Khuôn" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="Ghi_Chep_Khuon.aspx.cs" Inherits="CXWeb.schedule.Ghi_Chep_Khuon" EnableEventValidation="false" %>

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

    <div class="container">
          <table >
              <tr>
                  <td style="padding: 0;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnGetId" />
                         </Triggers>
                        <ContentTemplate>
                            <div id="frmTitle" style="background: #006699; text-align: center;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="GHI CHÉP DO KIỂM LÃNH KHUÔN"></asp:Label>
                            </div>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                 <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>
                                        <td class="work-log-input-col-1">Số kiểm tra 檢測單號</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtSo_Kiem_Tra" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">NV Kiểm nghiệm 檢驗人員</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtThanh_Tra" runat="server"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Công Đoạn 工序</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="ddlQua_Trinh" runat="server" Width= 100%>
                                                <asp:ListItem Value="P00" Text=""></asp:ListItem>
                                                <asp:ListItem Value="P10" Text="P10 XuỐNG PHÔI"></asp:ListItem>
                                                <asp:ListItem Value="P30" Text="P30 DẬP ĐiỂM"></asp:ListItem>
                                                <asp:ListItem Value="P31" Text="P31 ĐỘT LỖ"></asp:ListItem>
                                                <asp:ListItem Value="P33" Text="P33 CẮT BIÊN"></asp:ListItem>
                                                <asp:ListItem Value="P35" Text="P35 CHỈNH PHẲNG"></asp:ListItem>
                                                <asp:ListItem Value="T10" Text="T10 DẬP NẤM"></asp:ListItem>
                                                <asp:ListItem Value="T22" Text="T22 DẬP NẤM PFM"></asp:ListItem>
                                                <asp:ListItem Value="T11" Text="T11 CHỈNH HÌNH 1"></asp:ListItem>
                                                <asp:ListItem Value="T12" Text="T12 CHỈNH HÌNH 2"></asp:ListItem>
                                                <asp:ListItem Value="T13" Text="T13 CHỈNH HÌNH 3"></asp:ListItem>
                                                <asp:ListItem Value="T14" Text="T14 CHỈNH HÌNH 4"></asp:ListItem>
                                                <asp:ListItem Value="T18" Text="T18 LIÊN HỢP CHỈNH HÌNH"></asp:ListItem>
                                                <asp:ListItem Value="T20" Text="T20 THÀNH HÌNH"></asp:ListItem>
                                                <asp:ListItem Value="T21" Text="T21 LIÊN HỢP THÀNH  HÌNH"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Ngày kiểm 檢測日期</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtNgay_Kiem" runat="server" ClientIDMode="Static" Width="130px" BackColor="#00ff99" OnTextChanged="txtNgay_Kiem_TextChanged" OnDataBinding="txtNgay_Kiem_DataBinding" ></asp:TextBox>
                                            <asp:Button ClientIDMode="Static" ID="btnGetId" runat="server" Text="Lấy mã"
                                                    CssClass="btn btn-default" Font-Bold="true" Width="60px" OnClick="btnGetId_Click" />
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Số lượng kiểm tra 檢驗數量</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtSo_Luong_Kiem_Tra" runat="server" ClientIDMode="Static" Width="150px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Kích thước NG 尺寸NG數量  </td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtKich_Thuoc_NG" runat="server" ClientIDMode="Static" Width="150px" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                     <tr>
                                        <td class="work-log-input-col-1">Chủng loại 機種</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtChung_Loai" runat="server" ClientIDMode="Static" Width="150px" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                         <td class="work-log-input-col-1">Loại khuôn 模具類別</td>
                                        <td class="work-log-input-col-2">
                                            <%--<asp:TextBox ID="txtLoai_Khuon" runat="server" ClientIDMode="Static"  Width="100%" TextMode="MultiLine" Height="60px"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlLoai_Khuon" runat="server" Width= 100%>
                                                <asp:ListItem Value="K0" Text=""></asp:ListItem>
                                                <asp:ListItem Value="K1" Text="K1 - Khuôn Trên"></asp:ListItem>
                                                <asp:ListItem Value="K2" Text="K2 - Khuôn Dưới"></asp:ListItem>
                                                <asp:ListItem Value="K3" Text="K3 - Khuôn Giữa"></asp:ListItem>
                                                <asp:ListItem Value="K4" Text="K4 - Tìn Chu"></asp:ListItem>
                                                <asp:ListItem Value="K5" Text="K5 - Nguyên Bộ"></asp:ListItem>
                                                                                               
                                            </asp:DropDownList>
                                        </td>
                                        <%--<td class="work-log-input-col-1">M.So khuon 模套編號</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtMa_So_Khuon" runat="server" ClientIDMode="Static" Width="150px" ></asp:TextBox>
                                        </td>--%>
                                        <td></td>
                                        <td class="work-log-input-col-1">Kích thước OK 尺寸OK數量 </td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtKich_Thuoc_OK" runat="server" ClientIDMode="Static" Width="150px" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                     </tr>
                                     <tr>
                                        <td></td>
                                         <td></td>
                                        <td></td>
                                        <%--<td class="work-log-input-col-1">Sử dụng đơn vị 使用單位</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtSu_Dung_Don_Vi" runat="server" ClientIDMode="Static" Width="150px" ></asp:TextBox>
                                        </td>--%>
                                         <td></td>
                                         <td></td>
                                        <td></td>
                                          

                                     </tr>
                                     <tr>
                                         <td class="work-log-input-col-1">Hình ảnh 圖片</td>
                                        <td class="work-log-input-col-2">
                                            <%--<asp:FileUpload ID="FileUpload1" runat="server" Width="100%" Height="27px" />--%>
                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" Height="27px" />
                                            <asp:Label ClientIDMode="Static" ID="lblHinh_Anh" runat="server" Text=""></asp:Label>                                     
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Thực tế đo 實際量測</td>
                                        <td class="work-log-input-col-2">
                                            <asp:FileUpload ID="File_Thuc_Te" runat="server" Width="100%" Height="27px" />
                                            <asp:Label ClientIDMode="Static" ID="lblThuc_Te" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td></td>
                                     </tr>
                                      <tr>
                                        <td colspan="9" style="text-align: center; padding: 5px;">
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnInsert" runat="server" Text="Thêm"
                                                    CssClass="btn btn-default" Font-Bold="true" Width="60px" OnClick="btnInsert_Click"/>
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnModify" runat="server" Text="Sửa"
                                                    CssClass="btn btn-default" Font-Bold="true" Width="60px" OnClick="btnModify_Click" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnDelete" runat="server" Text="Xóa"
                                                    CssClass="btn btn-default" Font-Bold="true" Width="60px" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có muốn xóa ghi chép này?');"  />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ID="btnSave" runat="server" Text="Lưu"
                                                    CssClass="btn btn-default" Font-Bold="true" ForeColor="Black" Width="60px" OnClick="btnSave_Click" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnCancel" runat="server" Text="Hủy"
                                                    CssClass="btn btn-danger" Font-Bold="true" ForeColor="Black" Width="60px" OnClick="btnCancel_Click" />
                                            </div>
                                            <div class="btn-group">
                                                <asp:Button ClientIDMode="Static" ID="btnReport" runat="server" Text="Báo cáo"
                                                    CssClass="btn btn-primary" Font-Bold="true" ForeColor="Black" Width="70px" OnClientClick="window.open('Ghi_Chep_Khuon_report.aspx','_newtab'); return false" />
                                            </div>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td colspan="9" style="text-align: center;">
                                            <asp:Label ClientIDMode="Static" ID="lblMessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div style="width: 100%;">
                                <div id="frmCaption" style="background: #006699;">
                                    <asp:Label ID="lblBody" runat="server" Font-Bold="true" ForeColor="White" Text="GHI CHÉP ĐÃ NHẬP TRONG NGÀY"></asp:Label>
                                </div>
                                <div id="frmList" class="grvContainer">
                                    <asp:GridView ID="grvWLog" runat="server" CssClass="grvWLog"
                                        AutoGenerateColumns="false" BackColor="#CACBE1" ShowFooter="false" Width="1300px" OnRowUpdating="grvWLog_RowUpdating"
                                        OnSelectedIndexChanged="grvWLog_SelectedIndexChanged" OnRowDataBound="grvWLog_RowDataBound"
                                       DataKeyNames="So_Kiem_Tra">
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle Font-Names="Calibri"></RowStyle>
                                        <SelectedRowStyle BackColor="#A1DCF2" />

                                        <Columns>
                                            <asp:BoundField DataField="Ngay_Kiem" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Ngày" HeaderStyle-Width="60px" ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="So_Kiem_Tra" HeaderText="Số kiểm tra 檢測單號" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="Thanh_Tra" HeaderText="Thanh tra 檢驗人員" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="Qua_Trinh" HeaderText="Quá trình 工序" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="So_Luong_Kiem_Tra" DataFormatString="{0:0.00}"  HeaderText="Số lượng kiểm tra 檢驗數量" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="Kich_Thuoc_NG" DataFormatString="{0:0.00}" HeaderText="KÍCH THƯỚC NG 尺寸NG數量 " HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:BoundField DataField="Chung_Loai" HeaderText="Chủng loại 機種" HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            
                                            <asp:BoundField DataField="Kich_Thuoc_Ok" DataFormatString="{0:0.00}" HeaderText="KÍCH THƯỚC OK 尺寸OK數量 " HeaderStyle-Width="85px" ItemStyle-Width="85px" />
                                            <asp:TemplateField HeaderText="Hình Ảnh" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Image ID="ImageProb" runat="server" ImageUrl='<%# Eval("Hinh_Anh") %>'  Width="150px"/>
                                                <asp:HyperLink ID="HyperLink1"  Target="_blank"  runat="server" NavigateUrl='<%# Eval("Hinh_Anh") %>'
                                                    Text='<%# Eval("Ten_Hinh") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Thực Tế" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Image ID="ImageProb" runat="server" ImageUrl='<%# Eval("Thuc_Te") %>'  Width="150px"/>
                                                <asp:HyperLink ID="HyperLink1"  Target="_blank"  runat="server" NavigateUrl='<%# Eval("Thuc_Te") %>'
                                                    Text='<%# Eval("Ten_File") %>'></asp:HyperLink>
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
   

    <script type="text/javascript">
        

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#txtNgay_Kiem').datetimepicker({ format: 'Y-m-d' });
            
        });
    </script>

</asp:Content>


