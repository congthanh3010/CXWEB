<%@ Page Title="" Language="C#" MasterPageFile="~/Site_kanban.Master" AutoEventWireup="true" CodeBehind="healthy_notice.aspx.cs" Inherits="CXWeb.kanban.healthy_notice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
        </Triggers>
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Enabled="false" OnTick="Timer1_Tick"></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="slideshow-container" style="position: absolute; width: 100%; height: 100%; padding: 3px">
        <%--<div class="mySlides">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/image/note-do-not-thing.jpg" />
            <asp:Image ID="Image2" runat="server" />
        </div>
        <div class="mySlides">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/image/slide.png" />
        </div>--%>
        <%--<div class="mySlides">
            <asp:Image ID="Image4" runat="server" ImageUrl="~/image/cachlixh.jpg" />
        </div--%>
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="fieldPage" runat="server" Value='<%# Eval("Key") %>' />
                <div class="mySlides">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("url") %>' />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <script src="/Scripts/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="/js/bootstrap.min.js"></script>

    <style type="text/css">
        .mySlides {
            width: 100%;
            height: 100%;
            text-align: center;
        }

        .mySlides::after {
            clear: both;
        }

        .mySlides img.wide {
            max-width: 100%;
            max-height: 100%;
            width: 100%;
            height: auto;
        }

        .mySlides img.tall {
            max-height: 100%;
            max-width: 100%;
            height: 100%;
            width: auto;
        }
    </style>

    <script type="text/javascript">
        var slideIndex = 0;
        showSlides();

        function showSlides() {
            var i;
            var slides = document.getElementsByClassName("mySlides");
            if (slides.length > 0) {
                for (i = 0; i < slides.length; i++) {
                    slides[i].style.display = "none";
                }
                slideIndex++;
                if (slideIndex > slides.length) { slideIndex = slides.length }
                slides[slideIndex - 1].style.display = "block";
                setTimeout(showSlides, 15000);
            }            
        }
    </script>

    <script type="text/javascript">
        $(window).load(function () {
            $('.mySlides').find('img').each(function () {
                var imgClass = (this.width / this.height > 1) ? 'wide' : 'tall';
                $(this).addClass(imgClass);
            })
        })
    </script>
</asp:Content>
