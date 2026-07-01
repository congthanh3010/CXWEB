<%@Page EnableViewState="true" EnableEventValidation= "false " Title=""  Language="C#" AutoEventWireup="true" CodeBehind="ex2.aspx.cs" Inherits="CXWeb.test.ex2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thuy.vk's Blog | Demo Ajax in Asp.Net</title>
    
</head> 
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lbTime" CssClass="lbTime" runat="server" Text="Label"></asp:Label>
        <input type="button" value="Get Time" id="btGetTime" />
    </form>
    <script src="js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btGetTime').click(function () {
                $.ajax({
                    type: "POST",
                    url: "ex2.aspx/GetTime",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('.lbTime').text(data.d);
                    },
                    error: function () { alert('False!');}
                });
            });
        });
    </script>
</body>
</html>
