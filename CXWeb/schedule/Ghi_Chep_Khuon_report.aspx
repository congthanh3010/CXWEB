<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="Ghi_Chep_Khuon_report.aspx.cs" Inherits="CXWeb.schedule.Ghi_Chep_Khuon_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style2.css" rel="stylesheet" />
    <link href="../css/dashboard.css" rel="stylesheet" />
    <link href="../css/foundation-datepicker.min.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/foundation-datepicker.min.js"></script>

    <style type="text/css">
        /* Style the form - display items horizontally */
        .form-inline {
            display: flex;
            flex-flow: row wrap;
            align-items: center;
        }

        /* Add some margins for each label */
        .form-inline label {
            margin: 10px 10px 10px 0;
        }

        /* Style the input fields */
        .form-inline input,select {
            margin: 10px 10px 10px 0;
            height: 26px;
        }

        /* Add responsiveness - display the form controls vertically instead of horizontally on screens that are less than 800px wide */
        @media (max-width: 800px) {
            .form-inline input {
                margin: 10px 0;
            }

            .form-inline {
                flex-direction: column;
                align-items: stretch;
            }
        }

        .grvContainer {
            width: 100%;
            max-height: 1200px;
            overflow: auto;
            position: relative;
            z-index: 2;
        }

        @media (max-height: 800px) {
            .grvContainer {
                max-height: 650px;
            }
        }

        .grvData {
            position: relative;
            background-color: #CACBE1;
            width: 1700px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container" id="frmContent">
        <div class="form-inline">
            <label for="txtDFrom">Từ ngày: </label>
            <asp:TextBox ClientIDMode="Static" ID="txtDFrom" runat="server" Width="80px"></asp:TextBox>
            <label for="txtDTo">đến ngày: </label>
            <asp:TextBox ClientIDMode="Static" ID="txtDTo" runat="server" Width="80px"></asp:TextBox>

            <label for="ddlMachine">Chủng Loại: </label>
            <asp:TextBox ClientIDMode="Static" ID="txtChung_Loai" runat="server" Width="100px"></asp:TextBox>

            <asp:Button ID="btnView" runat="server" Text="Xem" OnClick="btnView_Click" />
            <asp:Button ID="btnExcel" runat="server" Text="Excel"   OnClick="btnExcel_Click"/> 

            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" Text=""></asp:Label>
        </div>
        <div style="width: 100%;">
            <div style="background: #006699; text-align: center;">
                <asp:Label ID="lblTiltle" runat="server" Font-Bold="true" ForeColor="White" Font-Size="13" Text="GHI CHÉP THEO DÕI LÃNH DÙNG KHUÔN"></asp:Label>
            </div>
            <div class="grvContainer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvData" CssClass="grvData" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                            RowStyle-Font-Names="Calibri" OnDataBound="grvData_DataBound">
                            <HeaderStyle BackColor="#006699" ForeColor="White" Font-Bold="true" Font-Names="Cambria" />
                            <Columns>
                                <asp:BoundField DataField="Ngay_Kiem" HeaderText="Ngày" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="40px" ItemStyle-Width="40px" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="So_Kiem_Tra" HeaderText="Số Kiểm Tra" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="Thanh_Tra" HeaderText="Thanh tra 檢驗人員" HeaderStyle-Width="75px" ItemStyle-Width="75px" ItemStyle-VerticalAlign="Top" />
                                <asp:BoundField DataField="Qua_trinh" HeaderText="Quá trình 工序" HeaderStyle-Width="75px" ItemStyle-Width="75px" />
                                <asp:BoundField DataField="So_Luong_Kiem_Tra" HeaderText="Số lượng kiểm tra 檢驗數量" DataFormatString="{0:0.00}" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Kich_Thuoc_NG" HeaderText="KÍCH THƯỚC NG 尺寸NG數量 " DataFormatString="{0:0.00}" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Chung_Loai" HeaderText="Chủng loại 機種" HeaderStyle-Width="70px" ItemStyle-Width="70px" />                                
                                <asp:BoundField DataField="Kich_Thuoc_Ok" HeaderText="KÍCH THƯỚC OK 尺寸OK數量 " DataFormatString="{0:0.00}" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Loai_Khuon" HeaderText="Loại khuôn 模具類別" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#txtDFrom').fdatepicker({ format: 'yyyy-mm-dd' });
        $('#txtDTo').fdatepicker({ format: 'yyyy-mm-dd' });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            $('#txtDFrom').fdatepicker({ format: 'yyyy-mm-dd' });
            $('#txtDTo').fdatepicker({ format: 'yyyy-mm-dd' });
        });
    </script>
</asp:Content>
