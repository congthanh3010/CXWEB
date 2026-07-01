<%@ Page Title="" Language="C#" MasterPageFile="~/Site_tracking.Master" AutoEventWireup="true" CodeBehind="tracking_table.aspx.cs" Inherits="CXWeb.tracking.tracking_table" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: absolute; width: 100%; height: 90%;">
        <asp:HiddenField ClientIDMode="Static" ID="hproduct" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hlot_number" runat="server" />
       
        <table style="width: 100%;">
            <tr style="height: 26px;">
                <td style="text-align: left">
                    <asp:Button ID="btnBack" Style="font-size: large;" runat="server" Text="返回上頁 Quay về trước" OnClick="btnBack_Click" Font-Bold="true"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="position:absolute;width: 100%; height: 95%; overflow: auto;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                            OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
                            ShowFooter="false" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" Font-Size="18px" />
                            <RowStyle Font-Names="Calibri" Font-Size="18px" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol00" runat="server" Text='<%# Eval(tracking_col_name[0]) %>' data-html="true" ToolTip="Some &#13;long text"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol01" runat="server" Text='<%# Eval(tracking_col_name[1]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol02" runat="server" Text='<%# Eval(tracking_col_name[2]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol03" runat="server" Text='<%# Eval(tracking_col_name[3]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol04" runat="server" Text='<%# Eval(tracking_col_name[4]) %>'></asp:Label>--%>
                                          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("link1") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol05" runat="server" Text='<%# Eval(tracking_col_name[5]) %>'></asp:Label>--%>
                                        <asp:HyperLink ID="HyperLinkQA1" runat="server" NavigateUrl='<%# Eval("linkQA1") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol06" runat="server" Text='<%# Eval(tracking_col_name[6]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol07" runat="server" Text='<%# Eval(tracking_col_name[7]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol08" runat="server" Text='<%# Eval(tracking_col_name[8]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol09" runat="server" Text='<%# Eval(tracking_col_name[9]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol10" runat="server" Text='<%# Eval(tracking_col_name[10]) %>'></asp:Label>--%>
                                          <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("link2") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol11" runat="server" Text='<%# Eval(tracking_col_name[11]) %>'></asp:Label>--%>
                                        <asp:HyperLink ID="HyperLinkQA2" runat="server" NavigateUrl='<%# Eval("linkQA2") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol12" runat="server" Text='<%# Eval(tracking_col_name[12]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol13" runat="server" Text='<%# Eval(tracking_col_name[13]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol14" runat="server" Text='<%# Eval(tracking_col_name[14]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol15" runat="server" Text='<%# Eval(tracking_col_name[15]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol16" runat="server" Text='<%# Eval(tracking_col_name[16]) %>'></asp:Label>--%>
                                         <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("link3") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol17" runat="server" Text='<%# Eval(tracking_col_name[17]) %>'></asp:Label>--%>
                                        <asp:HyperLink ID="HyperLinkQA3" runat="server" NavigateUrl='<%# Eval("linkQA3") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol18" runat="server" Text='<%# Eval(tracking_col_name[18]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol19" runat="server" Text='<%# Eval(tracking_col_name[19]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol20" runat="server" Text='<%# Eval(tracking_col_name[20]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol21" runat="server" Text='<%# Eval(tracking_col_name[21]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol22" runat="server" Text='<%# Eval(tracking_col_name[22]) %>'></asp:Label>--%>
                                         <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("link4") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol23" runat="server" Text='<%# Eval(tracking_col_name[23]) %>'></asp:Label>--%>
                                        <asp:HyperLink ID="HyperLinkQA4" runat="server" NavigateUrl='<%# Eval("linkQA4") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol24" runat="server" Text='<%# Eval(tracking_col_name[24]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol25" runat="server" Text='<%# Eval(tracking_col_name[25]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol26" runat="server" Text='<%# Eval(tracking_col_name[26]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol27" runat="server" Text='<%# Eval(tracking_col_name[27]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol28" runat="server" Text='<%# Eval(tracking_col_name[28]) %>'></asp:Label>--%>
                                         <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl='<%# Eval("link5") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblcol29" runat="server" Text='<%# Eval(tracking_col_name[29]) %>'></asp:Label>--%>
                                        <asp:HyperLink ID="HyperLinkQA5" runat="server" NavigateUrl='<%# Eval("linkQA5") %>' target="_blank"
                                        Text='click here'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol30" runat="server" Text='<%# Eval(tracking_col_name[30]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol31" runat="server" Text='<%# Eval(tracking_col_name[31]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol32" runat="server" Text='<%# Eval(tracking_col_name[32]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol33" runat="server" Text='<%# Eval(tracking_col_name[33]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol34" runat="server" Text='<%# Eval(tracking_col_name[34]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol35" runat="server" Text='<%# Eval(tracking_col_name[35]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol36" runat="server" Text='<%# Eval(tracking_col_name[36]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol37" runat="server" Text='<%# Eval(tracking_col_name[37]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol38" runat="server" Text='<%# Eval(tracking_col_name[38]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol39" runat="server" Text='<%# Eval(tracking_col_name[39]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcol40" runat="server" Text='<%# Eval(tracking_col_name[40]) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>

        </table>

    </div>
    <style type="text/css">
        /* Tooltip container */
        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black; /* If you want dots under the hoverable text */
        }

        /* Tooltip text */
        .tooltip .tooltiptext {
            visibility: hidden;
            width: 120px;
            background-color: #555;
            color: #fff;
            text-align: center;
            padding: 5px 0;
            border-radius: 6px;
            /* Position the tooltip text */
            position: absolute;
            z-index: 1;
            bottom: 125%;
            left: 50%;
            margin-left: -60px;
            /* Fade in tooltip */
            opacity: 0;
            transition: opacity 0.3s;
        }

            /* Tooltip arrow */
            .tooltip .tooltiptext::after {
                content: "";
                position: absolute;
                top: 100%;
                left: 50%;
                margin-left: -5px;
                border-width: 5px;
                border-style: solid;
                border-color: #555 transparent transparent transparent;
            }

        /* Show the tooltip text when you mouse over the tooltip container */
        .tooltip:hover .tooltiptext {
            visibility: visible;
            opacity: 1;
        }
    </style>
</asp:Content>
