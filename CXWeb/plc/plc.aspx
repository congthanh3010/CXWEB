<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plc.aspx.cs" Inherits="CXWeb.plc.plc" %>

<%@ Register Src="~/plc/wuc/wuc_PLC.ascx" TagPrefix="uc1" TagName="wuc_PLC" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <title>PLC</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/plc.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:wuc_PLC runat="server" id="wuc_PLC" />
    </form>
</body>
</html>
