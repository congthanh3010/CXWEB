<%@ Page Title="" Language="C#" MasterPageFile="~/Site_plc.Master" AutoEventWireup="true" CodeBehind="mr.aspx.cs" Inherits="CXWeb.plc.mr"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/plc.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;
            width: 500px;
            height: 430px;
        }

        .col1st {
            text-align: right;
            width: 40%;
            padding-right: 10px;
        }

        .col2nd {
            width: 57%;
            padding-right: 5px;
        }

        .txtClass {
            cursor: pointer;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="600000" OnTick="Timer1_Tick"></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="lnkTarget" runat="server" Style="display: none;">Target</asp:LinkButton>
                    <cc1:ModalPopupExtender ID="mpeDetail" runat="server" TargetControlID="lnkTarget" PopupControlID="pnlDetail"
                        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlDetail" runat="server" Style="display: none;" CssClass="modalPopup">
                        <table style="width: 100%; height: 100%;">
                            <tr>
                                <td class="col1st">機台代號</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtMachine" runat="server" ReadOnly="true" CssClass="txtClass" Width="100%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="col1st">Run Card 生產工單號</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtRuncard" runat="server" ReadOnly="true" CssClass="txtClass" Width="100%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="col1st">生產機種</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtItem" runat="server" ReadOnly="true" CssClass="txtClass" Width="100%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="col1st">預計生產數量</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtPlanQty" runat="server" ReadOnly="true" CssClass="txtClass" Text="-" Width="100%"></asp:TextBox>
                                </td>
                                <td>Pcs</td>
                            </tr>
                            <tr>
                                <td class="col1st">計數器數量(IoT)</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtIoTQty" runat="server" ReadOnly="true" CssClass="txtClass" Text="-" Width="100%"></asp:TextBox>
                                </td>
                                <td>Pcs</td>
                            </tr>
                            <tr>
                                <td class="col1st">報工數量</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtReportQty" runat="server" ReadOnly="true" CssClass="txtClass" Text="-" Width="100%"></asp:TextBox>
                                </td>
                                <td>Pcs</td>
                            </tr>
                            <tr>
                                <td class="col1st">達成比例</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtSuccessRate" runat="server" ReadOnly="true" CssClass="txtClass" Text="-" Width="100%"></asp:TextBox>
                                </td>
                                <td>%</td>
                            </tr>
                            <tr>
                                <td class="col1st">SPM比例</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtSpmRate" runat="server" ReadOnly="true" CssClass="txtClass" Text="-" Width="100%"></asp:TextBox>
                                </td>
                                <td>%</td>
                            </tr>
                            <tr>
                                <td class="col1st">操作人員</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtOperator" runat="server" ReadOnly="true" CssClass="txtClass" 
                                        TextMode="MultiLine" Width="100%" Height="100%" style="margin-top: 5px;"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="col1st">生管人員</td>
                                <td class="col2nd">
                                    <asp:TextBox ID="txtScheduler" runat="server" ReadOnly="true" CssClass="txtClass" Width="100%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table class="plcTitle table-bordered table-striped table-responsive" style="border: 0px;">
                        <thead>
                            <tr>
                                <th class="cbbTitle" style="padding: 2px;">Department
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlDep" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDep_SelectedIndexChanged">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="CT1">製一</asp:ListItem>
                                        <asp:ListItem Value="CT2;CT21">製二單打</asp:ListItem>
                                        <asp:ListItem Value="CT2_LH">製二連續</asp:ListItem>
                                        <asp:ListItem Value="CT3;CT31;CT3_LH">製三</asp:ListItem>
                                        <asp:ListItem Value="CNC1;CNC11;CNC2;CNC21">CNC區</asp:ListItem>
                                        <asp:ListItem Value="NC1;NC11;NC2;NC21">NC加工區</asp:ListItem>
                                        <asp:ListItem Value="DE">鋁製區</asp:ListItem>
                                        <asp:ListItem Value="JA">精密沖壓</asp:ListItem>
                                        <asp:ListItem Value="XLSD">鍛造後處理組</asp:ListItem>
                                        <asp:ListItem Value="DG">包裝課</asp:ListItem>
                                        <asp:ListItem Value="PK">模具課</asp:ListItem>
                                        <asp:ListItem Value="GC1;GC2">加工組</asp:ListItem>
                                        <asp:ListItem Value="MB">滾毛邊區</asp:ListItem>
                                        <asp:ListItem Value="XLBM">表面處理課</asp:ListItem>
                                        <asp:ListItem Value="MT">磁器迴路課</asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                                <th class="ctrTitle" rowspan="2" style="padding: 2px;">
                                    <asp:CheckBox ID="chkAuto" runat="server" Text="&nbsp;Automatically" />
                                </th>
                                <th rowspan="2" style="vertical-align: middle; text-align: left; padding-left: 200px;"><%= plc_title %></th>
                            </tr>
                            <tr>
                                <th class="cbbTitle" style="padding: 2px;">Area
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                                </th>
                            </tr>
                        </thead>
                    </table>
                    <table class="plcCaption table-bordered table-striped table-responsive">
                        <tr>
                            <td style="width: 100px; padding: 0px;"></td>
                            <%if (shift == "CA1") { %>
                            <td colspan="12"><%= dStart.ToString("yyyy-MM-dd") %></td>
                            <%} %>
                            <%else { %>
                            <td colspan="6"><%= dStart.ToString("yyyy-MM-dd") %></td>
                            <td colspan="6"><%= dEnd.ToString("yyyy-MM-dd") %></td>
                            <%} %>
                            <td></td>
                            <td rowspan="3" style="width: 6px;"></td>
                            <td colspan="8"></td>
                        </tr>
                        <tr>
                            <td style="width: 100px; padding: 0px;">機台代號</td>
                            <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemHour">
                                <ItemTemplate>
                                    <td rowspan="2" style="width: 30px; padding: 0px; margin: 0px; border-width: 1px 1px 1px 0px;"><%# Container.DataItem %></td>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="itemHour" runat="server"></asp:PlaceHolder>
                                    <td style="width: 35px;" rowspan="2">%</td>
                                    <td style="width: 50px;">換科件</td>
                                    <td style="width: 150px;">Run Card 生產工單號</td>
                                    <td style="width: 50px;" rowspan="2">SPM%</td>
                                    <td style="width: 70px;">料號</td>
                                    <td style="width: 40px;">製程</td>
                                    <td style="width: 60px;">標準時間</td>
                                    <td style="width: 50px;">開始</td>
                                    <td style="width: 60px;">IoT數量</td>
                                    <td style="width: 70px;">操作人員</td>
                                </LayoutTemplate>
                            </asp:ListView>
                        </tr>
                        <tr>
                            <td style="width: 100px; padding: 0px;">Machine</td>
                            <td style="width: 50px;">Type</td>
                            <td style="width: 150px;">Runcard</td>
                            <td style="width: 70px;">PN</td>
                            <td style="width: 40px;">pro</td>
                            <td style="width: 60px;">Std Time</td>
                            <td style="width: 50px;">Begin</td>
                            <td style="width: 60px;">IoT (Pcs)</td>
                            <td style="width: 70px;">Operator</td>
                        </tr>
                    </table>
                    <table class="plcContent table-bordered table-striped table-responsive">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" OnItemCreated="Repeater1_ItemCreated">
                            <ItemTemplate>
                                <asp:HiddenField ID="fieldId" runat="server" Value='<%# Eval("MachineId") %>' />
                                <tr>
                                    <td class="<%# Eval("cssSpm") %>" style="width: 100px; text-align: left; padding-left: 10px; border-right: 0px;">
                                        <asp:LinkButton ClientIDMode="Static" ID="lbtMachine" runat="server" Text='<%# Eval("MachineId") %>'
                                            OnClick="lbtMachine_Click" ForeColor="Black"></asp:LinkButton>
                                    </td>
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <td class="<%# Eval("css") %>"></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td style="width: 34px; text-align: center;">
                                        <%# Convert.ToInt32(Eval("WorkingRate")) > 0 ? Eval("WorkingRate") : "-" %>
                                    </td>
                                    <td class="<%# Eval("cssStatus") %>" style="width: 7px;"></td>
                                    <td style="width: 50px; border-left: 1px solid;">
                                        <%# Convert.ToInt32(Eval("ChangeItem")) > 0 ? Eval("ChangeItem") : "-" %>
                                    </td>
                                    <td class="<%# Eval("cssSpm") %>" style="width: 150px; border-left: 1px solid;"><%# Eval("RuncardId") %></td>
                                    <td class="<%# Eval("cssSpm") %>" style="width: 50px; border-left: 1px solid; text-align: center; ">
                                        <%# Convert.ToInt32(Eval("ShiftSpm")) > 0 ? Convert.ToInt32(Eval("ShiftSpm")).ToString("N0") : "-" %>
                                    </td>
                                    <td style="width: 70px; border-left: 1px solid;"><%# Eval("Item").ToString().Trim().Length > 0 ? Eval("Item").ToString().Substring(1, 7) : Eval("Item") %></td>
                                    <td style="width: 40px; border-left: 1px solid;"><%# Eval("Process") %></td>
                                    <td style="width: 60px; border-left: 1px solid;"><%# Eval("ProTime") %></td>
                                    <td style="width: 50px; border-left: 1px solid;">
                                        <%# Eval("BeginDate").ToString().Length > 0 ? Convert.ToDateTime(Eval("BeginDate")).ToString("HH:mm") : "-" %>
                                    </td>
                                    <td style="width: 60px; text-align: center; border-left: 1px solid;">
                                        <%# Convert.ToInt32(Eval("ShiftQty")) > 0 ? Convert.ToInt32(Eval("ShiftQty")).ToString("N0") : "-" %>
                                    </td>
                                    <td class="<%# Eval("cssSpm") %>" style="width: 69px; text-align: center; border-left: 1px solid;">
                                        <%# Eval("Operator").ToString().Length > 0 ? Eval("Operator").ToString().Substring(0, 6) : "-" %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
