<%@Page EnableViewState="true" EnableEventValidation= "false " Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="report5.aspx.cs" Inherits="CXWeb.sys.report5" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="/css/foundation-datepicker.css" rel="stylesheet" type="text/css"/>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid report">
        <div class="row">
            <div class="col-md-12 main" style="padding: 0px;">
                <div class="wrap" style="height: 90px; padding: 0px;">
                    <div class="page-header">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID1="UpdatePanel1" AssociatedUpdatePanelID2="UpdatePanel2">
                            <ProgressTemplate>
                                <div style="width: 220px; height: 46px; left: 700px; position: fixed; font-size: 15px; color: #FFF; z-index: 3; float: right;">
                                    正在加载数据，请稍等...
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <div style="position: fixed; width: 100%; height: 30px; background: #22324C;">
                            <font style="font-size: 15px; font-weight: bold; color: #C8DAF2; position: absolute;">
                           採購月報-物輔料漲跌幅表查询
                       </font>
                        </div>
                        <br />
                        <div style="width: 100%; margin: auto; height: 60px;  top: 30px; position: absolute; background: #22324C;">

                            <span hidden="true" style="margin-right: 15px;">From </span>
                            <asp:TextBox ID="date1" hidden='true' ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                            <span hidden='true' style="margin-right: 15px;">To </span>
                            <asp:TextBox ID="date2" hidden='true' ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox>


                            <span style="margin-right: 15px; color: #C8DAF2;">第一年 </span>
                            <asp:TextBox ID="textyear1" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                             <span style="margin-right: 15px; color: #C8DAF2;"> - </span>
                            <asp:TextBox ID="textyear1_to" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                            <span style="margin-right: 15px; color: #C8DAF2;">第二年 </span>
                            <asp:TextBox ID="textyear2" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                            <span style="margin-right: 15px; color: #C8DAF2;"> - </span>
                            <asp:TextBox ID="textyear2_to" ClientIDMode="Static" Style="margin-right: 15px;" runat="server" CssClass="some_class"></asp:TextBox>

                           
                            <br />

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnXem" runat="server" Text="查詢" OnClick="btnXem_Click" Style="position: absolute; left: 20px; top: 30px; width: 50px; height: 25px; font-size: 13px;"></asp:Button>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btnExcel" runat="server" Text="導出Excel" OnClick="btnExcel_Click" Style="position: absolute; left: 100px; top: 30px; width: 90px; height: 25px; font-size: 13px;"></asp:Button>
                             <asp:Button ID="Button1" runat="server" Text="對照表"  OnClientClick = "window.open('report5_1.aspx');return false;" Style="position: absolute; left: 217px; top: 30px; width: 90px; height: 25px; font-size: 13px;"></asp:Button>
                        </div>
                    </div>

                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>


                        <table class="table-bordered table-striped" style="width: 1180px; margin-left: 0px" <%=code_hide_table%>>

                            <%-- <tr hidden ='true'>
                    <th >Time:
                <asp:Label ID="time" ClientIDMode="Static" runat="server" Text="---" ></asp:Label>
                         <asp:HiddenField ClientIDMode="Static" ID="hvender" runat="server" />
                        
                    </th>
                </tr>--%>

                            <tr>

                                <td>
                                    <div style="width: 1180px; height: 420px; overflow: auto; margin: auto;">
                                        <table class="table-bordered table-striped" style="width: <%=table_width%>px; text-align: right">

                                            <thead>
                                                <tr>
                                                    <th style="width: 150px; height: 42px"></th>
                                                    <th style="width: 500px; height: 42px"></th>
                                                    <th style="width: 80px; height: 42px"></th>
                                                     <th style="width: 100px; height: 42px"></th>
                                                     <th style="width: 200px; height: 42px"></th>
                                                    <th colspan="2" style="width: 160px; height: 42px">
                                                        <asp:Label ID="Label5" ClientIDMode="Static" runat="server" Text="---"></asp:Label>
                                                    </th>
                                                    <th colspan="<%=colspan%>" style="width: 3520px; height: 42px">
                                                        <asp:Label ID="Label6" ClientIDMode="Static" runat="server" Text="---"></asp:Label>
                                                    </th>
                                                    <th style="width: 80px; height: 42px"></th>
                                                    <th style="width: 80px; height: 42px"></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 150px">材料编号</th>
                                                    <th style="width: 500px">品名规格</th>
                                                    <th style="width: 80px">采购单位</th>                                                    
                                                     <th style="width: 100px">分類</th>
                                                     <th style="width: 200px">原物料波動說明原因</th>
                                                    <th style="width: 80px">
                                                        <asp:Label ID="Label1" ClientIDMode="Static" runat="server" Text="---"></asp:Label>采购量</th>
                                                    <th style="width: 80px">
                                                        <asp:Label ID="Label2" ClientIDMode="Static" runat="server" Text="---"></asp:Label>平均单价(USD)</th>
                                                    <th <%=code_hiden[0]%> style="width: 80px">采购量1</th>
                                                    <th <%=code_hiden[0]%> style="width: 80px">单价1(USD)</th>
                                                    <th <%=code_hiden[1]%> style="width: 80px">采购量2</th>
                                                    <th <%=code_hiden[1]%> style="width: 80px">单价2(USD)</th>
                                                    <th <%=code_hiden[2]%> style="width: 80px">采购量3</th>
                                                    <th <%=code_hiden[2]%> style="width: 80px">单价3(USD)</th>
                                                    <th <%=code_hiden[3]%> style="width: 80px">采购量4</th>
                                                    <th <%=code_hiden[3]%> style="width: 80px">单价4(USD)</th>
                                                    <th <%=code_hiden[4]%> style="width: 80px">采购量5</th>
                                                    <th <%=code_hiden[4]%> style="width: 80px">单价5(USD)</th>
                                                    <th <%=code_hiden[5]%> style="width: 80px">采购量6</th>
                                                    <th <%=code_hiden[5]%> style="width: 80px">单价6(USD)</th>
                                                    <th <%=code_hiden[6]%> style="width: 80px">采购量7</th>
                                                    <th <%=code_hiden[6]%> style="width: 80px">单价7(USD)</th>
                                                    <th <%=code_hiden[7]%> style="width: 80px">采购量8</th>
                                                    <th <%=code_hiden[7]%> style="width: 80px">单价8(USD)</th>
                                                    <th <%=code_hiden[8]%> style="width: 80px">采购量9</th>
                                                    <th <%=code_hiden[8]%> style="width: 80px">单价9(USD)</th>
                                                    <th <%=code_hiden[9]%> style="width: 80px">采购量10</th>
                                                    <th <%=code_hiden[9]%> style="width: 80px">单价10(USD)</th>
                                                    <th <%=code_hiden[10]%> style="width: 80px">采购量11</th>
                                                    <th <%=code_hiden[10]%> style="width: 80px">单价11(USD)</th>
                                                    <th <%=code_hiden[11]%> style="width: 80px">采购量12</th>
                                                    <th <%=code_hiden[11]%> style="width: 80px">单价12(USD)</th>
                                                    <th <%=code_hiden[12]%> style="width: 80px">采购量13</th>
                                                    <th <%=code_hiden[12]%> style="width: 80px">单价13(USD)</th>
                                                    <th <%=code_hiden[13]%> style="width: 80px">采购量14</th>
                                                    <th <%=code_hiden[13]%> style="width: 80px">单价14(USD)</th>
                                                    <th <%=code_hiden[14]%> style="width: 80px">采购量15</th>
                                                    <th <%=code_hiden[14]%> style="width: 80px">单价15(USD)</th>
                                                    <th <%=code_hiden[15]%> style="width: 80px">采购量16</th>
                                                    <th <%=code_hiden[15]%> style="width: 80px">单价16(USD)</th>
                                                    <th <%=code_hiden[16]%> style="width: 80px">采购量17</th>
                                                    <th <%=code_hiden[16]%> style="width: 80px">单价17(USD)</th>
                                                    <th <%=code_hiden[17]%> style="width: 80px">采购量18</th>
                                                    <th <%=code_hiden[17]%> style="width: 80px">单价18(USD)</th>
                                                    <th <%=code_hiden[18]%> style="width: 80px">采购量19</th>
                                                    <th <%=code_hiden[18]%> style="width: 80px">单价19(USD)</th>
                                                    <th <%=code_hiden[19]%> style="width: 80px">采购量20</th>
                                                    <th <%=code_hiden[19]%> style="width: 80px">单价20(USD)</th>
                                                    <th <%=code_hiden[20]%> style="width: 80px">采购量21</th>
                                                    <th <%=code_hiden[20]%> style="width: 80px">单价21(USD)</th>
                                                    <th <%=code_hiden[21]%> style="width: 80px">采购量22</th>
                                                    <th <%=code_hiden[21]%> style="width: 80px">单价22(USD)</th>
                                                    <th <%=code_hiden[22]%> style="width: 80px">采购量23</th>
                                                    <th <%=code_hiden[22]%> style="width: 80px">单价23(USD)</th>
                                                    <th <%=code_hiden[23]%> style="width: 80px">采购量24</th>
                                                    <th <%=code_hiden[23]%> style="width: 80px">单价24(USD)</th>
                                                    <th <%=code_hiden[24]%> style="width: 80px">采购量25</th>
                                                    <th <%=code_hiden[24]%> style="width: 80px">单价25(USD)</th>
                                                    <th <%=code_hiden[25]%> style="width: 80px">采购量26</th>
                                                    <th <%=code_hiden[25]%> style="width: 80px">单价26(USD)</th>
                                                    <th <%=code_hiden[26]%> style="width: 80px">采购量27</th>
                                                    <th <%=code_hiden[26]%> style="width: 80px">单价27(USD)</th>
                                                    <th <%=code_hiden[27]%> style="width: 80px">采购量28</th>
                                                    <th <%=code_hiden[27]%> style="width: 80px">单价28(USD)</th>
                                                    <th <%=code_hiden[28]%> style="width: 80px">采购量29</th>
                                                    <th <%=code_hiden[28]%> style="width: 80px">单价29(USD)</th>
                                                    <th <%=code_hiden[29]%> style="width: 80px">采购量30</th>
                                                    <th <%=code_hiden[29]%> style="width: 80px">单价30(USD)</th>
                                                    <th <%=code_hiden[30]%> style="width: 80px">采购量31</th>
                                                    <th <%=code_hiden[30]%> style="width: 80px">单价31(USD)</th>
                                                    <th <%=code_hiden[31]%> style="width: 80px">采购量32</th>
                                                    <th <%=code_hiden[31]%> style="width: 80px">单价32(USD)</th>
                                                    <th <%=code_hiden[32]%> style="width: 80px">采购量33</th>
                                                    <th <%=code_hiden[32]%> style="width: 80px">单价33(USD)</th>
                                                    <th <%=code_hiden[33]%> style="width: 80px">采购量34</th>
                                                    <th <%=code_hiden[33]%> style="width: 80px">单价34(USD)</th>
                                                    <th <%=code_hiden[34]%> style="width: 80px">采购量35</th>
                                                    <th <%=code_hiden[34]%> style="width: 80px">单价35(USD)</th>
                                                    <th <%=code_hiden[35]%> style="width: 80px">采购量36</th>
                                                    <th <%=code_hiden[35]%> style="width: 80px">单价36(USD)</th>
                                                    <th <%=code_hiden[36]%> style="width: 80px">采购量37</th>
                                                    <th <%=code_hiden[36]%> style="width: 80px">单价37(USD)</th>
                                                    <th <%=code_hiden[37]%> style="width: 80px">采购量38</th>
                                                    <th <%=code_hiden[37]%> style="width: 80px">单价38(USD)</th>
                                                    <th <%=code_hiden[38]%> style="width: 80px">采购量39</th>
                                                    <th <%=code_hiden[38]%> style="width: 80px">单价39(USD)</th>
                                                    <th <%=code_hiden[39]%> style="width: 80px">采购量40</th>
                                                    <th <%=code_hiden[39]%> style="width: 80px">单价40(USD)</th>
                                                    <th <%=code_hiden[40]%> style="width: 80px">采购量41</th>
                                                    <th <%=code_hiden[40]%> style="width: 80px">单价41(USD)</th>
                                                    <th <%=code_hiden[41]%> style="width: 80px">采购量42</th>
                                                    <th <%=code_hiden[41]%> style="width: 80px">单价42(USD)</th>
                                                    <th <%=code_hiden[42]%> style="width: 80px">采购量43</th>
                                                    <th <%=code_hiden[42]%> style="width: 80px">单价43(USD)</th>
                                                    <th <%=code_hiden[43]%> style="width: 80px">采购量44</th>
                                                    <th <%=code_hiden[43]%> style="width: 80px">单价44(USD)</th>
                                                    <th <%=code_hiden[44]%> style="width: 80px">采购量45</th>
                                                    <th <%=code_hiden[44]%> style="width: 80px">单价45(USD)</th>
                                                    <th <%=code_hiden[45]%> style="width: 80px">采购量46</th>
                                                    <th <%=code_hiden[45]%> style="width: 80px">单价46(USD)</th>
                                                    <th <%=code_hiden[46]%> style="width: 80px">采购量47</th>
                                                    <th <%=code_hiden[46]%> style="width: 80px">单价47(USD)</th>
                                                    <th <%=code_hiden[47]%> style="width: 80px">采购量48</th>
                                                    <th <%=code_hiden[47]%> style="width: 80px">单价48(USD)</th>
                                                    <th <%=code_hiden[48]%> style="width: 80px">采购量49</th>
                                                    <th <%=code_hiden[48]%> style="width: 80px">单价49(USD)</th>
                                                    <th <%=code_hiden[49]%> style="width: 80px">采购量50</th>
                                                    <th <%=code_hiden[49]%> style="width: 80px">单价50(USD)</th>

                                                    <th style="width: 80px">
                                                        <asp:Label ID="Label3" ClientIDMode="Static" runat="server" Text="---"></asp:Label>采购量</th>
                                                    <th style="width: 80px">
                                                        <asp:Label ID="Label4" ClientIDMode="Static" runat="server" Text="---"></asp:Label>平均单价(USD)</th>
                                                    <th style="width: 80px">金額</th>
                                                    <th style="width: 80px">价差</th>
                                                    <th style="width: 80px">价差差异(%)</th>
                                                    <th style="width: 80px">差异金额</th>
                                                </tr>
                                            </thead>

                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("pmn04") %></td>
                                                            <td><%# Eval("ima02") %></td>
                                                            <td><%# Eval("pmn07") %></td>
                                                            <td><%# Eval("remark") %></td>
                                                            <td><%# Eval("remark2") %></td>
                                                            <td style="text-align: right"><%# Eval("pmn20_1") %></td>
                                                            <td style="text-align: right"><%# Eval("pmn31t_1") %></td>
                                                            <td <%=code_hiden[0]%> style="text-align: right"><%# Eval("0") %></td>
                                                            <td <%=code_hiden[0]%> style="text-align: right"><%# Eval("1") %></td>
                                                            <td <%=code_hiden[1]%> style="text-align: right"><%# Eval("2") %></td>
                                                            <td <%=code_hiden[1]%> style="text-align: right"><%# Eval("3") %></td>
                                                            <td <%=code_hiden[2]%> style="text-align: right"><%# Eval("4") %></td>
                                                            <td <%=code_hiden[2]%> style="text-align: right"><%# Eval("5") %></td>
                                                            <td <%=code_hiden[3]%> style="text-align: right"><%# Eval("6") %></td>
                                                            <td <%=code_hiden[3]%> style="text-align: right"><%# Eval("7") %></td>
                                                            <td <%=code_hiden[4]%> style="text-align: right"><%# Eval("8") %></td>
                                                            <td <%=code_hiden[4]%> style="text-align: right"><%# Eval("9") %></td>
                                                            <td <%=code_hiden[5]%> style="text-align: right"><%# Eval("10") %></td>
                                                            <td <%=code_hiden[5]%> style="text-align: right"><%# Eval("11") %></td>
                                                            <td <%=code_hiden[6]%> style="text-align: right"><%# Eval("12") %></td>
                                                            <td <%=code_hiden[6]%> style="text-align: right"><%# Eval("13") %></td>
                                                            <td <%=code_hiden[7]%> style="text-align: right"><%# Eval("14") %></td>
                                                            <td <%=code_hiden[7]%> style="text-align: right"><%# Eval("15") %></td>
                                                            <td <%=code_hiden[8]%> style="text-align: right"><%# Eval("16") %></td>
                                                            <td <%=code_hiden[8]%> style="text-align: right"><%# Eval("17") %></td>
                                                            <td <%=code_hiden[9]%> style="text-align: right"><%# Eval("18") %></td>
                                                            <td <%=code_hiden[9]%> style="text-align: right"><%# Eval("19") %></td>
                                                            <td <%=code_hiden[10]%> style="text-align: right"><%# Eval("20") %></td>
                                                            <td <%=code_hiden[10]%> style="text-align: right"><%# Eval("21") %></td>
                                                            <td <%=code_hiden[11]%> style="text-align: right"><%# Eval("22") %></td>
                                                            <td <%=code_hiden[11]%> style="text-align: right"><%# Eval("23") %></td>
                                                            <td <%=code_hiden[12]%> style="text-align: right"><%# Eval("24") %></td>
                                                            <td <%=code_hiden[12]%> style="text-align: right"><%# Eval("25") %></td>
                                                            <td <%=code_hiden[13]%> style="text-align: right"><%# Eval("26") %></td>
                                                            <td <%=code_hiden[13]%> style="text-align: right"><%# Eval("27") %></td>
                                                            <td <%=code_hiden[14]%> style="text-align: right"><%# Eval("28") %></td>
                                                            <td <%=code_hiden[14]%> style="text-align: right"><%# Eval("29") %></td>
                                                            <td <%=code_hiden[15]%> style="text-align: right"><%# Eval("30") %></td>
                                                            <td <%=code_hiden[15]%> style="text-align: right"><%# Eval("31") %></td>
                                                            <td <%=code_hiden[16]%> style="text-align: right"><%# Eval("32") %></td>
                                                            <td <%=code_hiden[16]%> style="text-align: right"><%# Eval("33") %></td>
                                                            <td <%=code_hiden[17]%> style="text-align: right"><%# Eval("34") %></td>
                                                            <td <%=code_hiden[17]%> style="text-align: right"><%# Eval("35") %></td>
                                                            <td <%=code_hiden[18]%> style="text-align: right"><%# Eval("36") %></td>
                                                            <td <%=code_hiden[18]%> style="text-align: right"><%# Eval("37") %></td>
                                                            <td <%=code_hiden[19]%> style="text-align: right"><%# Eval("38") %></td>
                                                            <td <%=code_hiden[19]%> style="text-align: right"><%# Eval("39") %></td>
                                                            <td <%=code_hiden[20]%> style="text-align: right"><%# Eval("40") %></td>
                                                            <td <%=code_hiden[20]%> style="text-align: right"><%# Eval("41") %></td>
                                                            <td <%=code_hiden[21]%> style="text-align: right"><%# Eval("42") %></td>
                                                            <td <%=code_hiden[21]%> style="text-align: right"><%# Eval("43") %></td>
                                                            <td <%=code_hiden[22]%> style="text-align: right"><%# Eval("44") %></td>
                                                            <td <%=code_hiden[22]%> style="text-align: right"><%# Eval("45") %></td>
                                                            <td <%=code_hiden[23]%> style="text-align: right"><%# Eval("46") %></td>
                                                            <td <%=code_hiden[23]%> style="text-align: right"><%# Eval("47") %></td>
                                                            <td <%=code_hiden[24]%> style="text-align: right"><%# Eval("48") %></td>
                                                            <td <%=code_hiden[24]%> style="text-align: right"><%# Eval("49") %></td>
                                                            <td <%=code_hiden[25]%> style="text-align: right"><%# Eval("50") %></td>
                                                            <td <%=code_hiden[25]%> style="text-align: right"><%# Eval("51") %></td>
                                                            <td <%=code_hiden[26]%> style="text-align: right"><%# Eval("52") %></td>
                                                            <td <%=code_hiden[26]%> style="text-align: right"><%# Eval("53") %></td>
                                                            <td <%=code_hiden[27]%> style="text-align: right"><%# Eval("54") %></td>
                                                            <td <%=code_hiden[27]%> style="text-align: right"><%# Eval("55") %></td>
                                                            <td <%=code_hiden[28]%> style="text-align: right"><%# Eval("56") %></td>
                                                            <td <%=code_hiden[28]%> style="text-align: right"><%# Eval("57") %></td>
                                                            <td <%=code_hiden[29]%> style="text-align: right"><%# Eval("58") %></td>
                                                            <td <%=code_hiden[29]%> style="text-align: right"><%# Eval("59") %></td>
                                                            <td <%=code_hiden[30]%> style="text-align: right"><%# Eval("60") %></td>
                                                            <td <%=code_hiden[30]%> style="text-align: right"><%# Eval("61") %></td>
                                                            <td <%=code_hiden[31]%> style="text-align: right"><%# Eval("62") %></td>
                                                            <td <%=code_hiden[31]%> style="text-align: right"><%# Eval("63") %></td>
                                                            <td <%=code_hiden[32]%> style="text-align: right"><%# Eval("64") %></td>
                                                            <td <%=code_hiden[32]%> style="text-align: right"><%# Eval("65") %></td>
                                                            <td <%=code_hiden[33]%> style="text-align: right"><%# Eval("66") %></td>
                                                            <td <%=code_hiden[33]%> style="text-align: right"><%# Eval("67") %></td>
                                                            <td <%=code_hiden[34]%> style="text-align: right"><%# Eval("68") %></td>
                                                            <td <%=code_hiden[34]%> style="text-align: right"><%# Eval("69") %></td>
                                                            <td <%=code_hiden[35]%> style="text-align: right"><%# Eval("70") %></td>
                                                            <td <%=code_hiden[35]%> style="text-align: right"><%# Eval("71") %></td>
                                                            <td <%=code_hiden[36]%> style="text-align: right"><%# Eval("72") %></td>
                                                            <td <%=code_hiden[36]%> style="text-align: right"><%# Eval("73") %></td>
                                                            <td <%=code_hiden[37]%> style="text-align: right"><%# Eval("74") %></td>
                                                            <td <%=code_hiden[37]%> style="text-align: right"><%# Eval("75") %></td>
                                                            <td <%=code_hiden[38]%> style="text-align: right"><%# Eval("76") %></td>
                                                            <td <%=code_hiden[38]%> style="text-align: right"><%# Eval("77") %></td>
                                                            <td <%=code_hiden[39]%> style="text-align: right"><%# Eval("78") %></td>
                                                            <td <%=code_hiden[39]%> style="text-align: right"><%# Eval("79") %></td>
                                                            <td <%=code_hiden[40]%> style="text-align: right"><%# Eval("80") %></td>
                                                            <td <%=code_hiden[40]%> style="text-align: right"><%# Eval("81") %></td>
                                                            <td <%=code_hiden[41]%> style="text-align: right"><%# Eval("82") %></td>
                                                            <td <%=code_hiden[41]%> style="text-align: right"><%# Eval("83") %></td>
                                                            <td <%=code_hiden[42]%> style="text-align: right"><%# Eval("84") %></td>
                                                            <td <%=code_hiden[42]%> style="text-align: right"><%# Eval("85") %></td>
                                                            <td <%=code_hiden[43]%> style="text-align: right"><%# Eval("86") %></td>
                                                            <td <%=code_hiden[43]%> style="text-align: right"><%# Eval("87") %></td>
                                                            <td <%=code_hiden[44]%> style="text-align: right"><%# Eval("88") %></td>
                                                            <td <%=code_hiden[44]%> style="text-align: right"><%# Eval("89") %></td>
                                                            <td <%=code_hiden[45]%> style="text-align: right"><%# Eval("90") %></td>
                                                            <td <%=code_hiden[45]%> style="text-align: right"><%# Eval("91") %></td>
                                                            <td <%=code_hiden[46]%> style="text-align: right"><%# Eval("92") %></td>
                                                            <td <%=code_hiden[46]%> style="text-align: right"><%# Eval("93") %></td>
                                                            <td <%=code_hiden[47]%> style="text-align: right"><%# Eval("94") %></td>
                                                            <td <%=code_hiden[47]%> style="text-align: right"><%# Eval("95") %></td>
                                                            <td <%=code_hiden[48]%> style="text-align: right"><%# Eval("96") %></td>
                                                            <td <%=code_hiden[48]%> style="text-align: right"><%# Eval("97") %></td>
                                                            <td <%=code_hiden[49]%> style="text-align: right"><%# Eval("98") %></td>
                                                            <td <%=code_hiden[49]%> style="text-align: right"><%# Eval("99") %></td>

                                                            <td style="text-align: right"><%# Eval("pmn20_2") %></td>
                                                            <td style="text-align: right"><%# Eval("pmn31t_2") %></td>
                                                            <td style="text-align: right"><%# Eval("sum_price_2") %></td>
                                                            <td style="text-align: right"><%# Eval("price_diff") %></td>
                                                            <td style="text-align: right"><%# Eval("price_diff_percent") %></td>
                                                            <td style="text-align: right"><%# Eval("price_diff2") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
   
    <!--Reference the SignalR library. -->
    <!--Reference the autogenerated SignalR hub script. -->
    <%--<script src="/signalr/hubs"></script>--%>

    <script>

        var count = 0;
        var colors = ["none", "green", "yellow", "red"];
        function getcolor() {

            return 'green';
        }

        function formatSeconds(seconds) {
            var date = new Date(1970, 0, 1);
            date.setSeconds(seconds);
            return date.toTimeString().replace(/.*(\d{2}:\d{2}:\d{2}).*/, "$1");
        }
        function getmachinedata(value, value2) {
            $('#data').empty();
            $('#mamay').text(value2);
            $('#hmamay').attr("value", value);
            document.getElementById("btnMachine").innerText = value2;
        }
        function loadDoc(area) {
            var xhttp;
            if (window.XMLHttpRequest) {
                // code for modern browsers
                xhttp = new XMLHttpRequest();
            } else {
                // code for IE6, IE5
                xhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhttp.onreadystatechange = function () {
                if (xhttp.readyState == 4 && xhttp.status == 200) {
                    document.getElementById("lstmachine").innerHTML = xhttp.responseText;
                }
            };
            document.getElementById("btnArea").innerText = area;

            xhttp.open("GET", "/sys/getmachines.aspx?m=" + area, true);
            xhttp.send();
        }

    </script>

    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>
     <script src="/js/foundation-datepicker.js"></script>
    <script src="/js/foundation-datepicker.zh-CN.js"></script>

    <script type="text/javascript">
        //var currentdate = new Date();
        //var datetime1 = currentdate.getDate() + "/"
        //                + (currentdate.getMonth() + 1) + "/"
        //                + currentdate.getFullYear() + " 0:0";
        //var datetime2 =  currentdate.getDate() + "/"
        //                + (currentdate.getMonth() + 1) + "/"
        //                + currentdate.getFullYear() + " 23:59";

        $.datetimepicker.setLocale('en');
        $('#date1').datetimepicker({ format: 'd/m/Y' });
        $('#date2').datetimepicker({ format: 'd/m/Y' });

        $('#textyear1').fdatepicker({
            format: 'yyyy/mm/dd',
        });
        $('#textyear2').fdatepicker({
            format: 'yyyy/mm/dd',
        });
        $('#textyear1_to').fdatepicker({
            format: 'yyyy/mm/dd',
        });
        $('#textyear2_to').fdatepicker({
            format: 'yyyy/mm/dd',
        });

       

    </script>
</asp:Content>
