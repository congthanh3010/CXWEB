<%@ Page Language="C#" MasterPageFile="~/Site.Master"   AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CXWeb._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <%--  <form id="form1" runat="server">--%>
    <link href="/css/jquery.datetimepicker.css" rel="stylesheet" />
    <div class="container-fluid reports">
      <div class="row">        
        <div class="col-sm-12 col-md-12 main">          
			<div class="page-header">
                 <div class="wrap">                     
                  

                </div>
            </div>          
        </div>
      </div>
    </div>


    <%--</form>--%>

    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/jquery.datetimepicker.full.js"></script>

    <script type="text/javascript">
         //var currentdate = new Date();
         //var datetime1 = currentdate.getDate() + "/"
         //                + (currentdate.getMonth() + 1) + "/"
         //                + currentdate.getFullYear() + " 0:0";
         //var datetime2 =  currentdate.getDate() + "/"
         //                + (currentdate.getMonth() + 1) + "/"
         //                + currentdate.getFullYear() + " 23:59";
        
         $.datetimepicker.setLocale('en');
         $('#date_from').datetimepicker({  format: 'd/m/Y H:i' });
         $('#date_to').datetimepicker({ format: 'd/m/Y H:i' });

       
       
    </script>

</asp:Content>
