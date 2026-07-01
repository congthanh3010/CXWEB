<%@ Page Title="" Language="C#" MasterPageFile="~/Site_checklist.Master" AutoEventWireup="true" CodeBehind="checklist_machine_selection.aspx.cs" Inherits="CXWeb.checklist.checklist_machine_selection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: absolute; width: 100%; height: 90%;">
         <asp:HiddenField ClientIDMode="Static" ID="hsub_unit" runat="server" />
        <%--<table style="width:100%; height:100%;">--%>
        <table class="table-bordered table-striped" style="width: 100%; font-size: large; padding: 0px; border: 0px;">     
            <tr style="height:26px;">
                <td style="text-align:left">
                    <asp:Button ID="btnBack"  style="font-size:large;"  runat="server" Text="返回上頁 Quay về trước" OnClick="btnBack_Click" Font-Bold="true"    ></asp:Button>
                </td>
            </tr>
            <tr>
                <td style="padding: 0px;text-align: left;">點檢日期 Ngày điểm kiểm
                    <asp:TextBox ID="textDate" AutoPostBack="true" ClientIDMode="Static" AutoCompleteType="Disabled" Style="margin-right: 15px;" runat="server" CssClass="some_class" OnTextChanged="textDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
             <%--<tr>
                <td style="padding: 0px;text-align: left;">
                    <asp:Button ID="btnXem" runat="server" Style="font-size: large;" Text="查詢 Tìm kiếm" OnClick="btnXem_Click" Font-Bold="true"></asp:Button>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <div style="position: absolute;  width: 100%; height:85%; overflow:auto;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                        OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
                        ShowFooter="false" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px"/>
                        <RowStyle Font-Names="Calibri" Font-Size="18px" />
                        <Columns>
                            <asp:TemplateField HeaderText="群組<br/>Nhóm máy" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblcol01" runat="server" Text='<%# Eval("machine_no") %>'></asp:Label>--%>
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("link2") %>'
                                        Text='<%# Eval("machine_group") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="機台<br/>Tên máy" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblcol01" runat="server" Text='<%# Eval("machine_no") %>'></asp:Label>--%>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("link") %>'
                                        Text='<%# Eval("machine_no") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="機台異常</br>Máy có vấn đề" HeaderStyle-Width="100px">    
                                                       
                                <ItemTemplate>
                                    <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("count_prob") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="早班操機人<br/>MSNV CA1" HeaderStyle-Width="100px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtcol02" Width="100%" runat="server" Text='<%# Eval("ca1_msnv") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("ca1_msnv") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="晚班操機人<br/>MSNV CA2" HeaderStyle-Width="100px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtcol03" Width="100%" runat="server" Text='<%# Eval("ca2_msnv") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("ca2_msnv") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="sum_point" Visible="true" HeaderText="sum_point" HeaderStyle-Width="0px" />--%>
                         <%--   <asp:TemplateField HeaderText="sum_point" HeaderStyle-Width="100px">                               
                                <ItemTemplate>
                                    <asp:Label ID="lblsum_point" runat="server" Text='<%# Eval("sum_point") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                          </div>
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

        $('#textDate').fdatepicker({
            format: 'yyyy/mm/dd',
        });
      
      

    </script>
</asp:Content>
