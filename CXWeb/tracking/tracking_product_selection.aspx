<%@ Page Title="" Language="C#" MasterPageFile="~/Site_tracking.Master" AutoEventWireup="true" CodeBehind="tracking_product_selection.aspx.cs" Inherits="CXWeb.tracking.tracking_product_selection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: absolute; width: 100%; height: 90%;">
         <asp:HiddenField ClientIDMode="Static" ID="hsub_unit" runat="server" />
        <table class="table-bordered table-striped" style="width: 100%; font-size: large; padding: 0px; border: 0px;">     
              <tr>
                <td style="text-align: left; background-color: #006699; color: white; font-weight: bold;">查詢條件 Điều kiện tìm kiếm
                </td>
            </tr>
            <tr>
                <td style="text-align: left;padding: 0px;">
                    Invoice No
                    <asp:TextBox ID="invoice_text" ClientIDMode="Static" AutoCompleteType="Disabled" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>
                    產品編號 Chủng loại
                    <asp:TextBox ID="product_no" ClientIDMode="Static" AutoCompleteType="Disabled" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>
                     批號 Số lô
                    <asp:TextBox ID="lot_number" ClientIDMode="Static" AutoCompleteType="Disabled" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;padding: 0px;">
                    <asp:Button ID="btnXem" runat="server" Style="font-size: large;" Text="查詢 Tìm kiếm" OnClick="btnXem_Click" Font-Bold="true"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="position: absolute; width: 100%; height:86%; overflow:auto;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                        OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
                        ShowFooter="false" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px"/>
                        <RowStyle Font-Names="Calibri" Font-Size="18px" />
                        <Columns>
                            
                            <asp:TemplateField HeaderText="機種<br/>Chủng loại" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblcol01" runat="server" Text='<%# Eval("machine_no") %>'></asp:Label>--%>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("link") %>'
                                        Text='<%# Eval("ogb04") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="批號</br>Số lô" HeaderStyle-Width="100px">    
                                                       
                                <ItemTemplate>
                                    <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("ogb092") %>'></asp:Label>
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
</asp:Content>
