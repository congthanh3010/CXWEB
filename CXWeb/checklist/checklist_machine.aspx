<%@ Page  EnableViewState="true" EnableEventValidation= "false "   Language="C#" MasterPageFile="~/Site_checklist.Master" AutoEventWireup="true" CodeBehind="checklist_machine.aspx.cs" Inherits="CXWeb.checklist.checklist_machine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
    if (isNaN(position)) {
        position = 0;
    }
    div.scrollTop = position;
    div.onscroll = function () {
        div_position.value = div.scrollTop;
    };
};
    </script>

    <input type="hidden" id="div_position" name="div_position" />
   
    <asp:Button ID="btn1" Style="font-size: large;" runat="server" Text="返回并保存 Quay về trước và lưu lại" OnClick="btn1_Click" Font-Bold="true"></asp:Button>
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>--%>
   
    <div id="dvScroll" style="position: absolute; width: 100%; height: 86%; padding: 0px; overflow: auto;">

        <asp:HiddenField ClientIDMode="Static" ID="hmamay" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hsub_unit" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hmgroup" runat="server" />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="400%"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" BackColor="#CACBE1"
            ShowFooter="false" OnRowCommand="GridView1_RowCommand">
            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" />
            <RowStyle Font-Names="Calibri" Font-Size="16px" />
            <Columns>

                <asp:TemplateField HeaderText="操作 Thao tác" HeaderStyle-Width="50px">
                    <EditItemTemplate>
                        <%--<asp:LinkButton ID="btnUpdate" CommandName="Update" runat="server">Update</asp:LinkButton>--%>
                        <asp:LinkButton ID="btnCancel" CommandName="Cancel" runat="server">取消 Hủy</asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit">更改 Sửa  </asp:LinkButton>

                    </ItemTemplate>
                    <%--  <FooterTemplate>
                              <asp:LinkButton ID="btnBack" runat="server" CommandName="Back">Back</asp:LinkButton>
                        </FooterTemplate>--%>
                       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="機台 chi tiết máy" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Label ID="lblcol01" runat="server" Text='<%# Eval("1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作員 技術員 TTV KTV" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Label ID="lblcol02" runat="server" Text='<%# Eval("2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol03" type="number" Width="100%" runat="server" Text='<%# Eval("3") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol03" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol03" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("3") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol04" type="number" Width="100%" runat="server" Text='<%# Eval("4") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol04" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol04" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol04" runat="server" Text='<%# Eval("4") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol05" type="number" Width="100%" runat="server" Text='<%# Eval("5") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol05" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol05" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol05" runat="server" Text='<%# Eval("5") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol06" type="number" Width="100%" runat="server" Text='<%# Eval("6") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol06" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol06" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol06" runat="server" Text='<%# Eval("6") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol07" type="number" Width="100%" runat="server" Text='<%# Eval("7") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol07" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol07" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol07" runat="server" Text='<%# Eval("7") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol08" type="number" Width="100%" runat="server" Text='<%# Eval("8") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol08" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol08" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol08" runat="server" Text='<%# Eval("8") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol09" type="number" Width="100%" runat="server" Text='<%# Eval("9") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol09" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol09" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol09" runat="server" Text='<%# Eval("9") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol10" type="number" Width="100%" runat="server" Text='<%# Eval("10") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol10" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol10" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol10" runat="server" Text='<%# Eval("10") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol11" type="number" Width="100%" runat="server" Text='<%# Eval("11") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol11" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol11" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol11" runat="server" Text='<%# Eval("11") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol12" type="number" Width="100%" runat="server" Text='<%# Eval("12") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol12" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol12" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol12" runat="server" Text='<%# Eval("12") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol13" type="number" Width="100%" runat="server" Text='<%# Eval("13") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol13" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol13" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol13" runat="server" Text='<%# Eval("13") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol14" type="number" Width="100%" runat="server" Text='<%# Eval("14") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol14" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol14" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol14" runat="server" Text='<%# Eval("14") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol15" type="number" Width="100%" runat="server" Text='<%# Eval("15") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol15" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol15" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol15" runat="server" Text='<%# Eval("15") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol16" type="number" Width="100%" runat="server" Text='<%# Eval("16") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol16" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol16" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol16" runat="server" Text='<%# Eval("16") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol17" type="number" Width="100%" runat="server" Text='<%# Eval("17") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol17" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol17" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol17" runat="server" Text='<%# Eval("17") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol18" type="number" Width="100%" runat="server" Text='<%# Eval("18") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol18" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol18" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol18" runat="server" Text='<%# Eval("18") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol19" type="number" Width="100%" runat="server" Text='<%# Eval("19") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol19" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol19" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol19" runat="server" Text='<%# Eval("19") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol20" type="number" Width="100%" runat="server" Text='<%# Eval("20") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol20" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol20" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>


                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol20" runat="server" Text='<%# Eval("20") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tổng điểm 總分" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Label ID="lblcolSum" runat="server" Text='<%# Eval("colSum") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作 Thao tác" HeaderStyle-Width="50px">
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdate" CommandName="Update" runat="server">保存 Lưu lại</asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <table  class="table-bordered table-striped" style="width:100%;font-size:large;text-align:left;" <%=prob_table_code %>  >
            <tr>
                <th style="text-align:left;background-color:#006699;color:white;">
                      機台異常報告 Báo cáo vấn đề máy
                </th>
            </tr>
              <tr>
                <td>
                    <asp:TextBox ID="txtProb_desc" ClientIDMode="Static" TextMode="MultiLine" Style="width: 100%;height:100px;" runat="server" CssClass="some_class"></asp:TextBox>
        
                </td>
            </tr>
              <tr>
                <td  style="text-align:left;">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="348px" Height="27px" />
                </td>
            </tr>
              <tr>
                <td  style="text-align:left;">
                    <asp:Button ID="btnUpload" runat="server"  Font-Bold="true" Text="上傳 Lưu lại" Height="27px" OnClick="btnUpload_Click" />
                 
                  
                      </td>
            </tr>
            <tr>
                <td  style="text-align:left;">
                    <asp:RadioButton id="rb1" Text="未解決<br/>Chưa giải quyết" Checked="true" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true" runat="server" GroupName="Source"></asp:RadioButton>
                    <asp:RadioButton id="rb2" Text="已接收<br/>Đã tiếp nhận" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true" runat="server" GroupName="Source"></asp:RadioButton>
                    <asp:RadioButton id="rb3" Text="已解決<br/>Đã giải quyết" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true" runat="server" GroupName="Source"></asp:RadioButton>
                    <asp:RadioButton id="rb4" Text="全部<br/>Tất cả" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="true" runat="server" GroupName="Source"></asp:RadioButton>

                </td>
            </tr>
             <tr>
                <td>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" 
            OnRowCancelingEdit="GridView2_RowCancelingEdit"
            OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing"
            OnRowUpdating="GridView2_RowUpdating" BackColor="#CACBE1"
            ShowFooter="false" OnRowCommand="GridView2_RowCommand">
            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="cambria" ForeColor="White" />
            <RowStyle Font-Names="Calibri" Font-Size="16px" />
            <Columns>
                <asp:TemplateField HeaderText="機台<br/>Máy" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblMa_no" runat="server" Text='<%# Eval("machine_no") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="日期<br/>Ngày" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblprob_date" runat="server" Text='<%# Eval("problem_start_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="異常<br/>Vấn đề" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblprob_desc" runat="server" Text='<%# Eval("problem_desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="異常圖片<br/>Trước" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Image ID="ImageProb" runat="server" ImageUrl='<%# Eval("problem_picture_addr") %>'  Width="150px"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="接收<br/>Tiếp nhận" HeaderStyle-Width="10%">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAcc" Width="100%" runat="server" Text='<%# Eval("accept_desc") %>'></asp:TextBox>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAcc" runat="server" Text='<%# Eval("accept_desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="預計完成<br/>Dự kiến hoàn thành" HeaderStyle-Width="10%">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPreF"  ClientIDMode="Static" Width="100%" runat="server"  CssClass="some_class" Text='<%# Eval("predict_finish_date") %>'></asp:TextBox>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPreF" runat="server" Text='<%# Eval("predict_finish_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="解决<br/>Giải quyết" HeaderStyle-Width="10%">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSol" Width="100%" runat="server" Text='<%# Eval("solution_desc") %>'></asp:TextBox>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSol" runat="server" Text='<%# Eval("solution_desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="解决后<br/>Sau" HeaderStyle-Width="10%">
                    <EditItemTemplate>
                        <asp:FileUpload ID="FileUploadSol" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Image ID="ImageSol" runat="server" ImageUrl='<%# Eval("solution_picture_addr") %>'  Width="150px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderStyle-Width="100px" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcol03" type="number" Width="100%" runat="server" Text='<%# Eval("3") %>'></asp:TextBox>

                        <asp:RangeValidator ID="RVCol03" Type="Integer" MinimumValue="0" MaximumValue="20"
                            ControlToValidate="txtcol03" runat="server" ErrorMessage="Error" ForeColor="Red"></asp:RangeValidator>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblcol03" runat="server" Text='<%# Eval("3") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="操作<br/>Thao tác" HeaderStyle-Width="5%">

                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit">更改 Sửa  </asp:LinkButton>

                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnCancel" CommandName="Cancel" runat="server">取消 Hủy</asp:LinkButton>
                        <asp:LinkButton ID="btnUpdate" CommandName="Update" runat="server">保存 Lưu lại</asp:LinkButton>
                    </EditItemTemplate>

                    <%--  <FooterTemplate>
                              <asp:LinkButton ID="btnBack" runat="server" CommandName="Back">Back</asp:LinkButton>
                        </FooterTemplate>--%>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                </td>
            </tr>
        </table>
      
        
        
        <%--<br />
        <br />--%>

        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1" Display="None" ErrorMessage="Bạn cần chọn một tệp ảnh trước khi ấn nút &quot;Upload&quot;"></asp:RequiredFieldValidator>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />--%>
        <%--<asp:Image ID="Image1" runat="server" Width="150px" />--%>

        <%-- <table class=" table table-bordered table-striped" >
             
                    <thead>
                        <tr>
                            <th colspan="5">Machine:
                               <asp:Label ID="mamay" ClientIDMode="Static" runat="server" Text="---"></asp:Label> </th>
                            
                        </tr>
                        <tr>

                            <th style="width: 135px">Time</th>
                            <th style="width: 150px">P/N</th>
                            <th style="width: 95px">Process</th>
                            <th style="width: 95px">Times/Min</th>
                            <th style="width: 320px">Status</th>

                        </tr>
                    </thead>

                        <tbody id="data">

                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td > <%# Eval("machine_no") %></td>
                                    <td ><%# Eval("picture_addr") %></td>
                                    <td <%# Eval("code") %>>
                                   
                                    </td>
                                    <td ></td>
                                    <td ></td>

                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tbody>                 
        
            </table>--%>

        

    </div>
   
           
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>
    <script src="/js/foundation-datepicker.js"></script>
    <script src="/js/foundation-datepicker.zh-CN.js"></script>
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="/css/foundation-datepicker.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">

        $.datetimepicker.setLocale('en');

        $('#txtPreF').fdatepicker({
            format: 'yyyy/mm/dd',
        });
      
      

    </script>
</asp:Content>
