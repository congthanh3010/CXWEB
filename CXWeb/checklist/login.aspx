<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CXWeb.checklist.login" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>點檢表</title>
    <link href="/css/alogin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
     <%if(false){ %>
    <script type="text/javascript" src="Scripts/jquery-1.4.1-vsdoc.js"></script>
    <%} %>
    
</head>
<body>
    <form id="form1" runat="server">
        <div style="position: absolute; width: 100%; height: 100%; padding: 0px;">
            <table style="width: 100%; font-size: 16px; font-weight: bold;">
                <tr>
                    <td colspan="2">
                        <div style="background-image: url(/image/header_mobile.jpg); height: 213px; background-size: cover; background-position: left; left: 135px;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px;">登入賬號</td>
                    <td>
                        <input id="txtLoginName" type="text" runat="server" style="width: 100%; height: 35px; font-weight: bold; font-size: 16px;" />
                    </td>
                </tr>
                <tr>
                    <td>登入密碼</td>
                    <td>
                        <input id="txtPassword" type="password" runat="server" style="width: 100%; height: 35px; font-weight: bold; font-size: 16px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px;">&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp </td>
                    <td style="text-align: left;">
                        <asp:Button ID="btnLogin" runat="server" Text="登入 Đăng nhập" OnClick="btnLogin_Click" Style="width: 50%; height: 35px; font-weight: bold; font-size: 16px;"></asp:Button>
                    </td>
                </tr>
            </table>
            
        </div>
    </form>
</body>
</html>
