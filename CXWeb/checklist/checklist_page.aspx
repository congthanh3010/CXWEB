<%@ Page EnableViewState="true" EnableEventValidation= "false " Title="" Language="C#" MasterPageFile="~/Site_checklist.Master" AutoEventWireup="true" CodeBehind="checklist_page.aspx.cs" Inherits="CXWeb.checklist.checklist_page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%--    <div class="col-md-12 main" style="padding: 0px;">--%>
        <div  id="GridViewContainer" class="GridViewContainer" style="position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; padding: 0px;">
            <%--<asp:GridView ID="gvData" runat="server" CellPadding="4" ForeColor="#333333"
                DataKeyNames="id" GridLines="None" Width="800px" AutoGenerateColumns="False"
                CssClass="gvDataCss" AllowSorting="True" OnRowCancelingEdit="gvData_RowCancelingEdit" OnRowDeleting="gvData_RowDeleting" OnRowEditing="gvData_RowEditing">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />          
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div hidden="hidden">
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="nhom_may" HeaderText="nhom_may" />
                    <asp:BoundField DataField="ten_may" HeaderText="ten_may" />
                    <asp:BoundField DataField="thao_tac_vien" HeaderText="thao_tac_vien" />
                    <asp:BoundField DataField="quality01" HeaderText="quality01" />
                    <asp:BoundField DataField="quality02" HeaderText="quality02" />
                    <asp:BoundField DataField="quality03" HeaderText="quality03" />
                    <asp:BoundField DataField="quality04" HeaderText="quality04" />
                    <asp:BoundField DataField="s01" HeaderText="s01" />
                    <asp:BoundField DataField="s02" HeaderText="s02" />
                    <asp:BoundField DataField="s03" HeaderText="s03" />
                    <asp:BoundField DataField="s04" HeaderText="s04" />
                    <asp:BoundField DataField="s05" HeaderText="s05" />
                    <asp:BoundField DataField="s06" HeaderText="s06" />
                    <asp:BoundField DataField="s07" HeaderText="s07" />
                    <asp:BoundField DataField="s08" HeaderText="s08" />
                    <asp:BoundField DataField="security01" HeaderText="security01" />
                    <asp:BoundField DataField="security02" HeaderText="security02" />
                    <asp:BoundField DataField="security03" HeaderText="security03" />
                    <asp:BoundField DataField="security04" HeaderText="security04" />
                    <asp:BoundField DataField="other01" HeaderText="other01" />



                    <asp:CommandField ShowEditButton="True" HeaderText="操作/Thao tác" ShowDeleteButton="false" />
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

            </asp:GridView>--%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="200%" DataKeyNames="nhom_may" 
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
                ShowFooter="true" OnRowCommand="GridView1_RowCommand" HeaderStyle-CssClass="header">
                <%--<HeaderStyle  BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" />--%>
                <RowStyle Font-Names="Calibri" />
                <Columns>
                   
<%--                    <asp:TemplateField HeaderText="nhom_may"  HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="lblnhom_may" runat="server" Text='<%# Eval("nhom_may") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px"></HeaderStyle>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                        <EditItemTemplate>
                            <%--<asp:LinkButton ID="btnUpdate" CommandName="Update" runat="server">Update</asp:LinkButton>--%>
                            <asp:LinkButton ID="btnCancel" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit">Edit</asp:LinkButton>
                            <%--<asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>--%>
                        </ItemTemplate>
                      <%--  <FooterTemplate>
                            <asp:LinkButton ID="btnAdd" runat="server" CommandName="ThemMoi">Add new</asp:LinkButton>
                        </FooterTemplate>--%>
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ten_may" HeaderStyle-Width="50px">                       
                        <ItemTemplate>
                            <asp:Label ID="lblten_may" runat="server" Text='<%# Eval("ten_may") %>'></asp:Label>
                        </ItemTemplate>
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="thao_tac_vien" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="lblthao_tac_vien" runat="server" Text='<%# Eval("thao_tac_vien") %>'></asp:Label>
                        </ItemTemplate>
                       <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="quality01" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtquality01" Width="100%" runat="server" Text='<%# Eval("quality01") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblquality01" runat="server" Text='<%# Eval("quality01") %>'></asp:Label>
                        </ItemTemplate>
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>                    
                     <asp:TemplateField HeaderText="quality02" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtquality02" Width="100%"  runat="server" Text='<%# Eval("quality02") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblquality02" runat="server" Text='<%# Eval("quality02") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="quality03" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtquality03" Width="100%"  runat="server" Text='<%# Eval("quality03") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblquality03" runat="server" Text='<%# Eval("quality03") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="quality04" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtquality04" Width="100%"  runat="server" Text='<%# Eval("quality04") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblquality04" runat="server" Text='<%# Eval("quality04") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s01" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts01"  Width="100%" runat="server" Text='<%# Eval("s01") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls01" runat="server" Text='<%# Eval("s01") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s02" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts02" Width="100%"  runat="server" Text='<%# Eval("s02") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls02" runat="server" Text='<%# Eval("s02") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s03" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts03" Width="100%"  runat="server" Text='<%# Eval("s03") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls03" runat="server" Text='<%# Eval("s03") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s04" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts04" Width="100%"  runat="server" Text='<%# Eval("s04") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls04" runat="server" Text='<%# Eval("s04") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s05" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts05"  Width="100%" runat="server" Text='<%# Eval("s05") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls05" runat="server" Text='<%# Eval("s05") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s06" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts06" Width="100%"  runat="server" Text='<%# Eval("s06") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls06" runat="server" Text='<%# Eval("s06") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s07" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts07" Width="100%"  runat="server" Text='<%# Eval("s07") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls07" runat="server" Text='<%# Eval("s07") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="s08" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txts08" Width="100%"  runat="server" Text='<%# Eval("s08") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbls08" runat="server" Text='<%# Eval("s08") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="security01" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtsecurity01" Width="100%"  runat="server" Text='<%# Eval("security01") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblsecurity01" runat="server" Text='<%# Eval("security01") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="security02" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtsecurity02" Width="100%"  runat="server" Text='<%# Eval("security02") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblsecurity02" runat="server" Text='<%# Eval("security02") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="security03" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtsecurity03" Width="100%"  runat="server" Text='<%# Eval("security03") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblsecurity03" runat="server" Text='<%# Eval("security03") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>                     
                     <asp:TemplateField HeaderText="other01" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtother01" Width="100%"  runat="server" Text='<%# Eval("other01") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblother01" runat="server" Text='<%# Eval("other01") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="sum_point" HeaderStyle-Width="50px">
                      
                        <ItemTemplate>
                            <asp:Label ID="lblsum_point" runat="server" Text='<%# Eval("sum_point") %>'></asp:Label>
                        </ItemTemplate>                        
                        <%--<HeaderStyle Width="100px"></HeaderStyle>--%>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnUpdate" CommandName="Update" runat="server">Update</asp:LinkButton>                         
                        </EditItemTemplate>
                       
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
          
        </div>
  <%--  </div>--%>
    
</asp:Content>
