<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getmachines.aspx.cs" Inherits="CXWeb.sys.getmachines" %>
        
<asp:repeater ID="repeater1" runat="server">
    <ItemTemplate>
        <li><a href="javascript: void(0)" onclick="<%# string.Format("getmachinedata('{0}','{1}')",Eval("MachineId"),Eval("MachineId")) %>"><%#Eval("MachineId")  %></a></li>
    </ItemTemplate>
</asp:repeater>


