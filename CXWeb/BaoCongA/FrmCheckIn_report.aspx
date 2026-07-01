<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="FrmCheckIn_report.aspx.cs" Inherits="CXWeb.schedule.FrmCheckIn_report" %>
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
            max-height: 500px;
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
            
            <label for="ddlDep">Bộ phận: </label>
            <asp:DropDownList ID="ddlDep" runat="server">
                <asp:ListItem Value="CT1" Text="CT1"></asp:ListItem>
                <asp:ListItem Value="CT2" Text="CT2"></asp:ListItem>
                <asp:ListItem Value="CT3" Text="CT3"></asp:ListItem>
            </asp:DropDownList>

            <label for="txtMachineId">Mã máy: </label>
            <asp:TextBox ClientIDMode="Static" ID="txtMachineId" runat="server" Width="100px"></asp:TextBox>

            <asp:Button ID="btnView" runat="server" Text="Xem" OnClick="btnView_Click" />
            <asp:Button ID="btnExcel" runat="server" Text="Excel"   OnClick="btnExcel_Click"/> 

            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" Text=""></asp:Label>
        </div>
        <div style="width: 100%;">
            <div style="background: #006699; text-align: center;">
                <asp:Label ID="lblTiltle" runat="server" Font-Bold="true" ForeColor="White" Font-Size="13" Text="GHI CHÉP THEO BÁO CÔNG CHECK IN-OUT"></asp:Label>
            </div>
            <div class="grvContainer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvData" runat="server" CssClass="grvWLog"
                            AutoGenerateColumns="false" BackColor="#CACBE1" ShowFooter="false" Width="1300px" High="500px">
                            <HeaderStyle CssClass="grvHeader" />
                            <RowStyle Font-Names="Calibri"></RowStyle>
                            <SelectedRowStyle BackColor="#A1DCF2" />
                            <Columns>
                                <asp:BoundField DataField="Runcard_ID" HeaderText="Run Card" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="Process" HeaderText="Trạm/Process" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Machine_ID" HeaderText="Mã Máy" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="BeginDate_TT" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="Begin Date Tiptop" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="EndDate_TT" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="End Date Tiptop" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Qty_TT" HeaderText="QTY Tiptop" DataFormatString="{0:###,###.##}" HtmlEncode="false" HeaderStyle-Width="50px" ItemStyle-Width="50px" />   
                                <asp:BoundField DataField="BeginDate" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="Begin Date IoT" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="EndDate" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="End Date IoT" HeaderStyle-Width="70px" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="Different_Time" HeaderText="IoT Runtime" DataFormatString="{0:###,###.##}" HtmlEncode="false" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Qty_PLC" HeaderText="IoT PLC" DataFormatString="{0:###,###.##}" HtmlEncode="false" HeaderStyle-Width="50px" ItemStyle-Width="50px" />  
                                <asp:BoundField DataField="Different_Qty" HeaderText="Difference Qty" DataFormatString="{0:###,###.##}" HtmlEncode="false" HeaderStyle-Width="50px" ItemStyle-Width="50px" />                                                                                                                 
                                <%--<asp:BoundField DataField="Ngay_Nhap" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Date Input" HeaderStyle-Width="60px" ItemStyle-Width="60px" />--%>                                            
                                <%--<asp:BoundField DataField="Emp_ID" HeaderText="Thao tác viên" HeaderStyle-Width="70px" ItemStyle-Width="70px" />--%>
                                            
                                            
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
