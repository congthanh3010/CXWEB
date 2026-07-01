<%@Page EnableViewState="true" EnableEventValidation= "false " Title=""  Language="C#" AutoEventWireup="true" MasterPageFile="~/Site_checklist.Master" CodeBehind="ex1.aspx.cs" Inherits="CXWeb.test.ex1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: absolute; left: 0px; top: 0px; width: 100%; height: 500px; overflow: auto;padding: 0px;">
    
       
                    <asp:GridView ID="gvSinhVien" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" DataKeyNames="nhom_may" 
                        onrowcancelingedit="gvSinhVien_RowCancelingEdit" 
                        onrowdeleting="gvSinhVien_RowDeleting" onrowediting="gvSinhVien_RowEditing" 
                        onrowupdating="gvSinhVien_RowUpdating" HeaderStyle-CssClass="GVFixedHeader" >
                        <RowStyle BackColor="White" ForeColor="#330099" />
                        <Columns>
                            <asp:TemplateField HeaderText="Mã SV">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("nhom_may") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="nhom_may" HeaderText="nhom_may" />
                     <asp:TemplateField HeaderText="ten_may" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtten_may" Width="100px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblten_may" runat="server" Text='<%# Eval("ten_may") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px"></HeaderStyle>
                    </asp:TemplateField>
                   
                  
                           <%-- <asp:BoundField DataField="tensv" HeaderText="Tên SV">
                            <HeaderStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ngaysinh" HeaderText="Ngày Sinh">
                            <HeaderStyle Width="200px" />
                            </asp:BoundField>--%>
                            <%--<asp:TemplateField HeaderText="Giới Tính">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# CheckGioiTinh(Eval("phai")) %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="drlGioiTinh" runat="server">
                                        <asp:ListItem Value="1">Nam</asp:ListItem>
                                        <asp:ListItem Value="0">Nữ</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>--%>
                         <%--   <asp:BoundField DataField="diachi" HeaderText="Địa Chỉ" />
                            <asp:BoundField DataField="dienthoai" HeaderText="Điện Thoại" />
                            <asp:BoundField DataField="email" HeaderText="Email" />--%>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <%--<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />--%>
                    </asp:GridView>
               
    
    </div>
  </asp:Content>