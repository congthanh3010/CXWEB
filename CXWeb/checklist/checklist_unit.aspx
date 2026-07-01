<%@ Page Title="" Language="C#" MasterPageFile="~/Site_checklist.Master" AutoEventWireup="true" CodeBehind="checklist_unit.aspx.cs" Inherits="CXWeb.checklist.checklist_unit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%; height: 100%">
        <tr>
             <td>
                  <asp:Button ID="btn1" runat="server" Text="CT1" OnClick="btn1_Click" Style="width: 80%; height: 80%; font-size: 13px;"></asp:Button>
             </td>
             <td>
                  <asp:Button ID="btn2" runat="server" Text="CT2"  Style="width: 80%; height: 80%; font-size: 13px;"></asp:Button>
             </td>

        </tr>
    </table>

</asp:Content>
