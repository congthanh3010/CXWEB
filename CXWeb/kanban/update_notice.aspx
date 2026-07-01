<%@ Page Title="" Language="C#" MasterPageFile="~/Site_kanban.Master" AutoEventWireup="true" CodeBehind="update_notice.aspx.cs" Inherits="CXWeb.kanban.update_notice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div style="position: absolute; width: 100%; height: 100%; padding: 3px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container" style="width: 50%; float: left">
                    <div>
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Thêm" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Lưu" Enabled="false" OnClick="btnSave_Click" />
                        <div class="clearfix"></div>
                    </div>
                    
                    <table class="table-bordered table-striped" style="margin-top: 5px; width: 100%">
                        <thead style="background: #006699; color: white; font: bold; font-size: 14px">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Đường dẫn"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Trang"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Thao tác"></asp:Label>
                                </td>
                            </tr>
                        </thead>
                        <tbody style="background: #CACBE1; font-size: 14px">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <asp:HiddenField ID="fieldIndex" runat="server" Value='<%# Eval("index") %>' />
                                    <tr>
                                        <td style="padding: 2px; width: auto">
                                            <asp:TextBox ID="txtImageUrl" runat="server" CssClass="form-control" Width="100%" Text='<%# Eval("url") %>'></asp:TextBox>
                                        </td>
                                        <td style="padding: 2px; width: 40px">
                                            <asp:TextBox ID="txtPage" runat="server" CssClass="form-control" Width="100%" TextMode="Number" Text='<%# Eval("page") %>'></asp:TextBox>
                                        </td>
                                        <td style="padding: 2px; width: 30px">
                                            <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-danger" Text="Xóa" OnClick="btnRemove_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <div class="container" style="width: 50%; float: left; height: 99vh; overflow: auto">
                    <asp:ListView ID="ListView1" runat="server" GroupItemCount="3" GroupPlaceholderID="groupImage" ItemPlaceholderID="itemImage">
                        <ItemTemplate>
                            <div class="col-md-4 top_brand_left">
                                <div class="hover14 column">
                                    <div class="agile_top_brand_left_grid">
                                        <div class="agile_top_brand_left_grid_pos">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("url") %>' CssClass="img-responsive" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <GroupTemplate>
                            <div class="agile_top_brands_grids" style="margin-bottom: 10px">
                                <asp:PlaceHolder ID="itemImage" runat="server"></asp:PlaceHolder>
                                <div class="clearfix"></div>
                            </div>
                        </GroupTemplate>
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="groupImage" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="/js/bootstrap.min.js"></script>
</asp:Content>
