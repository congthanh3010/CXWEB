<%@ Page Title="" Language="C#" MasterPageFile="~/Site_plc.Master" AutoEventWireup="true" CodeBehind="mr.aspx.cs" Inherits="CXWeb.plc.mr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <meta http-equiv="refresh" content="80">
    <%--<asp:ScriptManager ID="ScriptManager1" EnableCdn="true" runat="server">
    </asp:ScriptManager>
   
    <asp:Timer ID="Timer1" Enabled="true"  runat="server" OnTick="Timer1_Tick" Interval="80000">
    </asp:Timer>--%>
    <div class="container-fluid myreport">

        <div class="col-md-12 main" hidden="hidden">

            <div class="wrap">
                <div class="page-header">

                    <span style="margin-right: 15px;">日期 </span>
                    <asp:TextBox ID="date" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                    <span style="margin-right: 15px;">班次 </span>
                    <asp:TextBox ID="shift" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox>

                    <asp:Button ID="btnXem" runat="server" Text="View" OnClick="btnXem_Click" />

                </div>
            </div>

        </div>

<div class="row">

        <table class=" table table-bordered table-striped" style="width: 1226px; margin-left: unset">
            <%--      <div class="table-responsive"> --%>
            <thead>
                <tr>
                    <%-- <th colspan="13">機台編號:
                         <asp:Label ID="mamay" ClientIDMode="Static" runat="server" Text="---"></asp:Label> </th>--%>
                   
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th colspan="29"><%=head_title%>
                    </th>
                </tr>

                <tr>

                    <td style="width: 100px; text-align: left; font-weight: bold">
                        <asp:DropDownList ID="area" runat="server" AutoPostBack="true" OnSelectedIndexChanged="area_SelectedIndexChanged" Width="100px">
                            <asp:ListItem Value=" "> </asp:ListItem>
                            <asp:ListItem Value="CT1">製一</asp:ListItem>
                            <asp:ListItem Value="CT2">製二單打</asp:ListItem>
                            <asp:ListItem Value="CT21">製二單打-2</asp:ListItem>
                            <asp:ListItem Value="CT2_LH">製二連續</asp:ListItem>
                            <asp:ListItem Value="CT3">製三</asp:ListItem>
                            <asp:ListItem Value="CT31">製三-1</asp:ListItem>
                            <asp:ListItem Value="CNC1">CNC一區</asp:ListItem>
                            <asp:ListItem Value="CNC11">CNC一區-2</asp:ListItem>
                            <asp:ListItem Value="CNC2">CNC二區</asp:ListItem>
                            <asp:ListItem Value="CNC21">CNC二區-2</asp:ListItem>
                            <asp:ListItem Value="NC">NC加工區</asp:ListItem>
                            <asp:ListItem Value="NC1">NC加工區-2</asp:ListItem>
                            <asp:ListItem Value="DE">鋁製區</asp:ListItem>
                        </asp:DropDownList>
                         <asp:HiddenField ClientIDMode="Static" ID="harea_code" runat="server"  />
                    </td>
                    <td style="width: 30px; text-align: left;"><%=time[0]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[1]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[2]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[3]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[4]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[5]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[6]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[7]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[8]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[9]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[10]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[11]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[12]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[13]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[14]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[15]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[16]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[17]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[18]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[19]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[20]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[21]%></td>
                    <td style="width: 30px; text-align: left;"><%=time[22]%></td>
                    <td style="width: 36px; text-align: left;"><%=time[23]%></td>

                    <td style="width: 70px; text-align: left">PN</td>
                    <td style="width: 30px; text-align: left">pro</td>
                    <td style="width: 50px; text-align: left">Time</td>
                    <td style="width: 246px; text-align: left">Status</td>
                </tr>
            </tbody>

        </table>

        <%-- <div style="width: 970px; height: 410px; overflow: auto; margin: auto;">--%>
        
            <table class=" table table-bordered table-striped table-responsive" style="width: 1226px; margin-left: unset">

                <tbody>

                    <tr <%=code_row_machine[0]%>>
                        <td style="width: 100px; text-align: left"><%=machine_code[0]%></td>

                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tr>
                    <tr <%=code_row_machine[1]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[1]%></td>

                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[2]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[2]%></td>

                        <asp:Repeater ID="Repeater3" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[3]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[3]%></td>

                        <asp:Repeater ID="Repeater4" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[4]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[4]%></td>

                        <asp:Repeater ID="Repeater5" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[5]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[5]%></td>

                        <asp:Repeater ID="Repeater6" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[6]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[6]%></td>

                        <asp:Repeater ID="Repeater7" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[7]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[7]%></td>

                        <asp:Repeater ID="Repeater8" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[8]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[8]%></td>

                        <asp:Repeater ID="Repeater9" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tr>
                    <tr <%=code_row_machine[9]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[9]%></td>

                        <asp:Repeater ID="Repeater10" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[10]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[10]%></td>

                        <asp:Repeater ID="Repeater11" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[11]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[11]%></td>

                        <asp:Repeater ID="Repeater12" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[12]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[12]%></td>

                        <asp:Repeater ID="Repeater13" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[13]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[13]%></td>

                        <asp:Repeater ID="Repeater14" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[14]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[14]%></td>

                        <asp:Repeater ID="Repeater15" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[15]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[15]%></td>

                        <asp:Repeater ID="Repeater16" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[16]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[16]%></td>

                        <asp:Repeater ID="Repeater17" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[17]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[17]%></td>

                        <asp:Repeater ID="Repeater18" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[18]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[18]%></td>

                        <asp:Repeater ID="Repeater19" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[19]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[19]%></td>

                        <asp:Repeater ID="Repeater20" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[20]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[20]%></td>

                        <asp:Repeater ID="Repeater21" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[21]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[21]%></td>

                        <asp:Repeater ID="Repeater22" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[22]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[22]%></td>

                        <asp:Repeater ID="Repeater23" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[23]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[23]%></td>

                        <asp:Repeater ID="Repeater24" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tr>
                    <tr <%=code_row_machine[24]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[24]%></td>

                        <asp:Repeater ID="Repeater25" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <%--   <tr <%=code_row_machine[25]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[25]%></td>

                        <asp:Repeater ID="Repeater26" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[26]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[26]%></td>

                        <asp:Repeater ID="Repeater27" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[27]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[27]%></td>

                        <asp:Repeater ID="Repeater28" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[28]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[28]%></td>

                        <asp:Repeater ID="Repeater29" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[29]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[29]%></td>

                        <asp:Repeater ID="Repeater30" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[30]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[30]%></td>

                        <asp:Repeater ID="Repeater31" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[31]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[31]%></td>

                        <asp:Repeater ID="Repeater32" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[32]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[32]%></td>

                        <asp:Repeater ID="Repeater33" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tr>
                    <tr <%=code_row_machine[33]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[33]%></td>

                        <asp:Repeater ID="Repeater34" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[34]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[34]%></td>

                        <asp:Repeater ID="Repeater35" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[35]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[35]%></td>

                        <asp:Repeater ID="Repeater36" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[36]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[36]%></td>

                        <asp:Repeater ID="Repeater37" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[37]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[37]%></td>

                        <asp:Repeater ID="Repeater38" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[38]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[38]%></td>

                        <asp:Repeater ID="Repeater39" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr <%=code_row_machine[39]%>>
                        <td style="width: 80px; text-align: left"><%=machine_code[39]%></td>

                        <asp:Repeater ID="Repeater40" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                   <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[40]%></td>

                        <asp:Repeater ID="Repeater41" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[41]%></td>

                        <asp:Repeater ID="Repeater42" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[42]%></td>

                        <asp:Repeater ID="Repeater43" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[43]%></td>

                        <asp:Repeater ID="Repeater44" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[44]%></td>

                        <asp:Repeater ID="Repeater45" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[45]%></td>

                        <asp:Repeater ID="Repeater46" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[46]%></td>

                        <asp:Repeater ID="Repeater47" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[47]%></td>

                        <asp:Repeater ID="Repeater48" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[48]%></td>

                        <asp:Repeater ID="Repeater49" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[49]%></td>

                        <asp:Repeater ID="Repeater50" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[50]%></td>

                        <asp:Repeater ID="Repeater51" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: left"><%=machine_code[51]%></td>

                        <asp:Repeater ID="Repeater52" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <tr>
                    <td style="width: 80px; text-align: left"><%=machine_code[52]%></td>
                    
                        <asp:Repeater ID="Repeater53" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                </tr>
                        <tr>
                    <td style="width: 80px; text-align: left"><%=machine_code[53]%></td>
                    
                        <asp:Repeater ID="Repeater54" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                </tr>
                        <tr>
                    <td style="width: 80px; text-align: left"><%=machine_code[54]%></td>
                    
                        <asp:Repeater ID="Repeater55" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                </tr>
                        <tr>
                    <td style="width: 80px; text-align: left"><%=machine_code[55]%></td>
                    
                        <asp:Repeater ID="Repeater56" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    
                </tr>
                        <tr>
                    <td style="width: 80px; text-align: left"><%=machine_code[56]%></td>
                    
                        <asp:Repeater ID="Repeater57" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                </tr>
                        <tr>
                    <td style="width: 80px; text-align: left"><%=machine_code[57]%></td>
                    
                        <asp:Repeater ID="Repeater58" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    
                </tr>
                        <tr>
                    <td style="width: 80px; text-align: left"><%=machine_code[58]%></td>
                    
                        <asp:Repeater ID="Repeater59" runat="server">
                            <ItemTemplate>
                                <td <%# Eval("code") %>><%# Eval("MACHINE_CODE") %></td>
                            </ItemTemplate>
                        </asp:Repeater>
                </tr>--%>
                </tbody>
            </table>
        </div>
    </div>


    <%--</form>--%>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <!--Reference the SignalR library. -->
    <!--Reference the autogenerated SignalR hub script. -->
    <script src="/signalr/hubs"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>

    <script type="text/javascript">

        $.datetimepicker.setLocale('en');
        $('#date').datetimepicker({ format: 'd/m/Y' });



    </script>
</asp:Content>
