<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="CXWeb.sys.main" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>EIP</title>
    <link href="/css/styles.css" rel="stylesheet" type="text/css"/>
    <script>
	$(function() {
		var dialog, form,

		// From http://www.whatwg.org/specs/web-apps/current-work/multipage/states-of-the-type-attribute.html#e-mail-state-%28type=email%29
		emailRegex = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/, name = $("#name"), email = $("#email"), password = $("#password"), allFields = $(
				[]).add(name).add(email).add(password), tips = $(".validateTips");

		function updateTips(t) {
			tips.text(t).addClass("ui-state-highlight");
			setTimeout(function() {
				tips.removeClass("ui-state-highlight", 1500);
			}, 500);
		}

		function checkLength(o, n, min, max) {
			if (o.val().length > max || o.val().length < min) {
				o.addClass("ui-state-error");
				updateTips("Length of " + n + " must be between " + min
						+ " and " + max + ".");
				return false;
			} else {
				return true;
			}
		}

		function checkRegexp(o, regexp, n) {
			if (!(regexp.test(o.val()))) {
				o.addClass("ui-state-error");
				updateTips(n);
				return false;
			} else {
				return true;
			}
		}

		function addUser() {
			var valid = true;
			allFields.removeClass("ui-state-error");

			valid = valid && checkLength(name, "username", 3, 16);
			valid = valid && checkLength(email, "email", 6, 80);
			valid = valid && checkLength(password, "password", 5, 16);

			valid = valid
					&& checkRegexp(
							name,
							/^[a-z]([0-9a-z_\s])+$/i,
							"Username may consist of a-z, 0-9, underscores, spaces and must begin with a letter.");
			valid = valid
					&& checkRegexp(email, emailRegex, "eg. ui@jquery.com");
			valid = valid
					&& checkRegexp(password, /^([0-9a-zA-Z])+$/,
							"Password field only allow : a-z 0-9");

			if (valid) {
				$("#users tbody").append(
						"<tr>" + "<td>" + name.val() + "</td>" + "<td>"
								+ email.val() + "</td>" + "<td>"
								+ password.val() + "</td>" + "</tr>");
				dialog.dialog("close");
			}
			return valid;
		}

		dialog = $("#dialog-form").dialog({
			autoOpen : false,
			height : 400,
			width : 350,
			modal : true,
			buttons : {
				"Create an account" : addUser,
				Cancel : function() {
					dialog.dialog("close");
				}
			},
			close : function() {
				form[0].reset();
				allFields.removeClass("ui-state-error");
			}
		});

		form = dialog.find("form").on("submit", function(event) {
			event.preventDefault();
			addUser();
		});

		$("#create-user").button().on("click", function() {
			dialog.dialog("open");
		});
	});
</script>
</head>
<body>
    <%--<form id="form1" runat="server">--%>

        <div id="head">
            <div class="img">
                <img src="/image/header.jpg" />
            </div>
            <ul class="head">
                <li><a href="main.aspx">首 頁</a></li>
                <li><a href="http://10.10.10.20/tiptop.html#">ERP系統</a></li>
                <li><a href="http://eso.cxtechnology.com:8086/NaNaWeb/">電子簽核</a></li>
                <li><a href="http://www.cxtechnology.com:10021">電子郵件</a></li>
                <li><a href="../plc/mr.aspx">PLC現狀查詢</a></li>
                <%--<li><a href="http://eip.cxtechnology.vn:8080/CXHOME/index">管理報表</a></li>--%>
                <%--<li><a href="http://eip.cxtechnology.vn/sys/index.aspx">管理報表</a></li>--%>
                <li><a href="index.aspx">管理報表</a></li>
                <li><a href="http://eip.cxtechnology.vn:8081/Login.aspx">會議預約</a></li>
                <li><a href="../tracking/工藝工程課專案進度追蹤系統_龍蝦測試2026.html">工藝工程</a></li>
                <%--<li><a href="../checklist/login.aspx">點檢表</a></li>--%>
                <li><a href="../kanban/main.aspx">看板</a></li>
                <li><a href="../tracking/tracking_product_selection.aspx">追溯系統</a></li>
                <li><a href="../schedule/main.aspx">生管排程</a></li>
                <li><a href="../schedule/InputMenu.aspx">資料輸入</a></li>
                
            </ul>
        </div>
        <!--网页的中间部分-->
        <div id="body">
            <div id="right">
                <p>行 情 看 板</p>
                <dl style="margin-left: 33px">
                    <dt>
                        <a href="https://www.lme.com/en-GB/Metals/Non-ferrous/Aluminium#tabIndex=2">
                            <img src="/image/caigou_lv.png" /></a>
                    </dt>
                    <dd style="margin-left: 27px">LME 鋁 歷 史 價 格</dd>
                </dl>

                <dl>
                    <dt>
                        <a href="https://www.lme.com/en-GB/Metals/Non-ferrous/Copper#tabIndex=0">
                            <img src="/image/caigou_tong.png" /></a>
                    </dt>
                    <dd style="margin-left: 27px">LME 銅 官 方 價 格</dd>
                </dl>

                <dl>
                    <dt>
                        <a href="https://www.lme.com/en-GB/Metals/Non-ferrous/Zinc#tabIndex=0">
                            <img src="/image/caigou_xin.png" /></a>
                    </dt>
                    <dd style="margin-left: 27px">LME 鋅 官 方 價 格</dd>
                </dl>

                <dl style="margin-left: 173px">
                    <dt style="margin-top: -15px">
                        <a href="http://steelnet.com.tw/commerce.do;jsessionid=9285D79FC366056C9607D9BAF46E03B9.node1">
                            <img src="/image/gang.jpg" /></a>
                    </dt>
                    <dd style="margin-left: 27px">鋼 材 的 國 際 趨 勢</dd>
                </dl>

                <dl style="margin-left: 47px">
                    <dt style="margin-top: -15px">
                        <a href="http://www.stockq.org/commodity/FUTRNGAS.php">
                            <img src="/image/gang01.png" /></a>
                    </dt>
                    <dd style="margin-left: 27px">原 物 料 商 品 價 格</dd>
                </dl>

            </div>
        </div>
        <!--网页底部-->
        <div id="foot" align="center">
            Copyright © 2018 CX Technology Corporation. All rights reserved.</br>
請使用Internet Explorer 7.0或以上版本，Firefox等瀏覽器瀏覽本網頁，獲取最佳瀏覽效果。<br>
            瀏覽本網站的最佳解析度為 1024x768 像素；較低解析度可能會在畫面中出現水平或垂直的滾動卷軸。
        </div>


    <%--</form>--%>
	<h1>test demo</h1>
</body>
</html>

