<%@ Page Title="" Language="C#" MasterPageFile="~/Site_schedule.Master" AutoEventWireup="true" CodeBehind="machine_status_upload_New.aspx.cs" Inherits="CXWeb.plc.machine_status_upload_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <style>
.btn-group button {

  background-color: #DDDDDD; /* Green background */
  border: 1px solid green; /* Green border */
  color: #000000; /* Black text */
  padding: 10px 24px; /* Some padding */
  border-radius:16px;/*border button*/
  cursor: pointer; /* Pointer/hand icon */
  /*float: left;*/ /* Float the buttons side by side */
  /*vertical-align: middle;*/
  font-size: 15px;
  text-align: center;
}

/* Clear floats (clearfix hack) */
.btn-group:after {
  content: "";
  clear: both;
  display: table;
}

.btn-group button:not(:last-child) {
  border-right: none; /* Prevent double borders */
}

/* Add a background color on hover */
.btn-group button:hover {
  background-color: #b02c2c;/*#AAAAAA*/
}
.btn:hover{ background-color: #b02c2c}

</style>
<style>
/* width */
::-webkit-scrollbar {
  width: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1; 
}
 
/* Handle */
::-webkit-scrollbar-thumb {
  background: #888; 
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555; 
}
</style>
<style>
    .myButton {
	box-shadow: 0px 0px 0px 1px #ded3d3;
	/*background:linear-gradient(to bottom, #7892c2 5%, #476e9e 100%);
	background-color:#d2d2d2;*/
	border-radius:16px;
    color: #000000; /* Black text */
	border:1px solid green /*#4e6096*/;
	display:inline-block;
	cursor:pointer;
	font-family:'Times New Roman';
	font-size:15px;
	padding:14px 14px;
	text-decoration:none;
    text-align:center;
    /*white-space:normal;*/
    height: 50px;
    /*word-break:break-all;*/
	/*text-shadow:0px 1px 0px #283966;*/
}

.myButton:hover {
	background:linear-gradient(to bottom, #ded3d3 5%, #b39898 100%);
	background-color:#c9b6b6;
}
.myButton:active {
	position:relative;
	top:1px;
}
    .auto-style1 {
        width: 260px;
    }
    .auto-style2 {
        width: 120px;
    }
    .auto-style10 {
        width: 109px;
        float: left;
        height: 28px;
    }
    .auto-style11 {
        width: 145px;
    }
    .auto-style12 {
        width: 94px;
    }
    .auto-style13 {
        width: 80%;
    }
    .auto-style16 {
        width: 147px;
    }
    .auto-style17 {
        width: 66px;
    }
    .auto-style18 {
        width: 100%;
        height: 39px;
    }
    .auto-style19 {
        width: 298px;
    }
    .auto-style20 {
        float: left;
        width: 254px;
    }
    .auto-style21 {
        width: 150px;
    }
    .auto-style22 {
        margin-left: 20
    }
    .auto-style23 {
        width: 432px;
    }
    .auto-style24 {
        width: 350px;
    }
</style>

     <style type="text/css">
        .work-log-input-col-1 {
            text-align: right;
            padding: 2px;
            padding-right: 5px;
        }

        .work-log-input-col-2 {
            text-align: left;
            padding: 2px;
        }

        .work-log-input-col-2 input {
            width: 100%;
        }

        /* Chrome, Safari, Edge, Opera */
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        input[type=number] {
            -moz-appearance: textfield;
            height: 28px;
        }

        input[type=text] {
            height: 28px;
        }

        select {
            height: 28px;
        }

        .grvContainer {
            width: 100%;
            height: 350px;
            overflow: auto;
            position: relative;
            z-index: 2;
        }

        .grvWLog {
            position: absolute;
        }

        .grvHeader {
            background-color: #006699;
            text-align: center;
            color: white;
            font-weight: bold;
            font-family: Cambria;
        }
    </style>

  <div style="overflow:auto"  >
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <div class="container" >
          <table >
              <tr>
                  <td style="padding: 0;">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          
                        <ContentTemplate>
                            <div id="frmTitle" style="background: #006699; text-align: center;">
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" ForeColor="White" Text="CẬP NHẬT TÌNH TRẠNG MÁY"></asp:Label>
                            </div>
                            <div id="frmInput" style="background: #CACBE1; padding: 5px;">
                                 <table class="work-log-input" style="width: 100%; padding: 0; border: 0; font-size: 16px;">
                                    <tr>
                                        <td class="work-log-input-col-1">Ngày</td>
                                        <td class="work-log-input-col-2">
                                             <asp:TextBox ID="report_date" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Thời gian</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="status_time_start" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class"></asp:TextBox>
                                        </td>
                                        <td></td>

                                        <td class="work-log-input-col-1">Ngày Kt</td>
                                        <td class="work-log-input-col-2">
                                             <asp:TextBox ID="report_dateEnd" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">TG KT</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="status_time_end" Style="width: 120px;font-size: 20px;text-align:left; margin-left: 0;" ClientIDMode="Static" runat="server" CssClass="some_class" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>   
                                    <tr>
                                        <td class="work-log-input-col-1">Mã Nhân viên</td>
                                        <td class="work-log-input-col-2">
                                              <asp:DropDownList ID="txtMa_Nv0" Style="width: 60px;font-size: 20px;height: 30px" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="txtMa_Nv1_SelectedIndexChanged" ></asp:DropDownList>
                                              <asp:TextBox ID="txtMa_So"  Style="font-size: 20px;" runat="server" Width="80px" MaxLength ="4" OnTextChanged="txtMa_So_TextChanged"  AutoPostBack="true" ></asp:TextBox>
                                              <asp:TextBox ID="TxtMa_Nv"  Style="font-size: 20px;" runat="server" ReadOnly ="true" Width="100%"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Công đoạn 工序</td>
                                        <td class="work-log-input-col-2">
                                             <asp:DropDownList ID="ddlQua_Trinh" runat="server" >
                                                <asp:ListItem Value="P00" Text=""></asp:ListItem>
                                                <asp:ListItem Value="P10" Text="P10 XuỐNG PHÔI"></asp:ListItem>
                                                <asp:ListItem Value="P30" Text="P30 DẬP ĐiỂM"></asp:ListItem>
                                                <asp:ListItem Value="T10" Text="T10 DẬP NẤM"></asp:ListItem>
                                                <asp:ListItem Value="T22" Text="T22 DẬP NẤM PFM"></asp:ListItem>
                                                <asp:ListItem Value="T11" Text="T11 CHỈNH HÌNH 1"></asp:ListItem>
                                                <asp:ListItem Value="T12" Text="T12 CHỈNH HÌNH 2"></asp:ListItem>
                                                <asp:ListItem Value="T13" Text="T13 CHỈNH HÌNH 3"></asp:ListItem>
                                                <asp:ListItem Value="T14" Text="T14 CHỈNH HÌNH 4"></asp:ListItem>
                                                <asp:ListItem Value="T18" Text="T18 LIÊN HỢP CHỈNH HÌNH"></asp:ListItem>
                                                <asp:ListItem Value="T20" Text="T20 THÀNH HÌNH"></asp:ListItem>
                                                <asp:ListItem Value="T21" Text="T21 LIÊN HỢP THÀNH  HÌNH"></asp:ListItem>                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Mã NV làm Khuôn</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtUser_Mod"  Style="font-size: 20px;text-transform: uppercase;" runat="server" Width="120px" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Chủng Loại</td>
                                        <td class="work-log-input-col-2">
                                            <asp:TextBox ID="txtChung_Loai"  Style="font-size: 20px;text-transform: uppercase;" runat="server"  Width="200px"></asp:TextBox>
                                        </td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td class="work-log-input-col-1">Trạm</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="txtTram" Style="font-size: 20px;" runat="server" Width="110px" AutoPostBack="true"  OnSelectedIndexChanged="txtTram_SelectedIndexChanged" CssClass="auto-style22"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">Máy</td>
                                        <td class="work-log-input-col-2">
                                            <asp:DropDownList ID="txtMachine_no" Style="font-size: 20px;" runat="server" Width="110px"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td class="work-log-input-col-1">
                                            <asp:Button ID="btnSave" Text="Lưu" runat="server"  OnClick="btnSave_Click" Width="120px" CssClass="col-xs-offset-0" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    
                                 </table>
                            </div>
                        </ContentTemplate>
                          <Triggers>
                                    <asp:PostBackTrigger ControlID ="btnSave" />
                                    <asp:PostBackTrigger ControlID="TxtMa_Nv" />
                              <asp:PostBackTrigger ControlID="txtMa_So" />
                              <asp:PostBackTrigger ControlID="txtMa_Nv0" />
                         </Triggers>
                      </asp:UpdatePanel>

                  </td>
             </tr>
         
          </table>
           <div >
         <table style="margin: 0 auto;text-align:left;" >
            <tr>
                <td  colspan="8"  style=" font-size: 20px; color: #f00; float:left; ">
                                    <%=report_msg%>                                   
                                </td>  
            </tr>
            <tr>
                 <td colspan="8"  style="width:600px; font-size: 20px; color: #f00;font-weight :900; float:left;">  
                     TRẠNG THÁI   </td>

             </tr>
            </table>
      </div>


     <div class="container" style=" position: absolute; "><%--overflow: auto;--%>
          
        <div   style="width:100%; text-align: center; height:60px;" >
       
            <asp:Button ID="Button17" style="width:20%;   margin: 0 auto;  border-radius:16px;display:inline-block;"  CssClass="myButton"  runat="server" ToolTip="A01:正常生產 (Sản xuất bình thường)" Text="A01:(Sản xuất bình thường)正常生產 " OnClick="btn_Button17"> </asp:Button>
            <asp:Button ID="Button6"  style="width:20%;   margin: 0 auto;  border-radius:16px;" CssClass="myButton"  runat="server" ToolTip="B01:休息時間 (Thời gian nghỉ ngơi)" Text="B01:(Thời gian nghỉ ngơi)休息時間 "  OnClick="btn_Button6"></asp:Button>
            <asp:Button ID="Button7"  style="width:20%;   margin: 0 auto;  border-radius:16px;" CssClass="myButton" runat="server" ToolTip="C01:缺單 (Thiếu đơn)" Text="C01:(Thiếu đơn)缺單"  OnClick="btn_Button7"></asp:Button>
            <asp:Button ID="Button8"  style="width:20%;   margin: 0 auto;  border-radius:16px;" CssClass="myButton" runat="server" ToolTip="C02:教育訓練 (Giáo dục huấn luyện)" Text="C02:(Giáo dục huấn luyện)教育訓練 "  OnClick="btn_Button8"></asp:Button>       

       
        </div>
        <div  class="btn-group" style="width:100%; text-align: center; height:60px;">
        <asp:Button ID="Button1"  style="width:20%; overflow: unset;    border-radius:16px;" CssClass="myButton" runat="server" ToolTip="C03:機台保養(Bảo dưỡng máy)" Text= "C03:(Bảo dưỡng máy)機台保養"   OnClick="btn_Button1"></asp:Button>     
        <asp:Button ID="Button2"  style="width:20%;    border-radius:16px;" CssClass="myButton" runat="server" ToolTip="C04:樣品試作 (Thử mẫu)" Text="C04:(Thử mẫu)樣品試作"  OnClick="btn_Button2"></asp:Button>
        <asp:Button ID="Button3"  style="width:20%;    border-radius:16px;" CssClass="myButton" runat="server" ToolTip="C05:調機 (Chỉnh máy)" Text="C05:(Chỉnh máy)調機 "  OnClick="btn_Button3"></asp:Button>
        <asp:Button ID="Button4"  style="width:20%;    border-radius:16px;" CssClass="myButton" runat="server" ToolTip="C06:換模 (Thay khuôn)" Text="C06:(Thay khuôn)換模"  OnClick="btn_Button4"></asp:Button>
        </div>
        <div  class="btn-group" style="width:100%; text-align: center; height:60px;">
        <asp:Button ID="Button5"  style="width:20%;    border-radius:16px;" CssClass="myButton" runat="server" ToolTip="C07:模具保養 &#13;&#10;(Bảo dưỡng khuôn)" Text="C07:(Bảo dưỡng khuôn)模具保養" OnClick="btn_Button5"></asp:Button>     
        <asp:Button ID="Button9"  style="width:20%;    border-radius:16px;" CssClass="myButton" runat="server" ToolTip="D01:設備故障 (Thiết bị sự cố)" Text="D01:(Thiết bị sự cố)設備故障" OnClick="btn_Button9"></asp:Button>
        <asp:Button ID="Button11"  style="width:20%;   border-radius:16px;" CssClass="myButton" runat="server" ToolTip="D02:修模待模 (Sửa khuôn chờ khuôn)" Text="D02:(Sửa khuôn chờ khuôn)修模待模 " OnClick="btn_Button11" ></asp:Button>
        <asp:Button ID="Button10"  style="width:20%;   border-radius:16px;" CssClass="myButton" runat="server" ToolTip="D03:缺料 (Thiếu phôi)" Text="D03:(Thiếu phôi)缺料" OnClick="btn_Button10" ></asp:Button>
        </div>
       
         <div  class="btn-group" style="width:100%; text-align: center; height:60px;">     
        <asp:Button ID="Button13"  style="width:20%;   border-radius:16px;" CssClass="myButton" runat="server" ToolTip="D04:缺工 (Thiếu người)" Text="D04:(Thiếu người)缺工" OnClick="btn_Button13" ></asp:Button>
        <asp:Button ID="Button14"  style="width:20%;   border-radius:16px;" CssClass="myButton" runat="server" ToolTip="D05:質量檢討 (Kiểm thảo chất lượng)" Text="D05:(Kiểm thảo chất lượng)質量檢討 " OnClick="btn_Button14" ></asp:Button>
        <asp:Button ID="Button12"  style="width:20%;   border-radius:16px;" CssClass="myButton" runat="server" ToolTip="D06:其他 (Khác)" Text="D06: (Khác)其他" OnClick="Button12_Click" ></asp:Button>
        <asp:Button ID="Button15"  style="width:20%;   border-radius:16px;" CssClass="myButton" runat="server" ToolTip="...." Text="....  " OnClick="btn_Button15" ></asp:Button>       
        </div>
            
    </div>
    </div>
      
    </div>
    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
     <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>
     <script src="/js/foundation-datepicker.js"></script>
    <script src="/js/foundation-datepicker.zh-CN.js"></script>
 
     <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />

    
    <script type="text/javascript">

        $.datetimepicker.setLocale('en');

        $('#report_dateEnd').fdatepicker({
            format: 'yyyy-mm-dd',
            width: 100
        });

    </script>

    <script src="/js/jquery.js"></script>
    <script>
        // Wait until the DOM has loaded before querying the document
        $(document).ready(function () {
            $('table.tabs').each(function () {
                // For each set of tabs, we want to keep track of
                // which tab is active and it's associated content
                var $active, $content, $links = $(this).find('a');

                // If the location.hash matches one of the links, use that as the active tab.
                // If no match is found, use the first link as the initial active tab.
                $active = $($links.filter('[href="' + location.hash + '"]')[0] || $links[0]);
                $active.addClass('active');
                $content = $($active.attr('href'));

                // Hide the remaining content
                $links.not($active).each(function () {
                    $($(this).attr('href')).hide();
                });

                // Bind the click event handler
                $(this).on('click', 'a', function (e) {
                    // Make the old tab inactive.
                    $active.removeClass('active');
                    $content.hide();

                    // Update the variables with the new link and content
                    $active = $(this);
                    $content = $($(this).attr('href'));

                    // Make the tab active.
                    $active.addClass('active');
                    $content.show();

                    // Prevent the anchor's default click action
                    e.preventDefault();
                });
            });
        });
    </script>
    <script>
        // Wait until the DOM has loaded before querying the document
        $(document).ready(function () {
            $('ul.tabs').each(function () {
                // For each set of tabs, we want to keep track of
                // which tab is active and it's associated content
                var $active, $content, $links = $(this).find('a');

                // If the location.hash matches one of the links, use that as the active tab.
                // If no match is found, use the first link as the initial active tab.
                $active = $($links.filter('[href="' + location.hash + '"]')[0] || $links[0]);
                $active.addClass('active');
                $content = $($active.attr('href'));

                // Hide the remaining content
                $links.not($active).each(function () {
                    $($(this).attr('href')).hide();
                });

                // Bind the click event handler
                $(this).on('click', 'a', function (e) {
                    // Make the old tab inactive.
                    $active.removeClass('active');
                    $content.hide();

                    // Update the variables with the new link and content
                    $active = $(this);
                    $content = $($(this).attr('href'));

                    // Make the tab active.
                    $active.addClass('active');
                    $content.show();

                    // Prevent the anchor's default click action
                    e.preventDefault();
                });
            });
        });
    </script>


</asp:Content>
