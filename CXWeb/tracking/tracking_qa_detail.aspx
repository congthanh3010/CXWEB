<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tracking_qa_detail.aspx.cs" Inherits="CXWeb.tracking.tracking_qa_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>檢驗明細資料</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        BackColor="#CACBE1" ShowFooter="false">
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px"/>
                        <RowStyle Font-Names="Calibri" Font-Size="18px" />
                        <Columns>
                            <asp:TemplateField HeaderText="檢驗單號<br/>Đơn kiểm nghiệm" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol00" runat="server" Text='<%# Eval("tc_qcm01") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="檢驗日期<br/>Ngày kiểm nghiệm" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("tc_qcm09") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Runcard" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("tc_qcm02") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="工單<br/>Công đơn" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("tc_qcm04") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="料件編號<br/>Mã sản phẩm" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("tc_qcm05") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="作業編號<br/>Mã công đoạn" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol05" runat="server" Text='<%# Eval("tc_qcm06") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="檢驗人員<br/>Người kiểm nghiệm" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol06" runat="server" Text='<%# Eval("tc_qcm10") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="客訴內容<br/>Khách hàng than phiền" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol07" runat="server" Text='<%# Eval("tc_imq02") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="填寫客訴內容<br/>Trả lời khách hàng than phiền" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblcol08" runat="server" Text='<%# Eval("tc_imq03") %>'></asp:Label>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                          
                        </Columns>
                    </asp:GridView>
    </div>
    </form>
</body>
</html>
