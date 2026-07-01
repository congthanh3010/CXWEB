<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tracking_rw_detail.aspx.cs" Inherits="CXWeb.tracking.tracking_rw_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>報工明細資料</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        BackColor="#CACBE1" ShowFooter="false">
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px"/>
                        <RowStyle Font-Names="Calibri" Font-Size="18px" />
                        <Columns>
                            <asp:TemplateField HeaderText="開工日期<br/>Ngày" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol00" runat="server" Text='<%# Eval("shb02") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="開工時間<br/>Giờ" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("shb021") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Runcard" HeaderStyle-Width="100px">    
                                                       
                                <ItemTemplate>
                                    <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("shb16") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工單<br/>Công đơn" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("shb05") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工序<br/>Công đoạn" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("shb082") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="生產機台<br/>MS máy" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("shb09") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作人員<br/>Nhân viên thao tác" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("shb04") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                        </Columns>
                    </asp:GridView>
    </div>
    </form>
</body>
</html>
