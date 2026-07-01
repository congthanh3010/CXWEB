<%@Page EnableViewState="true" EnableEventValidation= "false " Title="" Language="C#"  MasterPageFile="~/Site_plc.Master" AutoEventWireup="true" CodeBehind="machine_status_upload.aspx.cs" Inherits="CXWeb.plc.machine_status_upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="/css/foundation-datepicker.css" rel="stylesheet" type="text/css"/>
    <div class="container-fluid report">
        <table style="width: auto; margin: 0 auto">
            
            <tr>
                <td class="tbTitle" Style="width: 50px;">Ngày：</td>
                <td class="tbContent">
                    <asp:TextBox ID="report_date" Style="width: 100px;" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox></td>
                <td class="tbTitle" Style="width: 70px;">Tên máy：</td>
                <td class="tbContent">
                    <asp:TextBox ID="machine_no"  Style="width: 70px;" runat="server"></asp:TextBox></td>
                <td class="tbTitle" Style="width: 180px;">Thời gian bắt đầu(hh:mm)：</td>
                <td class="tbContent">
                    <asp:TextBox ID="status_time_start"  Style="width: 50px;" runat="server"></asp:TextBox>
                </td>
                <td class="tbTitle" Style="width: 80px;">Trạng thái：</td>
                <td class="tbContent">
                    <asp:DropDownList ID="machine_status" Style="width: 250px;" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="8" style="text-align: center;">
                    <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" />
                </td>

            </tr>
            <tr>
                <td  colspan="8"  style=" font-size: 20px; color: #f00;">
                                   <%=report_msg%>                                   
                                </td>  
            </tr>
            <tr>
                <td colspan="8">
                    <asp:GridView ID="gvData" runat="server" CellPadding="4" ForeColor="#333333"
                        DataKeyNames="dMachine" GridLines="None" Width="800px" AutoGenerateColumns="False"
                        CssClass="gvDataCss" AllowSorting="True" OnRowDeleting="gvData_RowDeleting">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="dStTimeStart" HeaderText="Thời gian" />
                            <asp:BoundField DataField="dShift" HeaderText="Ca" />
                            <asp:BoundField DataField="dMachine" HeaderText="Tên máy" />
                            <asp:BoundField DataField="cDesc" HeaderText="Trạng thái" />
                            <asp:BoundField DataField="dStTime" HeaderText="Thời gian trạng thái (phút)" />

                            <%--<asp:CommandField HeaderText="操作" ShowDeleteButton="false" />--%>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                    </asp:GridView>
                </td>
            </tr>
        </table>
        <%--<input type="hidden" id="hiddenID" runat="server" />--%>
    </div>

     <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
     <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>
     <script src="/js/foundation-datepicker.js"></script>
    <script src="/js/foundation-datepicker.zh-CN.js"></script>

    <script type="text/javascript">       

        $.datetimepicker.setLocale('en');
       

        $('#report_date').fdatepicker({
            format: 'yyyy-mm-dd',
        });      
       

    </script>
</asp:Content>
